using WalletAppTestTask.DbContext;

namespace WalletAppTestTask.Services
{
    public class TransactionsService
    {
        public readonly WalletAppDbContext _dbContext;

        public TransactionsService(WalletAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetUserTransactionsAsync(long userId)
        {


            return "";
        }
    }
}
