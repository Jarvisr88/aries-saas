namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class RangeParameterEditorOptions
    {
        static RangeParameterEditorOptions()
        {
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_Today.GetString(), () => DateTime.Today, () => DateTime.Today);
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_Yesterday.GetString(), () => DateTime.Today.AddDays(-1.0), () => DateTime.Today.AddDays(-1.0));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_CurrentWeek.GetString(), () => DateTime.Today.AddDays((double) (Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek)), () => DateTime.Today.AddDays((double) ((Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek) + 6)));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_LastWeek.GetString(), () => DateTime.Today.AddDays((double) ((Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek) - 7)), () => DateTime.Today.AddDays((double) ((Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek) - 1)));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_Last7Days.GetString(), () => DateTime.Today.AddDays(-7.0), () => DateTime.Today);
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_CurrentMonth.GetString(), () => DateTime.Today.AddDays((double) (-DateTime.Today.Day + 1)), () => DateTime.Today.AddMonths(1).AddDays((double) -DateTime.Today.Day));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_LastMonth.GetString(), () => DateTime.Today.AddMonths(-1).AddDays((double) (-DateTime.Today.Day + 1)), () => DateTime.Today.AddDays((double) -DateTime.Today.Day));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_CurrentQuarter.GetString(), () => DateTime.Today.AddMonths(-(DateTime.Today.Month - 1) % 3).AddDays((double) (-DateTime.Today.Day + 1)), () => DateTime.Today.AddMonths((-(DateTime.Today.Month - 1) % 3) + 3).AddDays((double) -DateTime.Today.Day));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_PreviousQuarter.GetString(), () => DateTime.Today.AddMonths((-(DateTime.Today.Month - 1) % 3) - 3).AddDays((double) (-DateTime.Today.Day + 1)), () => DateTime.Today.AddMonths(-(DateTime.Today.Month - 1) % 3).AddDays((double) -DateTime.Today.Day));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_CurrentYear.GetString(), () => new DateTime(DateTime.Today.Year, 1, 1), () => new DateTime(DateTime.Today.Year + 1, 1, 1).AddDays(-1.0));
            RegisterDateRange(PreviewStringId.DateRangeParameterEditor_LastYear.GetString(), () => new DateTime(DateTime.Today.Year - 1, 1, 1), () => new DateTime(DateTime.Today.Year, 1, 1).AddDays(-1.0));
        }

        public static void RegisterDateRange(string name, Func<DateTime> getStart, Func<DateTime> getEnd)
        {
            PredefinedDateRanges.Add(name, () => new Range<DateTime>(getStart(), getEnd()));
        }

        public static Dictionary<string, Func<Range<DateTime>>> PredefinedDateRanges { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RangeParameterEditorOptions.<>c <>9 = new RangeParameterEditorOptions.<>c();

            internal DateTime <.cctor>b__3_0() => 
                DateTime.Today;

            internal DateTime <.cctor>b__3_1() => 
                DateTime.Today;

            internal DateTime <.cctor>b__3_10() => 
                DateTime.Today.AddDays((double) (-DateTime.Today.Day + 1));

            internal DateTime <.cctor>b__3_11() => 
                DateTime.Today.AddMonths(1).AddDays((double) -DateTime.Today.Day);

            internal DateTime <.cctor>b__3_12() => 
                DateTime.Today.AddMonths(-1).AddDays((double) (-DateTime.Today.Day + 1));

            internal DateTime <.cctor>b__3_13() => 
                DateTime.Today.AddDays((double) -DateTime.Today.Day);

            internal DateTime <.cctor>b__3_14() => 
                DateTime.Today.AddMonths(-(DateTime.Today.Month - 1) % 3).AddDays((double) (-DateTime.Today.Day + 1));

            internal DateTime <.cctor>b__3_15() => 
                DateTime.Today.AddMonths((-(DateTime.Today.Month - 1) % 3) + 3).AddDays((double) -DateTime.Today.Day);

            internal DateTime <.cctor>b__3_16() => 
                DateTime.Today.AddMonths((-(DateTime.Today.Month - 1) % 3) - 3).AddDays((double) (-DateTime.Today.Day + 1));

            internal DateTime <.cctor>b__3_17() => 
                DateTime.Today.AddMonths(-(DateTime.Today.Month - 1) % 3).AddDays((double) -DateTime.Today.Day);

            internal DateTime <.cctor>b__3_18() => 
                new DateTime(DateTime.Today.Year, 1, 1);

            internal DateTime <.cctor>b__3_19() => 
                new DateTime(DateTime.Today.Year + 1, 1, 1).AddDays(-1.0);

            internal DateTime <.cctor>b__3_2() => 
                DateTime.Today.AddDays(-1.0);

            internal DateTime <.cctor>b__3_20() => 
                new DateTime(DateTime.Today.Year - 1, 1, 1);

            internal DateTime <.cctor>b__3_21() => 
                new DateTime(DateTime.Today.Year, 1, 1).AddDays(-1.0);

            internal DateTime <.cctor>b__3_3() => 
                DateTime.Today.AddDays(-1.0);

            internal DateTime <.cctor>b__3_4() => 
                DateTime.Today.AddDays((double) (Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek));

            internal DateTime <.cctor>b__3_5() => 
                DateTime.Today.AddDays((double) ((Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek) + 6));

            internal DateTime <.cctor>b__3_6() => 
                DateTime.Today.AddDays((double) ((Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek) - 7));

            internal DateTime <.cctor>b__3_7() => 
                DateTime.Today.AddDays((double) ((Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek) - 1));

            internal DateTime <.cctor>b__3_8() => 
                DateTime.Today.AddDays(-7.0);

            internal DateTime <.cctor>b__3_9() => 
                DateTime.Today;
        }
    }
}

