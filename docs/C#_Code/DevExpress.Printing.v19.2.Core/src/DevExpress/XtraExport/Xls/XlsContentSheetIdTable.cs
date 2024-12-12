namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XlsContentSheetIdTable : XlsContentBase
    {
        private List<int> sheetIdTable = new List<int>();

        public override int GetSize() => 
            this.sheetIdTable.Count * 2;

        public override void Read(XlReader reader, int size)
        {
            this.sheetIdTable.Clear();
            int num = size / 2;
            for (int i = 0; i < num; i++)
            {
                this.sheetIdTable.Add(reader.ReadInt16());
            }
        }

        public override void Write(BinaryWriter writer)
        {
            int count = this.sheetIdTable.Count;
            for (int i = 0; i < count; i++)
            {
                writer.Write((short) this.sheetIdTable[i]);
            }
        }

        public List<int> SheetIdTable =>
            this.sheetIdTable;
    }
}

