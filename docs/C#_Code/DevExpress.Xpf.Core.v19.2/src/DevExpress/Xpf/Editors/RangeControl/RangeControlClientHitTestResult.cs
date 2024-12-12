namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RangeControlClientHitTestResult
    {
        public static readonly RangeControlClientHitTestResult Nothing = new RangeControlClientHitTestResult(RangeControlClientRegionType.Nothing, null, null);

        public RangeControlClientHitTestResult(RangeControlClientRegionType regionType, object start = null, object end = null)
        {
            this.RegionType = regionType;
            this.RegionStart = start;
            this.RegionEnd = end;
        }

        public RangeControlClientRegionType RegionType { get; private set; }

        public object RegionStart { get; private set; }

        public object RegionEnd { get; private set; }
    }
}

