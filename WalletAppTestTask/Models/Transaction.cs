using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletAppTestTask.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("BankCard")]
        [Column("id_bank_card")]
        [Required]
        public long BankCardId { get; set; }

        [Column("payment_type")]
        [Required]
        public PaymentType Type { get; set; }

        [Column("sum")]
        [Required]
        [Range(0, 1500)]
        public decimal Sum { get; set; }

        [Column("status")]
        [Required]
        public PaymentStatus Status { get; set; }

        [Column("transaction_name")]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        public string? Description { get; set; }

        [Column("authorized_user_name")]
        public string? AuthorizedUser { get; set; }

        [Column("completed_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("icon")]
        [Required]
        public string? Icon { get; set; } //For this version this is just a field without information

        public BankCard Card { get; set; }

    }

    public enum PaymentType
    {
        Payment = 0,
        Credit = 1
    }

    public enum PaymentStatus
    {
        Approved = 0,
        Declined = 1
    }
}
