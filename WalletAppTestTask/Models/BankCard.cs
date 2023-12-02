using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletAppTestTask.Models
{
    [Table("BankCards")]
    public class BankCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Column("id_user")]
        [Required]
        public long UserId { get; set; }

        [ForeignKey("Bank")]
        [Column("id_bank")]
        public long BankId { get; set; }

        [Column("balance")]
        [Required]
        public decimal Balance { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("type")]
        [Required]
        public CardType Type { get; set; }

        public User User { get; set; }

        public Bank Bank { get; set; }

        public List<Transaction> Transactions { get; set; }
    }

    public enum CardType
    {
        Debit = 0    
    }
}
