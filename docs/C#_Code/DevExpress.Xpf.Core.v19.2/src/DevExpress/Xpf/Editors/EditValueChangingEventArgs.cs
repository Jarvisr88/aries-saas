namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EditValueChangingEventArgs : RoutedEventArgs
    {
        public EditValueChangingEventArgs(object oldValue, object newValue) : base(BaseEdit.EditValueChangingEvent)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public object OldValue { get; private set; }

        public object NewValue { get; private set; }

        public bool IsCancel { get; set; }
    }
}

