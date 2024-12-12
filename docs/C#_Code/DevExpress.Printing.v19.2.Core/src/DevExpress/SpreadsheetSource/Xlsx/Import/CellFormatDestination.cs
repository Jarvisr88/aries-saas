namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class CellFormatDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        public CellFormatDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            int item = this.Importer.GetWpSTIntegerValue(reader, "numFmtId", 0);
            this.Importer.Source.NumberFormatIds.Add(item);
        }
    }
}

