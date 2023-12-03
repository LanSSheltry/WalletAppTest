namespace WalletAppTestTask
{
    public static class DailyPointsCalculator
    {
        public static string GetFormattedDailyPoints(DateTime currentDate)
        {
            var seasonStartDate = getDateOfStartSeason(currentDate);

            var points = CalculatePoints(seasonStartDate, currentDate);

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

        static DateTime getDateOfStartSeason(DateTime currentDate)
        {
            var springStart = new DateTime(currentDate.Year, 3, 1);
            var summerStart = new DateTime(currentDate.Year, 6, 1);
            var autumnStart = new DateTime(currentDate.Year, 9, 1);
            var winterStart = new DateTime(currentDate.Year, 12, 1);

            if (currentDate < summerStart && currentDate > springStart) return springStart;
            else if (currentDate < autumnStart && currentDate > summerStart) return summerStart;
            else if (currentDate < winterStart && currentDate > autumnStart) return autumnStart;
            else if (currentDate < springStart)
            {
                return winterStart.AddYears(-1);
            }
            else return winterStart;
        }

        static long CalculatePoints(DateTime seasonStartDate, DateTime currentDate)
        {
            double points = 0;

            Queue<double> pointsQueue = new Queue<double>(2);
            double previousDayPoints;
            double beforePreviousDayPoints;
            long days = 0;
            pointsQueue.Enqueue(0);
            pointsQueue.Enqueue(0);

            while (seasonStartDate <= currentDate)
            {
                days++;
                if (IsFirstDayOfSeason(seasonStartDate))
                {
                    points += 2;
                    pushValueIntoPointsQueue(2);
                }
                else if (IsSecondDayOfSeason(seasonStartDate))
                {
                    points += 3;
                    pushValueIntoPointsQueue(3);
                }
                else
                {
                    beforePreviousDayPoints = pointsQueue.ToArray<double>()[0];
                    previousDayPoints = pointsQueue.ToArray<double>()[1];
                    double addPoints = beforePreviousDayPoints + 0.6 * previousDayPoints;
                    points += addPoints;
                    if (points > 0)
                        pushValueIntoPointsQueue(addPoints);
                }
                seasonStartDate = seasonStartDate.AddDays(1);
            }

            return (long)Math.Round(points, 0);

            void pushValueIntoPointsQueue(double p)
            {
                pointsQueue.Enqueue(p);
                pointsQueue.Dequeue();
            }
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
