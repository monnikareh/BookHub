using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherService _publisherService;

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("GetPublishers")]
        public async Task<ActionResult<IEnumerable<PublisherDetail>>> GetPublishers()
        {
            try
            {
                return Ok(await _publisherService.GetPublishersAsync());
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            }
            
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PublisherDetail>> GetPublisherById(int id)
        {
            try
            {
                return Ok(await _publisherService.GetPublisherByIdAsync(id));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            }        
        }

        // GET: api/Book/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<PublisherDetail>> GetGenreByName(string name)
        {
            try
            {
                return Ok(await _publisherService.GetGenreByNameAsync(name));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
        }

        [HttpPost("CreatePublisher")]
        public async Task<ActionResult<PublisherDetail>> PostGenre(PublisherCreate publisherCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _publisherService.PostGenreAsync(publisherCreate));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
           
        }
        
        [HttpPut("UpdatePublisher/{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherDetail publisherDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                await _publisherService.UpdatePublisherAsync(id, publisherDetail);
                return Ok();
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
        }


        [HttpDelete("DeletePublisher/{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
        }
        
        private ActionResult HandlePublisherException(Exception e)
        {
            return e is PublisherNotFoundException or UserNotFoundException
                or BookNotFoundException
                ? NotFound(e.Message)
                : Problem(e is BooksEmptyException or EntityUpdateException
                    ? e.Message
                    : "Unknown problem occured");
        }

    }
    
}
