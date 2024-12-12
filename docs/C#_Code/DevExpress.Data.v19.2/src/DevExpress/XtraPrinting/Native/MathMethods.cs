namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class MathMethods
    {
        public static double Scale(double val, double ratio) => 
            val * ratio;

        public static PointF Scale(PointF val, double ratio) => 
            new PointF((float) (ratio * val.X), (float) (ratio * val.Y));

        public static PointF Scale(PointF val, float ratio) => 
            Scale(val, (double) ratio);

        public static RectangleF Scale(RectangleF val, double ratio) => 
            new RectangleF((float) (ratio * val.X), (float) (ratio * val.Y), (float) (ratio * val.Width), (float) (ratio * val.Height));

        public static RectangleF Scale(RectangleF val, float ratio) => 
            Scale(val, (double) ratio);

        public static SizeF Scale(SizeF val, double ratio) => 
            new SizeF((float) (ratio * val.Width), (float) (ratio * val.Height));

        public static SizeF Scale(SizeF val, float ratio) => 
            Scale(val, (double) ratio);

        public static float Scale(float val, double ratio) => 
            (float) Scale((double) val, ratio);

        public static SizeF ZoomInto(SizeF outer, SizeF inner)
        {
            SizeF ef = new SizeF();
            float num = inner.Width / inner.Height;
            if (num < (outer.Width / outer.Height))
            {
                ef.Height = outer.Height;
                ef.Width = outer.Height * num;
            }
            else
            {
                ef.Width = outer.Width;
                ef.Height = outer.Width / num;
            }
            return ef;
        }
    }
}

