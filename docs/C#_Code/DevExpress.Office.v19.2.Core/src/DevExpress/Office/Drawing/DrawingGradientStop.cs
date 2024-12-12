namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class DrawingGradientStop : ISupportsCopyFrom<DrawingGradientStop>
    {
        private readonly IDocumentModel documentModel;
        private readonly InvalidateProxy innerParent;
        private DrawingColor color;
        private int position;

        public DrawingGradientStop(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.innerParent = new InvalidateProxy();
            this.documentModel = documentModel;
            DrawingColor color1 = new DrawingColor(documentModel);
            color1.Parent = this.innerParent;
            this.color = color1;
            this.position = 0;
        }

        private void AssignProperties(System.Drawing.Color color, int position)
        {
            this.color = DrawingColor.Create(this.documentModel, color);
            this.SetPositionCore(position);
        }

        public void CopyFrom(DrawingGradientStop value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.Color.CopyFrom(value.Color);
            this.Position = value.Position;
        }

        internal static DrawingGradientStop Create(IDocumentModel documentModel, System.Drawing.Color color, int position)
        {
            DrawingGradientStop stop = new DrawingGradientStop(documentModel);
            stop.AssignProperties(color, position);
            return stop;
        }

        public override bool Equals(object obj)
        {
            DrawingGradientStop stop = obj as DrawingGradientStop;
            return ((stop != null) ? ((this.position == stop.position) && this.color.Equals(stop.color)) : false);
        }

        public override int GetHashCode() => 
            this.position ^ this.color.GetHashCode();

        private void SetPosition(int value)
        {
            GradientStopPositionPropertyChangedHistoryItem item = new GradientStopPositionPropertyChangedHistoryItem(this.documentModel.MainPart, this, this.position, value);
            this.documentModel.History.Add(item);
            item.Execute();
        }

        protected internal void SetPositionCore(int value)
        {
            this.position = value;
            this.innerParent.Invalidate();
        }

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        public DrawingColor Color =>
            this.color;

        public int Position
        {
            get => 
                this.position;
            set
            {
                ValueChecker.CheckValue(value, 0, 0x186a0, "Position");
                if (this.position != value)
                {
                    this.SetPosition(value);
                }
            }
        }
    }
}

