using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;

//TODO

namespace BookHub.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public UserController(BookHubDbContext context)
        {
            _context = context;
        }
    }
}