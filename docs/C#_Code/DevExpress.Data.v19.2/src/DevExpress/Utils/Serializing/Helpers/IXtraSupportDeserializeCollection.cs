namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSupportDeserializeCollection
    {
        void AfterDeserializeCollection(string propertyName, XtraItemEventArgs e);
        void BeforeDeserializeCollection(string propertyName, XtraItemEventArgs e);
        bool ClearCollection(string propertyName, XtraItemEventArgs e);
    }
}

