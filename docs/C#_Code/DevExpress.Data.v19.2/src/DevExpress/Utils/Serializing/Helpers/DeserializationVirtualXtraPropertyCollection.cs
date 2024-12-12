namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Xml;

    public class DeserializationVirtualXtraPropertyCollection : VirtualXtraPropertyCollectionBase
    {
        private XmlReader tr;
        private PrintingSystemXmlSerializer serializer;

        public DeserializationVirtualXtraPropertyCollection(XmlReader tr, PrintingSystemXmlSerializer serializer)
        {
            this.tr = tr;
            this.serializer = serializer;
        }

        protected override CollectionItemInfosEnumeratorBase CreateEnumerator() => 
            new DevExpress.Utils.Serializing.Helpers.DeserializationCollectionItemInfosEnumerator(this.tr, this.serializer);
    }
}

