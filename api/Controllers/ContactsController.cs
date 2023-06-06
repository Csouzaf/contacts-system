using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _icontactsRepository;

        public ContactsController(IContactsRepository iContactsRepository)
        {
            _icontactsRepository = iContactsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersModel>>> getAllContacts()
        {
            return await _icontactsRepository.findAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsersModel>> getContacts(int id)
        {
            UsersModel users = await _icontactsRepository.findById(id);

            if(users == null)
            {
                return NotFound();
            }
            return users;
        }

        [HttpPost]
        public async Task<IActionResult> createContacts([FromBody] UsersModel usersModel)
        {
        
            var createdUsers = await _icontactsRepository.createUser(usersModel);
            return Ok(createdUsers);
        }

       [HttpPut("{id}")]
       public async Task<IActionResult> updateContacts([FromBody] UsersModel usersModel, int id)
       {
            try{
                var update = await _icontactsRepository.updateUser(usersModel, id);
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