namespace DevExpress.Office.Utils
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class XmlCharsDecoder
    {
        private static Regex xmlCharDecodingRegex = new Regex(@"_x(?<value>([\da-fA-F]){4})_");

        public static string Decode(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                if (val.IndexOf('_') < 0)
                {
                    return val;
                }
                MatchCollection matchs = xmlCharDecodingRegex.Matches(val);
                if ((matchs == null) || (matchs.Count <= 0))
                {
                    return val;
                }
                for (int i = matchs.Count - 1; i >= 0; i--)
                {
                    int num3;
                    Match match = matchs[i];
                    string s = match.Groups["value"].Value;
                    if (int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3) && ((num3 <= 0x1f) || (num3 >= 0xffff)))
                    {
                        val = val.Remove(match.Index, match.Length);
                        val = val.Insert(match.Index, new string((char) num3, 1));
                    }
                }
            }
            return val;
        }
    }
}

