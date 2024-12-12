namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentLabelSst : XlsContentCellBase
    {
        public override int GetSize() => 
            base.GetSize() + 4;

        public override void Read(XlReader reader, int size)
        {
            base.Read(reader, size);
            this.StringIndex = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.StringIndex);
        }

        public int StringIndex { get; set; }
    }
}

