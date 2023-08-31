using System;
using api.Models;
using api.Repository;

using Microsoft.AspNetCore.Mvc;

using api.Models.auth.Data;

using System.Security.Claims;

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
        
        public ContactsController(IContactsRepository iContactsRepository, IHttpContextAccessor ihttpContextAccessor)
        {
            _icontactsRepository = iContactsRepository;
            _ihttpContextAccessor = ihttpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactsModel>>> getAllContacts()
        {
            return await _icontactsRepository.findAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactsModel>> getContacts(int id)
        {
            ContactsModel users = await _icontactsRepository.findById(id);

            if(users == null)
            {
                return NotFound();
            }
            return users;
        }

    //TODO - Get userAuthenticated, because is returned ok but userAuthenticated = null
        [HttpGet("auth")]
        public IActionResult UserAuthenticate()
        {
            var userAuthenticated = _ihttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            // if(userAuthenticated != null){

               

                return Ok(new {userAuthenticated, message = "Working"});
                
          


           
        }

       [HttpPut("{id}")]
       public async Task<IActionResult> updateContacts([FromBody] ContactsModel contactsModel, int id)
       {
            try{
                var update = await _icontactsRepository.updateUser(contactsModel, id);
                return Ok(update);
            }

            catch(Exception e){
                return BadRequest(e.Message);
            }   
            
       }

       [HttpDelete("{id}")]
       public async Task<IActionResult> deleteContacts(int id)
       {
            try{
                var delete = await _icontactsRepository.deleteUser(id);
                return Ok(delete);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }

       }
    }
}