using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortFolio.Models.DTO;

namespace PortFolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            var status = new Status();
            status.StatusCode = 1;
            status.StatusMessage = "Data from protected controller";
            return Ok(status);
        }
    }
}
