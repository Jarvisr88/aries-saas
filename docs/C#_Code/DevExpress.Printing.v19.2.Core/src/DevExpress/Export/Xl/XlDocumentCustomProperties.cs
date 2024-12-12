namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class XlDocumentCustomProperties
    {
        private readonly Dictionary<string, XlCustomPropertyValue> items = new Dictionary<string, XlCustomPropertyValue>();

        public void Clear()
        {
            this.items.Clear();
        }

        public int Count =>
            this.items.Count;

        public XlCustomPropertyValue this[string name]
        {
            get
            {
                XlCustomPropertyValue value2;
                return (!this.items.TryGetValue(name, out value2) ? XlCustomPropertyValue.Empty : value2);
            }
            set
            {
                if (value.Type == XlVariantValueType.None)
                {
                    this.items.Remove(name);
                }
                else
                {
                    this.items[name] = value;
                }
            }
        }

        public IEnumerable<string> Names =>
            this.items.Keys;
    }
}

