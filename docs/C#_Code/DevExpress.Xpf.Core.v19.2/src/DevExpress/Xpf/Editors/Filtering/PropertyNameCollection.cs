namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Collections.Generic;

    public class PropertyNameCollection : List<string>
    {
        public PropertyNameCollection()
        {
        }

        public PropertyNameCollection(IEnumerable<string> collection) : base(collection)
        {
        }
    }
}

