namespace DevExpress.Emf
{
    using System;

    public class DXLinearGradientBrush : DXTilingBrush
    {
        private static readonly DXBlend defaultBlend;
        private ARGBColor[] linearColors;
        private DXRectangleF rectangle;
        private DXBlend blend;
        private DXColorBlend interpolationColors;

        static DXLinearGradientBrush()
        {
            double[] factors = new double[] { 1.0 };
            defaultBlend = new DXBlend(new double[1], factors);
        }

        public DXLinearGradientBrush(DXRectangleF rectangle, ARGBColor color1, ARGBColor color2) : base(DXWrapMode.Tile)
        {
            this.rectangle = rectangle;
            this.linearColors = new ARGBColor[] { color1, color2 };
            this.blend = defaultBlend;
        }

        public override void Accept(IDXBrushVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(4);
            bool shouldWriteTransform = this.ShouldWriteTransform;
            bool shouldWriteBlend = this.ShouldWriteBlend;
            bool shouldWriteColorBlend = this.ShouldWriteColorBlend;
            EmfPlusBrushData data = 0;
            if (shouldWriteTransform)
            {
                data |= EmfPlusBrushData.BrushDataTransform;
            }
            if (shouldWriteBlend)
            {
                data |= EmfPlusBrushData.BrushDataBlendFactors;
            }
            if (shouldWriteColorBlend)
            {
                data |= EmfPlusBrushData.BrushDataPresetColors;
            }
            writer.Write((int) data);
            writer.Write((int) base.WrapMode);
            writer.Write(this.rectangle);
            for (int i = 0; i < 2; i++)
            {
                writer.Write(this.linearColors[0]);
                writer.Write(this.linearColors[1]);
            }
            if (shouldWriteTransform)
            {
                writer.Write(base.Transform);
            }
            if (shouldWriteBlend)
            {
                writer.Write(this.blend);
            }
            if (shouldWriteColorBlend)
            {
                writer.Write(this.interpolationColors);
            }
        }

        public DXColorBlend InterpolationColors
        {
            get => 
                this.interpolationColors;
            set
            {
                this.blend = null;
                this.interpolationColors = value;
            }
        }

        public DXBlend Blend
        {
            get => 
                this.blend;
            set => 
                this.blend = value;
        }

        public ARGBColor[] LinearColors
        {
            get => 
                this.linearColors;
            set => 
                this.linearColors = new ARGBColor[] { value[0], value[1] };
        }

        public DXRectangleF Rectangle
        {
            get => 
                this.rectangle;
            set => 
                this.rectangle = value;
        }

        public override int DataSize
        {
            get
            {
                int num = 0x2c;
                if (this.ShouldWriteTransform)
                {
                    num += 0x18;
                }
                if (this.ShouldWriteBlend)
                {
                    num += this.blend.Size;
                }
                if (this.ShouldWriteColorBlend)
                {
                    num += 4 + (8 * this.interpolationColors.Colors.Length);
                }
                return num;
            }
        }

        private bool ShouldWriteBlend =>
            (this.blend != null) && !ReferenceEquals(this.blend, defaultBlend);

        private bool ShouldWriteTransform =>
            !base.Transform.IsIdentity;

        private bool ShouldWriteColorBlend =>
            this.interpolationColors != null;
    }
}

