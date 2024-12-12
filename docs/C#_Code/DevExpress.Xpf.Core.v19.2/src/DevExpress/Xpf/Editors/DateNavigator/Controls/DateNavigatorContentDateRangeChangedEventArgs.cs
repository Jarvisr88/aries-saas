namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;
    using System.Runtime.CompilerServices;

    public class DateNavigatorContentDateRangeChangedEventArgs : EventArgs
    {
        public DateNavigatorContentDateRangeChangedEventArgs(bool isScrolling)
        {
            this.IsScrolling = isScrolling;
        }

        public bool IsScrolling { get; private set; }
    }
}

