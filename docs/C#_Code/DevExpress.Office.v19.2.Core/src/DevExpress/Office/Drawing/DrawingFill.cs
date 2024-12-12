namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Drawing;

    public sealed class DrawingFill : IDrawingFill, IOfficeNotifyPropertyChanged, IUnderlineFill
    {
        public static DrawingFill Automatic = new DrawingFill(DrawingFillType.Automatic);
        public static DrawingFill None = new DrawingFill(DrawingFillType.None);
        public static DrawingFill Group = new DrawingFill(DrawingFillType.Group);
        private DrawingFillType fillType;

        event EventHandler<OfficePropertyChangedEventArgs> IOfficeNotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        private DrawingFill(DrawingFillType fillType)
        {
            this.fillType = fillType;
        }

        public IDrawingFill CloneTo(IDocumentModel documentModel) => 
            (this.fillType != DrawingFillType.Automatic) ? ((this.fillType != DrawingFillType.None) ? Group : None) : Automatic;

        IUnderlineFill IUnderlineFill.CloneTo(IDocumentModel documentModel) => 
            this.CloneTo(documentModel) as IUnderlineFill;

        public override bool Equals(object obj)
        {
            DrawingFill fill = obj as DrawingFill;
            return ((fill != null) ? (this.fillType == fill.fillType) : false);
        }

        public static DrawingColor GetColor(IDocumentModel documentModel, IDrawingFill fill)
        {
            if (fill.FillType == DrawingFillType.Solid)
            {
                return ((DrawingSolidFill) fill).Color;
            }
            if (fill.FillType == DrawingFillType.Pattern)
            {
                return ((DrawingPatternFill) fill).ForegroundColor;
            }
            if (fill.FillType == DrawingFillType.Gradient)
            {
                DrawingGradientFill fill2 = (DrawingGradientFill) fill;
                if (fill2.GradientStops.Count > 0)
                {
                    return fill2.GradientStops[0].Color;
                }
            }
            return DrawingColor.Create(documentModel, DrawingColorModelInfo.CreateARGB(Color.FromArgb(0, 0xff, 0xff, 0xff)));
        }

        public override int GetHashCode() => 
            (int) this.fillType;

        public void Visit(IDrawingFillVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingFillType FillType =>
            this.fillType;

        public ISupportsInvalidate Parent
        {
            get => 
                null;
            set
            {
            }
        }

        DrawingUnderlineFillType IUnderlineFill.Type =>
            DrawingUnderlineFillType.Fill;
    }
}

