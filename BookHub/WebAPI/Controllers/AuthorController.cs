using BookHub.Models;
using BusinessLayer.Exceptions;
using DataAccessLayer;
using BusinessLayer.Mapper;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        
        [HttpGet("GetAuthors")]
        public async Task<ActionResult<IEnumerable<AuthorDetail>>> GetAuthors(string? name, int? bookId, string? bookName)
        {
            try
            {
                return Ok(await _authorService.GetAuthorsAsync(name, bookId, bookName));
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AuthorDetail>> GetAuthorById(int id)
        {
           try
           {
               return Ok(await _authorService.GetAuthorByIdAsync(id));
           }
           catch (Exception e)
           {
               return HandleAuthorException(e);
           }
        }

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<AuthorDetail>> PostAuthor(AuthorCreate authorCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _authorService.PostAuthorAsync(authorCreate));
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }
        
        [HttpPut("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdate authorUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _authorService.UpdateAuthorAsync(id, authorUpdate));
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }

        private ActionResult HandleAuthorException(Exception e)
        {
            return e is AuthorNotFoundException or BookNotFoundException
                ? NotFound(e.Message)
                : Problem("Unknown problem occured");
        }
    }
}