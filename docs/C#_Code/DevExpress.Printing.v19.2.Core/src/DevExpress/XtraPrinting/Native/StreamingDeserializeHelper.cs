namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class StreamingDeserializeHelper : DeserializeHelper
    {
        private StreamingSerializationContext streamingSerializationContext;

        public StreamingDeserializeHelper(object rootObject, StreamingSerializationContext context) : base(rootObject, true, context)
        {
            this.streamingSerializationContext = context;
        }

        public override void DeserializeCollection(XtraSerializableProperty attr, XtraPropertyInfo root, object owner, object collection, OptionsLayoutBase options)
        {
            ICollection what = collection as ICollection;
            if ((what != null) && (owner != null))
            {
                List<object> where = new List<object>();
                if (attr.MergeCollection)
                {
                    AddRange(where, what);
                }
                XtraItemEventArgs e = new XtraItemEventArgs(base.rootObject, owner, collection, root, options);
                base.Context.InvokeBeforeDeserializeCollection(e);
                try
                {
                    if (attr.ClearCollection)
                    {
                        base.Context.InvokeClearCollection(this, e);
                    }
                    int num = 0;
                    foreach (XtraPropertyInfo info in root.ChildProperties)
                    {
                        if (info != null)
                        {
                            base.DeserializeCollectionItem(attr, root, owner, collection, info, num++, options);
                        }
                    }
                    if ((num != 0) && attr.MergeCollection)
                    {
                        base.MergeCollection(attr, root, owner, where, what, options);
                    }
                }
                finally
                {
                    base.Context.InvokeAfterDeserializeCollection(e);
                }
            }
        }

        protected override List<SerializablePropertyDescriptorPair> GetProperties(object obj, IXtraPropertyCollection store) => 
            this.streamingSerializationContext.GetSortedProperties(obj);

        protected internal override IList<SerializablePropertyDescriptorPair> SortProps(object obj, List<SerializablePropertyDescriptorPair> pairsList) => 
            pairsList;
    }
}

