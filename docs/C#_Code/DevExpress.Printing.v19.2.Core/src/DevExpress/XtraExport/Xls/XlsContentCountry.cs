namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentCountry : XlsContentBase
    {
        public override int GetSize() => 
            4;

        public override void Read(XlReader reader, int size)
        {
            this.DefaultCountryIndex = reader.ReadUInt16();
            this.CountryIndex = reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.DefaultCountryIndex);
            writer.Write((ushort) this.CountryIndex);
        }

        public int DefaultCountryIndex { get; set; }

        public int CountryIndex { get; set; }
    }
}

