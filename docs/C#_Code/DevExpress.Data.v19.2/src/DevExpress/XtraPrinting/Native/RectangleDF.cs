namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleDF
    {
        public static readonly RectangleDF Empty;
        private double x;
        private double y;
        private float width;
        private float height;
        public double X
        {
            get => 
                this.x;
            set => 
                this.x = value;
        }
        public double Y
        {
            get => 
                this.y;
            set => 
                this.y = value;
        }
        public double Left =>
            this.x;
        public double Top =>
            this.y;
        public double Right =>
            this.x + this.width;
        public double Bottom =>
            this.y + this.height;
        public float Width
        {
            get => 
                this.width;
            set => 
                this.width = value;
        }
        public float Height
        {
            get => 
                this.height;
            set => 
                this.height = value;
        }
        public bool IsEmpty =>
            (this.Width <= 0.0) || (this.Height == 0.0);
        public RectangleDF(double x, double y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public static RectangleDF Offset(RectangleF val, double dx, double dy)
        {
            RectangleDF edf = FromRectangleF(val);
            edf.Offset(dx, dy);
            return edf;
        }

        public static RectangleDF Offset(RectangleDF val, double dx, double dy)
        {
            val.Offset(dx, dy);
            return val;
        }

        public void Offset(double dx, double dy)
        {
            this.x += dx;
            this.y += dy;
        }

        public static RectangleDF FromLTRB(double left, double top, double right, double bottom) => 
            new RectangleDF(left, top, (float) (right - left), (float) (bottom - top));

        public static RectangleDF FromRectangleF(RectangleF val) => 
            new RectangleDF { 
                x = val.X,
                y = val.Y,
                width = val.Width,
                height = val.Height
            };

        public RectangleF ToRectangleF() => 
            new RectangleF { 
                X = (float) this.X,
                Y = (float) this.Y,
                Width = this.Width,
                Height = this.Height
            };

        public Rectangle ToRectangle() => 
            new Rectangle((int) Math.Round(this.X), (int) Math.Round(this.Y), (int) Math.Round((double) this.Width), (int) Math.Round((double) this.Height));

        public void Intersect(RectangleDF rect)
        {
            RectangleDF edf = Intersect(rect, this);
            this.X = edf.X;
            this.Y = edf.Y;
            this.Width = edf.Width;
            this.Height = edf.Height;
        }

        public static RectangleDF Intersect(RectangleDF a, RectangleDF b)
        {
            double x = Math.Max(a.X, b.X);
            double num2 = Math.Min((double) (a.X + a.Width), (double) (b.X + b.Width));
            double y = Math.Max(a.Y, b.Y);
            double num4 = Math.Min((double) (a.Y + a.Height), (double) (b.Y + b.Height));
            return (((num2 < x) || (num4 < y)) ? Empty : new RectangleDF(x, y, (float) (num2 - x), (float) (num4 - y)));
        }

        public static implicit operator RectangleDF(Rectangle r) => 
            new RectangleDF((double) r.X, (double) r.Y, (float) r.Width, (float) r.Height);

        public override string ToString()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "{X=";
            textArray1[1] = this.X.ToString(CultureInfo.CurrentCulture);
            textArray1[2] = ",Y=";
            textArray1[3] = this.Y.ToString(CultureInfo.CurrentCulture);
            textArray1[4] = ",Width=";
            textArray1[5] = this.Width.ToString(CultureInfo.CurrentCulture);
            textArray1[6] = ",Height=";
            textArray1[7] = this.Height.ToString(CultureInfo.CurrentCulture);
            textArray1[8] = "}";
            return string.Concat(textArray1);
        }

        static RectangleDF()
        {
        }
    }
}

