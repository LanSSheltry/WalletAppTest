using WalletAppTestTask.DbContext;
using WalletAppTestTask.Interfaces;

using static WalletAppTestTask.DbContext.TransactionContext;

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

        public string Icon { get; set; }

        public long GetId()
        {
            return Id;
        }
    }
}
