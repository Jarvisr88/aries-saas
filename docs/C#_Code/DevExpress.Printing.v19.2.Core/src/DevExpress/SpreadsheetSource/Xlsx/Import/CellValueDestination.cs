namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class CellValueDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private WorksheetCellDestination cellDestination;
        [ThreadStatic]
        private static CellValueDestination instance;

        public CellValueDestination(XlsxSpreadsheetSourceImporter importer, WorksheetCellDestination cellDestination) : base(importer)
        {
            Guard.ArgumentNotNull(cellDestination, "cellDestination");
            this.cellDestination = cellDestination;
        }

        public static void ClearInstance()
        {
            if (instance != null)
            {
                instance.cellDestination = null;
            }
            instance = null;
        }

        public static CellValueDestination GetInstance(XlsxSpreadsheetSourceImporter importer, WorksheetCellDestination cellDestination)
        {
            if ((instance != null) && (instance.Importer == importer))
            {
                instance.cellDestination = cellDestination;
            }
            else
            {
                instance = new CellValueDestination(importer, cellDestination);
            }
            return instance;
        }

        public override bool ProcessText(XmlReader reader)
        {
            this.cellDestination.Value = reader.Value;
            return true;
        }
    }
}

