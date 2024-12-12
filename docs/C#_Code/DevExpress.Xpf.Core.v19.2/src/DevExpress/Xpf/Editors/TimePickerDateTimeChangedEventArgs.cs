namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TimePickerDateTimeChangedEventArgs : RoutedEventArgs
    {
        public TimePickerDateTimeChangedEventArgs(DateTime oldValue, DateTime newValue) : base(TimePicker.DateTimeChangedEvent)
        {
            this.<NewValue>k__BackingField = newValue;
            this.<OldValue>k__BackingField = oldValue;
        }

        public DateTime NewValue { get; }

        public DateTime OldValue { get; }
    }
}

