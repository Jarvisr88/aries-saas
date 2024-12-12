namespace DevExpress.Office.Drawing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    internal class WpfCustomGeometryToPathGeometryConverter : IPathInstructionWalker
    {
        private System.Windows.Media.PathGeometry pathGeometry;
        private PathFigure currentFigure;
        private Rect rect;
        private ModelShapePath path;
        private bool isFilled;
        private float defaultScale;
        private float scaleX;
        private float scaleY;
        private double left;
        private double top;
        private double lastX;
        private double lastY;

        public WpfCustomGeometryToPathGeometryConverter(Rect bounding, float defaultScale)
        {
            this.defaultScale = defaultScale;
            this.pathGeometry = new System.Windows.Media.PathGeometry();
            this.pathGeometry.FillRule = FillRule.Nonzero;
            this.rect = bounding;
            this.left = bounding.Left;
            this.top = bounding.Top;
        }

        protected virtual void AddCurrentFigure()
        {
            this.PathGeometry.Figures.Add(this.currentFigure);
        }

        private double EMUToLayoutUnitsX(double emu) => 
            emu * this.scaleX;

        private double EMUToLayoutUnitsY(double emu) => 
            emu * this.scaleY;

        private void EndFigure()
        {
            this.currentFigure.IsClosed = false;
            this.AddCurrentFigure();
        }

        public void Visit(PathArc pathArc)
        {
        }

        public virtual void Visit(PathClose value)
        {
            this.currentFigure.Segments.Add(new LineSegment(this.currentFigure.StartPoint, this.path.Stroke));
            this.currentFigure.IsClosed = true;
            this.AddCurrentFigure();
            this.currentFigure = new PathFigure();
        }

        public void Visit(PathCubicBezier value)
        {
            double[] numArray = new double[3];
            double[] numArray2 = new double[3];
            for (int i = 0; i < 3; i++)
            {
                numArray[i] = value.Points[i].X.ValueEMU;
                numArray2[i] = value.Points[i].Y.ValueEMU;
            }
            this.currentFigure.Segments.Add(new BezierSegment(new Point(this.left + this.EMUToLayoutUnitsX(numArray[0]), this.top + this.EMUToLayoutUnitsY(numArray2[0])), new Point(this.left + this.EMUToLayoutUnitsX(numArray[1]), this.top + this.EMUToLayoutUnitsY(numArray2[1])), new Point(this.left + this.EMUToLayoutUnitsX(numArray[2]), this.top + this.EMUToLayoutUnitsY(numArray2[2])), this.path.Stroke));
        }

        public void Visit(PathLine pathLine)
        {
            double valueEMU = pathLine.Point.X.ValueEMU;
            double emu = pathLine.Point.Y.ValueEMU;
            this.currentFigure.Segments.Add(new LineSegment(new Point(this.left + this.EMUToLayoutUnitsX(valueEMU), this.top + this.EMUToLayoutUnitsY(emu)), this.path.Stroke));
        }

        public void Visit(PathMove pathMove)
        {
            double valueEMU = pathMove.Point.X.ValueEMU;
            double emu = pathMove.Point.Y.ValueEMU;
            if (this.currentFigure != null)
            {
                this.AddCurrentFigure();
            }
            this.currentFigure = new PathFigure();
            this.lastX = this.EMUToLayoutUnitsX(valueEMU) + this.left;
            this.lastY = this.EMUToLayoutUnitsY(emu) + this.top;
            this.currentFigure.StartPoint = new Point(this.lastX, this.lastY);
            this.currentFigure.IsClosed = false;
            this.currentFigure.IsFilled = this.isFilled;
        }

        public void Visit(PathQuadraticBezier value)
        {
        }

        public void Walk(ModelShapePath shapePath)
        {
            this.path = shapePath;
            this.isFilled = this.path.FillMode != PathFillMode.None;
            this.scaleX = (shapePath.Width == 0) ? this.defaultScale : (((float) this.rect.Width) / ((float) shapePath.Width));
            this.scaleY = (shapePath.Height == 0) ? this.defaultScale : (((float) this.rect.Height) / ((float) shapePath.Height));
            foreach (IPathInstruction instruction in this.path.Instructions)
            {
                instruction.Visit(this);
            }
            if (this.path.Instructions.Last.GetType() != typeof(PathClose))
            {
                this.EndFigure();
            }
        }

        public System.Windows.Media.PathGeometry PathGeometry =>
            this.pathGeometry;

        protected PathFigure CurrentFigure
        {
            get => 
                this.currentFigure;
            set => 
                this.currentFigure = value;
        }
    }
}

