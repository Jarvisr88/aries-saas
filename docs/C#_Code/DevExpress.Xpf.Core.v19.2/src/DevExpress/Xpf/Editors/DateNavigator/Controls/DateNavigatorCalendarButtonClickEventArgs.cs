namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;
    using System.Runtime.CompilerServices;

    public class DateNavigatorCalendarButtonClickEventArgs : EventArgs
    {
        public DateNavigatorCalendarButtonClickEventArgs(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            this.ButtonDate = buttonDate;
            this.ButtonKind = buttonKind;
        }

        public DateTime ButtonDate { get; private set; }

        public DateNavigatorCalendarButtonKind ButtonKind { get; private set; }
    }
}

