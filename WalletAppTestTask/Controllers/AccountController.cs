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

        [HttpGet("getAllAccountData/{accountId}", Name = "GetAllDataForAccount")]
        public async Task<IActionResult> GetAllDataForAccount(long accountId)
        {
            var accountInfoDto = await _usersService.GetAccountDataWithTransactionsByIdAsync(accountId);

            var accountJson = JsonConvert.SerializeObject(accountInfoDto, Formatting.Indented);

            return Ok(accountJson);
        }

        [HttpGet("getTransactionList/{cardId}", Name = "Get transaction list")]
        public async Task<IActionResult> GetTransactionListByCardId(long cardId)
        {
            var transactionList = await _usersService.GetTransactionsListByCardAsync(cardId);

            var transactionListDto = JsonConvert.SerializeObject(transactionList, Formatting.Indented);

            return Ok(transactionListDto);
        }

        [HttpGet("getTransactionListForAccount/{accountId}", Name = "Get transaction list for account")]
        public async Task<IActionResult> GetTransactionListByAccountId(long accountId, Currency outInCurrency = Currency.USD)
        {
            var transactionList = await _usersService.GetTranactionListByAccountIdAsync(accountId, outInCurrency);

            var transactionListDto = JsonConvert.SerializeObject(transactionList, Formatting.Indented);

            return Ok(transactionList);
        }
    }
}
