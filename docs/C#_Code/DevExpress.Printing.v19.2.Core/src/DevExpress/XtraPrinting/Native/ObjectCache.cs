namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ObjectCache
    {
        private readonly Indexator indexator;
        private Dictionary<object, SerializationInfo> dictionary;
        private Dictionary<int, object> indicesDictionary;
        private Dictionary<object, object> sharedObjects;
        private int collectSharedObjectsLockCount;

        public ObjectCache();
        public ObjectCache(Indexator indexator);
        public ObjectCache(IEqualityComparer<object> equalityComparer);
        public ObjectCache(IEqualityComparer<object> equalityComparer, Indexator indexator);
        public void AddDeserializationObject(object obj, XtraItemEventArgs e);
        public void AddDeserializationObject(object obj, int index);
        private void AddObject(object obj, int index);
        public void AddSerializationObject(object obj);
        private void AddSharedObject(object obj);
        public SerializationInfo GetIndexByObject(object obj);
        public object GetObjectByIndex(int index);
        public void Merge(ObjectCache objectCache);
        public void StartCollectSharedObjects();
        public void StopCollectSharedObjects();

        private bool CollectSharedObjects { get; }

        public ICollection SharedObjectsCollection { get; }

        public ICollection Collection { get; }
    }
}

