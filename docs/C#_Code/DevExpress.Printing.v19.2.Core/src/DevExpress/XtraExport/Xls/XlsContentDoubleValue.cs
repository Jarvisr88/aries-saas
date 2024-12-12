namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDoubleValue : XlsContentBase
    {
        public override int GetSize() => 
            8;

        public override void Read(XlReader reader, int size)
        {
            this.Value = reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Value);
        }

        public double Value { get; set; }
    }
}

