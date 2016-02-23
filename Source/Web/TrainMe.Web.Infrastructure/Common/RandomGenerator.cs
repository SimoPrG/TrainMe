namespace TrainMe.Web.Infrastructure.Common
{
    using System;
    using System.Text;

    public static class RandomGenerator
    {
        private const string SymbolsWithSpaces = "ABCDE FGHIJ KLMNO PQRST UVWXY Zabcd efghi jklmn opqrs tuvwx yz123 45678 90";
        private const string SymbolsOnly = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        private static readonly Random Random = new Random();

        public static int GetRandomNumber(int min = 0, int max = int.MaxValue)
        {
            if (min > max)
            {
                RandomGenerator.Swap(ref min, ref max);
            }

            if (max == int.MaxValue)
            {
                max--;
            }

            return RandomGenerator.Random.Next(min, max + 1);
        }

        public static string GetRandomString(int minLength = 0, int maxLength = int.MaxValue)
        {
            if (minLength > maxLength)
            {
                RandomGenerator.Swap(ref minLength, ref maxLength);
            }

            if (maxLength == int.MaxValue)
            {
                maxLength--;
            }

            var length = RandomGenerator.Random.Next(minLength, maxLength + 1);
            var result = new StringBuilder();
            bool isPreviousSpace = false;
            for (int i = 0; i < length; i++)
            {
                if (i == 0 || i == length - 1 || isPreviousSpace)
                {
                    result.Append(RandomGenerator.SymbolsOnly[RandomGenerator.Random.Next(RandomGenerator.SymbolsOnly.Length)]);
                    if (isPreviousSpace)
                    {
                        isPreviousSpace = false;
                    }

                    continue;
                }

                char current =
                    RandomGenerator.SymbolsWithSpaces[
                        RandomGenerator.Random.Next(RandomGenerator.SymbolsWithSpaces.Length)];
                result.Append(current);

                if (current == ' ')
                {
                    isPreviousSpace = true;
                }
            }

            return result.ToString();
        }

        public static double GetRandomDouble(double min = 0.0, double max = double.MaxValue)
        {
            if (min > max)
            {
                RandomGenerator.Swap(ref min, ref max);
            }

            return min + ((max - min) * RandomGenerator.Random.NextDouble());
        }

        public static DateTime GetRandomDate(DateTime? after = null, DateTime? before = null)
        {
            var minDateTime = after ?? DateTime.MinValue;
            var maxDateTime = before ?? DateTime.MaxValue;

            if (minDateTime > maxDateTime)
            {
                RandomGenerator.Swap(ref minDateTime, ref maxDateTime);
            }

            int days = (maxDateTime - minDateTime).Days;
            return minDateTime.AddDays(RandomGenerator.Random.Next(days));
        }

        private static void Swap<T>(ref T first, ref T second)
        {
            var temporary = first;
            first = second;
            second = temporary;
        }
    }
}
