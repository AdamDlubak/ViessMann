using System;

namespace Adam
{
    public static class StringExtensions
    {
        public static bool IsPrimaryColor(this string inString)
        {
            string[] primaryColors = { "Red", "Yellow", "Blue" };
            foreach (var color in primaryColors)
            {
                if (inString.Equals(color, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}