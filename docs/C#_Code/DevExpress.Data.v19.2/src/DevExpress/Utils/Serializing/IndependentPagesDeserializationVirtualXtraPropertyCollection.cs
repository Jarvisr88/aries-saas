namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.IO;

    public class IndependentPagesDeserializationVirtualXtraPropertyCollection : VirtualXtraPropertyCollectionBase
    {
        private Stream stream;
        private IList objects;

        public IndependentPagesDeserializationVirtualXtraPropertyCollection(Stream stream, IList objects)
        {
            this.stream = stream;
            this.objects = objects;
        }

        protected override CollectionItemInfosEnumeratorBase CreateEnumerator() => 
            new DevExpress.Utils.Serializing.DeserializationCollectionItemInfosEnumerator(this.stream, this.objects);
    }
}

