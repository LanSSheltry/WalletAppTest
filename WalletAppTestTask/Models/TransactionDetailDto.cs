using static WalletAppTestTask.DbContext.TransactionContext;

namespace WalletAppTestTask.Models
{
    public class TransactionDetailDto
    {
        public long Id { get; set; }

        public decimal Total { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime TransactionDatetime { get; set; }

        public string PaymentStatusMessage { get; set; }

        public string DayOfWeek { get; set; }

        public string? TransactionName { get; set; }

        public string Description { get; set; }

        public string AuthorizedUser { get; set; }

        public string CardName { get; set; }
    }
}
