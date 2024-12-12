namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EditValueChangedEventArgs : RoutedEventArgs
    {
        public EditValueChangedEventArgs(object oldValue, object newValue) : base(BaseEdit.EditValueChangedEvent)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public object OldValue { get; private set; }

        public object NewValue { get; private set; }
    }
}

