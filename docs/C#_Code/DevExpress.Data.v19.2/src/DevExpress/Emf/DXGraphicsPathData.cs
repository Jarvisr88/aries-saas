namespace DevExpress.Emf
{
    using System;

    public class DXGraphicsPathData
    {
        private readonly DXPointF[] points;
        private readonly DXPathPointTypes[] types;
        private readonly bool isWindingFillMode;

        public DXGraphicsPathData()
        {
            this.points = new DXPointF[0];
            this.types = new DXPathPointTypes[0];
        }

        public DXGraphicsPathData(DXPointF[] points, DXPathPointTypes[] types, bool isWindingFillMode)
        {
            this.points = points;
            this.types = types;
            this.isWindingFillMode = isWindingFillMode;
        }

        public DXPointF[] Points =>
            this.points;

        public DXPathPointTypes[] PathTypes =>
            this.types;

        public bool IsWindingFillMode =>
            this.isWindingFillMode;
    }
}

