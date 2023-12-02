using Microsoft.AspNetCore.Mvc;
using WalletAppTestTask.Services;

namespace WalletAppTestTask.Controllers
{
    public class DatabaseSeedController : Controller
    {
        private readonly DatabaseSeeder _dataSeeder;

        public DatabaseSeedController(DatabaseSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }

        //Endpoint to fill database with random information
        [HttpGet("seeddatabase")]
        public IActionResult SeedTestData()
        {
            var res = _dataSeeder.SeedData();
            return Ok(res);
        }
    }
}
