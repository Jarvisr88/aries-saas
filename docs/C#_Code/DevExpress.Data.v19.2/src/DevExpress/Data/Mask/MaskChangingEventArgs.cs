namespace DevExpress.Data.Mask
{
    using System;
    using System.ComponentModel;

    public class MaskChangingEventArgs : CancelEventArgs
    {
        private object oldValue;
        private object newValue;

        public MaskChangingEventArgs(object oldValue, object newValue);
        public MaskChangingEventArgs(object oldValue, object newValue, bool cancel);

        public object OldValue { get; }

        public object NewValue { get; set; }
    }
}

