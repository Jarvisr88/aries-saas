namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;

    public class SerializationCollectionItemInfosEnumerator : CollectionItemInfosEnumeratorBase
    {
        private SerializeHelper helper;
        protected OptionsLayoutBase options;
        protected IEnumerator en;

        public SerializationCollectionItemInfosEnumerator(SerializeHelper helper, OptionsLayoutBase options, ICollection collection)
        {
            this.helper = helper;
            this.options = options;
            this.en = collection.GetEnumerator();
            this.Reset();
        }

        protected override bool MoveNextCore()
        {
            if (!this.en.MoveNext())
            {
                return false;
            }
            XtraObjectInfo current = (XtraObjectInfo) this.en.Current;
            base.currentInfo = new XtraPropertyInfo(current, this.helper.SerializeObject(current.Instance, this.options));
            return true;
        }

        public override void Reset()
        {
            base.Reset();
            this.en.Reset();
        }
    }
}

