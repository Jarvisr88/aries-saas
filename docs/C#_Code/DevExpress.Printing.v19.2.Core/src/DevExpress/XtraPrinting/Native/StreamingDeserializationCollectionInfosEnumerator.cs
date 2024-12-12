namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    internal class StreamingDeserializationCollectionInfosEnumerator : CollectionItemInfosEnumeratorBase
    {
        private bool finished;
        private IStreamingXmlDeserializerContext context;
        private IStreamingXmlSerializer serializer;

        public StreamingDeserializationCollectionInfosEnumerator(IStreamingXmlDeserializerContext context)
        {
            this.context = context;
            this.serializer = context.Serializer;
            this.Reset();
        }

        protected override bool MoveNextCore()
        {
            if (this.finished)
            {
                return false;
            }
            base.currentInfo = this.serializer.ReadNode(this.context);
            this.finished = ReferenceEquals(base.currentInfo, null);
            return !this.finished;
        }

        public override void Reset()
        {
            base.Reset();
            this.finished = !this.context.Reader.IsStartElement();
        }
    }
}

