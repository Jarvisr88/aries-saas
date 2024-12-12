namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class DXXmlNodeCollection : DXCollection<System.Xml.XmlNode>
    {
        public DXXmlNodeCollection()
        {
            base.UniquenessProviderType = DXCollectionUniquenessProviderType.None;
        }

        protected override System.Xml.XmlNode GetItem(int index) => 
            ((index < 0) || (index >= this.InnerList.Count)) ? null : base.GetItem(index);
    }
}

