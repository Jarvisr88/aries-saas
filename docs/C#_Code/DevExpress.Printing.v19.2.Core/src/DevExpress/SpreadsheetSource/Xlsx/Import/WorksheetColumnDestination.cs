namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.SpreadsheetSource.Xlsx;
    using System;
    using System.Xml;

    public class WorksheetColumnDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        public WorksheetColumnDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            int firstIndex = this.Importer.GetWpSTIntegerValue(reader, "min", 0);
            if (firstIndex <= 0)
            {
                this.Importer.ThrowInvalidFile("Invalid first column index");
            }
            int lastIndex = this.Importer.GetWpSTIntegerValue(reader, "max", 0);
            if (lastIndex <= 0)
            {
                this.Importer.ThrowInvalidFile("Invalid last column index");
            }
            if (lastIndex < firstIndex)
            {
                this.Importer.ThrowInvalidFile("Last column index less than first");
            }
            firstIndex--;
            lastIndex--;
            XlsxSourceDataReader dataReader = this.Importer.Source.DataReader;
            if (dataReader.CanAddColumn(firstIndex, lastIndex))
            {
                ColumnInfo item = new ColumnInfo(firstIndex, lastIndex, this.Importer.GetWpSTOnOffValue(reader, "hidden", false), this.Importer.GetWpSTIntegerValue(reader, "style", -1));
                dataReader.Columns.Add(item);
            }
        }
    }
}

