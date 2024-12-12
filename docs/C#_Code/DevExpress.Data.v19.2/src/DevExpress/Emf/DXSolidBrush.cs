namespace DevExpress.Emf
{
    using System;

    public class DXSolidBrush : DXBrush
    {
        private ARGBColor color;

        public DXSolidBrush(ARGBColor color)
        {
            this.color = color;
        }

        public override void Accept(IDXBrushVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(0);
            writer.Write(this.color);
        }

        public ARGBColor Color
        {
            get => 
                this.color;
            set => 
                this.color = value;
        }

        public override int DataSize =>
            8;
    }
}

