using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public async Task<string> GetAccountDataWithTransactionsByIdAsync(long id)
        {
            var accountInfo = await _dbContext.GetAccountInfoByIdAsync(id);

            accountInfo.BankCards = await _dbContext.GetCardsForUserByIdAsync(id);

            foreach (var card in accountInfo.BankCards)
            {
                await _dbContext.GetTransactionsByCardId(card.Id);
            }            

            var accountInfoDto = accountInfo.ToDto();

            foreach(var card in accountInfoDto.BankCards)
            {
                card.BankName = await _dbContext.GetBankNameForCardsById(card.BankId);
            }

            var userJson = JsonConvert.SerializeObject(accountInfoDto, Formatting.Indented);

            return userJson;
        }

        public async Task<string> GetTransactionsListForCard(long cardId)
        {
            var card = await _dbContext.GetBankCardDetailsByIdAsync(cardId);
            card.Transactions = await _dbContext.GetTransactionsByCardId(cardId);

            var accountInfo = await _dbContext.GetAccountInfoByIdAsync(card.AccountId);
            accountInfo.BankCards.Add(card);

            var accountInfoDto = accountInfo.ToDto();

            var mapper = new DtoModelsMapper();
            var transactionList = mapper.BuildTransactionListForCardByAccount(accountInfoDto);

            return JsonConvert.SerializeObject(transactionList, Formatting.Indented);
        }



    }
}
