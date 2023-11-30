namespace WalletAppTestTask.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public int DueStatus { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
