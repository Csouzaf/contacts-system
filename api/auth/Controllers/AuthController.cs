using api.auth.Data;
using api.auth.Dtos;
using api.auth.jwt;
using api.auth.Model;
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
        private readonly IAuthUserEmailRepository _iAuthUserEmailRepository;
        
        public AuthController(IUsersAuthRepository usersAuthRepository, JwtService jwtService, IAuthUserEmailRepository iAuthUserEmailRepository )
        {
            _usersAuthRepository = usersAuthRepository;
            _jwtService = jwtService;
            _iAuthUserEmailRepository = iAuthUserEmailRepository;
        }


        [HttpPost("signup")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var user = new UsersAuth
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                telefone = registerDto.telefone,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            
            var createdUser = _usersAuthRepository.Create(user);

            if(createdUser != null){
            
            var findUserById = _usersAuthRepository.getById(createdUser.Id);
            
            var authUserEmail = new AuthUserEmail
                {
                  
                   Email = createdUser.Email,
                   UserAuthId = createdUser.Id
 
                };

              
               var createdAuthUserEmail = _iAuthUserEmailRepository.Create(authUserEmail);

                return Created("succes", new {
                    createdUser, createdAuthUserEmail});   
                        
            }
            else
                {
                    return BadRequest(new { message = "Failed to create user." });
                }

        }
                 
          
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            
            var findUser = _usersAuthRepository.getByEmail(loginDto.Email);
            
            if( findUser == null )
            {
                return BadRequest(new {message = "Email or Password Invalid"});
            }

            if( !BCrypt.Net.BCrypt.Verify(loginDto.Password, findUser.Password) ){
                return BadRequest(new {message ="Email or Password Invalid"});
            }

            var jwt = _jwtService.generateJwt(findUser.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });

          

            return Ok(new {message = "succes"});
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

        [HttpGet("logout")]
        public IActionResult logoutUser()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });

            return Ok(new {message = "User logout"});
        }  
    }
}