namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Generic;

    public static class DefaultSvgPalette
    {
        public static readonly Dictionary<string, string> Colors;
        public static readonly Dictionary<string, string> HexColors;
        private static readonly HashSet<string> AlternativeColors;

        static DefaultSvgPalette()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            dictionary1.Add("#d04d2f", "Red");
            dictionary1.Add("#4dae89", "Green");
            dictionary1.Add("#377ab5", "Blue");
            dictionary1.Add("#eeb764", "Yellow");
            dictionary1.Add("#000000", "Black");
            dictionary1.Add("#ffffff", "White");
            Colors = dictionary1;
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            dictionary2.Add("Red", "#d04d2f");
            dictionary2.Add("Green", "#4dae89");
            dictionary2.Add("Blue", "#377ab5");
            dictionary2.Add("Yellow", "#eeb764");
            dictionary2.Add("Black", "#000000");
            dictionary2.Add("White", "#ffffff");
            HexColors = dictionary2;
            HashSet<string> set1 = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            set1.Add("altred");
            set1.Add("altgreen");
            set1.Add("altblue");
            set1.Add("altyellow");
            set1.Add("altblack");
            set1.Add("altwhite");
            AlternativeColors = set1;
        }

        public static KeyValuePair<string, string> GetColor(string value, string name)
        {
            string key = value;
            if (value == null)
            {
                string local1 = value;
                key = string.Empty;
            }
            if (Colors.TryGetValue(key, out name))
            {
                return new KeyValuePair<string, string>(value, name);
            }
            string text2 = name;
            if (name == null)
            {
                string local2 = name;
                text2 = string.Empty;
            }
            return (!HexColors.TryGetValue(text2, out value) ? new KeyValuePair<string, string>(null, null) : new KeyValuePair<string, string>(value, name));
        }

        public static string GetHexColor(string name)
        {
            string str;
            string key = name;
            if (name == null)
            {
                string local1 = name;
                key = string.Empty;
            }
            return (HexColors.TryGetValue(key, out str) ? str : null);
        }

        public static bool IsAlternativeColor(string name)
        {
            string item = name;
            if (name == null)
            {
                string local1 = name;
                item = string.Empty;
            }
            return AlternativeColors.Contains(item);
        }
    }
}

