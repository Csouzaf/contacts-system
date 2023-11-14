using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using api.Models.auth.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using api.auth.jwt;
using api.Models.auth.Model;

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
       
       [HttpGet("allcontacts")]
       public ActionResult<IEnumerable<ContactsModel>> GetAllContacts(){

        //FIXME - Retrieve contacts from user autenticated
            var userRetrievered = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userRetrievered != null && int.TryParse(userRetrievered, out int userId))
            {
                var contacts = _icontactsRepository.findAll(userId);
                return Ok(contacts);
            }
            return Unauthorized();
        }
            

        [HttpGet("auth")]
        public ActionResult<UsersAuth> UserAuthenticated()
        {

            
            try{
                
                //FIXME - Get id user authenticated with NameIdentifier
                var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var name = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

                if(id != null){
     
                    return Ok(new {id, jwtToken, name, message = "User RetrieveD in Contacts Router"});
                    
                }

            }
            catch(Exception e){
                 return BadRequest(e.Message);
            }

           return null;
    
        }

        
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