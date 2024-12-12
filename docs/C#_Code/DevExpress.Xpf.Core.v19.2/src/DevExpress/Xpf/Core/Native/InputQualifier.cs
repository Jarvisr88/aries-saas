namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class InputQualifier : IBindableUriQualifier, IBaseUriQualifier
    {
        private const string nameValue = "input";
        private const string mouseValue = "mouse";
        private const string touchValue = "touch";

        public virtual int GetAltitude(DependencyObject context, string value, IEnumerable<string> values, out int maxAltitude);
        public virtual Binding GetBinding(RelativeSource source);
        public virtual Binding GetBinding(DependencyObject source);
        public int GetMaxAltitude(DependencyObject context, IEnumerable<string> values);
        private bool IsMouseString(string value);
        private bool IsTouchString(string value);
        public bool IsValidValue(string value);

        public string Name { get; }

        public string DefaultValue { get; }
    }
}

