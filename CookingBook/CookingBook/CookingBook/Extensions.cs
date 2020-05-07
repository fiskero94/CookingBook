using System.Collections.Generic;

namespace CookingBook
{
    public static class Extensions
    {
        public static string Separate<T>(this IEnumerable<T> values, char separator)
        {
            string result = string.Empty;

            foreach (T value in values)
            {
                result += value.ToString() + separator;
            }

            return result.TrimEnd(separator);
        }
    }
}