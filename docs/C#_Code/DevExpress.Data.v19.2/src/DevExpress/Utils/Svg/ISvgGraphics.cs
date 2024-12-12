namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    public interface ISvgGraphics : IDisposable
    {
        void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
        void DrawPath(Pen pen, GraphicsPath path);
        void FillPath(Brush brush, GraphicsPath path);
        SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat);
        void Restore(object graphicsState);
        object Save();
        void ScaleTransform(float sx, float sy, MatrixOrder order = 0);
        void SetClip(GraphicsPath path, CombineMode combineMode = 0);
        void TranslateTransform(float dx, float dy, MatrixOrder order = 0);

        System.Drawing.Drawing2D.SmoothingMode SmoothingMode { get; set; }

        Matrix Transform { get; set; }
    }
}

