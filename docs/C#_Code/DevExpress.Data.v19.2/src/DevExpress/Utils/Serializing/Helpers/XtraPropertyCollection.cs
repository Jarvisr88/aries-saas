namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class XtraPropertyCollection : DXCollection<XtraPropertyInfo>, IXtraPropertyCollection, ICollection, IEnumerable
    {
        private Dictionary<string, XtraPropertyInfo> hash;

        public XtraPropertyCollection() : base(DXCollectionUniquenessProviderType.None)
        {
            this.hash = new Dictionary<string, XtraPropertyInfo>();
        }

        public XtraPropertyCollection(int capacity) : base(capacity, DXCollectionUniquenessProviderType.None)
        {
            this.hash = new Dictionary<string, XtraPropertyInfo>();
        }

        void IXtraPropertyCollection.Add(XtraPropertyInfo prop)
        {
            this.Add(prop);
        }

        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            this.hash.Clear();
        }

        protected override void OnInsertComplete(int index, XtraPropertyInfo value)
        {
            base.OnInsertComplete(index, value);
            this.hash[value.Name] = value;
        }

        protected override void OnRemoveComplete(int index, XtraPropertyInfo value)
        {
            base.OnRemoveComplete(index, value);
            this.hash.Remove(value.Name);
        }

        public XtraPropertyInfo this[string name]
        {
            get
            {
                XtraPropertyInfo info;
                return (this.hash.TryGetValue(name, out info) ? info : null);
            }
        }

        public bool IsSinglePass =>
            false;
    }
}

