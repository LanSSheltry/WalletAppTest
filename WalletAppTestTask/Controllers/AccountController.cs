using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WalletAppTestTask.DbContext;
using WalletAppTestTask.Services;

namespace WalletAppTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountsService _usersService;

        public AccountController(
            AccountsService usersService)
        {
            _usersService = usersService;
        }


        /*SUMMARY:
         * This endpoint could fetch all data for current account in the same format with
         * SQL tables. It allows to check items user has.
         */ 

        [HttpGet("getAllAccountData/{accountId}", Name = "GetAllDataForAccount")]
        public async Task<IActionResult> GetAllDataForAccount(long accountId)
        {
            var accountInfoDto = await _usersService.GetAccountDataWithTransactionsByIdAsync(accountId);

            var accountJson = JsonConvert.SerializeObject(accountInfoDto, Formatting.Indented);

            return Ok(accountJson);
        }

        /*SUMMARY:
        * This endpoint was created according to the task.
        * It could fetch data in the TransactionList format using cardId
        * (After adding accounts "cardId" has the same meaning as "userId" that described in the task)
        */

        [HttpGet("getTransactionList/{cardId}", Name = "Get transaction list")]
        public async Task<IActionResult> GetTransactionListByCardId(long cardId)
        {
            var transactionList = await _usersService.GetTransactionsListByCardAsync(cardId);

            var transactionListDto = JsonConvert.SerializeObject(transactionList, Formatting.Indented);

            return Ok(transactionListDto);
        }

        /*SUMMARY:
        * Current endpoint could fetch all data in the similar format as TransactionList but for several cards.
        * Also it could convert currencies and display common balance on the all cards.
        * currency:
        * 1 - UAH
        * 2 - EUR
        * 3 - USD
        * 4 - CAD
        */

        [HttpGet("getTransactionListForAccount/{accountId}", Name = "Get transaction list for account")]
        public async Task<IActionResult> GetTransactionListByAccountId(long accountId, Currency currency = Currency.USD)
        {
            var transactionList = await _usersService.GetTranactionListByAccountIdAsync(accountId, currency);

            var transactionListDto = JsonConvert.SerializeObject(transactionList, Formatting.Indented);

            return Ok(transactionList);
        }
    }
}
