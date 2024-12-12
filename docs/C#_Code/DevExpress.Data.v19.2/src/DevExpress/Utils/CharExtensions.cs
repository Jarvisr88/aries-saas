namespace DevExpress.Utils
{
    using System;
    using System.Globalization;

    public static class CharExtensions
    {
        public static char ToLower(char ch, CultureInfo culture) => 
            char.ToLower(ch, culture);

        public static char ToUpper(char ch, CultureInfo culture) => 
            char.ToUpper(ch, culture);
    }
}

