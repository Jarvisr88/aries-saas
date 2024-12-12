namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    public class Data
    {
        private XElement dataElement;
        private DataType type;

        public Data(XElement dataElement)
        {
            this.dataElement = dataElement;
            XAttribute @this = dataElement.GetAttribute("Type");
            if (@this == null)
            {
                throw new Exception("Data must have type.");
            }
            Func<XAttribute, string> get = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<XAttribute, string> local1 = <>c.<>9__2_0;
                get = <>c.<>9__2_0 = x => x.Value;
            }
            Enum.TryParse<DataType>(@this.Get<XAttribute, string>(get, null, "Unknown exception"), out this.type);
        }

        public DataType Type =>
            this.type;

        public string Value =>
            (this.dataElement != null) ? this.dataElement.Value : string.Empty;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Data.<>c <>9 = new Data.<>c();
            public static Func<XAttribute, string> <>9__2_0;

            internal string <.ctor>b__2_0(XAttribute x) => 
                x.Value;
        }
    }
}

