using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WalletAppTestTask.DbContext;
using WalletAppTestTask.Interfaces;
using static WalletAppTestTask.DbContext.TransactionContext;

namespace WalletAppTestTask.Services
{

    //This class generates data for database and fills it
    public class DatabaseSeederService : IDataSeeder
    {
        private readonly WalletAppDbContext _dbContext;
        private readonly Random _random;

        private int _amountOfCardsCntr = 0;
        private int _amountOfTransactionsCntr = 0;

        private List<string> _bankNames = new List<string> {
            "PrivatBank",
            "Monobank",
            "UkrSibBank",
            "CrediAgricole Bank",
            "Oschadbank",
            "Raiffeisen Bank",
            "Prominvestbank",
            "OTP Bank",
            "JPMorgan Chase",
            "Bank of America",
            "Wells Fargo",
            "Citibank",
            "Goldman Sachs",
            "Morgan Stanley",
            "HSBC",
            "Barclays"};

        private readonly List<string> _cardNames = new List<string>() {
            "Gold",
            "Platinum",
            "Diamond",
            "Standard",
            "Bonus",
            "Debit" };

        private readonly List<string> _companyNames = new List<string>() {
            "Apple",
            "Microsoft",
            "Amazon", "Samsung",
            "Google",
            "Netflix",
            "AWS",
            "Porsche",
            "ATB",
            "Silpo",
            "Shawarma",
            "Elfbar"};

        private const int _amountOfGeneratedUsers = 15;
        private const int _maxAmountOfGeneratedCardsPerUser = 4;
        private const int _minAmountOfGeneratedCardsPerUser = 0;
        private const int _minTransactionsPerCardAmount = 0;
        private const int _maxTransactionsPerCardAmount = 70;

        private const decimal _maxBalance = 1500;
        private const decimal _minBalance = 0;



        public DatabaseSeederService(WalletAppDbContext dbContext)
        {
            _dbContext = dbContext;
            _random = new Random();
        }

        //Generates data to fill database randomly
        public async Task<string> SeedData()
        {
            try
            {
                await SeedBanks();
                await SeedUsers(_amountOfGeneratedUsers);

                return JsonConvert.SerializeObject(new
                {
                    Status = "Successfully generated!",
                    AmountOfUsers = _amountOfGeneratedUsers,
                    AmountOfCards = _amountOfCardsCntr,
                    AmountOfTransactions = _amountOfTransactionsCntr
                }, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Add banks into DB according to list of the names
        private async Task SeedBanks()
        {
            try
            {
                foreach (var bkName in _bankNames)
                {
                    var bank = new BankContext()
                    {
                        Title = bkName
                    };
                    if ((await _dbContext.Banks.Where(t => t.Title == bank.Title).FirstOrDefaultAsync()) == null)
                        _dbContext.Banks.Add(bank);
                }
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Generate and add into database random users
        private async Task SeedUsers(int amount)
        {

            for (int i = 0; i < amount; i++)
            {
                var user = new AccountContext
                {
                    //DueStatus = DueStatus.NoPaymentDue,
                    CreatedAt = DateTime.UtcNow.AddDays(-_random.Next(1, 1200))
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();


                await SeedBankCards(user.Id, _random.Next(
                    _minAmountOfGeneratedCardsPerUser,
                    _maxAmountOfGeneratedCardsPerUser));
            }
        }

        //Generate and add into database bank cards for user
        private async Task SeedBankCards(long accountId, int amount)
        {
            _amountOfCardsCntr += amount;

            for (int i = 0; i < amount; i++)
            {
                var rndIdx = _random.Next(0, _bankNames.Count);

                var bankCard = new BankCardContext
                {
                    AccountId = accountId,
                    DueStatus = DueStatus.NoPaymentDue,
                    BankId = (await _dbContext.Banks
                        .Where(t => t.Title == _bankNames[rndIdx])
                        .FirstOrDefaultAsync()).Id,
                    Currency = (Currency)_random.Next((int)Currency.UAH, (int)Currency.CAD + 1),
                    Balance = 0,
                    Name = GetRandomCardName(),
                    Type = (int)CardType.Debit
                };

                _dbContext.BankCards.Add(bankCard);
                _dbContext.SaveChanges();

                await SeedTransactions(bankCard.Id, _random.Next(_minTransactionsPerCardAmount, _maxTransactionsPerCardAmount));
            }
        }

        //Generate and add into database random transactions (0 < sum(group by bank_cards) < 1500)
        private async Task SeedTransactions(long bankCardId, int count)
        {
            _amountOfTransactionsCntr += count;

            for (int i = 0; i < count; i++)
            {
                var bankCard = await _dbContext.BankCards
                    .Where(t => t.Id == bankCardId)
                    .FirstOrDefaultAsync();

                var currentBalance = bankCard.Balance;

                var paymentType = (PaymentType)_random.Next(0, 2);
                var dayLimit = (await _dbContext.Users.Where(dl => dl.Id == bankCard.AccountId).FirstOrDefaultAsync()).CreatedAt;

                var transaction = new TransactionContext
                {
                    BankCardId = bankCardId,
                    Type = paymentType,
                    Total = CalculateTransactionSum(currentBalance, paymentType),
                    Currency = bankCard.Currency,
                    Status = (PaymentStatus)_random.Next(0, 2),
                    Name = GetRandomCompany(),
                    Description = $"Transaction Description #{i}",
                    AuthorizedUser = _random.Next(0, 2) == 0 ? null : $"SomeName #{bankCardId}",
                    CreatedAt = dayLimit.AddDays(_random.Next(1, (DateTime.UtcNow - dayLimit).Days)),
                    Icon = "Static/RandomIcon.png"
                };
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();

                UpdateBalance(transaction);
            }
        }

        private string GetRandomCardName()
        {
            return _cardNames[_random.Next(_cardNames.Count)];
        }

        private string GetRandomCompany()
        {
            return _companyNames[_random.Next(_companyNames.Count)];
        }

        //Update Balance according to sum of transactions for current card
        private void UpdateBalance(TransactionContext transaction)
        {
            var bankCard = _dbContext.BankCards.Find(transaction.BankCardId);

            if (transaction.Type == PaymentType.Payment) //Calculates current balance according to paymentType
            {
                bankCard.Balance = bankCard.Balance + transaction.Total;
            }
            else
            {
                bankCard.Balance = bankCard.Balance - transaction.Total;
            }

            _dbContext.SaveChanges();
        }

        //Calculates transaction sum to prevent possible OutOfRange according to task
        //Doesn`t contain logic for DateTime controlling, it will be randomly generated
        private decimal CalculateTransactionSum(decimal currentBalance, PaymentType paymentType)
        {
            decimal value;

            if (paymentType == 0)
            {
                value = getRandom(50, 1400);
                while (currentBalance + value > _maxBalance)
                {
                    value = getRandom(0, (long)(_maxBalance - currentBalance));
                }
            }
            else
            {
                if (currentBalance == 0) return 0;

                value = getRandom(50, 1400);
                while (currentBalance - value < _minBalance)
                {
                    value = getRandom((long)_minBalance, (long)(currentBalance));
                }
            }

            return value;

            decimal getRandom(long min, long max)
            {
                return (_random.NextInt64(min, max) + (decimal)_random.NextDouble());
            }
        }
    }
}

