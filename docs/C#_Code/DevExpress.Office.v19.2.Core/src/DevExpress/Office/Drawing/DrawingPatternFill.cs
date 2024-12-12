namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class DrawingPatternFill : IDrawingFill, IOfficeNotifyPropertyChanged, IUnderlineFill
    {
        public static readonly PropertyKey PatternTypePropertyKey = new PropertyKey(0);
        public static readonly PropertyKey ForegroundColorPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey BackgroundColorPropertyKey = new PropertyKey(2);
        private readonly InvalidateProxy innerParent;
        private readonly IDocumentModel documentModel;
        private DrawingPatternType patternType;
        private DrawingColor foregroundColor;
        private DrawingColor backgroundColor;
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

        public DrawingPatternFill(IDocumentModel documentModel) : this(documentModel, DrawingPatternType.Percent5, new DrawingColor(documentModel), new DrawingColor(documentModel))
        {
        }

        private DrawingPatternFill(IDocumentModel documentModel, DrawingPatternType patternType, DrawingColor foregroundColor, DrawingColor backgroundColor)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.innerParent = new InvalidateProxy();
            this.notifier = new PropertyChangedNotifier(this);
            this.documentModel = documentModel;
            this.patternType = patternType;
            this.foregroundColor = foregroundColor;
            this.foregroundColor.Parent = this.innerParent;
            this.foregroundColor.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnForegroundColorChanged);
            this.backgroundColor = backgroundColor;
            this.backgroundColor.Parent = this.innerParent;
            this.backgroundColor.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnBackgroundColorChanged);
        }

        public IDrawingFill CloneTo(IDocumentModel documentModel)
        {
            DrawingPatternFill fill = new DrawingPatternFill(documentModel) {
                PatternType = this.PatternType
            };
            fill.ForegroundColor.CopyFrom(this.ForegroundColor);
            fill.BackgroundColor.CopyFrom(this.BackgroundColor);
            return fill;
        }

        public static DrawingPatternFill Create(IDocumentModel documentModel, Color foregroundColor, Color backgroundColor, DrawingPatternType patternType) => 
            new DrawingPatternFill(documentModel, patternType, DrawingColor.Create(documentModel, foregroundColor), DrawingColor.Create(documentModel, backgroundColor));

        IUnderlineFill IUnderlineFill.CloneTo(IDocumentModel documentModel) => 
            this.CloneTo(documentModel) as IUnderlineFill;

        public override bool Equals(object obj)
        {
            DrawingPatternFill fill = obj as DrawingPatternFill;
            if (fill == null)
            {
                return false;
            }
            if (this.FillType != fill.FillType)
            {
                return false;
            }
            DrawingPatternFill fill2 = fill;
            return ((this.PatternType == fill2.PatternType) && (this.ForegroundColor.Equals(fill2.ForegroundColor) && this.BackgroundColor.Equals(fill2.BackgroundColor)));
        }

        public override int GetHashCode() => 
            (((int) this.patternType) ^ this.foregroundColor.GetHashCode()) ^ this.backgroundColor.GetHashCode();

        private void OnBackgroundColorChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(BackgroundColorPropertyKey);
        }

        private void OnForegroundColorChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(ForegroundColorPropertyKey);
        }

        private void SetPatternType(DrawingPatternType value)
        {
            PatternTypePropertyChangedHistoryItem item = new PatternTypePropertyChangedHistoryItem(this.documentModel.MainPart, this, this.patternType, value);
            this.documentModel.History.Add(item);
            item.Execute();
        }

        protected internal void SetPatternTypeCore(DrawingPatternType value)
        {
            this.patternType = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(PatternTypePropertyKey);
        }

        public void Visit(IDrawingFillVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingPatternType PatternType
        {
            get => 
                this.patternType;
            set
            {
                if (this.PatternType != value)
                {
                    this.SetPatternType(value);
                }
            }
        }

        public DrawingColor ForegroundColor =>
            this.foregroundColor;

        public DrawingColor BackgroundColor =>
            this.backgroundColor;

        public DrawingFillType FillType =>
            DrawingFillType.Pattern;

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

