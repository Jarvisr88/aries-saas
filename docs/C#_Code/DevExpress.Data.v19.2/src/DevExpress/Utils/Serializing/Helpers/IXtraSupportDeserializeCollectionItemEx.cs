namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSupportDeserializeCollectionItemEx : IXtraSupportDeserializeCollectionItem
    {
        void RemoveCollectionItem(string propertyName, XtraSetItemIndexEventArgs e);
    }
}

