namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandDefinedName : XlsSourceCommandContentBase
    {
        private XlsContentDefinedName content = new XlsContentDefinedName();

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (!this.content.IsMacro && (!this.content.IsVbaMacro && !this.content.IsXlmMacro))
            {
                XlCellRange range = this.GetRange(contentBuilder);
                if (range != null)
                {
                    string scopeSheetName = contentBuilder.GetScopeSheetName(this.content.SheetIndex);
                    if (!string.IsNullOrEmpty(range.SheetName) || !string.IsNullOrEmpty(scopeSheetName))
                    {
                        DefinedName item = new DefinedName(this.content.Name, scopeSheetName, this.content.IsHidden, range, range.ToString(true));
                        contentBuilder.InnerDefinedNames.Add(item);
                    }
                }
            }
        }

        protected override IXlsContent GetContent()
        {
            this.content = new XlsContentDefinedName();
            return this.content;
        }

        private XlCellRange GetRange(XlsSpreadsheetSource contentBuilder)
        {
            if (this.content.FormulaSize != 11)
            {
                return null;
            }
            byte[] formulaBytes = this.content.FormulaBytes;
            if (formulaBytes[0] != 0x3b)
            {
                return null;
            }
            int num = BitConverter.ToUInt16(formulaBytes, 1);
            if (num == 0xffff)
            {
                return null;
            }
            if (num >= contentBuilder.ExternSheets.Count)
            {
                return null;
            }
            int row = BitConverter.ToUInt16(formulaBytes, 3);
            int num3 = BitConverter.ToUInt16(formulaBytes, 5);
            int column = BitConverter.ToUInt16(formulaBytes, 7);
            int num5 = BitConverter.ToUInt16(formulaBytes, 9);
            if (((column & 0xc000) != 0) || ((num5 & 0xc000) != 0))
            {
                return null;
            }
            XlsXTI sxti = contentBuilder.ExternSheets[num];
            if (sxti.SupBookIndex != contentBuilder.SelfRefBookIndex)
            {
                return null;
            }
            if (sxti.FirstSheetIndex != sxti.LastSheetIndex)
            {
                return null;
            }
            string str = string.Empty;
            if (sxti.FirstSheetIndex != -2)
            {
                if ((sxti.FirstSheetIndex < 0) || (sxti.FirstSheetIndex >= contentBuilder.SheetNames.Count))
                {
                    return null;
                }
                str = contentBuilder.SheetNames[sxti.FirstSheetIndex];
                if (contentBuilder.Worksheets[str] == null)
                {
                    return null;
                }
            }
            return new XlCellRange(new XlCellPosition(column, row, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(num5, num3, XlPositionType.Absolute, XlPositionType.Absolute)) { SheetName = str };
        }
    }
}

