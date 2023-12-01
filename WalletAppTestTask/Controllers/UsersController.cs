using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WalletAppTestTask.Services;

namespace WalletAppTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("User Operations")]
    public class UsersController : Controller
    { 
        private readonly UsersService _testService;

        public UsersController(UsersService testService)
        {
            _testService = testService;
        }

        [HttpGet("getuser/{id}", Name = "GetUser")]
        public IActionResult GetUser(long id)
        {
            var result = _testService.GetUserByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("getusertransactions/{userId}")]
        public IActionResult getUserTransactions(long iserId)
        {


            return Ok();
        }



    }
}
