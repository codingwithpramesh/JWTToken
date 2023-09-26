using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPIs.Models.Domain;
using PortFolio.Models.Domain;
using PortFolio.Models.DTO;

namespace MovieAPIs.Controllers
{


    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        public UserController(ApplicationDbcontext context , IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            /*  var data = _context.users.ToList();
              return Ok(data);*/

            var users = _context.users.ToList();
            var userDTOs = _mapper.Map<List<UserDataDTO>>(users);
            return Ok(userDTOs);
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
            var user = _context.users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDataDTO>(user);
            return Ok(userDto);
        }

    }
}
