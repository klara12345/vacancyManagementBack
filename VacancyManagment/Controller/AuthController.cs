using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacancyManagment.DTO;
using VacancyManagment.Models;
using VacancyManagment.Services;

namespace VacancyManagment.Controller
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
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
       
    }
}
