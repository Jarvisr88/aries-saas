namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;

    public class XmlContextItemCollection : DXNamedItemCollection<IXmlContextItem>
    {
        protected internal override int AddIfNotAlreadyAdded(IXmlContextItem obj) => 
            (obj != null) ? base.AddIfNotAlreadyAdded(obj) : -1;

        protected override IXmlContextItem GetItem(int index) => 
            ((index < 0) || (index >= base.Count)) ? null : base.GetItem(index);

        protected override string GetItemName(IXmlContextItem item) => 
            item.Name;

        protected internal override bool RemoveIfAlreadyAdded(IXmlContextItem obj) => 
            (obj != null) ? base.RemoveIfAlreadyAdded(obj) : false;
    }
}

