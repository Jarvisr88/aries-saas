namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    internal class StreamingDeserializationVirtualPropertyCollection : VirtualXtraPropertyCollectionBase
    {
        private IStreamingXmlDeserializerContext context;

        public StreamingDeserializationVirtualPropertyCollection(IStreamingXmlDeserializerContext context)
        {
            this.context = context;
        }

        protected override CollectionItemInfosEnumeratorBase CreateEnumerator() => 
            new StreamingDeserializationCollectionInfosEnumerator(this.context);
    }
}

