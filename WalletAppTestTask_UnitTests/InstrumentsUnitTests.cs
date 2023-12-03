using System.Security.Cryptography.X509Certificates;
using WalletAppTestTask;
using Xunit;

namespace WalletAppTestTask_UnitTests
{
    public class InstrumentsUnitTests
    {
        [Fact]
        public void DailyPointsCalculateTest()
        {
            DateTime date1 = DateTime.Parse("2022-05-15");
            DateTime date2 = DateTime.Parse("2021-11-01");
            DateTime date3 = DateTime.Parse("2020-03-15");
            DateTime date4 = DateTime.Parse("2023-12-01");

            Assert.Equal("35876952K", DailyPointsCalculator.GetFormattedDailyPoints(date1));
            Assert.Equal("571593K", DailyPointsCalculator.GetFormattedDailyPoints(date2));
            Assert.Equal("521", DailyPointsCalculator.GetFormattedDailyPoints(date3));
            Assert.Equal("2", DailyPointsCalculator.GetFormattedDailyPoints(date4));

        }
    }
}