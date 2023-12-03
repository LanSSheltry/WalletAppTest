using Microsoft.AspNetCore.Mvc;
using WalletAppTestTask.Services;

namespace WalletAppTestTask.Controllers
{
    public class DatabaseSeedController : Controller
    {
        private readonly DatabaseSeederService _dataSeeder;

        public DatabaseSeedController(DatabaseSeederService dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }

        //Endpoint to fill database with random information
        [HttpGet("seeddatabase")]
        public async Task<IActionResult> SeedTestData()
        {
            var res = await _dataSeeder.SeedData();
            return Ok(res);
        }
    }
}
