using api.auth.Dtos;
using api.auth.jwt;
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
        private readonly JwtService _jwtService;
        
        public AuthController(IUsersAuthRepository usersAuthRepository, JwtService jwtService)
        {
            _usersAuthRepository = usersAuthRepository;
            _jwtService = jwtService;
        }

        [HttpPost("signinup")]
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
                return BadRequest(new {message = "Email or Password Invalid"});
            }

            if( !BCrypt.Net.BCrypt.Verify(loginDto.Password, foundUserByEmail.Password) ){
                return BadRequest(new {message ="Email or Password Invalid"});
            }

            var jwt = _jwtService.generateJwt(foundUserByEmail.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            // return Ok(new {
            //     jwt
            // });

            return Ok(new {
                message= "Success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try{

                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.verifyJwt(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _usersAuthRepository.getById(userId);

                return Ok(user);
            
            }catch(Exception e){

                return Unauthorized();
            }
        }  
    }
}