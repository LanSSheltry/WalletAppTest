using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WalletAppTestTask.DbContext;

namespace WalletAppTestTask.Services
{
    public class UsersService
    {
        public readonly WalletAppDbContext _dbContext;

        public UsersService(WalletAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetUserByIdAsync(long id)
        {
            var user = await _dbContext.Users
                       .Where(p => p.Id == id)
                       .FirstOrDefaultAsync();

            var userJson = JsonConvert.SerializeObject(user);

            return userJson;
        }

        
    }
}
