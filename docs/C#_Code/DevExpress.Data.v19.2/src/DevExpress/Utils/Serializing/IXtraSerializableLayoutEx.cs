namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IXtraSerializableLayoutEx
    {
        bool AllowProperty(OptionsLayoutBase options, string propertyName, int id);
        void ResetProperties(OptionsLayoutBase options);
    }
}

