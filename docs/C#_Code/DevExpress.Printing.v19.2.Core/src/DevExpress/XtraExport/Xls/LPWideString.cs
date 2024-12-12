namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class LPWideString : XLUnicodeCharactersArray
    {
        public static LPWideString FromStream(XlReader reader)
        {
            LPWideString str = new LPWideString();
            str.Read(reader);
            return str;
        }

        protected void Read(XlReader reader)
        {
            int charCount = reader.ReadUInt16();
            base.Read(reader, charCount);
        }

        public override void Write(BinaryWriter writer)
        {
            int length = base.Value.Length;
            writer.Write((ushort) length);
            base.Write(writer);
        }

        public override int Length =>
            2 + base.Length;
    }
}

