namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusCustomLineCapData
    {
        public EmfPlusCustomLineCapData(MetaReader reader)
        {
            CustomLineCapDataFlags flags = (CustomLineCapDataFlags) reader.ReadInt32();
            this.BaseCap = (LineCap) reader.ReadInt32();
            this.BaseInset = reader.ReadSingle();
            this.StrokeStartCap = (LineCap) reader.ReadInt32();
            this.StrokeEndCap = (LineCap) reader.ReadInt32();
            this.StrokeJoin = (LineJoin) reader.ReadInt32();
            this.StrokeMiterLimit = reader.ReadSingle();
            this.WidthScale = reader.ReadSingle();
            PointF tf = reader.ReadPointF();
            PointF tf2 = reader.ReadPointF();
            if (flags.HasFlag(CustomLineCapDataFlags.FillPath))
            {
                reader.ReadInt32();
                this.FillPath = new EmfPlusPath(reader);
            }
            if (flags.HasFlag(CustomLineCapDataFlags.LinePath))
            {
                reader.ReadInt32();
                this.LinePath = new EmfPlusPath(reader);
            }
        }

        private LineCap BaseCap { get; set; }

        private float BaseInset { get; set; }

        private LineCap StrokeStartCap { get; set; }

        private LineCap StrokeEndCap { get; set; }

        private LineJoin StrokeJoin { get; set; }

        private float StrokeMiterLimit { get; set; }

        private float WidthScale { get; set; }

        private PointF FillHotSpot { get; set; }

        private PointF StrokeHotSpot { get; set; }

        private EmfPlusPath FillPath { get; set; }

        private EmfPlusPath LinePath { get; set; }
    }
}

