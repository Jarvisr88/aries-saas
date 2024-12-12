namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Xml;

    public class DeserializationCollectionItemInfosEnumerator : CollectionItemInfosEnumeratorBase
    {
        private XmlReader tr;
        private PrintingSystemXmlSerializer serializer;
        private bool finished;

        public DeserializationCollectionItemInfosEnumerator(XmlReader tr, PrintingSystemXmlSerializer serializer)
        {
            this.tr = tr;
            this.serializer = serializer;
            this.Reset();
        }

        protected override bool MoveNextCore()
        {
            if (this.finished)
            {
                return false;
            }
            base.currentInfo = this.serializer.ReadInfo(this.tr);
            this.finished = ReferenceEquals(base.currentInfo, null);
            return !this.finished;
        }

        public override void Reset()
        {
            base.Reset();
            this.finished = !this.tr.IsStartElement();
        }
    }
}

