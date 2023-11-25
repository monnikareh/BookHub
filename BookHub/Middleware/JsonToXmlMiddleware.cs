using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Http;
using System;

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
        // Check if the request is for the specific API controller
        if (context.Request.Path.StartsWithSegments("/specific/fetch/xml"))
        {
            // Capture the original response
            var originalBodyStream = context.Response.Body;

            try
            {
                // Create a new memory stream to store the modified response
                using var responseBody = new MemoryStream();
                // Replace the original response stream with the memory stream
                context.Response.Body = responseBody;

                // Call the next middleware in the pipeline
                await _next(context);

                // Rewind the memory stream to read the JSON response
                responseBody.Seek(0, SeekOrigin.Begin);

                // Read the JSON response
                var jsonResponse = await new StreamReader(responseBody).ReadToEndAsync();

                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    // Convert JSON to XML
                    var xmlResponse = ConvertJsonToXml(jsonResponse);

                    if (xmlResponse != jsonResponse)
                    {
                        // Set the Content-Type to XML
                        context.Response.ContentType = "application/xml";

                        // Write the XML response to the original response stream
                        var xmlBytes = Encoding.UTF8.GetBytes(xmlResponse);
                        await originalBodyStream.WriteAsync(xmlBytes);
                        return;
                    }
                }

                // Write the JSON response to the original response stream
                var jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
                await originalBodyStream.WriteAsync(jsonBytes);
            }
            finally
            {
                // Restore the original response stream
                context.Response.Body = originalBodyStream;
            }
        }
        else
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

    private static string ConvertJsonToXml(string jsonResponse)
    {
        // In order to use this method, we need Newtonsoft JSON
        var doc = JsonConvert.DeserializeXmlNode("{\"user\":" + jsonResponse + "}", "response");
        return doc?.OuterXml ?? jsonResponse;
    }
}
