using System.Net;
using System;
using api.Models;
using api.Repository;

using Microsoft.AspNetCore.Mvc;

using api.Models.auth.Data;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using api.auth.jwt;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _icontactsRepository;

        //NOTE - User HttpContext for retrieve http but without comunication between services, so use HttpContextAccessor
        private readonly IHttpContextAccessor _ihttpContextAccessor;
        private readonly IUsersAuthRepository _usersAuthRepository;
        private readonly JwtService _jwtService;
        
        public ContactsController(JwtService jwtService, IContactsRepository iContactsRepository, IUsersAuthRepository usersAuthRepository, IHttpContextAccessor ihttpContextAccessor)
        {
            _jwtService = jwtService;
            _icontactsRepository = iContactsRepository;
            _ihttpContextAccessor = ihttpContextAccessor;
            _usersAuthRepository = usersAuthRepository;
            // _usersRegisteredRepository = usersRegisteredRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactsModel>> getAllContacts()
        {
            return _icontactsRepository.findAll();
        }


        [HttpGet("{id}")]
        public ActionResult<ContactsModel> getContacts(int id)
        {
            ContactsModel users = _icontactsRepository.findById(id);

            if(users == null)
            {
                return NotFound();
            }
            return users;
        }
       
       
        [HttpGet("auth")]
        public IActionResult UserAuthenticated()
        {

            //FIXME - _ihttpContextAccessor.HttpContext is null here, resolve

            try{
                
                //FIXME - Get id user authenticated with NameIdentifier
                var retrievedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var retrieveUserName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

                if(retrievedUserId != null){
     
                    return Ok(new {retrievedUserId, jwtToken, retrieveUserName, message = "User RetrieveD in Contacts Router"});
                    
                }

            }
            catch(Exception e){
                 return BadRequest(e.Message);
            }

           return null;
    
        }
    //    [AllowAnonymous] 
    //    [HttpGet("log")]
    //    public async Task<IActionResult> LogoutUser()
    //    {
    
    //         await HttpContext.SignOutAsync("jwt");
           
    //    }
        
        [HttpGet("logout")]
        public IActionResult LogoutUser()
        {
           
            var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            
         
                //NOTE - Method used with cookie-based authentication, not JWT tokens.
                //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                
            //     HttpContext.Session.Clear();
                
            
               if(jwtToken != null){
                
                    //NOTE - Delete in client side 
                    Response.Cookies.Delete("jwt", new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,

                });
                    Response.Headers.Remove(jwtToken);
              
               }
              return Ok(new { message = "User logout"});
            }
            
            // return Redirect("/api/auth/Controllers/AuthController.cs");
            // return Ok(new { message = "User logout"});
            //  await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    // await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
//    }
            //FIXME - FIND A WAY TO LOGOUT USER
            
            // await HttpContext.SignOutAsync("jwt");

         // Store revoked token IDs on the server
          
           

// Token is valid; proceed with authentication




       [HttpPost("create")]
       public IActionResult CreateContacts([FromBody] ContactsModel contactsModel)
       {

            try{

                var verifyUserIsAuthenticated = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
                    if(verifyUserIsAuthenticated != null)
                    {
                           

                        
                        int findUserAuthById = int.Parse(verifyUserIsAuthenticated);
                        
                        contactsModel.userRegisteredId = findUserAuthById;
                        
                        //TODO - create contact
                        var createContactsFromUsersAuth = new ContactsModel
                        {
                            
                            Nome = contactsModel.Nome,
                            Email = contactsModel.Email,
                            Telefone = contactsModel.Telefone,
                            userRegisteredId = findUserAuthById
                    
                        };

                        var contactsCreated = _icontactsRepository.createUser(createContactsFromUsersAuth);
                        
                        return Created("succes", new {contactsCreated});
                            
                
                    }
               
            }   

            catch(Exception e){
                return BadRequest(new{Message = "Bug", e});
            }

            return null;


       }

       [HttpPut("{id}")]
       public IActionResult updateContacts([FromBody] ContactsModel contactsModel, int id)
       {
            try{
                var update = _icontactsRepository.updateUser(contactsModel, id);
                return Ok(update);
            }

            catch(Exception e){
                return BadRequest(e.Message);
            }   
            
       }

       [HttpDelete("{id}")]
       public IActionResult deleteContacts(int id)
       {
            try{
                var delete = _icontactsRepository.deleteUser(id);
                return Ok(delete);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }

       }
    }
}