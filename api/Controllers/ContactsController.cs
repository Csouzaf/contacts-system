using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly UsersDbContextModel _context;

        public ContactsController(UsersDbContextModel context)
        {
            _context = context;

            _context.usersModels.AddAsync(new UsersModel{ Id = "1", Nome = "Caique", Email = "teste@hotmail.com"});

            _context.usersModels.AddAsync(new UsersModel{ Id = "2", Nome = "CaiqueS", Email = "teste1@hotmail.com"});

            _context.SaveChanges();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersModel>>> getUsers()
        {
            return await _context.usersModels.ToListAsync();
        }


    }
}