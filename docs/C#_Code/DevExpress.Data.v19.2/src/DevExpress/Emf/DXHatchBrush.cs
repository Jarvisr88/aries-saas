namespace DevExpress.Emf
{
    using System;

    public class DXHatchBrush : DXBrush
    {
        private DXHatchStyle hatchStyle;
        private ARGBColor foregroundColor;
        private ARGBColor backgroundColor;

        public DXHatchBrush(DXHatchStyle hatchStyle, ARGBColor foregroundColor, ARGBColor backgroundColor)
        {
            this.hatchStyle = hatchStyle;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
        }

        public override void Accept(IDXBrushVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(1);
            writer.Write((int) this.hatchStyle);
            writer.Write(this.foregroundColor);
            writer.Write(this.backgroundColor);
        }

        public DXHatchStyle HatchStyle =>
            this.hatchStyle;

        public ARGBColor ForegroundColor =>
            this.foregroundColor;

        public ARGBColor BackgroundColor =>
            this.backgroundColor;

        public override int DataSize =>
            0x10;
    }
}

