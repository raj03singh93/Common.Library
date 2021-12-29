using System.Text;

namespace Common.Utils
{
    /// <summary>
    /// String Utility class for Campaigner.
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// Checks whether a string contains a Unicode character such as emojis, Symbols etc.
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Returns true/false based on check.</returns>
        public static bool DoesContainsSymbols(string text)
        {
            /* 
             * Here, we checking for all the Letters and numbers including some special characters.
             */
            return Encoding.UTF8.GetByteCount(text) != text.Length;
        }
    }
}
