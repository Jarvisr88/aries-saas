namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusPen
    {
        public EmfPlusPen(MetaReader reader)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            reader.ReadInt32();
            System.Drawing.Pen pen = new System.Drawing.Pen(Color.Blue);
            EmfPlusPenData data1 = new EmfPlusPenData(reader, pen);
            this.Pen = new System.Drawing.Pen(new EmfPlusBrush(reader).Brush);
            this.Pen.Width = pen.Width;
            this.Pen.Alignment = pen.Alignment;
            this.Pen.Transform = pen.Transform;
            this.Pen.StartCap = pen.StartCap;
            this.Pen.EndCap = pen.EndCap;
            this.Pen.LineJoin = pen.LineJoin;
            this.Pen.MiterLimit = pen.MiterLimit;
            this.Pen.DashStyle = pen.DashStyle;
            this.Pen.DashCap = pen.DashCap;
            this.Pen.DashOffset = pen.DashOffset;
            if (this.Pen.DashStyle == DashStyle.Custom)
            {
                this.Pen.DashPattern = pen.DashPattern;
            }
            this.Pen.Alignment = pen.Alignment;
            if (pen.CompoundArray.Length != 0)
            {
                this.Pen.CompoundArray = pen.CompoundArray;
            }
            if (this.Pen.StartCap == LineCap.Custom)
            {
                this.Pen.CustomStartCap = pen.CustomStartCap;
            }
            if (this.Pen.EndCap == LineCap.Custom)
            {
                this.Pen.CustomEndCap = pen.CustomEndCap;
            }
        }

        public System.Drawing.Pen Pen { get; set; }
    }
}

