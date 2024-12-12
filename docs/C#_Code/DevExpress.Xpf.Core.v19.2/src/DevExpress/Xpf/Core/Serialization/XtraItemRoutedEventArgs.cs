namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Windows;

    public class XtraItemRoutedEventArgs : RoutedEventArgs
    {
        private readonly XtraItemEventArgs e;

        public XtraItemRoutedEventArgs(RoutedEvent routedEvent, object source, XtraItemEventArgs e) : base(routedEvent, source)
        {
            this.e = e;
        }

        public object Collection =>
            this.e.Collection;

        public XtraPropertyInfo Item =>
            this.e.Item;

        public OptionsLayoutBase Options =>
            this.e.Options;

        public object Owner =>
            this.e.Owner;

        public object RootObject =>
            this.e.RootObject;

        public int Index =>
            this.e.Index;
    }
}

