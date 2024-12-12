namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class XlNumFmtParser
    {
        private readonly XlNumFmtLocalizer localizer;
        private readonly List<Color> AllowedColors;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods2;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods3;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods4;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods5;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods6;
        private readonly Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod> parseMethods7;
        private string formatString;
        private List<XlNumFmtSimple> formats;
        private List<IXlNumFmtElement> elements;
        private XlNumFmtDisplayLocale locale;
        private XlNumFmtSimple part;
        private string elementString;
        private IXlNumFmtElement element;
        private int currentIndex;
        private char currentSymbol;
        private bool errorState;
        private XlNumFmtDesignator designator;
        private NumberFormatDesignatorParseMethod designatorParser;
        private bool elapsed;
        private bool hasMilliseconds;
        private int elementLength;
        private bool grouping;
        private bool isDecimal;
        private int displayFactor;
        private int percentCount;
        private int integerCount;
        private int decimalCount;
        private int dividentCount;
        private int decimalSeparatorIndex;
        private int preFractionIndex;
        private bool explicitSign;
        private int expCount;
        private int expIndex;
        private int divisorCount;
        private int divisor;
        private int divisorPow;
        private int fractionSeparatorIndex;

        public XlNumFmtParser()
        {
            List<Color> list1 = new List<Color>();
            list1.Add(DXColor.Red);
            list1.Add(DXColor.Black);
            list1.Add(DXColor.White);
            list1.Add(DXColor.Blue);
            list1.Add(DXColor.Magenta);
            list1.Add(DXColor.Yellow);
            list1.Add(DXColor.Cyan);
            list1.Add(DXColor.Green);
            this.AllowedColors = list1;
            this.localizer = new XlNumFmtLocalizer();
            this.parseMethods = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnAt));
            this.parseMethods.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket));
            this.parseMethods.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault));
            this.parseMethods.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnNumericSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnNumericSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnNumericSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnNumericSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart));
            this.parseMethods.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnESymbol));
            this.parseMethods.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral));
            this.parseMethods.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime));
            this.parseMethods.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime));
            this.parseMethods.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime));
            this.parseMethods.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime));
            this.parseMethods.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods[XlNumFmtDesignator.Hour]);
            this.parseMethods.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnESymbol));
            this.parseMethods.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnNumericSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnDateTimeSymbol));
            this.parseMethods2 = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods2.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnAmPmOrDayOfWeek));
            this.parseMethods2.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnAmPmOrMonth));
            this.parseMethods2.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnAmPmOrYear));
            this.parseMethods2.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods2[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods2.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods2.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods2.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods2.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket2));
            this.parseMethods2.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnDateSeparator));
            this.parseMethods2.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnDay));
            this.parseMethods2.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault2));
            this.parseMethods2.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods2.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnMilisecond));
            this.parseMethods2.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods2.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods2.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods2.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart2));
            this.parseMethods2.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnESymbol2));
            this.parseMethods2.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnDefaultDateSeparator));
            this.parseMethods2.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods2.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral2));
            this.parseMethods2.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDay));
            this.parseMethods2.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods2[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods2.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrSecond));
            this.parseMethods2.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrInvariantYear));
            this.parseMethods2.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrJapaneseEra));
            this.parseMethods2.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnHour));
            this.parseMethods2.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods2[XlNumFmtDesignator.Hour]);
            this.parseMethods2.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnESymbol2));
            this.parseMethods2.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnJapaneseEra));
            this.parseMethods2.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnMonthOrMinute));
            this.parseMethods2.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnMinute));
            this.parseMethods2.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnMonth));
            this.parseMethods2.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods2.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods2.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnSecond));
            this.parseMethods2.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnThaiYear));
            this.parseMethods2.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnTimeSeparator));
            this.parseMethods2.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods2.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnYear));
            this.parseMethods3 = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods3.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods3[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods3.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods3.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods3.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket3));
            this.parseMethods3.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault3));
            this.parseMethods3.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods3.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnDecimalSeparator));
            this.parseMethods3.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnDigitEmpty));
            this.parseMethods3.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnDigitSpace));
            this.parseMethods3.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnDigitZero));
            this.parseMethods3.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart3));
            this.parseMethods3.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnESymbol3));
            this.parseMethods3.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnFractionSeparator));
            this.parseMethods3.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnGroupSeparator));
            this.parseMethods3.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral3));
            this.parseMethods3.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods3[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods3.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods3[XlNumFmtDesignator.Hour]);
            this.parseMethods3.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnESymbol3));
            this.parseMethods3.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnPercent));
            this.parseMethods3.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods3.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods3.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods3.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4 = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods4.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods4[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods4.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods4.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods4.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket3));
            this.parseMethods4.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault3));
            this.parseMethods4.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods4.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnDecimalSeparator));
            this.parseMethods4.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnDigitEmpty4));
            this.parseMethods4.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnDigitSpace4));
            this.parseMethods4.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnDigitZero4));
            this.parseMethods4.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart4));
            this.parseMethods4.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods4.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral3));
            this.parseMethods4.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods4[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods4.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods4[XlNumFmtDesignator.Hour]);
            this.parseMethods4.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods4.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods4.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods4.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods4.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5 = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods5.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods5[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods5.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods5.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods5.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket3));
            this.parseMethods5.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault3));
            this.parseMethods5.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods5.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnDigitEmpty5));
            this.parseMethods5.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnDigitSpace5));
            this.parseMethods5.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnDigitZero5));
            this.parseMethods5.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart5));
            this.parseMethods5.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods5.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral3));
            this.parseMethods5.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods5[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods5.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods5[XlNumFmtDesignator.Hour]);
            this.parseMethods5.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnPercent));
            this.parseMethods5.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods5.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods5.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods5.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6 = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods6.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods6[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods6.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods6.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnAt6));
            this.parseMethods6.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods6.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket3));
            this.parseMethods6.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault3));
            this.parseMethods6.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods6.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart6));
            this.parseMethods6.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods6.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral3));
            this.parseMethods6.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods6[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods6.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods6[XlNumFmtDesignator.Hour]);
            this.parseMethods6.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods6.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods6.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods6.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods6.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7 = new Dictionary<XlNumFmtDesignator, NumberFormatDesignatorParseMethod>();
            this.parseMethods7.Add(XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Year | XlNumFmtDesignator.AmPm, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.DayOfWeek | XlNumFmtDesignator.AmPm, this.parseMethods7[XlNumFmtDesignator.Month | XlNumFmtDesignator.AmPm]);
            this.parseMethods7.Add(XlNumFmtDesignator.Asterisk, new NumberFormatDesignatorParseMethod(this.OnAsterisk));
            this.parseMethods7.Add(XlNumFmtDesignator.At, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Backslash, new NumberFormatDesignatorParseMethod(this.OnBackslash));
            this.parseMethods7.Add(XlNumFmtDesignator.Bracket, new NumberFormatDesignatorParseMethod(this.OnBracket3));
            this.parseMethods7.Add(XlNumFmtDesignator.DateSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods7.Add(XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.DayOfWeek, new NumberFormatDesignatorParseMethod(this.OnDayOfWeekOrDefault3));
            this.parseMethods7.Add(XlNumFmtDesignator.Default, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods7.Add(XlNumFmtDesignator.DecimalSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods7.Add(XlNumFmtDesignator.DigitEmpty, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.DigitSpace, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.DigitZero, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.EndOfPart, new NumberFormatDesignatorParseMethod(this.OnEndOfPart7));
            this.parseMethods7.Add(XlNumFmtDesignator.Exponent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.FractionOrDateSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods7.Add(XlNumFmtDesignator.GroupSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods7.Add(XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneral7));
            this.parseMethods7.Add(XlNumFmtDesignator.General | XlNumFmtDesignator.Day, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime7));
            this.parseMethods7.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General | XlNumFmtDesignator.Day, this.parseMethods7[XlNumFmtDesignator.General | XlNumFmtDesignator.Day]);
            this.parseMethods7.Add(XlNumFmtDesignator.Second | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime7));
            this.parseMethods7.Add(XlNumFmtDesignator.InvariantYear | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime7));
            this.parseMethods7.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.General, new NumberFormatDesignatorParseMethod(this.OnGeneralOrDateTime7));
            this.parseMethods7.Add(XlNumFmtDesignator.Hour, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.JapaneseEra | XlNumFmtDesignator.Hour, this.parseMethods7[XlNumFmtDesignator.Hour]);
            this.parseMethods7.Add(XlNumFmtDesignator.InvariantYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.JapaneseEra, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Month | XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Minute, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Month, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Percent, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.Quote, new NumberFormatDesignatorParseMethod(this.OnQuote));
            this.parseMethods7.Add(XlNumFmtDesignator.Second, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.ThaiYear, new NumberFormatDesignatorParseMethod(this.OnError));
            this.parseMethods7.Add(XlNumFmtDesignator.TimeSeparator, new NumberFormatDesignatorParseMethod(this.OnDefault));
            this.parseMethods7.Add(XlNumFmtDesignator.Underline, new NumberFormatDesignatorParseMethod(this.OnUnderline));
            this.parseMethods7.Add(XlNumFmtDesignator.Year, new NumberFormatDesignatorParseMethod(this.OnError));
        }

        private void BeforeDigit()
        {
            if (this.elements.Count > 0)
            {
                if (!this.elements[this.elements.Count - 1].IsDigit)
                {
                    this.preFractionIndex = this.elements.Count - 1;
                    this.integerCount += this.dividentCount;
                    this.dividentCount = 0;
                }
                else if (this.displayFactor == 1)
                {
                    this.grouping = true;
                }
            }
            this.displayFactor = 0;
            if (this.isDecimal)
            {
                this.decimalCount++;
            }
            else
            {
                this.dividentCount++;
            }
        }

        private bool CheckIsGeneral()
        {
            string generalDesignator = this.localizer.GeneralDesignator;
            return ((this.formatString.Length >= (this.currentIndex + generalDesignator.Length)) ? (string.Compare(generalDesignator, this.formatString.Substring(this.currentIndex, generalDesignator.Length), StringComparison.OrdinalIgnoreCase) == 0) : false);
        }

        private void ClearLocals()
        {
            this.formats = null;
            this.elements = null;
            this.locale = null;
            this.part = null;
            this.elementString = null;
            this.element = null;
            this.formatString = null;
            this.currentIndex = -1;
            this.currentSymbol = '\0';
            this.errorState = false;
            this.elapsed = false;
            this.hasMilliseconds = false;
            this.elementLength = -1;
            this.grouping = false;
            this.isDecimal = false;
            this.displayFactor = 0;
            this.percentCount = 0;
            this.integerCount = 0;
            this.decimalCount = 0;
            this.dividentCount = 0;
            this.decimalSeparatorIndex = -1;
            this.preFractionIndex = -1;
            this.explicitSign = false;
            this.expCount = 0;
            this.expIndex = -1;
            this.divisorCount = 0;
            this.divisor = 0;
            this.divisorPow = 0;
            this.fractionSeparatorIndex = -1;
        }

        private int GetDateTimeBlockLength()
        {
            int dateTimeBlockLength = this.GetDateTimeBlockLength(this.formatString, this.currentIndex);
            this.currentIndex += dateTimeBlockLength - 1;
            return dateTimeBlockLength;
        }

        private int GetDateTimeBlockLength(string formatString, int currentIndex)
        {
            int num = currentIndex;
            this.currentSymbol = char.ToLowerInvariant(this.currentSymbol);
            while (currentIndex < formatString.Length)
            {
                if (char.ToLowerInvariant(formatString[currentIndex]) != this.currentSymbol)
                {
                    currentIndex--;
                    return ((currentIndex - num) + 1);
                }
                currentIndex++;
            }
            return (formatString.Length - num);
        }

        private bool OnAmPmCore()
        {
            if (((this.currentIndex + 5) <= this.formatString.Length) && (this.formatString.Substring(this.currentIndex, 5).ToLowerInvariant() == "am/pm"))
            {
                if (this.elapsed)
                {
                    this.errorState = true;
                    return true;
                }
                this.currentIndex += 4;
                this.elements.Add(new XlNumFmtAmPm());
            }
            else
            {
                if ((this.currentIndex + 3) > this.formatString.Length)
                {
                    return false;
                }
                string str = this.formatString.Substring(this.currentIndex, 3);
                if (str.ToLowerInvariant() != "a/p")
                {
                    return false;
                }
                if (this.elapsed)
                {
                    this.errorState = true;
                    return true;
                }
                this.currentIndex += 2;
                this.elements.Add(new XlNumFmtAmPm(char.IsLower(str[0]), char.IsLower(str[2])));
            }
            for (int i = this.elements.Count - 2; i >= 0; i--)
            {
                this.element = this.elements[i];
                if (this.element is XlNumFmtTimeBase)
                {
                    XlNumFmtHours element = this.element as XlNumFmtHours;
                    if (element != null)
                    {
                        element.Is12HourTime = true;
                        break;
                    }
                }
                else if (this.element is XlNumFmtDateBase)
                {
                    break;
                }
            }
            return true;
        }

        private void OnAmPmOrDayOfWeek()
        {
            if (!this.OnAmPmCore())
            {
                this.OnDayOfWeekOrDefault2();
            }
        }

        private void OnAmPmOrMonth()
        {
            if (!this.OnAmPmCore())
            {
                this.OnMonth();
            }
        }

        private void OnAmPmOrYear()
        {
            if (!this.OnAmPmCore())
            {
                this.OnYear();
            }
        }

        private void OnAsterisk()
        {
            int num = this.currentIndex + 1;
            this.currentIndex = num;
            this.elements.Add(new XlNumFmtAsterisk(this.formatString[num]));
        }

        private void OnAt()
        {
            this.part = this.ParseText();
        }

        private void OnAt6()
        {
            this.elements.Add(XlNumFmtTextContent.Instance);
        }

        private void OnBackslash()
        {
            int num = this.currentIndex + 1;
            this.currentIndex = num;
            this.currentSymbol = this.formatString[num];
            this.OnDefault();
        }

        private void OnBracket()
        {
            int startIndex = this.currentIndex + 1;
            int index = this.formatString.IndexOf(']', startIndex);
            if (index < 0)
            {
                this.OnDefault();
            }
            else
            {
                this.elementString = this.formatString.Substring(startIndex, index - startIndex);
                this.element = this.TryParseColor(this.elementString);
                if (this.element == null)
                {
                    this.locale = this.TryParseLocale(this.elementString);
                    this.element = this.locale;
                    if (this.element == null)
                    {
                        int count = this.elements.Count;
                        this.OnDateTimeSymbol();
                        if (this.part != null)
                        {
                            return;
                        }
                        this.errorState = false;
                        this.currentIndex = startIndex - 1;
                        this.elements.RemoveRange(count, this.elements.Count - count);
                        this.element = this.TryParseExpr(this.elementString);
                        if (this.element == null)
                        {
                            this.errorState = true;
                            return;
                        }
                    }
                }
                this.elements.Add(this.element);
                this.currentIndex += this.elementString.Length + 1;
            }
        }

        private void OnBracket2()
        {
            this.TryParseDateTimeCondition(ref this.elapsed);
        }

        private void OnBracket3()
        {
            if (this.TryParseCondition() < 0)
            {
                this.errorState = true;
            }
        }

        private void OnDateSeparator()
        {
            this.elements.Add(XlNumFmtDateSeparator.Instance);
        }

        private void OnDateTimeSymbol()
        {
            this.part = this.ParseDateTime();
        }

        private void OnDay()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtDay((this.elementLength > 4) ? 4 : this.elementLength));
        }

        private bool OnDayOfWeekCore()
        {
            int currentIndex = this.currentIndex;
            this.elementLength = this.GetDateTimeBlockLength();
            if (this.elementLength >= 3)
            {
                this.elements.Add(new XlNumFmtDayOfWeek((this.elementLength > 3) ? 4 : 3));
                return true;
            }
            this.currentIndex = currentIndex;
            this.elementLength = 1;
            return false;
        }

        private void OnDayOfWeekOrDefault()
        {
            if (!this.OnDayOfWeekCore())
            {
                this.OnDefault();
            }
            else
            {
                this.currentIndex++;
                this.OnDateTimeSymbol();
            }
        }

        private void OnDayOfWeekOrDefault2()
        {
            if (!this.OnDayOfWeekCore())
            {
                this.OnDefault();
            }
        }

        private void OnDayOfWeekOrDefault3()
        {
            if (!this.OnDayOfWeekCore())
            {
                this.OnDefault();
            }
            else
            {
                this.elements.RemoveAt(this.elements.Count - 1);
                this.OnError();
            }
        }

        private void OnDecimalSeparator()
        {
            this.isDecimal = true;
            if (this.decimalSeparatorIndex < 0)
            {
                this.decimalSeparatorIndex = this.elements.Count;
            }
            this.elements.Add(XlNumFmtDecimalSeparator.Instance);
        }

        private void OnDefault()
        {
            this.elements.Add(new XlNumFmtBackslashedText(this.currentSymbol));
        }

        private void OnDefaultDateSeparator()
        {
            this.elements.Add(XlNumFmtDefaultDateSeparator.Instance);
        }

        private void OnDigitEmpty()
        {
            this.BeforeDigit();
            this.elements.Add(XlNumFmtDigitEmpty.Instance);
        }

        private void OnDigitEmpty4()
        {
            this.expCount++;
            this.elements.Add(XlNumFmtDigitEmpty.Instance);
        }

        private void OnDigitEmpty5()
        {
            this.divisorCount++;
            this.elements.Add(XlNumFmtDigitEmpty.Instance);
            this.displayFactor = this.elements.Count;
        }

        private void OnDigitSpace()
        {
            this.BeforeDigit();
            this.elements.Add(XlNumFmtDigitSpace.Instance);
        }

        private void OnDigitSpace4()
        {
            this.expCount++;
            this.elements.Add(XlNumFmtDigitSpace.Instance);
        }

        private void OnDigitSpace5()
        {
            this.divisorCount++;
            this.elements.Add(XlNumFmtDigitSpace.Instance);
            this.displayFactor = this.elements.Count;
        }

        private void OnDigitZero()
        {
            this.BeforeDigit();
            this.elements.Add(XlNumFmtDigitZero.Instance);
        }

        private void OnDigitZero4()
        {
            this.expCount++;
            this.elements.Add(XlNumFmtDigitZero.Instance);
        }

        private void OnDigitZero5()
        {
            if (this.divisor > 0)
            {
                this.divisorPow *= 10;
            }
            else
            {
                this.divisorCount++;
                this.elements.Add(XlNumFmtDigitZero.Instance);
                this.displayFactor = this.elements.Count;
            }
        }

        private void OnEndOfPart()
        {
            this.part = new XlNumFmtNumericSimple(this.elements, 0, 0, 0, 0, -1, false, this.formats.Count == 1);
        }

        private void OnEndOfPart2()
        {
            if (this.locale != null)
            {
                if (this.locale.HexCode == 0xf800)
                {
                    this.part = new XlNumFmtDateTimeSystemLongDate(this.elements);
                    return;
                }
                if (this.locale.HexCode == 0xf400)
                {
                    this.part = new XlNumFmtDateTimeSystemLongTime(this.elements);
                    return;
                }
            }
            this.part = new XlNumFmtDateTime(this.elements, this.locale, this.hasMilliseconds);
        }

        private void OnEndOfPart3()
        {
            this.integerCount += this.dividentCount;
            this.PrepareGrouping();
            this.part = new XlNumFmtNumericSimple(this.elements, this.percentCount, this.integerCount, this.decimalCount, this.displayFactor, this.decimalSeparatorIndex, this.grouping, this.formats.Count == 1);
        }

        private void OnEndOfPart4()
        {
            this.part = new XlNumFmtNumericExponent(this.elements, this.integerCount, this.decimalCount, this.decimalSeparatorIndex, this.expIndex, this.expCount, this.explicitSign, this.grouping, this.formats.Count == 1);
        }

        private void OnEndOfPart5()
        {
            if (this.divisor > 0)
            {
                while ((this.divisor % 10) == 0)
                {
                    this.divisor /= 10;
                }
                this.part = new XlNumFmtNumericFractionExplicit(this.elements, this.percentCount, this.integerCount, this.preFractionIndex, this.fractionSeparatorIndex, this.displayFactor, this.dividentCount, this.divisor, this.grouping, this.formats.Count == 1);
            }
            else if (this.divisorCount == 0)
            {
                this.errorState = true;
            }
            else
            {
                this.part = new XlNumFmtNumericFraction(this.elements, this.percentCount, this.integerCount, this.preFractionIndex, this.fractionSeparatorIndex, this.displayFactor, this.dividentCount, this.divisorCount, this.grouping, this.formats.Count == 1);
            }
        }

        private void OnEndOfPart6()
        {
            this.part = new XlNumFmtText(this.elements);
        }

        private void OnEndOfPart7()
        {
            if (this.fractionSeparatorIndex < 0)
            {
                this.errorState = true;
            }
            this.part = new XlNumFmtGeneral(this.elements, this.fractionSeparatorIndex);
        }

        private void OnError()
        {
            this.errorState = true;
        }

        private void OnESymbol()
        {
            if (char.IsLower(this.currentSymbol))
            {
                this.OnDateTimeSymbol();
            }
            else
            {
                this.OnError();
            }
        }

        private void OnESymbol2()
        {
            if (char.IsLower(this.currentSymbol))
            {
                this.OnInvariantYear();
            }
            else
            {
                this.OnError();
            }
        }

        private void OnESymbol3()
        {
            if (char.IsLower(this.currentSymbol))
            {
                this.OnError();
            }
            else
            {
                this.OnExponent();
            }
        }

        private void OnExponent()
        {
            this.integerCount += this.dividentCount;
            this.PrepareGrouping();
            this.part = this.ParseNumericExponent(this.integerCount, this.decimalSeparatorIndex, this.decimalCount, this.grouping);
        }

        private void OnFractionSeparator()
        {
            if (this.isDecimal || (this.dividentCount <= 0))
            {
                this.errorState = true;
            }
            else
            {
                if (this.integerCount > 0)
                {
                    this.PrepareGrouping();
                }
                else if (this.grouping)
                {
                    this.errorState = true;
                    return;
                }
                this.part = this.ParseNumericFraction(this.integerCount, this.preFractionIndex, this.dividentCount, this.percentCount, this.grouping);
            }
        }

        private void OnGeneral()
        {
            this.part = this.ParseGeneral();
        }

        private void OnGeneral2()
        {
            if (this.CheckIsGeneral())
            {
                this.errorState = true;
            }
            else
            {
                this.OnDefault();
            }
        }

        private void OnGeneral3()
        {
            if (this.CheckIsGeneral())
            {
                this.errorState = true;
            }
            else
            {
                this.OnDefault();
            }
        }

        private void OnGeneral7()
        {
            if (this.CheckIsGeneral())
            {
                this.OnGeneralCore();
            }
            else
            {
                this.OnDefault();
            }
        }

        private void OnGeneralCore()
        {
            if (this.fractionSeparatorIndex >= 0)
            {
                this.errorState = true;
            }
            else
            {
                this.currentIndex += this.localizer.GeneralDesignator.Length - 1;
                this.fractionSeparatorIndex = this.elements.Count;
            }
        }

        private void OnGeneralOrDateTime()
        {
            if (this.CheckIsGeneral())
            {
                this.OnGeneral();
            }
            else
            {
                this.OnDateTimeSymbol();
            }
        }

        private void OnGeneralOrDateTime7()
        {
            if (this.CheckIsGeneral())
            {
                this.OnGeneralCore();
            }
            else
            {
                this.errorState = true;
            }
        }

        private void OnGeneralOrDay()
        {
            if (this.CheckIsGeneral())
            {
                this.errorState = true;
            }
            else
            {
                this.OnDay();
            }
        }

        private void OnGeneralOrInvariantYear()
        {
            if (this.CheckIsGeneral())
            {
                this.errorState = true;
            }
            else
            {
                this.OnInvariantYear();
            }
        }

        private void OnGeneralOrJapaneseEra()
        {
            if (this.CheckIsGeneral())
            {
                this.errorState = true;
            }
            else
            {
                this.OnJapaneseEra();
            }
        }

        private void OnGeneralOrSecond()
        {
            if (this.CheckIsGeneral())
            {
                this.errorState = true;
            }
            else
            {
                this.OnSecond();
            }
        }

        private void OnGroupSeparator()
        {
            if ((this.elements.Count > 0) && this.elements[this.elements.Count - 1].IsDigit)
            {
                this.displayFactor++;
            }
            else
            {
                this.OnDefault();
            }
        }

        private void OnHour()
        {
            bool flag = false;
            for (int i = this.elements.Count - 1; i >= 0; i--)
            {
                this.element = this.elements[i];
                if (this.element is XlNumFmtDateBase)
                {
                    if (this.element is XlNumFmtAmPm)
                    {
                        flag = true;
                    }
                    if (!(this.element is XlNumFmtTimeBase) || (this.element is XlNumFmtHours))
                    {
                        break;
                    }
                }
            }
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtHours((this.elementLength > 1) ? 2 : 1, false, flag));
        }

        private void OnInvariantYear()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtInvariantYear((this.elementLength > 1) ? 2 : 1));
        }

        private void OnJapaneseEra()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtJapaneseEra((this.elementLength > 2) ? 3 : this.elementLength));
        }

        private void OnMilisecond()
        {
            if (!this.OnMilisecondCore())
            {
                this.OnDefault();
            }
        }

        private bool OnMilisecondCore()
        {
            this.elementLength = this.currentIndex;
            this.currentIndex++;
            while ((this.formatString.Length > this.currentIndex) && (this.formatString[this.currentIndex] == '0'))
            {
                this.currentIndex++;
            }
            this.currentIndex--;
            this.elementLength = this.currentIndex - this.elementLength;
            if (this.elementLength == 0)
            {
                return false;
            }
            if (this.elementLength > 3)
            {
                this.errorState = true;
            }
            else
            {
                this.hasMilliseconds = true;
                this.elements.Add(new XlNumFmtMilliseconds(this.elementLength));
            }
            return true;
        }

        private void OnMinute()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtMinutes((this.elementLength > 1) ? 2 : 1, false));
        }

        private void OnMonth()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtMonth((this.elementLength > 5) ? 4 : this.elementLength));
        }

        private void OnMonthOrMinute()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            if (this.elementLength > 2)
            {
                this.elements.Add(new XlNumFmtMonth((this.elementLength > 5) ? 4 : this.elementLength));
            }
            else
            {
                bool flag = false;
                for (int i = this.elements.Count - 1; i >= 0; i--)
                {
                    this.element = this.elements[i];
                    if (this.element is XlNumFmtDateBase)
                    {
                        if ((this.element is XlNumFmtSeconds) || (this.element is XlNumFmtHours))
                        {
                            flag = true;
                        }
                        if (!(this.element is XlNumFmtAmPm))
                        {
                            break;
                        }
                    }
                }
                if (flag)
                {
                    this.elements.Add(new XlNumFmtMinutes(this.elementLength, false));
                }
                else
                {
                    this.elements.Add(new XlNumFmtMonth(this.elementLength));
                }
            }
        }

        private void OnNumericSymbol()
        {
            this.part = this.ParseNumeric();
        }

        private void OnPercent()
        {
            this.percentCount++;
            this.elements.Add(XlNumFmtPercent.Instance);
        }

        private void OnQuote()
        {
            StringBuilder builder = new StringBuilder();
            this.currentIndex++;
            while (true)
            {
                if (this.currentIndex < this.formatString.Length)
                {
                    this.currentSymbol = this.formatString[this.currentIndex];
                    if (this.currentSymbol != '"')
                    {
                        builder.Append(this.currentSymbol);
                        this.currentIndex++;
                        continue;
                    }
                }
                this.elements.Add(new XlNumFmtQuotedText(builder.ToString()));
                return;
            }
        }

        private void OnSecond()
        {
            for (int i = this.elements.Count - 1; i >= 0; i--)
            {
                XlNumFmtDateBase base2 = this.elements[i] as XlNumFmtDateBase;
                if (base2 != null)
                {
                    if (base2 is XlNumFmtMonth)
                    {
                        this.elements[i] = new XlNumFmtMinutes(base2.Count, false);
                    }
                    if (!(base2 is XlNumFmtAmPm))
                    {
                        break;
                    }
                }
            }
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtSeconds((this.elementLength > 1) ? 2 : 1, false));
        }

        private void OnThaiYear()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.currentIndex++;
            if (this.currentIndex < this.formatString.Length)
            {
                char ch = this.formatString[this.currentIndex];
                if ((ch == '1') || (ch == '2'))
                {
                    if ((this.elements.Count > 0) || (this.elementLength > 1))
                    {
                        this.errorState = true;
                    }
                    this.elements.Add(new XlNumFmtNotImplementedLocale(ch - '0'));
                    return;
                }
            }
            this.currentIndex--;
            this.elements.Add(new XlNumFmtThaiYear((this.elementLength > 2) ? 4 : 2));
        }

        private void OnTimeSeparator()
        {
            this.elements.Add(XlNumFmtTimeSeparator.Instance);
        }

        private void OnUnderline()
        {
            int num = this.currentIndex + 1;
            this.currentIndex = num;
            this.elements.Add(new XlNumFmtUnderline(this.formatString[num]));
        }

        private void OnYear()
        {
            this.elementLength = this.GetDateTimeBlockLength();
            this.elements.Add(new XlNumFmtYear((this.elementLength > 2) ? 4 : 2));
        }

        public IXlNumFmt Parse(string formatString) => 
            this.Parse(formatString, CultureInfo.InvariantCulture);

        private IXlNumFmt Parse(string formatString, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                return null;
            }
            this.formatString = formatString;
            this.localizer.SetCulture(culture);
            IXlNumFmt fmt = this.ParseCore(culture);
            this.ClearLocals();
            return fmt;
        }

        private IXlNumFmt ParseCore(CultureInfo culture)
        {
            this.formatString = this.formatString + ";";
            this.formats = new List<XlNumFmtSimple>();
            this.elements = new List<IXlNumFmtElement>();
            this.currentIndex = 0;
            while (this.currentIndex < this.formatString.Length)
            {
                this.currentSymbol = this.formatString[this.currentIndex];
                if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                {
                    this.designator = XlNumFmtDesignator.Default;
                }
                if (!this.parseMethods.TryGetValue(this.designator, out this.designatorParser))
                {
                    this.designator = this.ParseSeparator(this.designator);
                    this.designatorParser = this.parseMethods[this.designator];
                }
                this.designatorParser();
                if (this.errorState)
                {
                    return null;
                }
                if (this.part != null)
                {
                    this.formats.Add(this.part);
                    this.part = null;
                    this.elements = new List<IXlNumFmtElement>();
                    if (this.formats.Count == 3)
                    {
                        int num2 = this.currentIndex + 1;
                        this.currentIndex = num2;
                        int num = num2;
                        if (num != this.formatString.Length)
                        {
                            this.ParseText();
                            if (this.errorState)
                            {
                                this.errorState = false;
                                this.elements.Clear();
                                this.currentIndex = num;
                                this.ParseGeneral();
                                if (this.errorState)
                                {
                                    return null;
                                }
                            }
                            if (this.currentIndex < (this.formatString.Length - 1))
                            {
                                return null;
                            }
                            if ((this.part.Type != XlNumFmtType.General) || (this.part.Count != 0))
                            {
                                this.formats.Add(this.part);
                            }
                        }
                        break;
                    }
                }
                this.currentIndex++;
            }
            if (this.formats.Count == 1)
            {
                return this.formats[0];
            }
            XlNumFmtComposite composite = new XlNumFmtComposite();
            composite.AddRange(this.formats);
            return composite;
        }

        private XlNumFmtSimple ParseDateTime()
        {
            this.elapsed = false;
            this.hasMilliseconds = false;
            this.elementLength = -1;
            while (this.currentIndex < this.formatString.Length)
            {
                this.currentSymbol = this.formatString[this.currentIndex];
                if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                {
                    this.designator = XlNumFmtDesignator.Default;
                }
                if (!this.parseMethods2.TryGetValue(this.designator, out this.designatorParser))
                {
                    this.ParseSeparator2(this.designator);
                }
                else
                {
                    this.designatorParser();
                }
                if (this.errorState)
                {
                    return null;
                }
                if (this.currentSymbol == ';')
                {
                    return this.part;
                }
                this.currentIndex++;
            }
            if (this.part == null)
            {
                this.errorState = true;
            }
            return this.part;
        }

        private XlNumFmtSimple ParseGeneral()
        {
            this.fractionSeparatorIndex = -1;
            while (this.currentIndex < this.formatString.Length)
            {
                this.currentSymbol = this.formatString[this.currentIndex];
                if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                {
                    this.designator = XlNumFmtDesignator.Default;
                }
                if (!this.parseMethods7.TryGetValue(this.designator, out this.designatorParser))
                {
                    this.designatorParser = new NumberFormatDesignatorParseMethod(this.OnDefault);
                }
                this.designatorParser();
                if (this.errorState)
                {
                    return null;
                }
                if (this.currentSymbol == ';')
                {
                    return this.part;
                }
                this.currentIndex++;
            }
            if (this.part == null)
            {
                this.errorState = true;
            }
            return this.part;
        }

        private XlNumFmtSimple ParseNumeric()
        {
            this.grouping = false;
            this.isDecimal = false;
            this.displayFactor = 0;
            this.percentCount = 0;
            this.integerCount = 0;
            this.decimalCount = 0;
            this.dividentCount = 0;
            this.decimalSeparatorIndex = -1;
            this.preFractionIndex = -1;
            while (this.currentIndex < this.formatString.Length)
            {
                this.currentSymbol = this.formatString[this.currentIndex];
                if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                {
                    this.designator = XlNumFmtDesignator.Default;
                }
                if (!this.parseMethods3.TryGetValue(this.designator, out this.designatorParser))
                {
                    this.designator = this.ParseSeparator3(this.designator);
                    this.designatorParser = this.parseMethods3[this.designator];
                }
                this.designatorParser();
                if (this.errorState)
                {
                    return null;
                }
                if (this.currentSymbol == ';')
                {
                    return this.part;
                }
                this.currentIndex++;
            }
            if (this.part == null)
            {
                this.errorState = true;
            }
            return this.part;
        }

        private XlNumFmtSimple ParseNumericExponent(int integerCount, int decimalSeparatorIndex, int decimalCount, bool grouping)
        {
            this.currentIndex++;
            if ((this.currentIndex + 1) >= this.formatString.Length)
            {
                return null;
            }
            this.currentSymbol = this.formatString[this.currentIndex];
            if (this.currentSymbol == '+')
            {
                this.explicitSign = true;
            }
            else
            {
                this.explicitSign = false;
                if (this.currentSymbol != '-')
                {
                    return null;
                }
            }
            this.expIndex = this.elements.Count;
            this.expCount = 0;
            this.currentIndex++;
            while (this.currentIndex < this.formatString.Length)
            {
                this.currentSymbol = this.formatString[this.currentIndex];
                if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                {
                    this.designator = XlNumFmtDesignator.Default;
                }
                if (!this.parseMethods4.TryGetValue(this.designator, out this.designatorParser))
                {
                    this.designator = this.ParseSeparator3(this.designator);
                    this.designatorParser = this.parseMethods4[this.designator];
                }
                this.designatorParser();
                if (this.errorState)
                {
                    return null;
                }
                if (this.currentSymbol == ';')
                {
                    return this.part;
                }
                this.currentIndex++;
            }
            if (this.part == null)
            {
                this.errorState = true;
            }
            return this.part;
        }

        private XlNumFmtSimple ParseNumericFraction(int integerCount, int preFractionIndex, int dividentCount, int percentCount, bool grouping)
        {
            this.displayFactor = -1;
            this.divisorCount = 0;
            this.divisor = 0;
            this.divisorPow = 0x2710;
            this.fractionSeparatorIndex = this.elements.Count;
            this.currentIndex++;
            while (true)
            {
                while (true)
                {
                    if (this.currentIndex >= this.formatString.Length)
                    {
                        if (this.part == null)
                        {
                            this.errorState = true;
                        }
                        return this.part;
                    }
                    this.currentSymbol = this.formatString[this.currentIndex];
                    if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                    {
                        if (char.IsNumber(this.currentSymbol))
                        {
                            if (this.divisorPow <= 0)
                            {
                                return null;
                            }
                            this.divisor += (this.currentSymbol - '0') * this.divisorPow;
                            this.divisorPow /= 10;
                            this.displayFactor = this.elements.Count;
                            break;
                        }
                        this.designator = XlNumFmtDesignator.Default;
                    }
                    if (!this.parseMethods5.TryGetValue(this.designator, out this.designatorParser))
                    {
                        this.designator = this.ParseSeparator3(this.designator);
                        this.designatorParser = this.parseMethods5[this.designator];
                    }
                    this.designatorParser();
                    if (this.errorState)
                    {
                        return null;
                    }
                    if (this.currentSymbol != ';')
                    {
                        break;
                    }
                    return this.part;
                }
                this.currentIndex++;
            }
        }

        private XlNumFmtDesignator ParseSeparator(XlNumFmtDesignator designator) => 
            ((designator & XlNumFmtDesignator.DateSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.TimeSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.DecimalSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.GroupSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.FractionOrDateSeparator) <= XlNumFmtDesignator.Default) ? XlNumFmtDesignator.Default : XlNumFmtDesignator.FractionOrDateSeparator) : XlNumFmtDesignator.GroupSeparator) : XlNumFmtDesignator.DecimalSeparator) : XlNumFmtDesignator.TimeSeparator) : XlNumFmtDesignator.DateSeparator;

        private bool ParseSeparator2(XlNumFmtDesignator designator)
        {
            if (((designator & XlNumFmtDesignator.DecimalSeparator) <= XlNumFmtDesignator.Default) || !this.OnMilisecondCore())
            {
                if ((designator & XlNumFmtDesignator.DateSeparator) > XlNumFmtDesignator.Default)
                {
                    this.OnDateSeparator();
                    return true;
                }
                if ((designator & XlNumFmtDesignator.FractionOrDateSeparator) > XlNumFmtDesignator.Default)
                {
                    this.OnDateSeparator();
                    return true;
                }
                if ((designator & XlNumFmtDesignator.TimeSeparator) > XlNumFmtDesignator.Default)
                {
                    this.OnTimeSeparator();
                    return true;
                }
                if ((designator & XlNumFmtDesignator.GroupSeparator) > XlNumFmtDesignator.Default)
                {
                    this.OnDefault();
                    return true;
                }
                this.OnError();
            }
            return true;
        }

        private XlNumFmtDesignator ParseSeparator3(XlNumFmtDesignator designator) => 
            ((designator & XlNumFmtDesignator.DecimalSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.GroupSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.FractionOrDateSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.DateSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.TimeSeparator) <= XlNumFmtDesignator.Default) ? XlNumFmtDesignator.Default : XlNumFmtDesignator.TimeSeparator) : XlNumFmtDesignator.DateSeparator) : XlNumFmtDesignator.FractionOrDateSeparator) : XlNumFmtDesignator.GroupSeparator) : XlNumFmtDesignator.DecimalSeparator;

        private XlNumFmtDesignator ParseSeparator6(XlNumFmtDesignator designator) => 
            ((designator & XlNumFmtDesignator.FractionOrDateSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.DecimalSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.GroupSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.DateSeparator) <= XlNumFmtDesignator.Default) ? (((designator & XlNumFmtDesignator.TimeSeparator) <= XlNumFmtDesignator.Default) ? XlNumFmtDesignator.Default : XlNumFmtDesignator.TimeSeparator) : XlNumFmtDesignator.DateSeparator) : XlNumFmtDesignator.GroupSeparator) : XlNumFmtDesignator.DecimalSeparator) : XlNumFmtDesignator.FractionOrDateSeparator;

        private XlNumFmtSimple ParseText()
        {
            while (this.currentIndex < this.formatString.Length)
            {
                this.currentSymbol = this.formatString[this.currentIndex];
                if (!this.localizer.Designators.TryGetValue(char.ToLowerInvariant(this.currentSymbol), out this.designator))
                {
                    if (char.IsNumber(this.currentSymbol))
                    {
                        this.part = null;
                        break;
                    }
                    this.designator = XlNumFmtDesignator.Default;
                }
                if (!this.parseMethods6.TryGetValue(this.designator, out this.designatorParser))
                {
                    this.designator = this.ParseSeparator6(this.designator);
                    this.designatorParser = this.parseMethods6[this.designator];
                }
                this.designatorParser();
                if (this.errorState)
                {
                    return null;
                }
                if (this.currentSymbol == ';')
                {
                    return this.part;
                }
                this.currentIndex++;
            }
            if (this.part == null)
            {
                this.errorState = true;
            }
            return this.part;
        }

        private void PrepareGrouping()
        {
            int integerCount = this.integerCount;
            if (this.grouping && (this.integerCount < 4))
            {
                int index = 0;
                while (true)
                {
                    if (index < this.elements.Count)
                    {
                        IXlNumFmtElement element = this.elements[index];
                        if (!element.IsDigit)
                        {
                            index++;
                            continue;
                        }
                    }
                    while (true)
                    {
                        if (this.integerCount >= 4)
                        {
                            if (this.decimalSeparatorIndex >= 0)
                            {
                                this.decimalSeparatorIndex += this.integerCount - integerCount;
                            }
                            break;
                        }
                        this.elements.Insert(index, XlNumFmtDigitEmpty.Instance);
                        this.integerCount++;
                    }
                    break;
                }
            }
        }

        private string TryGetConditionString()
        {
            int startIndex = this.currentIndex + 1;
            int index = this.formatString.IndexOf(']', startIndex);
            return ((index >= 0) ? this.formatString.Substring(startIndex, index - startIndex) : null);
        }

        private XlNumFmtColor TryParseColor(string colorString)
        {
            Color item = Color.FromName(colorString);
            return (this.AllowedColors.Contains(item) ? new XlNumFmtColor(item) : null);
        }

        private int TryParseCondition()
        {
            string str = this.TryGetConditionString();
            if (string.IsNullOrEmpty(str))
            {
                this.OnDefault();
                return 1;
            }
            this.element = this.TryParseColor(str);
            if (this.element != null)
            {
                this.elements.Insert(0, this.element);
            }
            else
            {
                this.element = this.TryParseLocale(str);
                if (this.element != null)
                {
                    this.elements.Add(this.element);
                }
                else
                {
                    this.element = this.TryParseExpr(str);
                    if (this.element == null)
                    {
                        return -1;
                    }
                    this.elements.Add(this.element);
                }
            }
            this.currentIndex += str.Length + 1;
            return (str.Length + 2);
        }

        private int TryParseDateTimeCondition(ref bool elapsed)
        {
            this.locale = null;
            string str = this.TryGetConditionString();
            if (string.IsNullOrEmpty(str))
            {
                this.OnDefault();
                return 1;
            }
            this.element = this.TryParseColor(str);
            if (this.element != null)
            {
                this.elements.Insert(0, this.element);
            }
            else
            {
                this.element = this.TryParseLocale(str);
                if (this.element != null)
                {
                    if (this.locale != null)
                    {
                        return -1;
                    }
                    this.locale = this.element as XlNumFmtDisplayLocale;
                    this.elements.Add(this.element);
                }
                else
                {
                    this.element = this.TryParseElapsed(str);
                    if (this.element == null)
                    {
                        this.element = this.TryParseExpr(str);
                        if (this.element == null)
                        {
                            return -1;
                        }
                        this.elements.Add(this.element);
                    }
                    else
                    {
                        if (elapsed)
                        {
                            return -1;
                        }
                        elapsed = true;
                        this.elements.Add(this.element);
                    }
                }
            }
            this.currentIndex += str.Length + 1;
            return (str.Length + 2);
        }

        private XlNumFmtTimeBase TryParseElapsed(string elapsedString)
        {
            XlNumFmtTimeBase base2 = null;
            this.currentSymbol = char.ToLowerInvariant(elapsedString[0]);
            if ((this.currentSymbol == 'h') || ((this.currentSymbol == 'm') || (this.currentSymbol == 's')))
            {
                int dateTimeBlockLength = this.GetDateTimeBlockLength(elapsedString, 0);
                if (dateTimeBlockLength == elapsedString.Length)
                {
                    char currentSymbol = this.currentSymbol;
                    if (currentSymbol == 'h')
                    {
                        base2 = new XlNumFmtHours(dateTimeBlockLength, true, false);
                    }
                    else if (currentSymbol == 'm')
                    {
                        base2 = new XlNumFmtMinutes(dateTimeBlockLength, true);
                    }
                    else if (currentSymbol == 's')
                    {
                        base2 = new XlNumFmtSeconds(dateTimeBlockLength, true);
                    }
                }
            }
            return base2;
        }

        private XlNumFmtExprCondition TryParseExpr(string expression) => 
            new XlNumFmtExprCondition(expression);

        private XlNumFmtDisplayLocale TryParseLocale(string localeString)
        {
            if (localeString[0] == '$')
            {
                int num2;
                int index = localeString.IndexOf('-', 1);
                if (index <= 0)
                {
                    return new XlNumFmtDisplayLocale(-1, localeString.Remove(0, 1));
                }
                string currency = localeString.Substring(1, index - 1);
                localeString = localeString.Remove(0, index + 1);
                if (int.TryParse(localeString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
                {
                    return new XlNumFmtDisplayLocale(num2, currency);
                }
            }
            return null;
        }

        public XlNumFmtLocalizer Localizer =>
            this.localizer;

        private delegate void NumberFormatDesignatorParseMethod();
    }
}

