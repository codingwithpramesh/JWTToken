using Microsoft.AspNetCore.Mvc;
using MovieAPIs.Models.Domain;

namespace MovieAPIs.Controllers
{


    [Route("[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public UserController(ApplicationDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
           var data = _context.UserData.ToList();
          return Ok(data);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _context.UserData.ToList();
            return Ok(data);
        }
    }
}
