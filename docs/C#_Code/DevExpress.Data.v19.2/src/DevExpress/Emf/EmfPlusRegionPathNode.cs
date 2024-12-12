namespace DevExpress.Emf
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusRegionPathNode : EmfPlusRegionNode
    {
        public EmfPlusRegionPathNode(DXGraphicsPathData pathData)
        {
            Converter<DXPointF, PointF> converter = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Converter<DXPointF, PointF> local1 = <>c.<>9__10_0;
                converter = <>c.<>9__10_0 = point => new PointF(point.X, point.Y);
            }
            this.<Points>k__BackingField = Array.ConvertAll<DXPointF, PointF>(pathData.Points, converter);
            Converter<DXPathPointTypes, byte> converter2 = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Converter<DXPathPointTypes, byte> local2 = <>c.<>9__10_1;
                converter2 = <>c.<>9__10_1 = t => (byte) t;
            }
            this.<Types>k__BackingField = Array.ConvertAll<DXPathPointTypes, byte>(pathData.PathTypes, converter2);
            this.<IsWindingFillMode>k__BackingField = pathData.IsWindingFillMode;
        }

        public EmfPlusRegionPathNode(EmfPlusPath path) : this(path.PathData)
        {
        }

        public EmfPlusRegionPathNode(PointF[] points, byte[] types, bool isWindingFillMode)
        {
            this.<Points>k__BackingField = points;
            this.<Types>k__BackingField = types;
            this.<IsWindingFillMode>k__BackingField = isWindingFillMode;
        }

        public override void Accept(IEmfPlusRegionNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override EmfPlusRegionNode Transform(Matrix transformMatrix)
        {
            PointF[] pts = (PointF[]) this.Points.Clone();
            transformMatrix.TransformPoints(pts);
            return new EmfPlusRegionPathNode(pts, (byte[]) this.Types.Clone(), this.IsWindingFillMode);
        }

        public PointF[] Points { get; }

        public byte[] Types { get; }

        public bool IsWindingFillMode { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EmfPlusRegionPathNode.<>c <>9 = new EmfPlusRegionPathNode.<>c();
            public static Converter<DXPointF, PointF> <>9__10_0;
            public static Converter<DXPathPointTypes, byte> <>9__10_1;

            internal PointF <.ctor>b__10_0(DXPointF point) => 
                new PointF(point.X, point.Y);

            internal byte <.ctor>b__10_1(DXPathPointTypes t) => 
                (byte) t;
        }
    }
}

