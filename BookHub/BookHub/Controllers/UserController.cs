using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;

//TODO

namespace BookHub.Controllers
{
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