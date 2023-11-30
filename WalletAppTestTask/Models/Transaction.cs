namespace WalletAppTestTask.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public int Type { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public long AuthorizedUser { get; set; }

        public string IconPath { get; set; }
    }
}
