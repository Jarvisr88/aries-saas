namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class HyperlinkString : XLUnicodeCharactersArray
    {
        public static HyperlinkString FromStream(XlReader reader)
        {
            HyperlinkString str = new HyperlinkString();
            str.Read(reader);
            return str;
        }

        protected void Read(XlReader reader)
        {
            int num = reader.ReadInt32();
            if (num > 1)
            {
                base.Read(reader, num - 1);
            }
            if (num > 0)
            {
                reader.ReadUInt16();
            }
        }

        public override void Write(BinaryWriter writer)
        {
            int num = base.Value.Length + 1;
            writer.Write(num);
            base.Write(writer);
            writer.Write((ushort) 0);
        }

        public override int Length =>
            6 + base.Length;
    }
}

