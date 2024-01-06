using System.Diagnostics;
using BookHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class BaseController : Controller
{
    public IActionResult Error(string message)
    {
        return View(new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}