namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EndDeserializingEventArgs : RoutedEventArgs
    {
        internal EndDeserializingEventArgs(object source, string restoredVersion) : base(DXSerializer.EndDeserializingEvent, source)
        {
            this.RestoredVersion = restoredVersion;
        }

        public string RestoredVersion { get; private set; }
    }
}

