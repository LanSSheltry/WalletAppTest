using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalletAppTestTask.Models;
using System.Runtime.CompilerServices;
using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.DbContext
{
    [Table("Accounts")]
    public class AccountContext : IDtoConvertable<AccountInfoDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public List<BankCardContext> BankCards { get; set; }

        public AccountInfoDto ToDto()
        {
            return new AccountInfoDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                BankCards = BankCards.Select(bc => bc.ToDto()).ToList() //To convert all Bank cards into dto
            };
        }
    }
}
