namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class AllowSubscribeDependecyPropertyNotificationEventArgs : EventArgs
    {
        public AllowSubscribeDependecyPropertyNotificationEventArgs(object item, DependencyPropertyDescriptor property, bool allow);

        public object Item { get; private set; }

        public DependencyPropertyDescriptor Property { get; private set; }

        public bool Allow { get; set; }
    }
}

