namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSupportDeserializeCollectionItem
    {
        object CreateCollectionItem(string propertyName, XtraItemEventArgs e);
        void SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e);
    }
}

