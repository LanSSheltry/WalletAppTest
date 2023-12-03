namespace WalletAppTestTask
{
    public static class DailyPointsCalculator
    {
        public static string GetFormattedDailyPoints(DateTime createdAt)
        {
            var currentDate = DateTime.UtcNow;

            var points = CalculatePoints(createdAt, currentDate);

            if (points >= 1000)
            {
                points /= 1000;
                return $"{points}K";
            }
            else
            {
                return $"{points}";
            }
        }

        static long CalculatePoints(DateTime accountCreationDate, DateTime currentDate)
        {
            long points = 0;

            Queue<long> pointsQueue = new Queue<long>(2);
            long previousDayPoints = 0;
            long beforePreviousDayPoints = 0;
            pointsQueue.Enqueue(0);
            pointsQueue.Enqueue(0);

            while (accountCreationDate <= currentDate)
            {
                if (IsFirstDayOfSeason(accountCreationDate))
                {
                    points += 2;
                }
                else if (IsSecondDayOfSeason(accountCreationDate))
                {
                    points += 3;
                }
                else
                {
                    previousDayPoints = pointsQueue.ToArray()[0];
                    beforePreviousDayPoints = pointsQueue.ToArray()[1];
                    points += (long)0.6 * beforePreviousDayPoints + previousDayPoints;

                }
                pointsQueue.Enqueue(points);
                accountCreationDate = accountCreationDate.AddDays(1);
            }

            return points;
        }

        static bool IsFirstDayOfSeason(DateTime date)
        {
            return date.Month % 3 == 0 && date.Day == 1;
        }

        static bool IsSecondDayOfSeason(DateTime date)
        {
            return date.Month % 3 == 0 && date.Day == 2;
        }
    }
}
