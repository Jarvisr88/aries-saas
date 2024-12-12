namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.Collections;

    public class SerializationVirtualXtraPropertyCollection : VirtualXtraPropertyCollectionBase
    {
        protected SerializeHelper helper;
        protected OptionsLayoutBase options;
        protected ICollection collection;

        public SerializationVirtualXtraPropertyCollection(SerializeHelper helper, OptionsLayoutBase options, ICollection collection)
        {
            this.helper = helper;
            this.options = options;
            this.collection = collection;
        }

        protected override CollectionItemInfosEnumeratorBase CreateEnumerator() => 
            new SerializationCollectionItemInfosEnumerator(this.helper, this.options, this.collection);

        public override int Count =>
            this.collection.Count;
    }
}

