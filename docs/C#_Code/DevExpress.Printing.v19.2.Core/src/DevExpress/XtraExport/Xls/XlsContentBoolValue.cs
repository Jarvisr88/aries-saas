namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentBoolValue : XlsContentBase
    {
        public override int GetSize() => 
            2;

        public override void Read(XlReader reader, int size)
        {
            this.Value = reader.ReadInt16() != 0;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Value ? ((short) 1) : ((short) 0));
        }

        public bool Value { get; set; }
    }
}

