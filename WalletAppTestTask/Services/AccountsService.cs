using WalletAppTestTask.DbContext;
using WalletAppTestTask.Mappers;
using WalletAppTestTask.Models;

namespace WalletAppTestTask.Services
{
    public class AccountsService
    {
        public readonly WalletAppDbContext _dbContext;

        public AccountsService(WalletAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AccountInfoDto> GetAccountDataWithTransactionsByIdAsync(long accountId)
        {
            var accountInfoDto = await getAllAccountDataAsync(accountId);

            return accountInfoDto;
        }

        public async Task<TransactionListDto> GetTransactionsListByCardAsync(long cardId)
        {
            try
            {
                var card = await _dbContext.GetBankCardDetailsByIdAsync(cardId);
                card.Transactions = await _dbContext.GetTransactionsByCardIdLimitedAsync(cardId);

                var accountInfo = await _dbContext.GetAccountInfoByIdAsync(card.AccountId);
                accountInfo.BankCards.Add(card);

                var accountInfoDto = accountInfo.ToDto();

                var mapper = new TransactionListBuilder();
                var transactionList = mapper.BuildTransactionListForCardByAccount(accountInfoDto);

                return transactionList;
            }
            catch (Exception ex)
            {
                //Here we can insert some handler of errors
                return null;
            }
        }

        public async Task<TransactionListDto> GetTranactionListByAccountIdAsync(long accountId, Currency outCurrency)
        {
            try
            {
                var accountInfoDto = await getAllAccountDataAsync(accountId);

                var transactions = new List<TransactionInfoDto>();

                if (accountInfoDto != null)
                {
                    foreach (var card in accountInfoDto.BankCards)
                    {
                        foreach (var transaction in card.Transactions)
                        {
                            transactions.Add(transaction);
                        }
                    }

                    var dtoBuilder = new TransactionListBuilder();

                    var transactionList = dtoBuilder.BuildTransactionListForAccountByAccountId(accountInfoDto, outCurrency);

                    return transactionList;
                }
                else return null;
            }
            catch(Exception ex)
            {
                //Here we can insert some handler of errors
                return null;
            }

        }

        private async Task<AccountInfoDto> getAllAccountDataAsync(long accountId)
        {
            try
            {
                var accountInfo = await _dbContext.GetAccountInfoByIdAsync(accountId);

                if (accountInfo != null)
                {
                    accountInfo.BankCards = await _dbContext.GetCardsForUserByIdAsync(accountId);

                    foreach (var card in accountInfo.BankCards)
                    {
                        await _dbContext.GetTransactionsByCardIdUnlimitedAsync(card.Id);
                    }

                    var accountInfoDto = accountInfo.ToDto();

                    foreach (var card in accountInfoDto.BankCards)
                    {
                        card.BankName = await _dbContext.GetBankNameForCardsByIdAsync(card.BankId);
                    }

                    return accountInfoDto;
                }
                else return null;
            }
            catch (Exception ex)
            {
                //Here we can insert some handler of errors
                return null;
            }
        }



    }
}
