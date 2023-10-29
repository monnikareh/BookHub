using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;


namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }


        [HttpGet("GetRatings")]
        public async Task<ActionResult<IEnumerable<RatingDetail>>> GetRatings(int? userId, string? userName,
            int? bookId, string? bookName)
        {
            try
            {
                return Ok(await _ratingService.GetRatingsAsync(userId, userName, bookId, bookName));
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RatingDetail>> GetRatingById(int id)
        {
            try
            {
                return Ok(await _ratingService.GetRatingByIdAsync(id));
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }


        [HttpPost("CreateRating")]
        public async Task<ActionResult<RatingDetail>> CreateRating(RatingCreate ratingCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _ratingService.CreateRatingAsync(ratingCreate));
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }
        
        [HttpPut("UpdateRating/{id}")]
        public async Task<ActionResult> UpdateRating(int id, RatingDetail ratingDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _ratingService.UpdateRatingAsync(id, ratingDetail));
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }

        [HttpDelete("DeleteRating/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _ratingService.DeleteBookAsync(id);
                return Ok();
                
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }
        
        private ActionResult HandleRatingException(Exception e)
        {
            return e is ContextNotFoundException or UserNotFoundException
                or BookNotFoundException or RatingNotFoundException or BooksEmptyException
                ? NotFound(e.Message)
                : Problem("Unknown problem occured");
        }
    }
}