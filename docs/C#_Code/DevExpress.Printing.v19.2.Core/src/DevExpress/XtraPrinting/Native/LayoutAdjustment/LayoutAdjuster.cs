namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LayoutAdjuster
    {
        protected float dpi;

        public LayoutAdjuster(float dpi);
        protected virtual void Adjust(List<ILayoutData> layoutData);
        private static float CalcMinInitialBottomSpan(ILayoutData data, IList<ILayoutData> collection);
        private bool NeedAdjust(List<ILayoutData> layoutData);
        public void Process(List<ILayoutData> layoutData);
        private static void SetControlViewDataY(ILayoutData data, IList<ILayoutData> collection);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutAdjuster.<>c <>9;
            public static Func<ILayoutData, float> <>9__5_0;

            static <>c();
            internal float <Adjust>b__5_0(ILayoutData x);
        }
    }
}

