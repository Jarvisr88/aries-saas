namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSupportAfterDeserialize
    {
        void AfterDeserialize(XtraItemEventArgs e);
    }
}

