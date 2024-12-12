namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IXtraSerializable
    {
        void OnEndDeserializing(string restoredVersion);
        void OnEndSerializing();
        void OnStartDeserializing(LayoutAllowEventArgs e);
        void OnStartSerializing();
    }
}

