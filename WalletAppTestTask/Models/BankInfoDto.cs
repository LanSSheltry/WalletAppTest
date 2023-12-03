using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.Models
{
    public class BankInfoDto : IHasId
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public long GetId()
        {
            return Id;
        }
    }
}
