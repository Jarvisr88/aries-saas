namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IBaseUriQualifier
    {
        int GetAltitude(DependencyObject context, string value, IEnumerable<string> values, out int maxAltitude);
        bool IsValidValue(string value);

        string Name { get; }

        string DefaultValue { get; }
    }
}

