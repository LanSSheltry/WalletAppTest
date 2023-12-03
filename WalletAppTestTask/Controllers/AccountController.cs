using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        [HttpGet("getAllAccountData/{id}", Name = "GetAllDataForAccount")]
        public async Task<IActionResult> GetAllAccountDataForUser(long id)
        {
            var accountData = await _usersService.GetAccountDataWithTransactionsByIdAsync(id);

            return Ok(accountData);
        }

        [HttpGet("getTransactionList/{cardId}", Name = "GetTransactionList")]
        public async Task<IActionResult> GetTransactionListByCardId(long cardId)
        {
            var transactionList = await _usersService.GetTransactionsListForCard(cardId);

            return Ok(transactionList);
        }


    }
}
