using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class BaseController : Controller
{
    public IActionResult ErrorView((Error err, string message) error)
    {
        return View("ErrorView", new ErrorViewModel { Message = error.message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}