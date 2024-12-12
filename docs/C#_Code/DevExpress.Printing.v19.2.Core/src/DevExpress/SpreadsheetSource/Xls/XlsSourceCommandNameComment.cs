namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandNameComment : XlsSourceCommandBase
    {
        private XLUnicodeStringNoCch name;
        private XLUnicodeStringNoCch comment;

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (contentBuilder.DefinedNames.Count != 0)
            {
                DefinedName name = contentBuilder.DefinedNames[contentBuilder.DefinedNames.Count - 1] as DefinedName;
                if ((name != null) && this.IsSameName(name.Name, this.name.Value))
                {
                    name.Comment = this.comment.Value;
                }
            }
        }

        private bool IsSameName(string name1, string name2)
        {
            if (string.IsNullOrEmpty(name1))
            {
                name1 = string.Empty;
            }
            if (name1.StartsWith("_xlnm."))
            {
                name1 = name1.Remove(0, 6);
            }
            return (name1 == name2);
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            FutureRecordHeader.FromStream(reader);
            int charCount = reader.ReadUInt16();
            int num2 = reader.ReadUInt16();
            this.name = XLUnicodeStringNoCch.FromStream(reader, charCount);
            this.comment = XLUnicodeStringNoCch.FromStream(reader, num2);
        }
    }
}

