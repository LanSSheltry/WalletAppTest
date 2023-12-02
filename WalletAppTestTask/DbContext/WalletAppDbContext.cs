using Microsoft.EntityFrameworkCore;
using WalletAppTestTask.Models;

namespace WalletAppTestTask.DbContext
{
    public class WalletAppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WalletAppDbContext(DbContextOptions<WalletAppDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<BankCard> BankCards { get; set; }

        public DbSet<Bank> Banks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.BankCards)
                .WithOne(bc => bc.User)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bank>()
                .HasMany(b => b.Cards)
                .WithOne(bc => bc.Bank)
                .HasForeignKey(bc => bc.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankCard>()
                .HasMany(bc => bc.Transactions)
                .WithOne(t => t.Card)
                .HasForeignKey(t => t.BankCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
