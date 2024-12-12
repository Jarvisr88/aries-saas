namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class VisibleDateRangeChangedEventArgs : RoutedEventArgs
    {
        internal VisibleDateRangeChangedEventArgs(DateTime firstVisibleDateOldValue, DateTime firstVisibleDateNewValue, DateTime lastVisibleDateOldValue, DateTime lastVisibleDateNewValue) : base(DevExpress.Xpf.Editors.DateNavigator.DateNavigator.VisibleDateRangeChangedEvent)
        {
            this.FirstVisibleDateNewValue = firstVisibleDateNewValue;
            this.FirstVisibleDateOldValue = firstVisibleDateOldValue;
            this.LastVisibleDateNewValue = lastVisibleDateNewValue;
            this.LastVisibleDateOldValue = lastVisibleDateOldValue;
        }

        public DateTime FirstVisibleDateOldValue { get; private set; }

        public DateTime FirstVisibleDateNewValue { get; private set; }

        public DateTime LastVisibleDateOldValue { get; private set; }

        public DateTime LastVisibleDateNewValue { get; private set; }
    }
}

