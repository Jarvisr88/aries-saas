namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentSupBookSelf : XlsContentBase
    {
        private const short selfRef = 0x401;

        public override int GetSize() => 
            4;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.SheetCount);
            writer.Write((short) 0x401);
        }

        public int SheetCount { get; set; }
    }
}

