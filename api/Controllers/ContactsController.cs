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

        [HttpPost]
        public async Task<IActionResult> createUsers([FromBody] UsersModel usersModel)
        {
        //   return await _icontactsRepository.createUser()
            var createdUsers = await _icontactsRepository.createUser(usersModel);
            return Ok(createdUsers);
        }

        
    }
}