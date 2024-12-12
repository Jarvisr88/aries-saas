namespace DevExpress.Emf
{
    using System;

    public class DXPathGradientBrush : DXTilingBrush
    {
        private static readonly DXBlend defaultBlend;
        private readonly DXGraphicsPathData path;
        private ARGBColor centerColor;
        private DXPointF? centerPoint;
        private ARGBColor[] surroundColors;
        private DXBlend blend;
        private DXColorBlend colorBlend;
        private DXPointF focusScales;

        static DXPathGradientBrush()
        {
            double[] positions = new double[2];
            positions[1] = 1.0;
            double[] factors = new double[2];
            factors[1] = 1.0;
            defaultBlend = new DXBlend(positions, factors);
        }

        public DXPathGradientBrush(DXGraphicsPathData path) : base(DXWrapMode.Clamp)
        {
            this.path = path;
            this.centerColor = ARGBColor.FromArgb(0xff, 0xff, 0xff);
            this.surroundColors = new ARGBColor[] { ARGBColor.FromArgb(0xff, 0xff, 0xff) };
            this.blend = defaultBlend;
        }

        public DXPathGradientBrush(DXPathGradientBrush brush) : base(brush.WrapMode, brush.Transform)
        {
            this.centerColor = brush.centerColor;
            this.centerPoint = brush.centerPoint;
            this.path = brush.path;
            this.surroundColors = brush.surroundColors;
            this.blend = brush.blend;
            this.colorBlend = brush.colorBlend;
            this.FocusScales = brush.FocusScales;
        }

        public override void Accept(IDXBrushVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(3);
            bool shouldWriteBlend = this.ShouldWriteBlend;
            EmfPlusBrushData brushDataPath = EmfPlusBrushData.BrushDataPath;
            if (shouldWriteBlend)
            {
                brushDataPath |= EmfPlusBrushData.BrushDataBlendFactors;
            }
            writer.Write((int) brushDataPath);
            writer.Write((int) base.WrapMode);
            writer.Write(this.centerColor);
            writer.Write(this.centerPoint.Value);
            writer.Write(this.surroundColors.Length);
            foreach (ARGBColor color in this.surroundColors)
            {
                writer.Write(color);
            }
            EmfPlusPath path = new EmfPlusPath(this.path);
            writer.Write(path.Size);
            path.Write(writer);
            if (shouldWriteBlend)
            {
                writer.Write(this.blend);
            }
        }

        public DXBlend Blend
        {
            get => 
                this.blend;
            set => 
                this.blend = value;
        }

        public DXColorBlend InterpolationColors
        {
            get => 
                this.colorBlend;
            set => 
                this.colorBlend = value;
        }

        public DXGraphicsPathData Path =>
            this.path;

        public DXPointF FocusScales
        {
            get => 
                this.focusScales;
            set => 
                this.focusScales = value;
        }

        public ARGBColor CenterColor
        {
            get => 
                this.centerColor;
            set => 
                this.centerColor = value;
        }

        public DXPointF? CenterPoint
        {
            get => 
                this.centerPoint;
            set => 
                this.centerPoint = value;
        }

        public ARGBColor[] SurroundColors
        {
            get => 
                this.surroundColors;
            set => 
                this.surroundColors = value;
        }

        public override int DataSize =>
            ((0x20 + (4 * this.surroundColors.Length)) + new EmfPlusPath(this.path).Size) + (this.ShouldWriteBlend ? this.blend.Size : 0);

        private bool ShouldWriteBlend =>
            (this.blend != null) && !ReferenceEquals(this.blend, defaultBlend);
    }
}

