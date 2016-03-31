using System;
using System.Globalization;
using System.Linq;

namespace ELFinder.Connector.Extensions
{

    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {

        #region Extension methods

        /// <summary>
        /// Convert given value to camel case
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Camel cased value</returns>
        public static string ToCamelCase(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return string.Join(".",
                from n in value.Split('.')
                select char.ToLower(n[0], CultureInfo.InvariantCulture) + n.Substring(1));
        }

        #endregion

    }
}