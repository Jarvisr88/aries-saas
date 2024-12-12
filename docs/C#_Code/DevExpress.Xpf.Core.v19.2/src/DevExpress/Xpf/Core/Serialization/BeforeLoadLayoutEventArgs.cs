namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BeforeLoadLayoutEventArgs : RoutedEventArgs
    {
        public BeforeLoadLayoutEventArgs(object source, string restoredVersion) : base(DXSerializer.BeforeLoadLayoutEvent, source)
        {
            this.RestoredVersion = restoredVersion;
            this.Allow = true;
        }

        public string RestoredVersion { get; private set; }

        public bool Allow { get; set; }
    }
}

