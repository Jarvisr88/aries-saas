namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum CustomUIFilterType
    {
        [Display(AutoGenerateFilter=false, GroupName="Special", Order=-1)]
        None = 0,
        [Display(GroupName="Custom", Order=100)]
        Custom = 1,
        [Display(GroupName="Common")]
        Equals = 2,
        [Display(GroupName="Common")]
        DoesNotEqual = 3,
        [Display(GroupName="Common", Order=1)]
        Between = 4,
        [Display(GroupName="Common")]
        IsNull = 5,
        [Display(GroupName="Common")]
        IsNotNull = 6,
        [Display(GroupName="Numeric")]
        GreaterThan = 7,
        [Display(GroupName="Numeric")]
        GreaterThanOrEqualTo = 8,
        [Display(GroupName="Numeric")]
        LessThan = 9,
        [Display(GroupName="Numeric")]
        LessThanOrEqualTo = 10,
        [Display(GroupName="Aggregates")]
        TopN = 11,
        [Display(GroupName="Aggregates")]
        BottomN = 12,
        [Display(GroupName="Aggregates")]
        AboveAverage = 13,
        [Display(GroupName="Aggregates")]
        BelowAverage = 14,
        [Display(GroupName="Text")]
        BeginsWith = 15,
        [Display(GroupName="Text")]
        EndsWith = 0x10,
        [Display(GroupName="Text", AutoGenerateFilter=false)]
        DoesNotBeginsWith = 0x11,
        [Display(GroupName="Text", AutoGenerateFilter=false)]
        DoesNotEndsWith = 0x12,
        [Display(GroupName="Text")]
        Contains = 0x13,
        [Display(GroupName="Text")]
        DoesNotContain = 20,
        [Display(GroupName="Text")]
        IsBlank = 0x15,
        [Display(GroupName="Text")]
        IsNotBlank = 0x16,
        [Display(GroupName="Text")]
        Like = 0x17,
        [Display(GroupName="Text")]
        NotLike = 0x18,
        [Display(GroupName="DateTime")]
        Before = 0x19,
        [Display(GroupName="DateTime")]
        After = 0x1a,
        [Display(GroupName="DateDay")]
        Yesterday = 0x1b,
        [Display(GroupName="DateDay")]
        Today = 0x1c,
        [Display(GroupName="DateDay")]
        Tomorrow = 0x1d,
        [Display(GroupName="DateWeek")]
        LastWeek = 30,
        [Display(GroupName="DateWeek")]
        ThisWeek = 0x1f,
        [Display(GroupName="DateWeek")]
        NextWeek = 0x20,
        [Display(GroupName="DateMonth")]
        LastMonth = 0x21,
        [Display(GroupName="DateMonth")]
        ThisMonth = 0x22,
        [Display(GroupName="DateMonth")]
        NextMonth = 0x23,
        [Display(GroupName="DateQuarter")]
        LastQuarter = 0x24,
        [Display(GroupName="DateQuarter")]
        ThisQuarter = 0x25,
        [Display(GroupName="DateQuarter")]
        NextQuarter = 0x26,
        [Display(GroupName="DateYear")]
        LastYear = 0x27,
        [Display(GroupName="DateYear")]
        ThisYear = 40,
        [Display(GroupName="DateYear")]
        NextYear = 0x29,
        [Display(GroupName="DateYearToDate")]
        YearToDate = 0x2a,
        [Display(GroupName="Period")]
        AllDatesInThePeriod = 0x2b,
        [Display(GroupName=@"Period\Quarter")]
        Quarter1 = 0x2c,
        [Display(GroupName=@"Period\Quarter")]
        Quarter2 = 0x2d,
        [Display(GroupName=@"Period\Quarter")]
        Quarter3 = 0x2e,
        [Display(GroupName=@"Period\Quarter")]
        Quarter4 = 0x2f,
        [Display(GroupName=@"Period\Month")]
        January = 0x30,
        [Display(GroupName=@"Period\Month")]
        February = 0x31,
        [Display(GroupName=@"Period\Month")]
        March = 50,
        [Display(GroupName=@"Period\Month")]
        April = 0x33,
        [Display(GroupName=@"Period\Month")]
        May = 0x34,
        [Display(GroupName=@"Period\Month")]
        June = 0x35,
        [Display(GroupName=@"Period\Month")]
        July = 0x36,
        [Display(GroupName=@"Period\Month")]
        August = 0x37,
        [Display(GroupName=@"Period\Month")]
        September = 0x38,
        [Display(GroupName=@"Period\Month")]
        October = 0x39,
        [Display(GroupName=@"Period\Month")]
        November = 0x3a,
        [Display(GroupName=@"Period\Month")]
        December = 0x3b,
        [Display(GroupName="DatePeriods", Order=-1)]
        DatePeriods = 60,
        [Display(GroupName="DatePeriods")]
        IsSameDay = 0x3d,
        [Display(GroupName="User", Order=-100)]
        User = 0x3e
    }
}

