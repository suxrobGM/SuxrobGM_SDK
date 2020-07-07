using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SuxrobGM.Sdk.Extensions
{
    public static class StringExtensions
    {       
        /// <summary>
        /// Method generates slugs
        /// </summary>
        /// <param name="str">Given string</param>
        /// <param name="useHyphen">Flag which indicates that slug will be generated with hyphens instead underscore</param>
        /// <param name="useLowerLetters">Flag which indicates that slug will be generated with lower letters</param>
        /// <returns>Slugified string</returns>
        public static string Slugify(this string str, bool useHyphen = true, bool useLowerLetters = true)
        {
            var url = str.TranslateToLatin();
            
            // invalid chars           
            url = Regex.Replace(url, @"[^A-Za-z0-9\s-]", "");

            // convert multiple spaces into one space 
            url = Regex.Replace(url, @"\s+", " ").Trim();
            var words = url.Split().Where(str => !string.IsNullOrWhiteSpace(str));
            url = string.Join(useHyphen ? '-' : '_', words);

            if (useLowerLetters)
                url = url.ToLower();

            return url;
        }

        /// <summary>
        /// Translate all letters to latin alphabets
        /// </summary>
        /// <param name="str">Given string</param>
        /// <returns></returns>
        public static string TranslateToLatin(this string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (var i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }
       
        /// <summary>
        /// Remove redundant chars from given string
        /// </summary>
        /// <param name="str">Given string</param>
        /// <param name="allowedChars">Array of chars that allowed in this string. Default value includes all English letters, numbers, dot and underscore</param>
        /// <returns></returns>
        public static string IgnoreChars(this string str, string[] allowedChars = null)
        {
            allowedChars ??= new[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W",
                "X", "Y", "Z",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w",
                "x", "y", "z",
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "_"
            };            

            foreach (var ch in str)
            {
                if (!allowedChars.Contains(ch.ToString()))
                    str = str.Replace(ch.ToString(), "");
            }

            return str;
        }

        /// <summary>
        /// Removes all URL reserved chars from given string
        /// </summary>
        /// <param name="text">Given string</param>
        /// <returns></returns>
        public static string RemoveReservedUrlCharacters(this string text)
        {
            var reservedCharacters = new[] { "!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\", "^", "_", "'", "{", "}", "|", "~", "`", "+" };

            return reservedCharacters.Aggregate(text, (current, chr) => current.Replace(chr, ""));
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Converts given string to upper pascal case string
        /// </summary>
        /// <param name="input">Given string</param>
        /// <param name="ignoreSpaces">Flag indicates that ignoring whitespaces from given string</param>
        /// <returns></returns>
        public static string ToUpperPascalCase(this string input, bool ignoreSpaces = true)
        {
            var sentences = input.Split();
            var output = "";

            foreach (var sentence in sentences)
            {
                if (ignoreSpaces)
                {
                    output += sentence.ToUpperFirstLetter();
                }
                else
                {
                    output += " " + sentence.ToUpperFirstLetter();
                }
            }

            return output;
        }

        /// <summary>
        /// Only First letter of string will be converted to upper case
        /// </summary>
        /// <param name="input">Given string</param>
        /// <returns></returns>
        public static string ToUpperFirstLetter(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1),
            };
        }
    }
}
