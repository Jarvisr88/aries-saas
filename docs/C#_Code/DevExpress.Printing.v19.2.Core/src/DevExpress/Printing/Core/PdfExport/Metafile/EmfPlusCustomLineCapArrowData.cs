namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusCustomLineCapArrowData
    {
        public EmfPlusCustomLineCapArrowData(MetaReader reader)
        {
            this.Width = reader.ReadSingle();
            this.Height = reader.ReadSingle();
            this.MiddleInset = reader.ReadSingle();
            this.FillState = reader.ReadInt32() != 0;
            this.LineStartCap = (LineCap) reader.ReadInt32();
            this.LineEndCap = (LineCap) reader.ReadInt32();
            this.LineJoin = (System.Drawing.Drawing2D.LineJoin) reader.ReadInt32();
            this.LineMiterLimit = reader.ReadSingle();
            this.WidthScale = reader.ReadSingle();
            this.FillHotSpot = reader.ReadPointF();
            this.LineHotSpot = reader.ReadPointF();
        }

        public float Width { get; set; }

        public float Height { get; set; }

        public float MiddleInset { get; set; }

        public bool FillState { get; set; }

        public LineCap LineStartCap { get; set; }

        public LineCap LineEndCap { get; set; }

        public System.Drawing.Drawing2D.LineJoin LineJoin { get; set; }

        public float LineMiterLimit { get; set; }

        public float WidthScale { get; set; }

        public PointF FillHotSpot { get; set; }

        public PointF LineHotSpot { get; set; }
    }
}

