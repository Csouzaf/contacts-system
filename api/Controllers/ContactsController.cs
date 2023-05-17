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

            _context.usersModels.Add(new UsersModel{ Id = "14", Nome = "Caiques", Email = "teste@hotmail.com", Telefone = "+99999919"});

            _context.usersModels.Add(new UsersModel{ Id = "15", Nome = "CaiqueSs", Email = "teste1@hotmail.com", Telefone = "+99999999" });

            _context.SaveChanges();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersModel>>> getUsers()
        {
            return await _context.usersModels.ToListAsync();
        }


    }
}