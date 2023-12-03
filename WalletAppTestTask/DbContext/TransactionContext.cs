using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalletAppTestTask.Models;
using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.DbContext
{
    [Table("Transactions")]
    public class TransactionContext : IDtoConvertable<TransactionInfoDto>
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

        [Column("total")]
        [Required]
        [Range(0, 1500)]
        public decimal Total { get; set; }

        [Column("currency")]
        [Required]
        public Currency Currency { get; set; }

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
        public string Icon { get; set; } //For this version this is just a field without information

        public BankCardContext Card { get; set; }

        public TransactionInfoDto ToDto()
        {
            return new TransactionInfoDto()
            {
                Id = this.Id,
                BankCardId = this.BankCardId,
                Type = this.Type,
                Total = this.Total,
                Currency = this.Currency,
                Status = this.Status,
                Name = this.Name,
                Description = this.Description,
                AuthorizedUser = this.AuthorizedUser,
                CreatedAt = this.CreatedAt,
                Icon = this.Icon
            };

        }

        public enum PaymentType
        {
            Payment = 0,
            Credit = 1
        }

        public enum PaymentStatus
        {
            Approved = 0,
            Pending = 1
        }
    }
}
