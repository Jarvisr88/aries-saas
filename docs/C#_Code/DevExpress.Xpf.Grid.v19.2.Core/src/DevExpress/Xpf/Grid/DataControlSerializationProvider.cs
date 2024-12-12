namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core.Serialization;
    using System;

    internal class DataControlSerializationProvider : SerializationProvider
    {
        protected override object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            object obj2 = base.OnCreateCollectionItem(e);
            if (obj2 == null)
            {
                IXtraSupportDeserializeCollectionItem source = e.Source as IXtraSupportDeserializeCollectionItem;
                if (source != null)
                {
                    obj2 = source.CreateCollectionItem(e.CollectionName, new XtraItemEventArgs(e.Owner, e.Collection, e.Item));
                }
            }
            return obj2;
        }
    }
}

