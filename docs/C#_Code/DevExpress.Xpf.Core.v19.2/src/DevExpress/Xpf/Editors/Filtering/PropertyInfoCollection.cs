namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Collections.Generic;

    public class PropertyInfoCollection : List<PropertyInfo>
    {
        public PropertyInfoCollection()
        {
        }

        public PropertyInfoCollection(IEnumerable<PropertyInfo> collection) : base(collection)
        {
        }
    }
}

