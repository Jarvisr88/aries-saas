namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class ContrastQualifier : IBindableUriQualifier, IBaseUriQualifier
    {
        private const string nameValue = "contrast";
        private const string blackValue = "black";
        private const string whiteValue = "white";

        public int GetAltitude(DependencyObject context, string value, IEnumerable<string> values, out int maxAltitude);
        public Binding GetBinding(RelativeSource source);
        public Binding GetBinding(DependencyObject source);
        private bool IsBlackString(string value);
        public bool IsValidValue(string value);
        private bool IsWhiteString(string value);

        public string Name { get; }

        public string DefaultValue { get; }
    }
}

