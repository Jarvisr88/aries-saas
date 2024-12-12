namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class XtraPropertyInfoCollection : List<XtraPropertyInfo>, IXtraPropertyCollection, ICollection, IEnumerable
    {
        public void AddRange(ICollection props)
        {
            foreach (XtraPropertyInfo info in props)
            {
                base.Add(info);
            }
        }

        public XtraPropertyInfo this[string name]
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public bool IsSinglePass =>
            false;
    }
}

