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


        [HttpPost("signinup")]
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
            // if(findUserById == null){
            //     return BadRequest(new{message = "id não encontrado"});
            // }
            
            // if(findUserById != null){
            //     return Created("success email", createdAuthUserEmail);
            // }

            //  return Ok(new {
                
            //     message= "Success"
            // });
            
         
            
         

          

         


 

        }
                 
          
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            
            var findUserByEmail = _usersAuthRepository.getByEmail(loginDto.Email);
            
            if( findUserByEmail == null )
            {
                return BadRequest(new {message = "Email or Password Invalid"});
            }

            if( !BCrypt.Net.BCrypt.Verify(loginDto.Password, findUserByEmail.Password) ){
                return BadRequest(new {message ="Email or Password Invalid"});
            }

            var jwt = _jwtService.generateJwt(findUserByEmail.Id);

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

        // [HttpGet("find")]
        // public IActionResult findByEmail(RegisterDto registerDto)
        // {
        //     UsersAuth users = new UsersAuth();

        //     AuthUserEmail authUserEmail = _iAuthUserEmailRepository.findUserByEmail(registerDto.Email);
        //     return Ok(authUserEmail);
            
            
        // }

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