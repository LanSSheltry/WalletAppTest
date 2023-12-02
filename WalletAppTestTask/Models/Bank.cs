using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletAppTestTask.Models
{
    [Table("Banks")]
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        [Required]
        public string Title { get; set; }

        public List<BankCard> Cards { get; set; }
    }
}
