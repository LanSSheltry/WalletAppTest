using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalletAppTestTask.Interfaces;
using WalletAppTestTask.Models;

namespace WalletAppTestTask.DbContext
{
    [Table("BankCards")]
    public class BankCardContext : IDtoConvertable<BankCardInfoDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("Account")]
        [Column("id_account")]
        [Required]
        public long AccountId { get; set; }

        [Column("due_status")]
        [Required]
        public DueStatus DueStatus { get; set; }

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

        [Column("currency")]
        [Required]
        public Currency Currency { get; set; }

        public AccountContext Account { get; set; }

        public BankContext Bank { get; set; }

        public List<TransactionContext> Transactions { get; set; }

        public BankCardInfoDto ToDto()
        {
            return new BankCardInfoDto()
            {
                Id = this.Id,
                AccountId = this.AccountId,
                BankId = this.BankId,
                Balance = this.Balance,
                DueStatus = this.DueStatus,
                Name = Name,
                Type = this.Type,
                Currency = this.Currency,
                Transactions = Transactions.Select(tr => tr.ToDto()).ToList() //To convert all transactions into dto
            };
        }
    }

    public enum CardType
    {
        Debit = 0
    }

    public enum Currency
    {
        UAH = 0,
        EUR = 1,
        USD = 2,
        CAD = 3
    }

    public enum DueStatus
    {
        NoPaymentDue = 0,
        PaymentDue = 1
    }
}
