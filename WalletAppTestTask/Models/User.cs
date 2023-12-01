using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletAppTestTask.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("balance")]
        [Required]
        public decimal Balance { get; set; }

        [Column("due_status")]
        [Required]
        public DueStatuses DueStatus { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public List<Transaction> Transactions { get; set; }
    }

    public enum DueStatuses
    {
        NoPaymentDue = 0,
        PaymentDue = 1
    }
}
