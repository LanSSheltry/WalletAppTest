namespace WalletAppTestTask.Models
{
    public class TransactionListDto
    {
        public decimal Balance { get; set; }

        public decimal Available { get; set; }

        public string DueStatusMessage { get; set; }

        public string DailyPoints { get; set; }

        public List<TransactionDetailDto> TransactionsDetail { get; set; }

    }
}
