namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class NumberFormatDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private int numberFormatId;
        private string numberFormatCode;

        public NumberFormatDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.Importer.Source.NumberFormatCodes.Add(this.numberFormatId, this.numberFormatCode);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.numberFormatId = this.Importer.GetIntegerValue(reader, "numFmtId", -2147483648);
            if (this.numberFormatId == -2147483648)
            {
                this.Importer.ThrowInvalidFile("numFmtId is not specified");
            }
            this.numberFormatCode = reader.GetAttribute("formatCode");
            if (this.numberFormatCode == null)
            {
                this.Importer.ThrowInvalidFile("formatCode is not specified");
            }
        }
    }
}

