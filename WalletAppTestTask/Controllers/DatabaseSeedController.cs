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

        /*SUMMARY:
         * This endpoint was created to simplify the process of filling the database with
         * simple random data.
         */

        [HttpGet("seeddatabase")]
        public async Task<IActionResult> SeedTestData()
        {
            var res = await _dataSeeder.SeedData();
            return Ok(res);
        }
    }
}
