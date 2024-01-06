﻿using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class AuthorController : Controller
{
    private readonly ILogger<AuthorController> _logger;
    private readonly IAuthorService _authorService;
    
    public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
    {
        _logger = logger;
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _authorService.GetAuthorsAsync(null, null, null);
        return View(authors);    
    }
    
    
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(AuthorCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _authorService.CreateAuthorAsync(model);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return View(new AuthorUpdate
        {
            Name = author.Name,
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, AuthorUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _authorService.UpdateAuthorAsync(id, model);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return RedirectToAction("Index");
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return View(author);
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}