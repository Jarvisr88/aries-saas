namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class XlNumFmtLocalizer
    {
        private CultureInfo lastCulture;
        private string generalDesignator;
        private Dictionary<char, XlNumFmtDesignator> designators = new Dictionary<char, XlNumFmtDesignator>();
        private Dictionary<XlNumFmtDesignator, char> chars = new Dictionary<XlNumFmtDesignator, char>();

        private void AddSeparator(char symbol, XlNumFmtDesignator designator)
        {
            if (!this.designators.ContainsKey(symbol))
            {
                this.designators.Add(symbol, designator);
            }
            else
            {
                Dictionary<char, XlNumFmtDesignator> designators = this.designators;
                char ch = symbol;
                designators[ch] = ((XlNumFmtDesignator) designators[ch]) | designator;
            }
        }

        private void GenerateDesignators(CultureInfo culture)
        {
            char symbol = culture.NumberFormat.NumberDecimalSeparator[0];
            char ch2 = culture.GetDateSeparator()[0];
            char c = culture.NumberFormat.NumberGroupSeparator[0];
            char ch4 = culture.GetTimeSeparator()[0];
            if (char.IsWhiteSpace(c))
            {
                c = ' ';
            }
            this.AddSeparator(symbol, XlNumFmtDesignator.DecimalSeparator);
            this.AddSeparator(ch2, XlNumFmtDesignator.DateSeparator);
            this.AddSeparator(c, XlNumFmtDesignator.GroupSeparator);
            this.AddSeparator(ch4, XlNumFmtDesignator.TimeSeparator);
            this.AddSeparator('/', XlNumFmtDesignator.FractionOrDateSeparator);
            this.designators.Add('a', XlNumFmtDesignator.AmPm);
            this.designators.Add('*', XlNumFmtDesignator.Asterisk);
            this.designators.Add('@', XlNumFmtDesignator.At);
            this.designators.Add('\\', XlNumFmtDesignator.Backslash);
            this.designators.Add('[', XlNumFmtDesignator.Bracket);
            this.designators.Add('#', XlNumFmtDesignator.DigitEmpty);
            this.designators.Add('?', XlNumFmtDesignator.DigitSpace);
            this.designators.Add('0', XlNumFmtDesignator.DigitZero);
            this.designators.Add(';', XlNumFmtDesignator.EndOfPart);
            this.designators.Add('E', XlNumFmtDesignator.Exponent);
            this.designators.Add('e', XlNumFmtDesignator.InvariantYear);
            this.designators.Add('%', XlNumFmtDesignator.Percent);
            this.designators.Add('"', XlNumFmtDesignator.Quote);
            this.designators.Add('b', XlNumFmtDesignator.ThaiYear);
            this.designators.Add('_', XlNumFmtDesignator.Underline);
            this.generalDesignator = GenerateInvariant(this.designators);
        }

        private static string GenerateInvariant(Dictionary<char, XlNumFmtDesignator> designators)
        {
            Dictionary<char, XlNumFmtDesignator> dictionary = designators;
            dictionary['a'] = ((XlNumFmtDesignator) dictionary['a']) | XlNumFmtDesignator.DayOfWeek;
            designators.Add('d', XlNumFmtDesignator.Day);
            designators.Add('h', XlNumFmtDesignator.Hour);
            designators.Add('m', XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute);
            designators.Add('s', XlNumFmtDesignator.Second);
            designators.Add('y', XlNumFmtDesignator.Year);
            designators.Add('g', XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General);
            return "General";
        }

        public void SetCulture(CultureInfo culture)
        {
            if (!ReferenceEquals(this.lastCulture, culture))
            {
                this.lastCulture = culture;
                this.designators.Clear();
                this.chars.Clear();
                this.GenerateDesignators(culture);
                foreach (char ch in this.designators.Keys)
                {
                    int count = this.chars.Count;
                    XlNumFmtDesignator key = this.designators[ch];
                    if ((key & XlNumFmtDesignator.AmPm) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.AmPm, ch);
                    }
                    if ((key & XlNumFmtDesignator.Year) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.Year, ch);
                    }
                    if ((key & XlNumFmtDesignator.InvariantYear) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.InvariantYear, ch);
                    }
                    if ((key & XlNumFmtDesignator.Month) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.Month, ch);
                    }
                    if ((key & XlNumFmtDesignator.Minute) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.Minute, ch);
                    }
                    if ((key & XlNumFmtDesignator.DateSeparator) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.DateSeparator, ch);
                    }
                    if ((key & XlNumFmtDesignator.DecimalSeparator) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.DecimalSeparator, ch);
                    }
                    if ((key & XlNumFmtDesignator.FractionOrDateSeparator) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.FractionOrDateSeparator, ch);
                    }
                    if ((key & XlNumFmtDesignator.GroupSeparator) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.GroupSeparator, ch);
                    }
                    if ((key & XlNumFmtDesignator.TimeSeparator) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.TimeSeparator, ch);
                    }
                    if ((key & XlNumFmtDesignator.Day) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.Day, ch);
                    }
                    if ((key & XlNumFmtDesignator.Second) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.Second, ch);
                    }
                    if ((key & XlNumFmtDesignator.General) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.General, ch);
                    }
                    if ((key & XlNumFmtDesignator.JapaneseEra) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.JapaneseEra, ch);
                    }
                    if ((key & XlNumFmtDesignator.DayOfWeek) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.DayOfWeek, ch);
                    }
                    if ((key & XlNumFmtDesignator.Hour) > XlNumFmtDesignator.Default)
                    {
                        this.chars.Add(XlNumFmtDesignator.Hour, ch);
                    }
                    if (this.chars.Count == count)
                    {
                        this.chars.Add(key, ch);
                    }
                }
            }
        }

        public CultureInfo Culture =>
            this.lastCulture;

        public Dictionary<char, XlNumFmtDesignator> Designators =>
            this.designators;

        public Dictionary<XlNumFmtDesignator, char> Chars =>
            this.chars;

        public string GeneralDesignator =>
            this.generalDesignator;

        private delegate string TableGenerator(Dictionary<char, XlNumFmtDesignator> designators);
    }
}

