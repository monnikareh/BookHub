using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;

//TODO

namespace BookHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public OrderController(BookHubDbContext context)
        {
            _context = context;
        }
    }
}