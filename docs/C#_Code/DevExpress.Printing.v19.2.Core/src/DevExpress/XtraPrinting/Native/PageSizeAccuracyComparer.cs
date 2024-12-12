namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class PageSizeAccuracyComparer : FloatsComparer
    {
        public const double AccuracyEpsilon = 1.5625;
        public static readonly FloatsComparer Instance = new PageSizeAccuracyComparer(1.5625);

        protected PageSizeAccuracyComparer(double epsilon) : base(epsilon)
        {
        }
    }
}

