using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace WalletAppTestTask.Models
{
    public class BankCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Column("uid")]
        [Required]
        public long UserId { get; set; }

        [ForeignKey("Bank")]
        [Column("id_bank")]
        public long BankId { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("type")]
        [Required]
        public CardTypes Type { get; set; }

        public User User { get; set; }

        public Bank Bank { get; set; }

        public List<Transaction> Transactions { get; set; }
    }

    public enum CardTypes
    {
        Debit = 0    
    }
}
