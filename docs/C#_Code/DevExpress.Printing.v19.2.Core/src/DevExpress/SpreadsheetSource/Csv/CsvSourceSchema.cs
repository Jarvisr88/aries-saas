namespace DevExpress.SpreadsheetSource.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class CsvSourceSchema
    {
        private readonly Dictionary<int, XlVariantValueType> items = new Dictionary<int, XlVariantValueType>();

        public void Add(int index, XlVariantValueType valueType)
        {
            Guard.ArgumentNonNegative(index, "index");
            this.items[index] = valueType;
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public XlVariantValueType this[int index]
        {
            get
            {
                XlVariantValueType type;
                Guard.ArgumentNonNegative(index, "index");
                return (!this.items.TryGetValue(index, out type) ? XlVariantValueType.None : type);
            }
        }

        public IEnumerable<CsvSourceSchemaItem> Items
        {
            get
            {
                Func<KeyValuePair<int, XlVariantValueType>, int> keySelector = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<KeyValuePair<int, XlVariantValueType>, int> local1 = <>c.<>9__4_0;
                    keySelector = <>c.<>9__4_0 = item => item.Key;
                }
                Func<KeyValuePair<int, XlVariantValueType>, CsvSourceSchemaItem> selector = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<KeyValuePair<int, XlVariantValueType>, CsvSourceSchemaItem> local2 = <>c.<>9__4_1;
                    selector = <>c.<>9__4_1 = item => new CsvSourceSchemaItem(item.Key, item.Value);
                }
                return this.items.OrderBy<KeyValuePair<int, XlVariantValueType>, int>(keySelector).Select<KeyValuePair<int, XlVariantValueType>, CsvSourceSchemaItem>(selector);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CsvSourceSchema.<>c <>9 = new CsvSourceSchema.<>c();
            public static Func<KeyValuePair<int, XlVariantValueType>, int> <>9__4_0;
            public static Func<KeyValuePair<int, XlVariantValueType>, CsvSourceSchemaItem> <>9__4_1;

            internal int <get_Items>b__4_0(KeyValuePair<int, XlVariantValueType> item) => 
                item.Key;

            internal CsvSourceSchemaItem <get_Items>b__4_1(KeyValuePair<int, XlVariantValueType> item) => 
                new CsvSourceSchemaItem(item.Key, item.Value);
        }
    }
}

