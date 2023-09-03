using System.Security.Claims;
using api.auth.Data;
using api.auth.Dtos;
using api.auth.jwt;
using api.auth.Model;
using api.Models.auth.Data;
using api.Models.auth.Model;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
        private readonly IContactsRepository _iContactsRepository;
        
        public AuthController(IUsersAuthRepository usersAuthRepository, JwtService jwtService, 
        IAuthUserEmailRepository iAuthUserEmailRepository,  IContactsRepository iContactsRepository)
        {
            _usersAuthRepository = usersAuthRepository;
            _jwtService = jwtService;
            _iAuthUserEmailRepository = iAuthUserEmailRepository;
            _iContactsRepository = iContactsRepository;

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
                
               
                var authUserEmail = new AuthUserEmail
                {
                    
                    Email = createdUser.Email,
                    UserAuthId = createdUser.Id
    
                };


               var createdAuthUserEmail = _iAuthUserEmailRepository.Create(authUserEmail);

                return Created("succes", new {
                    createdUser, createdAuthUserEmail });   
                        
            }
            
            else
            {
                return BadRequest(new { message = "Failed to create user." });
            }

        }
                 
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            //NOTE - Find the user e-mail already registered in the repository pass to Email in loginDto
            var findUser = _usersAuthRepository.getByEmail(loginDto.Email);

            var findUserPassword = findUser.Password;
            
            if( findUser == null )
            {
                return BadRequest(new {message = "Email or Password Invalid"});
            }
                
            if( !BCrypt.Net.BCrypt.Verify(loginDto.Password, findUserPassword) ){
                return BadRequest(new {message ="Email or Password Invalid"});
            }

            var jwt = _jwtService.generateJwt(findUser.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });

            return Ok(new {message = "User logged"});
        }

      //SECTION 2 - Attempt with new gererateJwt
        // [HttpGet]
        // public IActionResult Login(LoginDto loginDto)
        // {
        //     var findUser = _usersAuthRepository.getByEmail(loginDto.Email);
            
        //     var findUserPassword = findUser.Password;

        //     var authenticatedUser = 

        //    if( findUser == null )
        //     {
        //         return BadRequest(new {message = "Email or Password Invalid"});
        //     }
                
        //     if( !BCrypt.Net.BCrypt.Verify(loginDto.Password, findUserPassword) ){
        //         return BadRequest(new {message ="Email or Password Invalid"});
        //     }


        //     var jwt = _jwtService.generateJwt(findUser);          
          
        //     //TODO - Create new login with email and password for pass in generatedJwt(New login)

           
        //     Response.Cookies.Append("jwt", jwt, new CookieOptions
        //     {
        //         HttpOnly = true,
        //         Secure = true
        //     });

        //     return Ok(new {message = "User logged"});
        // }

        
        [HttpGet("user")]
        public IActionResult User()
        {
            try{

                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.verifyJwt(jwt);

                int userId = int.Parse(token.Issuer);
                
                var user = _usersAuthRepository.getById(userId);
                
                 var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _jwtService.generateJwt(userId)), //NOTE - User.Identify.Name
                // new Claim(ClaimTypes.Name, usersAuth.Name)
                //new Claim(ClaimTypes.Role, usersAuth.Role) - User.IsInRole
                  
            };
        

                return Ok( new{user,claims, user.Name});
            
            }catch(Exception e){

                return Unauthorized(e);
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