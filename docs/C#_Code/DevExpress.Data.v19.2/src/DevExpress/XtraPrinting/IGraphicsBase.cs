namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public interface IGraphicsBase
    {
        void ApplyTransformState(MatrixOrder order, bool removeState);
        void DrawCheckBox(RectangleF rect, CheckState state);
        void DrawEllipse(Pen pen, RectangleF rect);
        void DrawEllipse(Pen pen, float x, float y, float width, float height);
        void DrawImage(Image image, Point point);
        void DrawImage(Image image, RectangleF rect);
        void DrawImage(Image image, RectangleF rect, Color underlyingColor);
        void DrawLine(Pen pen, PointF pt1, PointF pt2);
        void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
        void DrawLines(Pen pen, PointF[] points);
        void DrawPath(Pen pen, GraphicsPath path);
        void DrawRectangle(Pen pen, RectangleF bounds);
        void DrawString(string s, Font font, Brush brush, PointF point);
        void DrawString(string s, Font font, Brush brush, RectangleF bounds);
        void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format);
        void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format);
        void FillEllipse(Brush brush, RectangleF rect);
        void FillEllipse(Brush brush, float x, float y, float width, float height);
        void FillPath(Brush brush, GraphicsPath path);
        void FillRectangle(Brush brush, RectangleF bounds);
        void FillRectangle(Brush brush, float x, float y, float width, float height);
        Brush GetBrush(Color color);
        void IntersectClip(GraphicsPath path);
        SizeF MeasureString(string text, Font font, GraphicsUnit graphicsUnit);
        SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit graphicsUnit);
        SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit graphicsUnit);
        SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit graphicsUnit);
        void ResetTransform();
        void Restore(IGraphicsState gstate);
        void RotateTransform(float angle);
        void RotateTransform(float angle, MatrixOrder order);
        IGraphicsState Save();
        void SaveTransformState();
        void ScaleTransform(float sx, float sy);
        void ScaleTransform(float sx, float sy, MatrixOrder order);
        void TranslateTransform(float dx, float dy);
        void TranslateTransform(float dx, float dy, MatrixOrder order);

        RectangleF ClipBounds { get; set; }

        GraphicsUnit PageUnit { get; set; }

        System.Drawing.Drawing2D.SmoothingMode SmoothingMode { get; set; }

        Matrix Transform { get; set; }
    }
}

