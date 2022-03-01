using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacancyManagment.DTO;
using VacancyManagment.Models;
using VacancyManagment.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VacancyManagment.Controller
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;

        }
        [HttpPost]
        [Route("register")]//<host>/api/auth/register
        public async Task<IActionResult> Register([FromBody] RegisterDTO userForRegisterDto) //Data Transfer Object containing username and password
        {
            Console.WriteLine("Erdha");
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            userForRegisterDto.Email = userForRegisterDto.Email.ToLower(); //Convert username to lower case before storing in database.

            if (await _repo.UserExists(userForRegisterDto.Email))
                return BadRequest("Username is already taken");

            var userToCreate = new VacancyUser
            {
                User = userForRegisterDto.User,
                Name = userForRegisterDto.Name,
                Surname= userForRegisterDto.Surname,
                Email = userForRegisterDto.Email,
                IdRole = userForRegisterDto.IdRole,
                IsActive = userForRegisterDto.IsActive,
                //CreationDate= DateTime.Now,
                //LastLoginDate= DateTime.Now,
                //LastPasswordChangedDate= DateTime.Now,
                //FailedPasswordAttemptCount= 3,
                
                
            };

            var createUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userForRegisterDto)
        {
            var userFromRepo = await _repo.Login(userForRegisterDto.Email.ToLower(), userForRegisterDto.Password);
            if (userFromRepo == null) //User login failed
                return Unauthorized();

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,userFromRepo.IdUser.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Email)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }
       
    }
}
