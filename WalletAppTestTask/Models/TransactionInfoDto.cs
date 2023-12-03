using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalletAppTestTask.DbContext;
using static WalletAppTestTask.DbContext.TransactionContext;
using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.Models
{
    public class TransactionInfoDto : IHasId
    {
        public long Id { get; set; }

        public long BankCardId { get; set; }

        public PaymentType Type { get; set; }

        public decimal Total { get; set; }

        public Currency Currency { get; set; }

        public PaymentStatus Status { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? AuthorizedUser { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Icon { get; set; } //For this version this is just a field without information

        public long GetId()
        {
            return Id;
        }
    }
}
