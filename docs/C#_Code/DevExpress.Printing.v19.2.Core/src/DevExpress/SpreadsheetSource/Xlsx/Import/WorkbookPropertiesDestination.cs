namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class WorkbookPropertiesDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        public WorkbookPropertiesDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.Importer.Source.UseDate1904 = this.Importer.GetWpSTOnOffValue(reader, "date1904", false);
        }
    }
}

