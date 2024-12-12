namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Input;

    public interface IDateEditCalendarBase
    {
        bool ProcessKeyDown(KeyEventArgs e);

        System.DateTime DateTime { get; set; }

        System.DateTime? MinValue { get; set; }

        System.DateTime? MaxValue { get; set; }

        string Mask { get; set; }

        bool ShowToday { get; set; }

        bool ShowWeekNumbers { get; set; }

        bool ShowClearButton { get; set; }
    }
}

