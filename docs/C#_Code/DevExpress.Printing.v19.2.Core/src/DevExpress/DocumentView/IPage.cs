namespace DevExpress.DocumentView
{
    using System;
    using System.Drawing;

    public interface IPage
    {
        RectangleF ApplyMargins(RectangleF pageRect);
        void Draw(Graphics gr, PointF position);

        int Index { get; }

        SizeF PageSize { get; }

        RectangleF UsefulPageRectF { get; }
    }
}

