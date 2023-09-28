using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieAPIs.Models.Domain;
using PortFolio.Models;
using PortFolio.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PortFolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        private readonly ApplicationDbcontext _context;
        private readonly JWT _jwtsetting;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IConfiguration _configuration;
       /* private readonly IrefreshHandler _refresh*/
        public AuthorizeController(ApplicationDbcontext context, IOptions<JWT> Jwtsetting, UserManager<ApplicationUser> usermanager, IConfiguration configuration)
        {
            _usermanager = usermanager;
            _configuration = configuration;
            _jwtsetting = Jwtsetting.Value;
            _context = context;  
        }

        [HttpPost ("GenerateToken")]

        public IActionResult GenerateToken([FromBody]  UserDataDTO userDataDTO)
        {
          var user = _context.users.FirstOrDefault( x => x.Email == userDataDTO.Email && x.Password == userDataDTO.Password);
            if(user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(_jwtsetting.Secret);
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim("Password", user.Password)

                        }),

                       Expires =  DateTime.UtcNow.AddSeconds(30),
                       SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

                };

                var token = tokenHandler.CreateToken(tokendesc);
                var finaltoken = tokenHandler.WriteToken(token);
                return Ok (finaltoken);

            }
            else
            {
                return Unauthorized();
            }
        
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromRoute] RegistrationModel registrationModel)
        {
            var userExist = await _usermanager.FindByEmailAsync(registrationModel.Email);
            if(userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    statusCode = "500",
                    StatusMessage =  "ERROR "
                }) ;
                ApplicationUser User = new ApplicationUser
                {
                    UserName = registrationModel.Username,
                    Email = registrationModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),

                };
                var result =  await _usermanager.CreateAsync(User , registrationModel.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response {

                        statusCode ="500",
                        StatusMessage ="Error"

                    });
                }

                return Ok(new Response
                {
                    statusCode = " 200",
                    StatusMessage="User Created Successfully"
                });

            }

            return Ok();
        }
    }
}
