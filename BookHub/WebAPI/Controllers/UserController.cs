using BookHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;


namespace WebAPI.Controllers
{
    // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUsers()
        {
            try
            {
                return Ok(await _userService.GetUsersAsync());
            }
            catch (Exception e)
            {
                return HandleUserException(e);
            }
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<UserDetail>> GetUserById(int id)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(id));
            }
            catch (Exception e)
            {
                return HandleUserException(e);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDetail>> CreateUser(UserCreate userCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _userService.CreateUserAsync(userCreate));
            }
            catch (Exception e)
            {
                return HandleUserException(e);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<UserDetail>> UpdateUser(int id, UserUpdate userUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _userService.UpdateUserAsync(id, userUpdate));
            }
            catch (Exception e)
            {
                return HandleUserException(e);
            }
            
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleUserException(e);
            }
        }

        private ActionResult HandleUserException(Exception e)
        {
            return e is OrderNotFoundException or UserNotFoundException
                or BookNotFoundException
                ? NotFound(e.Message)
                : e is UserAlreadyExistsException
                    ? Conflict(e.Message)
                    : Problem($"{e}Unknown problem occured");
        }
    }
}