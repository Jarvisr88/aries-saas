namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentNumber : XlsContentCellBase
    {
        public override int GetSize() => 
            base.GetSize() + 8;

        public override void Read(XlReader reader, int size)
        {
            base.Read(reader, size);
            this.Value = reader.ReadDouble();
            int count = size - this.GetSize();
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.Value);
        }

        public double Value { get; set; }
    }
}

