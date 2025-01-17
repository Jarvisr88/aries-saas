﻿namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public enum FunctionOperatorType
    {
        public const FunctionOperatorType None = FunctionOperatorType.None;,
        public const FunctionOperatorType Custom = FunctionOperatorType.Custom;,
        public const FunctionOperatorType CustomNonDeterministic = FunctionOperatorType.CustomNonDeterministic;,
        public const FunctionOperatorType Iif = FunctionOperatorType.Iif;,
        public const FunctionOperatorType IsNull = FunctionOperatorType.IsNull;,
        public const FunctionOperatorType IsNullOrEmpty = FunctionOperatorType.IsNullOrEmpty;,
        public const FunctionOperatorType Trim = FunctionOperatorType.Trim;,
        public const FunctionOperatorType Len = FunctionOperatorType.Len;,
        public const FunctionOperatorType Substring = FunctionOperatorType.Substring;,
        public const FunctionOperatorType Upper = FunctionOperatorType.Upper;,
        public const FunctionOperatorType Lower = FunctionOperatorType.Lower;,
        public const FunctionOperatorType Concat = FunctionOperatorType.Concat;,
        public const FunctionOperatorType Ascii = FunctionOperatorType.Ascii;,
        public const FunctionOperatorType Char = FunctionOperatorType.Char;,
        public const FunctionOperatorType ToStr = FunctionOperatorType.ToStr;,
        public const FunctionOperatorType Replace = FunctionOperatorType.Replace;,
        public const FunctionOperatorType Reverse = FunctionOperatorType.Reverse;,
        public const FunctionOperatorType Insert = FunctionOperatorType.Insert;,
        public const FunctionOperatorType CharIndex = FunctionOperatorType.CharIndex;,
        public const FunctionOperatorType Remove = FunctionOperatorType.Remove;,
        public const FunctionOperatorType Abs = FunctionOperatorType.Abs;,
        public const FunctionOperatorType Sqr = FunctionOperatorType.Sqr;,
        public const FunctionOperatorType Cos = FunctionOperatorType.Cos;,
        public const FunctionOperatorType Sin = FunctionOperatorType.Sin;,
        public const FunctionOperatorType Atn = FunctionOperatorType.Atn;,
        public const FunctionOperatorType Exp = FunctionOperatorType.Exp;,
        public const FunctionOperatorType Log = FunctionOperatorType.Log;,
        public const FunctionOperatorType Rnd = FunctionOperatorType.Rnd;,
        public const FunctionOperatorType Tan = FunctionOperatorType.Tan;,
        public const FunctionOperatorType Power = FunctionOperatorType.Power;,
        public const FunctionOperatorType Sign = FunctionOperatorType.Sign;,
        public const FunctionOperatorType Round = FunctionOperatorType.Round;,
        public const FunctionOperatorType Ceiling = FunctionOperatorType.Ceiling;,
        public const FunctionOperatorType Floor = FunctionOperatorType.Floor;,
        public const FunctionOperatorType Max = FunctionOperatorType.Max;,
        public const FunctionOperatorType Min = FunctionOperatorType.Min;,
        public const FunctionOperatorType Acos = FunctionOperatorType.Acos;,
        public const FunctionOperatorType Asin = FunctionOperatorType.Asin;,
        public const FunctionOperatorType Atn2 = FunctionOperatorType.Atn2;,
        public const FunctionOperatorType BigMul = FunctionOperatorType.BigMul;,
        public const FunctionOperatorType Cosh = FunctionOperatorType.Cosh;,
        public const FunctionOperatorType Log10 = FunctionOperatorType.Log10;,
        public const FunctionOperatorType Sinh = FunctionOperatorType.Sinh;,
        public const FunctionOperatorType Tanh = FunctionOperatorType.Tanh;,
        public const FunctionOperatorType PadLeft = FunctionOperatorType.PadLeft;,
        public const FunctionOperatorType PadRight = FunctionOperatorType.PadRight;,
        public const FunctionOperatorType StartsWith = FunctionOperatorType.StartsWith;,
        public const FunctionOperatorType EndsWith = FunctionOperatorType.EndsWith;,
        public const FunctionOperatorType Contains = FunctionOperatorType.Contains;,
        public const FunctionOperatorType ToInt = FunctionOperatorType.ToInt;,
        public const FunctionOperatorType ToLong = FunctionOperatorType.ToLong;,
        public const FunctionOperatorType ToFloat = FunctionOperatorType.ToFloat;,
        public const FunctionOperatorType ToDouble = FunctionOperatorType.ToDouble;,
        public const FunctionOperatorType ToDecimal = FunctionOperatorType.ToDecimal;,
        public const FunctionOperatorType LocalDateTimeThisYear = FunctionOperatorType.LocalDateTimeThisYear;,
        public const FunctionOperatorType LocalDateTimeThisMonth = FunctionOperatorType.LocalDateTimeThisMonth;,
        public const FunctionOperatorType LocalDateTimeLastWeek = FunctionOperatorType.LocalDateTimeLastWeek;,
        public const FunctionOperatorType LocalDateTimeThisWeek = FunctionOperatorType.LocalDateTimeThisWeek;,
        public const FunctionOperatorType LocalDateTimeYesterday = FunctionOperatorType.LocalDateTimeYesterday;,
        public const FunctionOperatorType LocalDateTimeToday = FunctionOperatorType.LocalDateTimeToday;,
        public const FunctionOperatorType LocalDateTimeNow = FunctionOperatorType.LocalDateTimeNow;,
        public const FunctionOperatorType LocalDateTimeTomorrow = FunctionOperatorType.LocalDateTimeTomorrow;,
        public const FunctionOperatorType LocalDateTimeDayAfterTomorrow = FunctionOperatorType.LocalDateTimeDayAfterTomorrow;,
        public const FunctionOperatorType LocalDateTimeNextWeek = FunctionOperatorType.LocalDateTimeNextWeek;,
        public const FunctionOperatorType LocalDateTimeTwoWeeksAway = FunctionOperatorType.LocalDateTimeTwoWeeksAway;,
        public const FunctionOperatorType LocalDateTimeNextMonth = FunctionOperatorType.LocalDateTimeNextMonth;,
        public const FunctionOperatorType LocalDateTimeNextYear = FunctionOperatorType.LocalDateTimeNextYear;,
        public const FunctionOperatorType LocalDateTimeTwoMonthsAway = FunctionOperatorType.LocalDateTimeTwoMonthsAway;,
        public const FunctionOperatorType LocalDateTimeTwoYearsAway = FunctionOperatorType.LocalDateTimeTwoYearsAway;,
        public const FunctionOperatorType LocalDateTimeLastMonth = FunctionOperatorType.LocalDateTimeLastMonth;,
        public const FunctionOperatorType LocalDateTimeLastYear = FunctionOperatorType.LocalDateTimeLastYear;,
        public const FunctionOperatorType LocalDateTimeYearBeforeToday = FunctionOperatorType.LocalDateTimeYearBeforeToday;,
        public const FunctionOperatorType IsOutlookIntervalBeyondThisYear = FunctionOperatorType.IsOutlookIntervalBeyondThisYear;,
        public const FunctionOperatorType IsOutlookIntervalLaterThisYear = FunctionOperatorType.IsOutlookIntervalLaterThisYear;,
        public const FunctionOperatorType IsOutlookIntervalLaterThisMonth = FunctionOperatorType.IsOutlookIntervalLaterThisMonth;,
        public const FunctionOperatorType IsOutlookIntervalNextWeek = FunctionOperatorType.IsOutlookIntervalNextWeek;,
        public const FunctionOperatorType IsOutlookIntervalLaterThisWeek = FunctionOperatorType.IsOutlookIntervalLaterThisWeek;,
        public const FunctionOperatorType IsOutlookIntervalTomorrow = FunctionOperatorType.IsOutlookIntervalTomorrow;,
        public const FunctionOperatorType IsOutlookIntervalToday = FunctionOperatorType.IsOutlookIntervalToday;,
        public const FunctionOperatorType IsOutlookIntervalYesterday = FunctionOperatorType.IsOutlookIntervalYesterday;,
        public const FunctionOperatorType IsOutlookIntervalEarlierThisWeek = FunctionOperatorType.IsOutlookIntervalEarlierThisWeek;,
        public const FunctionOperatorType IsOutlookIntervalLastWeek = FunctionOperatorType.IsOutlookIntervalLastWeek;,
        public const FunctionOperatorType IsOutlookIntervalEarlierThisMonth = FunctionOperatorType.IsOutlookIntervalEarlierThisMonth;,
        public const FunctionOperatorType IsOutlookIntervalEarlierThisYear = FunctionOperatorType.IsOutlookIntervalEarlierThisYear;,
        public const FunctionOperatorType IsOutlookIntervalPriorThisYear = FunctionOperatorType.IsOutlookIntervalPriorThisYear;,
        public const FunctionOperatorType IsThisWeek = FunctionOperatorType.IsThisWeek;,
        public const FunctionOperatorType IsThisMonth = FunctionOperatorType.IsThisMonth;,
        public const FunctionOperatorType IsThisYear = FunctionOperatorType.IsThisYear;,
        public const FunctionOperatorType IsNextMonth = FunctionOperatorType.IsNextMonth;,
        public const FunctionOperatorType IsNextYear = FunctionOperatorType.IsNextYear;,
        public const FunctionOperatorType IsLastMonth = FunctionOperatorType.IsLastMonth;,
        public const FunctionOperatorType IsLastYear = FunctionOperatorType.IsLastYear;,
        public const FunctionOperatorType IsYearToDate = FunctionOperatorType.IsYearToDate;,
        public const FunctionOperatorType IsSameDay = FunctionOperatorType.IsSameDay;,
        public const FunctionOperatorType IsJanuary = FunctionOperatorType.IsJanuary;,
        public const FunctionOperatorType IsFebruary = FunctionOperatorType.IsFebruary;,
        public const FunctionOperatorType IsMarch = FunctionOperatorType.IsMarch;,
        public const FunctionOperatorType IsApril = FunctionOperatorType.IsApril;,
        public const FunctionOperatorType IsMay = FunctionOperatorType.IsMay;,
        public const FunctionOperatorType IsJune = FunctionOperatorType.IsJune;,
        public const FunctionOperatorType IsJuly = FunctionOperatorType.IsJuly;,
        public const FunctionOperatorType IsAugust = FunctionOperatorType.IsAugust;,
        public const FunctionOperatorType IsSeptember = FunctionOperatorType.IsSeptember;,
        public const FunctionOperatorType IsOctober = FunctionOperatorType.IsOctober;,
        public const FunctionOperatorType IsNovember = FunctionOperatorType.IsNovember;,
        public const FunctionOperatorType IsDecember = FunctionOperatorType.IsDecember;,
        public const FunctionOperatorType DateDiffTick = FunctionOperatorType.DateDiffTick;,
        public const FunctionOperatorType DateDiffSecond = FunctionOperatorType.DateDiffSecond;,
        public const FunctionOperatorType DateDiffMilliSecond = FunctionOperatorType.DateDiffMilliSecond;,
        public const FunctionOperatorType DateDiffMinute = FunctionOperatorType.DateDiffMinute;,
        public const FunctionOperatorType DateDiffHour = FunctionOperatorType.DateDiffHour;,
        public const FunctionOperatorType DateDiffDay = FunctionOperatorType.DateDiffDay;,
        public const FunctionOperatorType DateDiffMonth = FunctionOperatorType.DateDiffMonth;,
        public const FunctionOperatorType DateDiffYear = FunctionOperatorType.DateDiffYear;,
        public const FunctionOperatorType GetDate = FunctionOperatorType.GetDate;,
        public const FunctionOperatorType GetMilliSecond = FunctionOperatorType.GetMilliSecond;,
        public const FunctionOperatorType GetSecond = FunctionOperatorType.GetSecond;,
        public const FunctionOperatorType GetMinute = FunctionOperatorType.GetMinute;,
        public const FunctionOperatorType GetHour = FunctionOperatorType.GetHour;,
        public const FunctionOperatorType GetDay = FunctionOperatorType.GetDay;,
        public const FunctionOperatorType GetMonth = FunctionOperatorType.GetMonth;,
        public const FunctionOperatorType GetYear = FunctionOperatorType.GetYear;,
        public const FunctionOperatorType GetDayOfWeek = FunctionOperatorType.GetDayOfWeek;,
        public const FunctionOperatorType GetDayOfYear = FunctionOperatorType.GetDayOfYear;,
        public const FunctionOperatorType GetTimeOfDay = FunctionOperatorType.GetTimeOfDay;,
        public const FunctionOperatorType Now = FunctionOperatorType.Now;,
        public const FunctionOperatorType UtcNow = FunctionOperatorType.UtcNow;,
        public const FunctionOperatorType Today = FunctionOperatorType.Today;,
        public const FunctionOperatorType AddTimeSpan = FunctionOperatorType.AddTimeSpan;,
        public const FunctionOperatorType AddTicks = FunctionOperatorType.AddTicks;,
        public const FunctionOperatorType AddMilliSeconds = FunctionOperatorType.AddMilliSeconds;,
        public const FunctionOperatorType AddSeconds = FunctionOperatorType.AddSeconds;,
        public const FunctionOperatorType AddMinutes = FunctionOperatorType.AddMinutes;,
        public const FunctionOperatorType AddHours = FunctionOperatorType.AddHours;,
        public const FunctionOperatorType AddDays = FunctionOperatorType.AddDays;,
        public const FunctionOperatorType AddMonths = FunctionOperatorType.AddMonths;,
        public const FunctionOperatorType AddYears = FunctionOperatorType.AddYears;
    }
}

