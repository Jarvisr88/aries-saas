namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class StartDeserializingEventArgs : RoutedEventArgs
    {
        internal StartDeserializingEventArgs(object source, LayoutAllowEventArgs args) : base(DXSerializer.StartDeserializingEvent, source)
        {
            this.Args = args;
        }

        public LayoutAllowEventArgs Args { get; private set; }
    }
}

