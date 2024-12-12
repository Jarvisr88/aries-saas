namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.IO;

    public class DeserializationCollectionItemInfosEnumerator : CollectionItemInfosEnumeratorBase
    {
        private DeflateStreamsArchiveReader reader;
        private IList objects;
        private int index;

        public DeserializationCollectionItemInfosEnumerator(Stream stream, IList objects)
        {
            this.reader = new DeflateStreamsArchiveReader(stream);
            this.objects = objects;
            this.Reset();
        }

        protected override bool MoveNextCore()
        {
            this.index++;
            if (this.index >= this.reader.StreamCount)
            {
                base.currentInfo = null;
                return false;
            }
            if (((XtraObjectInfo) this.objects[this.index]).Skip)
            {
                base.currentInfo = new XtraPropertyInfo();
                return true;
            }
            using (Stream stream = this.reader.GetStream(this.index))
            {
                base.currentInfo = new PrintingSystemXmlSerializer().DeserializeObject(stream);
            }
            return true;
        }

        public override void Reset()
        {
            base.Reset();
            this.index = -1;
        }
    }
}

