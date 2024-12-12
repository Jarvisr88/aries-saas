namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;

    internal class StreamingVirtualPropertyEnumerator : SerializationCollectionItemInfosEnumerator
    {
        private StreamingSerializeHelper helper;

        public StreamingVirtualPropertyEnumerator(StreamingSerializeHelper helper, OptionsLayoutBase options, ICollection collection) : base(helper, options, collection)
        {
            this.helper = helper;
        }

        protected override bool MoveNextCore()
        {
            if (!base.en.MoveNext())
            {
                return false;
            }
            XtraObjectInfo current = (XtraObjectInfo) base.en.Current;
            base.currentInfo = new StreamingPropertyInfo(current, base.options, this.helper);
            return true;
        }
    }
}

