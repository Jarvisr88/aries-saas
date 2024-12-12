namespace DevExpress.Utils.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    public interface IGraphicsCache : IDisposable
    {
        Rectangle CalcClipRectangle(Rectangle r);
        Rectangle CalcRectangle(Rectangle r);
        SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth);
        SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth, int maxHeight);
        SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth, int maxHeight, out bool isCropped);
        void Clear();
        void DrawImage(Image image, Rectangle rect);
        void DrawImageUnscaled(Image image, Point point);
        void DrawLine(Point pt1, Point pt2, Color color, int thickness);
        void DrawLine(PointF pt1, PointF pt2, Color color, int thickness);
        void DrawLines(Point[] points, Color color, int thickness);
        void DrawLines(PointF[] points, Color color, int thickness);
        void DrawPath(Pen pen, GraphicsPath path);
        void DrawRectangle(Pen pen, Rectangle r);
        void DrawString(string text, Font font, Brush foreBrush, Rectangle bounds, StringFormat strFormat);
        void DrawVString(string text, Font font, Brush foreBrush, Rectangle bounds, StringFormat strFormat, int angle);
        void FillEllipse(float x, float y, float width, float height, Color color);
        void FillPath(Brush brush, GraphicsPath path);
        void FillPolygon(Point[] points, Color color);
        void FillPolygon(PointF[] points, Color color);
        void FillRectangle(Brush brush, Rectangle rect);
        void FillRectangle(Brush brush, RectangleF rect);
        void FillRectangle(Color color, Rectangle rect);
        void FillRectangle(Color color, RectangleF rect);
        Font GetFont(Font font, FontStyle fontStyle);
        Brush GetGradientBrush(Rectangle rect, Color startColor, Color endColor, LinearGradientMode mode);
        Brush GetGradientBrush(Rectangle rect, Color startColor, Color endColor, LinearGradientMode mode, int blendCount);
        Pen GetPen(Color color);
        Pen GetPen(Color color, int width);
        Brush GetSolidBrush(Color color);
        bool IsNeedDrawRect(Rectangle r);
        void ResetMatrix();
        void RestoreClip(IGraphicsClipState clipInfo);
        IGraphicsClipState SetClip(Rectangle rect);
        void TranslateTransform(float dx, float dy);

        Matrix TransformMatrix { get; }

        Point Offset { get; }

        System.Drawing.Graphics Graphics { get; }

        bool UseDirectXPaint { get; }
    }
}

