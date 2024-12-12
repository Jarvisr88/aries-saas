namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.SpreadsheetSource.Xlsx;
    using DevExpress.XtraExport.Xlsx;
    using System;
    using System.Xml;

    public class SheetDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        public SheetDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            string relationId = this.Importer.ReadAttribute(reader, "id", this.Importer.RelationsNamespace);
            XlSheetVisibleState visibleState = this.Importer.GetWpEnumValue<XlSheetVisibleState>(reader, "state", XlsxDataAwareExporter.VisibilityTypeTable, XlSheetVisibleState.Visible);
            string str2 = this.Importer.ReadAttribute(reader, "name");
            if (!string.IsNullOrEmpty(str2))
            {
                this.Importer.Source.InnerWorksheets.Add(new XlsxWorksheet(str2, visibleState, relationId));
            }
        }
    }
}

