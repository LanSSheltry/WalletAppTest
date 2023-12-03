using WalletAppTestTask.DbContext;
using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.Models
{
    public class AccountInfoDto : IHasId
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<BankCardInfoDto> BankCards { get; set; }

        public long GetId()
        {
            return Id;
        }
    }
}
