using System.Net;
using System;
using api.Models;
using api.Repository;

using Microsoft.AspNetCore.Mvc;

using api.Models.auth.Data;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _icontactsRepository;

        //NOTE - User HttpContext for retrieve http but without comunication between services, so use HttpContextAccessor
        private readonly IHttpContextAccessor _ihttpContextAccessor;
        private readonly IUsersAuthRepository _usersAuthRepository;
        private readonly IUserRegisteredRepository _usersRegisteredRepository;
        
        public ContactsController(IUserRegisteredRepository usersRegisteredRepository, IContactsRepository iContactsRepository, IHttpContextAccessor ihttpContextAccessor)
        {
            _icontactsRepository = iContactsRepository;
            _ihttpContextAccessor = ihttpContextAccessor;
            _usersRegisteredRepository = usersRegisteredRepository;
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

    //TODO - Get userAuthenticated, because is returned ok but userAuthenticated = null
        [Authorize]
        [HttpGet("auth")]
        public IActionResult UserAuthenticated()
        {

            //FIXME - _ihttpContextAccessor.HttpContext is null here, resolve

            try{
                
                //FIXME - Get id user authenticated with NameIdentifier
                var retrievedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var retrieveUserName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                
                if(retrievedUserId != null){
     
                    return Ok(new {retrievedUserId, retrieveUserName, message = "User RetrieveD in Contacts Router"});
                    
                }

            }
            catch(Exception e){
                 return BadRequest(e.Message);
            }

           return null;
    
        }

       [Authorize]
       [HttpPost("create")]
       public IActionResult CreateContacts([FromBody] ContactsModel contactsModel)
       {

            try{

                var verifyUserIsAuthenticated = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if(verifyUserIsAuthenticated != null)
                {
                
                    //TODO - create contact
                    // var createContactsFromUserRegistered = new ContactsModel{
                        
                    //     Nome = contactsModel.Nome,
                    //     Email = contactsModel.Email,
                    //     Telefone = contactsModel.Telefone
                        
                    // };
                    var contactsCreated = _icontactsRepository.createUser(contactsModel);
                    
                    // var findUserAuthById = _usersAuthRepository.getById(id);
                        
                        // if(findUserAuthById != null){
                    
                    //TODO - Create collection on UserRegisteredModel

                    //REVIEW - Error in https://localhost:7087/api/contacts/create => "userRegisteredModel": "The userRegisteredModel field is required."
                    
                    //NOTE - To test if without userRegisteredModel and one to N the userAuthenticated get add contacts
                            int findUserAuthById = int.Parse(verifyUserIsAuthenticated);
                            

                            var userRegisteredModel = new UserRegisteredModel
                            {   
                               
                                usersAuthenticatedId = findUserAuthById

                            };

                            var userRegisteredContacts = _usersRegisteredRepository.CreateContacts(userRegisteredModel);
                        
                            return Ok(new {contactsCreated, userRegisteredContacts});
                        // }    
              
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