using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Middleware;

public class JsonToXmlMiddleware
{
    private readonly RequestDelegate _next;

    public JsonToXmlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Query["format"].ToString() == "xml" ||
            context.Request.Query["Accept"].ToString().Contains("xml"))
        {
            var originalBodyStream = context.Response.Body;
            try
            {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                responseBody.Seek(0, SeekOrigin.Begin);

                var jsonResponse = await new StreamReader(responseBody).ReadToEndAsync();

                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {          
                    var xmlResponse = ConvertJsonToXml(jsonResponse);
                    if (xmlResponse != jsonResponse)
                    {
                        context.Response.ContentType = "application/xml";

                        var xmlBytes = Encoding.UTF8.GetBytes(xmlResponse);
                        await originalBodyStream.WriteAsync(xmlBytes);
                        return;
                    }
                }

                var jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
                await originalBodyStream.WriteAsync(jsonBytes);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
        else
        {
            await _next(context);
        }
    }

    private static string ConvertJsonToXml(string jsonResponse)
    {
        var doc = JsonConvert.DeserializeXmlNode("{\"user\":" + jsonResponse + "}", "response");
        return doc?.OuterXml ?? jsonResponse;
    }
}