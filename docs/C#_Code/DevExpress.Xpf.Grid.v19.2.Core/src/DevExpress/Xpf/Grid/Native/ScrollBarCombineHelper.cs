namespace DevExpress.Xpf.Grid.Native
{
    using System;

    public class ScrollBarCombineHelper
    {
        private double maxRatio;
        private double maxInvisiblePart;
        private double finalViewport;
        private double finalExtent;

        public double ConvertToRealOffset(double scrollBarOffset, double viewPort, double extent)
        {
            if ((this.finalExtent == this.finalViewport) || (viewPort >= extent))
            {
                return 0.0;
            }
            scrollBarOffset *= this.maxInvisiblePart / (this.finalExtent - this.finalViewport);
            return Math.Max(0.0, Math.Min(scrollBarOffset, extent - viewPort));
        }

        public void ProcessScrollInfo(double viewPort, double extent)
        {
            if (viewPort != 0.0)
            {
                double num = extent / viewPort;
                if (num > this.maxRatio)
                {
                    this.maxRatio = num;
                    this.finalExtent = extent;
                    this.finalViewport = viewPort;
                }
                this.maxInvisiblePart = Math.Max(this.maxInvisiblePart, extent - viewPort);
            }
        }

        public double FinalViewport =>
            this.finalViewport;

        public double FinalExtent =>
            this.finalExtent;
    }
}

