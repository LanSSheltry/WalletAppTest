using WalletAppTestTask.DbContext;
using WalletAppTestTask.Models;
using static WalletAppTestTask.DbContext.TransactionContext;

namespace WalletAppTestTask.Mappers
{
    public class DtoModelsMapper
    {

        public TransactionListDto BuildTransactionListForCardByAccount(AccountInfoDto account)
        {
            var transactionDetailList = new List<TransactionDetailDto>();
            var card = account.BankCards.FirstOrDefault();

            foreach (var transaction in card.Transactions)
            {
                var type = transaction.Type;
                var transactionTotal = Math.Round(transaction.Total, 2);
                transactionDetailList.Add(new TransactionDetailDto()
                {
                    Id = transaction.Id,
                    Total = (type == PaymentType.Payment) ? transactionTotal : (-transactionTotal),
                    TransactionDatetime = transaction.CreatedAt,
                    TransactionName = transaction.Name,
                    CardName = card.Name,
                    AuthorizedUser = transaction.AuthorizedUser,
                    DayOfWeek = transaction.CreatedAt.DayOfWeek.ToString(),
                    Description = transaction.Description,
                    PaymentStatusMessage = transaction.Status == PaymentStatus.Pending ? "Pending" : "Approved",
                    PaymentStatus = transaction.Status,
                });
            }

            var transactionsList = new TransactionListDto()
            {
                Balance = Math.Round(card.Balance, 2),
                Available = Math.Round((card.CardLimit - card.Balance), 2),
                DueStatusMessage = card.DueStatus == DueStatus.PaymentDue ? "You`ve a payment due" : "No payment due",
                TransactionsDetail = transactionDetailList,
                DailyPoints = DailyPointsCalculator.GetFormattedDailyPoints(account.CreatedAt)
            };

            return transactionsList;
        }

        
    }
}