namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public static class MathHelper
    {
        public const double Epsilon = 1E-10;

        public static bool AreDoubleClose(double d1, double d2) => 
            AreDoubleClose(d1, d2, 1E-10);

        public static bool AreDoubleClose(double d1, double d2, double precesion) => 
            (d1 != d2) ? IsZero(d1 - d2, precesion) : true;

        public static bool AreEqual(double d1, double d2) => 
            (d1 == d2) || (double.IsNaN(d1) && double.IsNaN(d2));

        public static bool AreEqual(Size s1, Size s2) => 
            AreEqual(s1.Width, s2.Width) && AreEqual(s1.Height, s2.Height);

        public static bool AreEqual(Thickness t1, Thickness t2) => 
            AreEqual(t1.Left, t2.Left) && (AreEqual(t1.Top, t2.Top) && (AreEqual(t1.Right, t2.Right) && AreEqual(t1.Bottom, t2.Bottom)));

        internal static double BottomDimension(double min, double dim) => 
            !double.IsNaN(dim) ? ((min > dim) ? min : dim) : min;

        public static double CalcRealDimension(double value)
        {
            double d = NormalizeConstraint(value);
            return (double.IsNaN(d) ? 0.0 : d);
        }

        public static double CalcRealMaxConstraint(double value) => 
            NormalizeConstraint(value);

        public static double CalcRealMinConstraint(double value)
        {
            double d = NormalizeConstraint(value);
            return (double.IsNaN(d) ? 0.0 : d);
        }

        public static double CenterRange(double targetStart, double targetRange, double range) => 
            targetStart + ((targetRange - range) * 0.5);

        public static Rect Edge(Rect source, Rect target, bool horz) => 
            !horz ? ((target.Top <= source.Top) ? new Rect(source.Left, target.Bottom, source.Width, Math.Max((double) 0.0, (double) (source.Bottom - target.Bottom))) : new Rect(source.Left, source.Top, source.Width, Math.Max((double) 0.0, (double) (target.Top - source.Top)))) : ((target.Left <= source.Left) ? new Rect(target.Right, source.Top, Math.Max((double) 0.0, (double) (source.Right - target.Right)), source.Height) : new Rect(source.Left, source.Top, Math.Max((double) 0.0, (double) (target.Left - source.Left)), source.Height));

        public static bool IsConstraintValid(double value) => 
            !double.IsNaN(value) && (!double.IsInfinity(value) && IsDimensionValid(value));

        public static bool IsDimensionValid(double value) => 
            value > double.Epsilon;

        public static bool IsEmpty(double value) => 
            double.IsNaN(value) || IsZero(value);

        public static bool IsEmpty(Point value) => 
            IsEmpty(value.X) || IsEmpty(value.Y);

        public static bool IsEmpty(Size value) => 
            IsEmpty(value.Width) || IsEmpty(value.Height);

        public static bool IsZero(double value) => 
            IsZero(value, 1E-10);

        public static bool IsZero(double value, double precesion) => 
            (value < precesion) && (value > -precesion);

        public static double MeasureDimension(double min, double max, double dim)
        {
            if (IsConstraintValid(max))
            {
                dim = TopDimension(max, dim);
            }
            if (IsConstraintValid(min))
            {
                dim = Math.Max(min, dim);
            }
            return dim;
        }

        public static Size MeasureMaxSize(Size[] maxSizes)
        {
            double maxValue = double.MaxValue;
            double max = double.MaxValue;
            for (int i = 0; i < maxSizes.Length; i++)
            {
                maxValue = TopDimension(maxValue, CalcRealMaxConstraint(maxSizes[i].Width));
                max = TopDimension(max, CalcRealMaxConstraint(maxSizes[i].Height));
            }
            if (maxValue == double.MaxValue)
            {
                maxValue = double.NaN;
            }
            if (max == double.MaxValue)
            {
                max = double.NaN;
            }
            return new Size(maxValue, max);
        }

        public static Size MeasureMaxSize(Size[] maxSizes, bool fHorz)
        {
            double naN = fHorz ? 0.0 : double.MaxValue;
            double naN = fHorz ? double.MaxValue : 0.0;
            for (int i = 0; i < maxSizes.Length; i++)
            {
                if (fHorz)
                {
                    naN += CalcRealMaxConstraint(maxSizes[i].Width);
                    naN = Math.Min(naN, CalcRealMaxConstraint(maxSizes[i].Height));
                }
                else
                {
                    naN = Math.Min(naN, CalcRealMaxConstraint(maxSizes[i].Width));
                    naN += CalcRealMaxConstraint(maxSizes[i].Height);
                }
            }
            if (naN == double.MaxValue)
            {
                naN = double.NaN;
            }
            if (naN == double.MaxValue)
            {
                naN = double.NaN;
            }
            return new Size(naN, naN);
        }

        public static Size MeasureMinSize(Size[] minSizes)
        {
            double num = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < minSizes.Length; i++)
            {
                num = Math.Max(num, CalcRealMinConstraint(minSizes[i].Width));
                num2 = Math.Max(num2, CalcRealMinConstraint(minSizes[i].Height));
            }
            return new Size(num, num2);
        }

        public static Size MeasureMinSize(Size[] minSizes, bool fHorz)
        {
            double num = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < minSizes.Length; i++)
            {
                if (fHorz)
                {
                    num += CalcRealMinConstraint(minSizes[i].Width);
                    num2 = Math.Max(num2, CalcRealMinConstraint(minSizes[i].Height));
                }
                else
                {
                    num = Math.Max(num, CalcRealMinConstraint(minSizes[i].Width));
                    num2 += CalcRealMinConstraint(minSizes[i].Height);
                }
            }
            return new Size(num, num2);
        }

        public static Size MeasureSize(Size minSize, Size maxSize, Size measuredSize) => 
            new Size(MeasureDimension(minSize.Width, maxSize.Width, measuredSize.Width), MeasureDimension(minSize.Height, maxSize.Height, measuredSize.Height));

        public static double NormalizeConstraint(double value) => 
            IsConstraintValid(value) ? value : double.NaN;

        public static int Round(double d) => 
            (d < 0.0) ? ((int) (d - 0.5)) : ((int) (d + 0.5));

        public static Point Round(Point p) => 
            new Point((double) Round(p.X), (double) Round(p.Y));

        public static Rect Round(Rect r)
        {
            double x = Round(r.Left);
            double y = Round(r.Top);
            double num4 = Round(r.Bottom);
            return new Rect(x, y, Round(r.Right) - x, num4 - y);
        }

        public static Size Round(Size s) => 
            new Size((double) Round(s.Width), (double) Round(s.Height));

        internal static double TopDimension(double max, double dim) => 
            !double.IsNaN(dim) ? ((max < dim) ? max : dim) : max;

        public static double ValidateDimension(double min, double max, double dim)
        {
            if (!double.IsNaN(dim))
            {
                if (IsConstraintValid(max))
                {
                    dim = Math.Min(max, dim);
                }
                if (IsConstraintValid(min))
                {
                    dim = Math.Max(min, dim);
                }
            }
            return dim;
        }

        public static Size ValidateSize(Size minSize, Size maxSize, Size measuredSize) => 
            new Size(ValidateDimension(minSize.Width, maxSize.Width, measuredSize.Width), ValidateDimension(minSize.Height, maxSize.Height, measuredSize.Height));
    }
}

