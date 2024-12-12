namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IXtraSerializable2
    {
        void Deserialize(IList props);
        XtraPropertyInfo[] Serialize();
    }
}

