using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WalletAppTestTask.Services;

namespace WalletAppTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("User Operations")]
    public class UserController : Controller
    { 
        private readonly TestService _testService;

        public UserController(TestService testService)
        {
            _testService = testService;
        }

        [HttpGet("getusers/{id}", Name = "GetUser")]
        public IActionResult GetUser(long id)
        {
            var result = _testService.GetSomeUser(id);

            return Ok(result);
        }
    }
}
