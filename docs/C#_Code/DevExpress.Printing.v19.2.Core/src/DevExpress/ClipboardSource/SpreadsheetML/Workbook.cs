namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    public class Workbook
    {
        private XNamespace @namespace;
        private XElement workbook;
        private Dictionary<string, Worksheet> worksheets;

        public Workbook(XDocument document)
        {
            Func<XDocument, XNamespace> get = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<XDocument, XNamespace> local1 = <>c.<>9__3_0;
                get = <>c.<>9__3_0 = x => x.Root.Name.Namespace;
            }
            this.@namespace = document.Get<XDocument, XNamespace>(get, null, "Namespace is undefined.");
            this.GetWorkbook(document);
            this.GetWorksheets();
        }

        private void GetWorkbook(XDocument document)
        {
            this.workbook = document.Element(this.@namespace + "Workbook");
            if (this.workbook == null)
            {
                throw new Exception("Invalid document. Workbook not found.");
            }
        }

        private void GetWorksheets()
        {
            this.worksheets = new Dictionary<string, Worksheet>();
            IEnumerable<XElement> elements = this.workbook.GetElements("Worksheet");
            if (elements.Count<XElement>() < 1)
            {
                throw new Exception("Invalid workbook. Worksheets not found.");
            }
            foreach (XElement element in elements)
            {
                Worksheet worksheet = new Worksheet(element);
                this.worksheets.Add(worksheet.Name, worksheet);
            }
        }

        public Dictionary<string, Worksheet> Worksheets =>
            this.worksheets;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Workbook.<>c <>9 = new Workbook.<>c();
            public static Func<XDocument, XNamespace> <>9__3_0;

            internal XNamespace <.ctor>b__3_0(XDocument x) => 
                x.Root.Name.Namespace;
        }
    }
}

