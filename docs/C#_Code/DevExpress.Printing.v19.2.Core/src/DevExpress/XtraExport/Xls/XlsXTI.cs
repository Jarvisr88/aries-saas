namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsXTI
    {
        public override bool Equals(object obj)
        {
            XlsXTI sxti = obj as XlsXTI;
            return ((sxti != null) ? ((this.SupBookIndex == sxti.SupBookIndex) && ((this.FirstSheetIndex == sxti.FirstSheetIndex) && (this.LastSheetIndex == sxti.LastSheetIndex))) : false);
        }

        public static XlsXTI FromStream(BinaryReader reader)
        {
            XlsXTI sxti = new XlsXTI();
            sxti.Read(reader);
            return sxti;
        }

        public override int GetHashCode() => 
            (this.SupBookIndex ^ this.FirstSheetIndex) ^ this.LastSheetIndex;

        protected void Read(BinaryReader reader)
        {
            this.SupBookIndex = reader.ReadUInt16();
            this.FirstSheetIndex = reader.ReadInt16();
            this.LastSheetIndex = reader.ReadInt16();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.SupBookIndex);
            writer.Write((short) this.FirstSheetIndex);
            writer.Write((short) this.LastSheetIndex);
        }

        public int SupBookIndex { get; set; }

        public int FirstSheetIndex { get; set; }

        public int LastSheetIndex { get; set; }
    }
}

