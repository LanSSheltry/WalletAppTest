using Newtonsoft.Json;
using WalletAppTestTask.DbContext;

namespace WalletAppTestTask.Services
{
    public class TestService
    {
        public readonly WalletAppDbContext _dbContext;

        public TestService(WalletAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetSomeUser(long id)
        {
            var user = _dbContext.Users.Where(p => p.Id == id)
                       .FirstOrDefault();

            var userJson = JsonConvert.SerializeObject(user);

            return userJson;
        }
    }
}
