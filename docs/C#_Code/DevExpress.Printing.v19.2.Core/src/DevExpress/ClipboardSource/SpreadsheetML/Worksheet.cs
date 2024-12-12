namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    public class Worksheet
    {
        private string name;
        private DevExpress.ClipboardSource.SpreadsheetML.Table table;
        private XElement worksheetElement;

        public Worksheet(XElement worksheetElement)
        {
            this.worksheetElement = worksheetElement;
            Func<XAttribute, string> get = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<XAttribute, string> local1 = <>c.<>9__3_0;
                get = <>c.<>9__3_0 = x => x.Value;
            }
            this.name = worksheetElement.GetAttribute("Name").Get<XAttribute, string>(get, null, "Unknown exception");
            if (this.name == null)
            {
                throw new Exception("Worksheet must have name.");
            }
            this.GetTable();
        }

        private void GetTable()
        {
            XElement tableElement = this.worksheetElement.GetElement("Table");
            if (tableElement != null)
            {
                this.table = new DevExpress.ClipboardSource.SpreadsheetML.Table(tableElement);
            }
        }

        public string Name =>
            this.name;

        public DevExpress.ClipboardSource.SpreadsheetML.Table Table =>
            this.table;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Worksheet.<>c <>9 = new Worksheet.<>c();
            public static Func<XAttribute, string> <>9__3_0;

            internal string <.ctor>b__3_0(XAttribute x) => 
                x.Value;
        }
    }
}

