namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DWriteTextLayout : ComObject<IDWriteTextLayout>
    {
        protected internal DWriteTextLayout(IDWriteTextLayout nativeObject) : base(nativeObject)
        {
        }

        public void Draw(IDWriteTextRenderer textRenderer, float originX, float originY)
        {
            base.WrappedObject.Draw(IntPtr.Zero, textRenderer, originX, originY);
        }

        public float GetWidth(int textLength)
        {
            int num;
            DWRITE_CLUSTER_METRICS[] metrics = new DWRITE_CLUSTER_METRICS[textLength];
            int hResult = base.WrappedObject.GetClusterMetrics(metrics, metrics.Length, out num);
            if (hResult == -2147024774)
            {
                metrics = new DWRITE_CLUSTER_METRICS[num];
                hResult = base.WrappedObject.GetClusterMetrics(metrics, metrics.Length, out num);
            }
            InteropHelpers.CheckHResult(hResult);
            Func<DWRITE_CLUSTER_METRICS, float> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<DWRITE_CLUSTER_METRICS, float> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = m => m.Width;
            }
            return metrics.Take<DWRITE_CLUSTER_METRICS>(num).Sum<DWRITE_CLUSTER_METRICS>(selector);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DWriteTextLayout.<>c <>9 = new DWriteTextLayout.<>c();
            public static Func<DWRITE_CLUSTER_METRICS, float> <>9__2_0;

            internal float <GetWidth>b__2_0(DWRITE_CLUSTER_METRICS m) => 
                m.Width;
        }
    }
}

