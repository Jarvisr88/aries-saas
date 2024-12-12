namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentSupBookAddInUDF : XlsContentBase
    {
        private const short addInUDF = 0x3a01;

        public override int GetSize() => 
            4;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 1);
            writer.Write((short) 0x3a01);
        }
    }
}

