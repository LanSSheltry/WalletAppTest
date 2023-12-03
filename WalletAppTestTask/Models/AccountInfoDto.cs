using WalletAppTestTask.DbContext;

namespace WalletAppTestTask.Models
{
    public class AccountInfoDto
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<BankCardInfoDto> BankCards { get; set; }
    }
}
