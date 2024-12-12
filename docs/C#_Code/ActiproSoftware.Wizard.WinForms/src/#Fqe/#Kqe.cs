namespace #Fqe
{
    using System;
    using System.Drawing;

    internal class #Kqe
    {
        internal static Point #nwe(Point #Vue, double #fre, Point #RGf)
        {
            PointF tf = #nwe(new PointF((float) #Vue.X, (float) #Vue.Y), #fre, new PointF((float) #RGf.X, (float) #RGf.Y));
            return new Point((int) Math.Round((double) tf.X), (int) Math.Round((double) tf.Y));
        }

        internal static PointF #nwe(PointF #Vue, double #fre, PointF #RGf)
        {
            double local1 = (#fre * 3.1415926535897931) / 180.0;
            double d = (#fre * 3.1415926535897931) / 180.0;
            double num = Math.Cos(d);
            double num2 = Math.Sin(d);
            double num3 = #Vue.X - #RGf.X;
            double num4 = #Vue.Y - #RGf.Y;
            return new PointF((float) ((#RGf.X + (num3 * num)) - (num4 * num2)), (float) ((#RGf.Y + (num4 * num)) + (num3 * num2)));
        }

        internal static Point[] #owe(Rectangle #Fi, double #fre, Point #RGf)
        {
            PointF[] tfArray = #owe(new RectangleF((float) #Fi.Left, (float) #Fi.Top, (float) #Fi.Width, (float) #Fi.Height), #fre, new PointF((float) #RGf.X, (float) #RGf.Y));
            return new Point[] { new Point((int) Math.Round((double) tfArray[0].X), (int) Math.Round((double) tfArray[0].Y)), new Point((int) Math.Round((double) tfArray[1].X), (int) Math.Round((double) tfArray[1].Y)), new Point((int) Math.Round((double) tfArray[2].X), (int) Math.Round((double) tfArray[2].Y)), new Point((int) Math.Round((double) tfArray[3].X), (int) Math.Round((double) tfArray[3].Y)) };
        }

        internal static PointF[] #owe(RectangleF #Fi, double #fre, PointF #RGf)
        {
            PointF[] tfArray1 = new PointF[4];
            PointF[] tfArray2 = new PointF[4];
            tfArray2[0] = #nwe(new PointF(#Fi.Left, #Fi.Top), #fre, #RGf);
            PointF[] local1 = tfArray2;
            local1[1] = #nwe(new PointF(#Fi.Right - 1f, #Fi.Top), #fre, #RGf);
            local1[2] = #nwe(new PointF(#Fi.Right - 1f, #Fi.Bottom - 1f), #fre, #RGf);
            local1[3] = #nwe(new PointF(#Fi.Left, #Fi.Bottom - 1f), #fre, #RGf);
            return local1;
        }

        internal #Kqe()
        {
        }

        internal static void DrawRectangle(Graphics #nYf, Pen #iwf, Point[] #5Zf)
        {
            if ((#5Zf != null) && (#5Zf.Length >= 4))
            {
                #nYf.DrawLine(#iwf, #5Zf[0], #5Zf[1]);
                #nYf.DrawLine(#iwf, #5Zf[1], #5Zf[2]);
                #nYf.DrawLine(#iwf, #5Zf[2], #5Zf[3]);
                #nYf.DrawLine(#iwf, #5Zf[3], #5Zf[0]);
            }
        }

        internal static void DrawRectangle(Graphics #nYf, Pen #iwf, PointF[] #5Zf)
        {
            if ((#5Zf != null) && (#5Zf.Length >= 4))
            {
                #nYf.DrawLine(#iwf, #5Zf[0], #5Zf[1]);
                #nYf.DrawLine(#iwf, #5Zf[1], #5Zf[2]);
                #nYf.DrawLine(#iwf, #5Zf[2], #5Zf[3]);
                #nYf.DrawLine(#iwf, #5Zf[3], #5Zf[0]);
            }
        }
    }
}

