using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletAppTestTask.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("due_status")]
        [Required]
        public DueStatus DueStatus { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public List<BankCard> BankCards { get; set; }
    }

    public enum DueStatus
    {
        NoPaymentDue = 0,
        PaymentDue = 1
    }
}
