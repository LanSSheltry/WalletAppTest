using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletAppTestTask.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Column("uid")]
        [Required]
        public long UserId { get; set; }

        [Column("payment_type")]
        [Required]
        public PaymentTypes Type { get; set; }

        [Column("sum")]
        [Required]
        [Range(0, 1500)]
        public decimal Sum { get; set; }

        [Column("transaction_name")]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        public string Description { get; set; }

        [Column("authorized_uid")]
        public long? AuthorizedUserId { get; set; }

        [Column("icon_path")]
        [Required]
        public string IconPath { get; set; }

        public User User { get; set; }
    }

    public enum PaymentTypes
    {
        Payment = 0,
        Credit = 1
    }
}
