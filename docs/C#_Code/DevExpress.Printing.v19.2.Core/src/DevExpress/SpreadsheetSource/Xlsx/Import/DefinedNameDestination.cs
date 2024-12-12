namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Implementation;
    using System;
    using System.Xml;

    public class DefinedNameDestination : LeafElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private string name;
        private string scope;
        private bool isHidden;
        private string comment;

        public DefinedNameDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.name = this.Importer.ReadAttribute(reader, "name");
            if (!string.IsNullOrEmpty(this.name))
            {
                int num = this.Importer.GetIntegerValue(reader, "localSheetId", -2147483648);
                this.scope = ((num == -2147483648) || (num >= this.Importer.Source.Worksheets.Count)) ? string.Empty : this.Importer.Source.Worksheets[num].Name;
                this.isHidden = this.Importer.GetWpSTOnOffValue(reader, "hidden", false);
                this.comment = ExcelXmlCharsCodec.Decode(this.Importer.ReadAttribute(reader, "comment"));
            }
        }

        public override bool ProcessText(XmlReader reader)
        {
            if (!string.IsNullOrEmpty(this.name))
            {
                XlCellRange range = XlRangeReferenceParser.Parse(reader.Value, false);
                if (range == null)
                {
                    return true;
                }
                DefinedName item = new DefinedName(this.name, this.scope, this.isHidden, range, range.ToString());
                if (!string.IsNullOrEmpty(this.comment))
                {
                    item.Comment = this.comment;
                }
                this.Importer.Source.InnerDefinedNames.Add(item);
            }
            return true;
        }
    }
}

