namespace DevExpress.Office.Model
{
    using System;
    using System.Drawing;

    public abstract class PatternLine<T> where T: struct
    {
        protected PatternLine()
        {
        }

        public abstract Rectangle CalcLineBounds(Rectangle r, int thickness);
        public virtual float CalcLinePenVerticalOffset(RectangleF lineBounds) => 
            lineBounds.Height / 2f;

        public abstract void Draw(IPatternLinePainter<T> painter, RectangleF bounds, Color color);

        public abstract T Id { get; }
    }
}

