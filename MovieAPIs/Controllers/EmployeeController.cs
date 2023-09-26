using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortFolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string[] strings = { "Pramesh", "Pradeep", "Rakesh" };
            return Ok(strings);
        }
    }
}
