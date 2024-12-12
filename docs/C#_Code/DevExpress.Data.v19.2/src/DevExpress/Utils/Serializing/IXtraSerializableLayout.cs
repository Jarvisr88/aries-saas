namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IXtraSerializableLayout
    {
        string LayoutVersion { get; }
    }
}

