namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDisplayTextProvider
    {
        bool? GetDisplayText(string originalDisplayText, object value, out string displayText);
    }
}

