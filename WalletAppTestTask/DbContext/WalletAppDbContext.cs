
using Microsoft.EntityFrameworkCore;
using WalletAppTestTask.Models;

namespace WalletAppTestTask.DbContext
{
    public class WalletAppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WalletAppDbContext(DbContextOptions<WalletAppDbContext> options) :base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
