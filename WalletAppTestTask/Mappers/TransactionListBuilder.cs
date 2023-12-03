using WalletAppTestTask.DbContext;
using WalletAppTestTask.Interfaces;
using WalletAppTestTask.Models;
using static WalletAppTestTask.DbContext.TransactionContext;

namespace WalletAppTestTask.Mappers
{
    public class TransactionListBuilder
    {

        //To build data model according to task
        public TransactionListDto BuildTransactionListForCardByAccount(AccountInfoDto account)
        {
            var transactionDetailList = new List<TransactionDetailDto>();
            var card = account.BankCards.FirstOrDefault();
            var currentMonth = mapDateTimeMonthToMonth(DateTime.UtcNow.Month);

            foreach (var transaction in card.Transactions)
            {
                var type = transaction.Type;
                var transactionTotal = Math.Round(transaction.Total, 2);
                transactionDetailList.Add(new TransactionDetailDto()
                {
                    Id = transaction.Id,
                    Total = (type == PaymentType.Payment) ? transactionTotal : (-transactionTotal),
                    Currency = mapCurrencyToText(transaction.Currency),
                    TransactionDatetime = transaction.CreatedAt,
                    TransactionName = transaction.Name,
                    CardName = card.Name,
                    AuthorizedUser = transaction.AuthorizedUser,
                    DayOfWeek = transaction.CreatedAt.DayOfWeek.ToString(),
                    Description = transaction.Description,
                    PaymentStatusMessage = mapPaymentStatusToText(transaction.Status),
                    PaymentStatus = transaction.Status,
                });
            }

            var transactionsList = new TransactionListDto()
            {
                Balance = Math.Round(card.Balance, 2),
                Currency = mapCurrencyToText(card.Currency),
                Available = Math.Round((card.CardLimit - card.Balance), 2),
                DueStatus = mapDueStatusToText(card.DueStatus),
                DueStatusMessage = card.DueStatus == DueStatus.NoPaymentDue ?
                    $"You`ve paid your {currentMonth} balance." :
                    $"You`ve NOT paid your {currentMonth} balance.",
                TransactionsDetail = transactionDetailList,
                DailyPoints = DailyPointsCalculator.GetFormattedDailyPoints(DateTime.UtcNow)
            };

            return transactionsList;
        }

        public TransactionListDto BuildTransactionListForAccountByAccountId(AccountInfoDto account, Currency currencyCode)
        {
            var transactions = new List<TransactionInfoDto>();

            decimal commonBalance = 0;
            decimal limitSum = 0;

            var currentMonth = mapDateTimeMonthToMonth(DateTime.UtcNow.Month);

            foreach (var card in account.BankCards)
            {
                transactions.AddRange(card.Transactions);
                commonBalance += CurrencyConverter.ConvertMoney(
                    from: card.Currency,
                    to: currencyCode,
                    amount: card.Balance);

                limitSum += CurrencyConverter.ConvertMoney(
                    from: card.Currency,
                    to: currencyCode,
                    amount: card.CardLimit);
            }

            transactions = transactions.OrderByDescending(t => t.CreatedAt).Take(10).ToList();

            var transactionDetailList = new List<TransactionDetailDto>();

            foreach (var transaction in transactions)
            {
                var type = transaction.Type;
                var transactionTotal = Math.Round(transaction.Total, 2);

                var currCard = findItemById(account.BankCards, transaction.BankCardId);

                transactionDetailList.Add(new TransactionDetailDto()
                {
                    Id = transaction.Id,
                    Total = (type == PaymentType.Payment) ? transactionTotal : (-transactionTotal),
                    Currency = mapCurrencyToText(currencyCode),
                    TransactionDatetime = transaction.CreatedAt,
                    TransactionName = transaction.Name,
                    CardName = $"{currCard.BankName} - {currCard.Name}",
                    AuthorizedUser = transaction.AuthorizedUser,
                    DayOfWeek = transaction.CreatedAt.DayOfWeek.ToString(),
                    Description = transaction.Description,
                    PaymentStatusMessage = mapPaymentStatusToText(transaction.Status),
                    PaymentStatus = transaction.Status,
                });
            }

            var transactionsList = new TransactionListDto()
            {
                Balance = Math.Round(commonBalance, 2),
                Currency = mapCurrencyToText(currencyCode),
                Available = Math.Round((limitSum - commonBalance), 2),
                DueStatus = mapDueStatusToText(getCommonDueStatus()),
                DueStatusMessage =
                    getCommonDueStatus() == DueStatus.NoPaymentDue ?
                    $"You`ve paid your {currentMonth} balance." :
                    $"You have not paid your {currentMonth} balance.",
                TransactionsDetail = transactionDetailList,
                DailyPoints = DailyPointsCalculator.GetFormattedDailyPoints(DateTime.UtcNow)
            };

            return transactionsList;

            DueStatus getCommonDueStatus()
            {
                foreach (var card in account.BankCards)
                {
                    if (card.DueStatus == DueStatus.PaymentDue)
                        return DueStatus.PaymentDue;
                }
                return DueStatus.NoPaymentDue;
            }
        }

        //To find any item in any List by ids
        private T? findItemById<T>(List<T> items, long id) where T : class, IHasId, new()
        {
            foreach (var item in items)
            {
                if (item.GetId() == id)
                    return item;
            }

            return new T();
        }

        /*
         * Further mappers created for easy upgrading text on the frontend without application updates
         */
        private string mapDueStatusToText(DueStatus status)
        {
            switch (status)
            {
                case DueStatus.PaymentDue:
                    return "You`ve a payment due";
                case DueStatus.NoPaymentDue:
                    return "No payment due";
                default:
                    return "Not mapped";
            }
        }

        private string mapPaymentStatusToText(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.Approved:
                    return "Approved";
                case PaymentStatus.Pending:
                    return "Pending";
                default:
                    return "Not mapped";
            }
        }

        private string mapCurrencyToText(Currency currency)
        {
            switch (currency)
            {
                case Currency.USD:
                    return "$";
                case Currency.UAH:
                    return "₴";
                case Currency.EUR:
                    return "€";
                case Currency.CAD:
                    return "CA$";
                default:
                    return "Not mapped";
            }
        }

        private string mapDateTimeMonthToMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "Not mapped";
            }
        }

    }
}