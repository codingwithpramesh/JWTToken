using Microsoft.AspNetCore.Mvc;
using MovieAPIs.Models.Domain;
using PortFolio.Models.Domain;

namespace MovieAPIs.Controllers
{


    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public UserController(ApplicationDbcontext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var data = _context.users.ToList();
            return Ok(data);
        }



        [HttpPost]
        public IActionResult Create(AddContactRequest contact)
        {
            var a = new User
            {
                Name=   contact.Name,
                Email = contact.Email,
                Password= contact.Password,
            };
            _context.users.Add(a);
            _context.SaveChanges();
            return Ok(a);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, UpdateContactRequest contact)
        {
            var data = _context.users.Find(id);
            if (data != null)
            {
                data.Name = contact.Name;
                data.Email = contact.Email;
                data.Password = contact.Password;
                _context.SaveChanges();
                return Ok(data);

            }
            return NotFound();


        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id, UpdateContactRequest contact)
        {
            var data = _context.users.Find(id);
            if (data != null)
            {
                _context.users.Remove(data);
                _context.SaveChanges();
                return Ok();

            }
            return NotFound();


        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Details([FromRoute] Guid id)
        {
            var data = _context.users.Find(id);
            if (data != null)
            {

                return Ok(data);
            }
            return NotFound();
        }

    }
}
