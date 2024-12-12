namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IOptionsProviderService
    {
        event EventHandler OptionsChanged;

        void Start();
        void Stop();

        DateTime Today { get; }

        DayOfWeek FirstDayOfWeek { get; }

        bool HighlightSpecialDates { set; }

        IList<DateTime> ExactWorkdays { get; }

        IList<DateTime> Holidays { get; }

        bool ScrollSelection { get; }

        IList<DayOfWeek> Workdays { get; }

        IList<DateTime> DisabledDates { get; }

        IList<DateTime> SpecialDates { get; }
    }
}

