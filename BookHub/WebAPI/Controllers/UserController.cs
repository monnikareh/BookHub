using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
            return e switch
            {
                OrderNotFoundException or UserNotFoundException
                    or BookNotFoundException => NotFound(e.Message),
                UserAlreadyExistsException => Conflict(e.Message),
                _ => Problem($"{e}Unknown problem occured")
            };
        }
    }
}