using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDetail>>> GetPublishers(string? name)
        {
            try
            {
                return Ok(await _publisherService.GetPublishersAsync(name));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<PublisherDetail>> CreatePublisher(PublisherCreate publisherCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _publisherService.CreatePublisherAsync(publisherCreate));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
           
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherUpdate publisherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _publisherService.UpdatePublisherAsync(id, publisherUpdate));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
        }


        [HttpDelete("{id}")]
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
                : Problem("Unknown problem occured");
        }
    }
}
