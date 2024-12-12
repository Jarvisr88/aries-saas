namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    internal static class ScaleXExtensions
    {
        public static double GetScaleX(this Visual visual)
        {
            Func<Visual, double> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<Visual, double> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => ScreenHelper.GetScaleX(x);
            }
            return visual.Return<Visual, double>(evaluator, (<>c.<>9__0_1 ??= () => ScreenHelper.ScaleX));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScaleXExtensions.<>c <>9 = new ScaleXExtensions.<>c();
            public static Func<Visual, double> <>9__0_0;
            public static Func<double> <>9__0_1;

            internal double <GetScaleX>b__0_0(Visual x) => 
                ScreenHelper.GetScaleX(x);

            internal double <GetScaleX>b__0_1() => 
                ScreenHelper.ScaleX;
        }
    }
}

