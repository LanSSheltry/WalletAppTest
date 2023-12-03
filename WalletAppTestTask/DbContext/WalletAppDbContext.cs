using Microsoft.EntityFrameworkCore;

namespace WalletAppTestTask.DbContext
{
    public class WalletAppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WalletAppDbContext(DbContextOptions<WalletAppDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<AccountContext> Users { get; set; }

        public DbSet<TransactionContext> Transactions { get; set; }

        public DbSet<BankCardContext> BankCards { get; set; }

        public DbSet<BankContext> Banks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountContext>()
                .HasMany(u => u.BankCards)
                .WithOne(bc => bc.Account)
                .HasForeignKey(bc => bc.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankContext>()
                .HasMany(b => b.Cards)
                .WithOne(bc => bc.Bank)
                .HasForeignKey(bc => bc.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankCardContext>()
                .HasMany(bc => bc.Transactions)
                .WithOne(t => t.Card)
                .HasForeignKey(t => t.BankCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public async Task<AccountContext> GetAccountInfoByIdAsync(long uid)
        {
            var userInfo = await Users
                .Where(u => u.Id == uid)
                .FirstOrDefaultAsync();

            return userInfo;
        }

        public async Task<List<BankCardContext>> GetCardsForUserByIdAsync(long uid)
        {
            var bankCards = await BankCards.Where(bc => bc.AccountId == uid).ToListAsync();

            return bankCards;
        }

        public async Task<BankCardContext> GetBankCardDetailsByIdAsync(long cardId)
        {
            var bankCard = await BankCards.Where(bc => bc.Id == cardId).FirstOrDefaultAsync();

            return bankCard;
        }

        public async Task<string> GetBankNameForCardsByIdAsync(long bankId)
        {
            var bankName = (await Banks.Where(b=>b.Id == bankId).FirstOrDefaultAsync()).Title;

            return bankName;
        }

        public async Task<List<TransactionContext>> GetTransactionsByCardIdLimitedAsync(long id)
        {
            var transactions = await Transactions.Where(tr => tr.BankCardId == id).Take(10).ToListAsync();

            return transactions;
        }

        public async Task<List<TransactionContext>> GetTransactionsByCardIdUnlimitedAsync(long id)
        {
            var transactions = await Transactions.Where(tr => tr.BankCardId == id).ToListAsync();

            return transactions;
        }
    }
}
