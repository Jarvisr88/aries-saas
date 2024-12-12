namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentRk : XlsContentCellBase
    {
        public override int GetSize() => 
            10;

        public override void Read(XlReader reader, int size)
        {
            base.RowIndex = reader.ReadUInt16();
            base.ColumnIndex = reader.ReadUInt16();
            XlsRkRec rec = XlsRkRec.Read(reader);
            base.FormatIndex = rec.FormatIndex;
            this.Value = rec.Rk.Value;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) base.RowIndex);
            writer.Write((ushort) base.ColumnIndex);
            new XlsRkRec { 
                FormatIndex = base.FormatIndex,
                Rk = { Value = this.Value }
            }.Write(writer);
        }

        public double Value { get; set; }
    }
}

