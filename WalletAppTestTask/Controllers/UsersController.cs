using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WalletAppTestTask.Services;

namespace WalletAppTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    { 
        private readonly UsersService _usersService;

        public UsersController(
            UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("getuser/{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(long id)
        {
            var result = await _usersService.GetUserByIdAsync(id);

            return Ok(result);
        }

    }
}
