namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;

    public class SvgPathWrapper : SvgElementWrapper
    {
        private List<SvgPathSegmentWrapper> segmentsCore;
        private ISvgPathSegmentWrapperFactory svgPathSegmentWrapperFactoryCore;

        public SvgPathWrapper(SvgElement element) : base(element)
        {
            this.segmentsCore = new List<SvgPathSegmentWrapper>();
            this.svgPathSegmentWrapperFactoryCore = this.CreateSvgPathSegmentWrapperFactory();
        }

        protected virtual ISvgPathSegmentWrapperFactory CreateSvgPathSegmentWrapperFactory() => 
            DevExpress.Utils.Svg.SvgPathSegmentWrapperFactory.Default;

        protected override GraphicsPath GetPathCore(double scale)
        {
            GraphicsPath result = new GraphicsPath {
                FillMode = (base.Element.FillRule == SvgFillRule.EvenOdd) ? FillMode.Alternate : FillMode.Winding
            };
            if (this.segmentsCore.Count != this.Path.Segments.Count)
            {
                this.segmentsCore.Clear();
                this.Path.Segments.ForEach(delegate (SvgPathSegment x) {
                    this.segmentsCore.Add(this.SvgPathSegmentWrapperFactory.Wrap(x));
                });
            }
            this.segmentsCore.ForEach(delegate (SvgPathSegmentWrapper x) {
                x.AddToPath(result, scale);
            });
            return result;
        }

        protected override SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            SmoothingMode.AntiAlias;

        private SvgPath Path =>
            base.Element as SvgPath;

        public ISvgPathSegmentWrapperFactory SvgPathSegmentWrapperFactory =>
            this.svgPathSegmentWrapperFactoryCore;
    }
}

