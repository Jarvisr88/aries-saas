namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;

    internal class StreamingVirtualPropertyCollection : SerializationVirtualXtraPropertyCollection
    {
        public StreamingVirtualPropertyCollection(StreamingSerializeHelper helper, OptionsLayoutBase options, IList objects) : base(helper, options, objects)
        {
        }

        protected override CollectionItemInfosEnumeratorBase CreateEnumerator() => 
            new StreamingVirtualPropertyEnumerator((StreamingSerializeHelper) base.helper, base.options, base.collection);
    }
}

