namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Drawing;

    public class DrawingSolidFill : IDrawingFill, IOfficeNotifyPropertyChanged, IUnderlineFill
    {
        public static readonly PropertyKey ColorPropertyKey = new PropertyKey(0);
        private readonly InvalidateProxy innerParent;
        private DrawingColor color;
        private readonly PropertyChangedNotifier notifier;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        private DrawingSolidFill(DrawingColor drawingColor)
        {
            this.innerParent = new InvalidateProxy();
            this.notifier = new PropertyChangedNotifier(this);
            this.color = drawingColor;
            this.color.Parent = this.innerParent;
            this.color.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnColorPropertyChanged);
        }

        public DrawingSolidFill(IDocumentModel documentModel) : this(new DrawingColor(documentModel))
        {
        }

        public IDrawingFill CloneTo(IDocumentModel documentModel)
        {
            DrawingSolidFill fill = new DrawingSolidFill(documentModel);
            fill.Color.CopyFrom(this.Color);
            return fill;
        }

        public static DrawingSolidFill Create(IDocumentModel documentModel, System.Drawing.Color color) => 
            new DrawingSolidFill(DrawingColor.Create(documentModel, color));

        IUnderlineFill IUnderlineFill.CloneTo(IDocumentModel documentModel) => 
            this.CloneTo(documentModel) as IUnderlineFill;

        public override bool Equals(object obj)
        {
            DrawingSolidFill fill = obj as DrawingSolidFill;
            return ((fill != null) ? ((this.FillType == fill.FillType) ? this.Color.Equals(fill.Color) : false) : false);
        }

        public override int GetHashCode() => 
            this.color.GetHashCode();

        private void OnColorPropertyChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(ColorPropertyKey);
        }

        public void Visit(IDrawingFillVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor Color =>
            this.color;

        public DrawingFillType FillType =>
            DrawingFillType.Solid;

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        DrawingUnderlineFillType IUnderlineFill.Type =>
            DrawingUnderlineFillType.Fill;
    }
}

