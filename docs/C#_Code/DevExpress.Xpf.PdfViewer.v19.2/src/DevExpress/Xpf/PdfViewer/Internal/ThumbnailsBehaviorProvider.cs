namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ThumbnailsBehaviorProvider : PdfBehaviorProvider
    {
        protected override List<double> GetZoomFactors() => 
            this.DefaultZoomLevels;

        protected override List<double> DefaultZoomLevels
        {
            get
            {
                List<double> list1 = new List<double>();
                list1.Add(1.0);
                list1.Add(1.25);
                list1.Add(1.5);
                list1.Add(1.75);
                list1.Add(2.0);
                list1.Add(2.25);
                list1.Add(2.5);
                list1.Add(2.75);
                list1.Add(3.0);
                list1.Add(3.25);
                list1.Add(3.5);
                list1.Add(3.75);
                list1.Add(4.0);
                list1.Add(4.25);
                list1.Add(4.5);
                list1.Add(4.75);
                list1.Add(5.0);
                return list1;
            }
        }

        public double MinZoomValue =>
            ((IEnumerable<double>) this.DefaultZoomLevels).Min();

        public double MaxZoomValue =>
            ((IEnumerable<double>) this.DefaultZoomLevels).Max();

        public virtual double LargeStep =>
            1.0;

        public virtual double SmallStep =>
            0.25;
    }
}

