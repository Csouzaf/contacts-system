using Microsoft.AspNetCore.Mvc;

namespace api.Models.auth
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return Ok("success");
        }
    }
}