namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentBoundSheet8 : XlsContentBase
    {
        private ShortXLUnicodeString name = new ShortXLUnicodeString();

        public override int GetSize() => 
            6 + this.name.Length;

        public override void Read(XlReader reader, int size)
        {
            reader.ReadBytes(size);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.StartPosition);
            writer.Write((ushort) this.VisibleState);
            this.name.Write(writer);
        }

        public int StartPosition { get; set; }

        public XlSheetVisibleState VisibleState { get; set; }

        public string Name
        {
            get => 
                this.name.Value;
            set => 
                this.name.Value = value;
        }
    }
}

