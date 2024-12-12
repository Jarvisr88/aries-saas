namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.SpreadsheetSource.Xlsx.Import.Internal;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class TextRunDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();
        [ThreadStatic]
        private static TextRunDestination instance;
        private List<SharedStringItem> stringItems;

        public TextRunDestination(XlsxSpreadsheetSourceImporter importer, List<SharedStringItem> stringItems) : base(importer)
        {
            Guard.ArgumentNotNull(stringItems, "stringItems");
            this.stringItems = stringItems;
        }

        public static void ClearInstance()
        {
            if (instance != null)
            {
                instance.stringItems = null;
            }
            instance = null;
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("t", new ElementHandler<XlsxSpreadsheetSourceImporter>(TextRunDestination.OnText));
            return table;
        }

        public static TextRunDestination GetInstance(XlsxSpreadsheetSourceImporter importer, List<SharedStringItem> stringItems)
        {
            if ((instance != null) && (instance.Importer == importer))
            {
                instance.stringItems = stringItems;
            }
            else
            {
                instance = new TextRunDestination(importer, stringItems);
            }
            return instance;
        }

        private static TextRunDestination GetThis(XlsxSpreadsheetSourceImporter importer) => 
            (TextRunDestination) importer.PeekDestination();

        private static Destination OnText(XlsxSpreadsheetSourceImporter importer, XmlReader reader)
        {
            SharedStringItem item = new SharedStringItem();
            GetThis(importer).stringItems.Add(item);
            return TextDestination.GetInstance(importer, item);
        }

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

