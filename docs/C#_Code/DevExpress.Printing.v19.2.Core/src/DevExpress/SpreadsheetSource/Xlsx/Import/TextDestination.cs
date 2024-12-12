namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Xlsx.Import.Internal;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class TextDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private SharedStringItem stringItem;
        [ThreadStatic]
        private static TextDestination instance;

        public TextDestination(XlsxSpreadsheetSourceImporter importer, SharedStringItem stringItem) : base(importer)
        {
            Guard.ArgumentNotNull(stringItem, "stringItem");
            this.stringItem = stringItem;
        }

        public static void ClearInstance()
        {
            if (instance != null)
            {
                instance.stringItem = null;
            }
            instance = null;
        }

        public static TextDestination GetInstance(XlsxSpreadsheetSourceImporter importer, SharedStringItem stringItem)
        {
            if ((instance != null) && (instance.Importer == importer))
            {
                instance.stringItem = stringItem;
            }
            else
            {
                instance = new TextDestination(importer, stringItem);
            }
            return instance;
        }

        public override bool ProcessText(XmlReader reader)
        {
            string str = reader.Value;
            if (!string.IsNullOrEmpty(str))
            {
                this.stringItem.Text = ExcelXmlCharsCodec.Decode(str);
            }
            return true;
        }
    }
}

