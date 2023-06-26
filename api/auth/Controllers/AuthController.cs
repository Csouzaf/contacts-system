using api.auth.Dtos;
using api.Models.auth.Data;
using api.Models.auth.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.auth.Controllersv 
{   
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : Controller
    {
        private readonly IUsersAuthRepository _usersAuthRepository;

        public AuthController(IUsersAuthRepository usersAuthRepository)
        {
            _usersAuthRepository = usersAuthRepository;
        }

        [HttpPost("signin")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var user = new UsersAuth
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
               var createdUser = _usersAuthRepository.Create(user);

            return Created("success",createdUser);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            
            var foundUserByEmail = _usersAuthRepository.getByEmail(loginDto.Email);
            
            if( foundUserByEmail == null )
            {
                return BadRequest(new {message = "User not found"});
            }

            if( !BCrypt.Net.BCrypt.Verify(loginDto.Password, foundUserByEmail.Password) ){
                return BadRequest(new {message ="Invalid Credentials"});
            }
            
            return Ok(foundUserByEmail);
        }

        
    }
}