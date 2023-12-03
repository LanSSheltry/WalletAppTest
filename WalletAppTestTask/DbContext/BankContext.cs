using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalletAppTestTask.Models;
using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.DbContext
{
    [Table("Banks")]
    public class BankContext : IDtoConvertable<BankInfoDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        [Required]
        public string Title { get; set; }

        public List<BankCardContext> Cards { get; set; }

        public BankInfoDto ToDto()
        {
            return new BankInfoDto()
            {
                Id = this.Id,
                Title = this.Title
            };
        }
    }
}
