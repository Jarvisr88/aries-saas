namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSerializableCollection
    {
        void AfterDeserialize(XtraItemEventArgs e);
        void BeforeDeserialize(XtraItemEventArgs e);
        bool Clear(XtraItemEventArgs e);
        object CreateItem(XtraItemEventArgs e);
        void RemoveItem(XtraSetItemIndexEventArgs e);
        bool SetItemIndex(XtraSetItemIndexEventArgs e);
    }
}

