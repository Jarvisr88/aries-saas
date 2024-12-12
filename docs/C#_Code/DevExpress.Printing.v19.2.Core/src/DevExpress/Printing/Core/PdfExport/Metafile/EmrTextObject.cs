namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [CLSCompliant(false)]
    public class EmrTextObject
    {
        public EmrTextObject(MetaReader reader)
        {
            this.Reference = new PointF(reader.ReadSingle(), reader.ReadSingle());
            this.CharsCount = reader.ReadUInt32();
            this.OffString = reader.ReadUInt32();
            this.Options = reader.ReadUInt32();
            this.Rectangle = reader.ReadRectF();
            this.OffDx = reader.ReadUInt32();
            this.Text = MetaImage.GetStringData(reader, (int) this.CharsCount);
        }

        public PointF Reference { get; set; }

        public uint CharsCount { get; set; }

        public uint OffString { get; set; }

        public uint Options { get; set; }

        public RectangleF Rectangle { get; set; }

        public uint OffDx { get; set; }

        public int UndefinedSpace1 { get; set; }

        public byte[] OutputString { get; set; }

        public int UndefinedSpace2 { get; set; }

        public uint[] OutputDx { get; set; }

        public string Text { get; set; }
    }
}

