namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSupportShouldSerializeCollectionItem
    {
        bool ShouldSerializeCollectionItem(XtraItemEventArgs e);
    }
}

