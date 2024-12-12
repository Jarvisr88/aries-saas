namespace DevExpress.Emf
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusRegionRectangleNode : EmfPlusRegionNode
    {
        public EmfPlusRegionRectangleNode(RectangleF rectangle)
        {
            this.<Rectangle>k__BackingField = rectangle;
        }

        public override void Accept(IEmfPlusRegionNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        private static bool IsZeroComponent(float component) => 
            Math.Abs(component) < float.Epsilon;

        public override EmfPlusRegionNode Transform(Matrix transformMatrix)
        {
            float[] elements = transformMatrix.Elements;
            if ((IsZeroComponent(elements[0]) && IsZeroComponent(elements[3])) || (IsZeroComponent(elements[1]) && IsZeroComponent(elements[2])))
            {
                PointF[] pts = new PointF[] { new PointF(this.Rectangle.Left, this.Rectangle.Top), new PointF(this.Rectangle.Right, this.Rectangle.Bottom) };
                transformMatrix.TransformPoints(pts);
                return new EmfPlusRegionRectangleNode(RectangleF.FromLTRB(Math.Min(pts[0].X, pts[1].X), Math.Min(pts[0].Y, pts[1].Y), Math.Max(pts[0].X, pts[1].X), Math.Max(pts[0].Y, pts[1].Y)));
            }
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(this.Rectangle);
                PointF[] pathPoints = path.PathPoints;
                transformMatrix.TransformPoints(pathPoints);
                return new EmfPlusRegionPathNode(pathPoints, path.PathTypes, false);
            }
        }

        public RectangleF Rectangle { get; }
    }
}

