namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandDefinedName5 : XlsSourceCommandBase
    {
        private const int fixedPartSize = 14;
        private bool isHidden;
        private bool isXlmMacro;
        private bool isVbaMacro;
        private bool isMacro;
        private bool isBuiltIn;
        private int sheetIndex;
        private int scopeSheetIndex;
        private string name = string.Empty;
        private byte[] formulaBytes;
        private int formulaSize;

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (!this.isMacro && (!this.isVbaMacro && !this.isXlmMacro))
            {
                XlCellRange range = this.GetRange(contentBuilder);
                if (range != null)
                {
                    XlsDefinedNameInfo item = new XlsDefinedNameInfo {
                        IsHidden = this.isHidden,
                        Name = XlsContentDefinedName.GetName(this.name, this.isBuiltIn),
                        SheetIndex = this.sheetIndex,
                        ScopeSheetIndex = this.scopeSheetIndex,
                        Range = range
                    };
                    contentBuilder.DefinedNameInfos.Add(item);
                }
            }
        }

        private XlCellRange GetRange(XlsSpreadsheetSource contentBuilder)
        {
            if (this.formulaSize != 0x15)
            {
                return null;
            }
            if (this.formulaBytes[0] != 0x3b)
            {
                return null;
            }
            if (BitConverter.ToUInt16(this.formulaBytes, 1) != 0xffff)
            {
                return null;
            }
            int num2 = BitConverter.ToInt16(this.formulaBytes, 11);
            int num3 = BitConverter.ToInt16(this.formulaBytes, 13);
            int row = BitConverter.ToUInt16(this.formulaBytes, 15);
            int num5 = BitConverter.ToUInt16(this.formulaBytes, 0x11);
            int column = this.formulaBytes[0x13];
            int num7 = this.formulaBytes[20];
            if (((row & 0xc000) != 0) || ((num5 & 0xc000) != 0))
            {
                return null;
            }
            if (num2 != num3)
            {
                return null;
            }
            if (num2 < 0)
            {
                return null;
            }
            this.sheetIndex = num2;
            return new XlCellRange(new XlCellPosition(column, row, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(num7, num5, XlPositionType.Absolute, XlPositionType.Absolute));
        }

        private int GetSizeWithoutFormula() => 
            14 + this.name.Length;

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            ushort num = reader.ReadUInt16();
            this.isHidden = Convert.ToBoolean((int) (num & 1));
            this.isXlmMacro = Convert.ToBoolean((int) (num & 2));
            this.isVbaMacro = Convert.ToBoolean((int) (num & 4));
            this.isMacro = Convert.ToBoolean((int) (num & 8));
            this.isBuiltIn = Convert.ToBoolean((int) (num & 0x20));
            reader.ReadByte();
            int length = reader.ReadByte();
            this.formulaSize = reader.ReadUInt16();
            reader.ReadUInt16();
            this.scopeSheetIndex = reader.ReadUInt16();
            reader.ReadUInt32();
            this.name = contentBuilder.ReadStringNoCch(reader, length);
            if (this.formulaSize > 0)
            {
                int count = base.Size - this.GetSizeWithoutFormula();
                this.formulaBytes = reader.ReadBytes(count);
            }
        }
    }
}

