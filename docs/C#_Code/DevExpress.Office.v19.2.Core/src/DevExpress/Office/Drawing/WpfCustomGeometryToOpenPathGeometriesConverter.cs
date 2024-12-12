namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    internal class WpfCustomGeometryToOpenPathGeometriesConverter : WpfCustomGeometryToPathGeometryConverter
    {
        private readonly List<PathGeometry> pathGeometries;

        public WpfCustomGeometryToOpenPathGeometriesConverter(Rect bounding, float defaultScale) : base(bounding, defaultScale)
        {
            this.pathGeometries = new List<PathGeometry>();
        }

        protected override void AddCurrentFigure()
        {
            PathGeometry item = new PathGeometry {
                Figures = { base.CurrentFigure }
            };
            this.pathGeometries.Add(item);
        }

        public override void Visit(PathClose value)
        {
            base.CurrentFigure.IsClosed = false;
            this.AddCurrentFigure();
            base.CurrentFigure = new PathFigure();
        }

        public List<PathGeometry> PathGeometries =>
            this.pathGeometries;
    }
}

