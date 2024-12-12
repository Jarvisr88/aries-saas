namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.SpreadsheetSource.Xlsx.Import.Internal;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class SharedStringDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();
        [ThreadStatic]
        private static SharedStringDestination instance;
        private List<SharedStringItem> runs;
        private SharedStringItem stringItem;

        public SharedStringDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
            this.runs = new List<SharedStringItem>();
            this.stringItem = new SharedStringItem();
        }

        protected virtual void ApplyText(string item)
        {
            item = (item == null) ? string.Empty : item;
            this.Importer.Source.SharedStrings.Add(item);
        }

        public static void ClearInstance()
        {
            instance = null;
            TextDestination.ClearInstance();
            TextRunDestination.ClearInstance();
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("t", new ElementHandler<XlsxSpreadsheetSourceImporter>(SharedStringDestination.OnText));
            table.Add("r", new ElementHandler<XlsxSpreadsheetSourceImporter>(SharedStringDestination.OnRun));
            return table;
        }

        public static SharedStringDestination GetInstance(XlsxSpreadsheetSourceImporter importer)
        {
            if ((instance != null) && (instance.Importer == importer))
            {
                instance.Reset();
            }
            else
            {
                instance = new SharedStringDestination(importer);
            }
            return instance;
        }

        private static SharedStringDestination GetThis(XlsxSpreadsheetSourceImporter importer) => 
            (SharedStringDestination) importer.PeekDestination();

        private void MergeRuns()
        {
            string str = string.Empty;
            foreach (SharedStringItem item in this.runs)
            {
                str = str + item.Text;
            }
            this.ApplyText(str);
        }

        private static Destination OnRun(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            TextRunDestination.GetInstance(importer, GetThis(importer).runs);

        private static Destination OnText(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            TextDestination.GetInstance(importer, GetThis(importer).stringItem);

        public override void ProcessElementClose(XmlReader reader)
        {
            if ((this.runs != null) && (this.runs.Count <= 0))
            {
                this.ApplyText(this.stringItem.Text);
            }
            else
            {
                this.MergeRuns();
            }
        }

        private void Reset()
        {
            this.runs.Clear();
            this.stringItem.Text = null;
        }

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

