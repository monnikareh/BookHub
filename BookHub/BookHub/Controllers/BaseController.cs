using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class BaseController : Controller
{
    public IActionResult Error((Error e, string message) error)
    {
        return View("Error", new ErrorViewModel { Message = error.message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}