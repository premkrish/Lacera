using System.Text.RegularExpressions;

namespace Lacera.Helper
{
    public static class Extensions
    {
        /// <summary>
        /// Removes the quotes.
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <returns></returns>
        public static string RemoveQuotes(this string inputValue)
        {
            var returnString = string.Empty;
            if (!string.IsNullOrWhiteSpace(inputValue))
            {
                returnString = Regex.Replace(inputValue, "['\"]", string.Empty).Trim();
            }
            return returnString;
        }
    }
}
