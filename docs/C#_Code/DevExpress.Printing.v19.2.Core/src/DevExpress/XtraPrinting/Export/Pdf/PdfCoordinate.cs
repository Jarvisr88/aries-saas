namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal static class PdfCoordinate
    {
        public static PointF BackTransformValue(GraphicsUnit pageUnit, PointF value) => 
            GraphicsUnitConverter.Convert(value, (float) 72f, GraphicsDpi.UnitToDpiI(pageUnit));

        public static SizeF BackTransformValue(GraphicsUnit pageUnit, SizeF value) => 
            GraphicsUnitConverter.Convert(value, (float) 72f, GraphicsDpi.UnitToDpiI(pageUnit));

        public static float BackTransformValue(GraphicsUnit pageUnit, float value) => 
            GraphicsUnitConverter.Convert(value, (float) 72f, GraphicsDpi.UnitToDpiI(pageUnit));

        public static PointF CorrectPoint(GraphicsUnit pageUnit, PointF pt, SizeF pageSize)
        {
            pt = TransformValue(pageUnit, pt);
            return new PointF(pt.X, pageSize.Height - pt.Y);
        }

        public static RectangleF CorrectRectangle(GraphicsUnit pageUnit, RectangleF rect)
        {
            rect.Location = TransformValue(pageUnit, rect.Location);
            rect.Size = TransformValue(pageUnit, rect.Size);
            return rect;
        }

        public static RectangleF CorrectRectangle(GraphicsUnit pageUnit, RectangleF rect, SizeF pageSize)
        {
            RectangleF ef = CorrectRectangle(pageUnit, rect);
            ef.Y = pageSize.Height - ef.Bottom;
            return ef;
        }

        public static PointF TransformValue(GraphicsUnit pageUnit, PointF value) => 
            GraphicsUnitConverter.Convert(value, GraphicsDpi.UnitToDpiI(pageUnit), (float) 72f);

        public static SizeF TransformValue(GraphicsUnit pageUnit, SizeF value) => 
            GraphicsUnitConverter.Convert(value, GraphicsDpi.UnitToDpiI(pageUnit), (float) 72f);

        public static float TransformValue(GraphicsUnit pageUnit, float value) => 
            GraphicsUnitConverter.Convert(value, GraphicsDpi.UnitToDpiI(pageUnit), (float) 72f);
    }
}

