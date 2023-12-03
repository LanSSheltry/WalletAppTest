using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalletAppTestTask.DbContext;
using WalletAppTestTask.Interfaces;

namespace WalletAppTestTask.Models
{
    public class BankCardInfoDto : IHasId
    {
        public decimal CardLimit { get; } = 1500;

        public long Id { get; set; }

        public long AccountId { get; set; }

        public long BankId { get; set; }

        public decimal Balance { get; set; }

        public string Name { get; set; }

        public string BankName { get; set; }

        public DueStatus DueStatus { get; set; }

        public CardType Type { get; set; }

        public Currency Currency { get; set; }

        public List<TransactionInfoDto> Transactions { get; set; }

        public long GetId()
        {
            return Id;
        }
    }
}
