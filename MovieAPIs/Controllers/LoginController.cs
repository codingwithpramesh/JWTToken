using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieAPIs.Models.Domain;
using PortFolio.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortFolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
       
        public LoginController(IConfiguration configuration)
        {

            _configuration=configuration;

        }


        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            if(user.Email == "Pramesh@gmail.com" && user.Password == "Pr@mesh#123")
            {
                var token = GenerateJWTToken(user);

            }
            return BadRequest("Invalid User");
        }

        private JWTToken GenerateJWTToken(User user)
        {
            var securityKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

            var claims = new Claim[] {
                    new Claim(ClaimTypes.Email,user.Email),
                   
                };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: credentials);


            var jwtToken = new JWTToken
            {
               // RefreshToken = new RefreshTokenGenerator().GenerateRefreshToken(32),
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return jwtToken;
        }
    }
}
