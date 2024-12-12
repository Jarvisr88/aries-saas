namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class PdfTextUtils
    {
        private static readonly Regex separator = new Regex("[,.!@#$%^&*()+_=`\\{\\}\\[\\];:\"<>\\\\/?\\|-]");
        private static readonly Regex whitespace = new Regex(@"[\s]");
        private static readonly Regex CJK = new Regex(@"[\p{IsKatakana}\p{IsHiragana}\p{IsCJKUnifiedIdeographs}\p{IsCJKUnifiedIdeographsExtensionA}]");
        private static readonly string wordWrappers;

        static PdfTextUtils()
        {
            char[] chArray1 = new char[] { '‐', '\x00ad', '-' };
            wordWrappers = new string(chArray1);
        }

        public static bool DoOverlap(PdfOrientedRectangle rect1, PdfOrientedRectangle rect2)
        {
            if ((rect1.Angle != rect2.Angle) || ((Math.Abs((double) (rect1.Width - rect2.Width)) >= 0.001) || (Math.Abs((double) (rect1.Height - rect2.Height)) >= 0.001)))
            {
                return false;
            }
            PdfPoint point = RotatePoint(rect1.TopLeft, -rect1.Angle) - RotatePoint(rect2.TopLeft, -rect2.Angle);
            double num = 0.25 * rect1.Width;
            return ((Math.Abs(point.X) < num) && (Math.Abs(point.Y) < num));
        }

        public static double GetOrientedDistance(PdfPoint first, PdfPoint second, double angle) => 
            (angle != 0.0) ? (((second.X - first.X) * Math.Cos(-angle)) - ((second.Y - first.Y) * Math.Sin(-angle))) : (second.X - first.X);

        public static bool HasCJKSymbols(string unicodeChar) => 
            CJK.IsMatch(unicodeChar);

        public static bool HasRTLMark(string unicodeChar)
        {
            foreach (char ch in unicodeChar)
            {
                if (PdfBidiCharacterClasses.GetCharacterClass(ch) == PdfBidiCharacterClass.RTL)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSeparator(string unicodeChar) => 
            separator.IsMatch(unicodeChar);

        public static bool IsWhitespace(string unicodeChar) => 
            whitespace.IsMatch(unicodeChar);

        public static bool IsWrapSymbol(string unicodeChar) => 
            wordWrappers.Contains(unicodeChar);

        public static string NormalizeAndCompose(string text)
        {
            bool flag = false;
            string str = text;
            int num = 0;
            while (true)
            {
                if (num >= str.Length)
                {
                    return (!flag ? text.Normalize(NormalizationForm.FormKC) : text);
                }
                char c = str[num];
                if (char.IsHighSurrogate(c))
                {
                    flag = true;
                }
                else if (char.IsLowSurrogate(c))
                {
                    if (!flag)
                    {
                        return text;
                    }
                    flag = false;
                }
                if ((c >= 0xfdd0) && (c <= 0xfdef))
                {
                    break;
                }
                if ((c == 0xfffe) || (c == 0xffff))
                {
                    break;
                }
                num++;
            }
            return text;
        }

        public static string NormalizeAndDecompose(string text) => 
            text.Normalize(NormalizationForm.FormKD);

        public static PdfPoint RotatePoint(PdfPoint point, double angle)
        {
            if (angle == 0.0)
            {
                return point;
            }
            double x = point.X;
            double y = point.Y;
            double num3 = Math.Sin(angle);
            double num4 = Math.Cos(angle);
            return new PdfPoint((x * num4) - (y * num3), (x * num3) + (y * num4));
        }
    }
}

