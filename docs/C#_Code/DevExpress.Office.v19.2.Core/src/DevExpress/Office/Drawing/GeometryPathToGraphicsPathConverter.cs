namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class GeometryPathToGraphicsPathConverter : IPathInstructionWalker
    {
        private readonly ModelShapePath path;
        private System.Drawing.Drawing2D.GraphicsPath graphicsPath;
        private readonly float scaleX;
        private readonly float scaleY;
        private readonly int left;
        private readonly int top;
        private double lastX;
        private double lastY;

        public GeometryPathToGraphicsPathConverter(ModelShapePath path, Rectangle bounding, float workbookDefaultScale)
        {
            this.path = path;
            this.scaleX = (path.Width == 0) ? workbookDefaultScale : (((float) bounding.Width) / ((float) path.Width));
            this.scaleY = (path.Height == 0) ? workbookDefaultScale : (((float) bounding.Height) / ((float) path.Height));
            this.left = bounding.Left;
            this.top = bounding.Top;
        }

        private static float EMUToLayoutUnits(double emu, float delta) => 
            (float) (emu * delta);

        private float EMUToLayoutUnitsX(double emu) => 
            EMUToLayoutUnits(emu, this.scaleX);

        private float EMUToLayoutUnitsY(double emu) => 
            EMUToLayoutUnits(emu, this.scaleY);

        public void Visit(PathArc pathArc)
        {
        }

        public void Visit(PathClose value)
        {
            this.GraphicsPath.CloseFigure();
            this.GraphicsPath.StartFigure();
        }

        public void Visit(PathCubicBezier value)
        {
            double[] numArray = new double[4];
            double[] numArray2 = new double[] { this.lastY };
            numArray[0] = this.lastX;
            for (int i = 1; i < 4; i++)
            {
                numArray[i] = value.Points[i - 1].X.ValueEMU;
                numArray2[i] = value.Points[i - 1].Y.ValueEMU;
            }
            this.GraphicsPath.AddBezier((float) (this.left + this.EMUToLayoutUnitsX(numArray[0])), (float) (this.top + this.EMUToLayoutUnitsY(numArray2[0])), (float) (this.left + this.EMUToLayoutUnitsX(numArray[1])), (float) (this.top + this.EMUToLayoutUnitsY(numArray2[1])), (float) (this.left + this.EMUToLayoutUnitsX(numArray[2])), (float) (this.top + this.EMUToLayoutUnitsY(numArray2[2])), (float) (this.left + this.EMUToLayoutUnitsX(numArray[3])), (float) (this.top + this.EMUToLayoutUnitsY(numArray2[3])));
            this.lastX = numArray[3];
            this.lastY = numArray2[3];
        }

        public void Visit(PathLine pathLine)
        {
            double valueEMU = pathLine.Point.X.ValueEMU;
            double emu = pathLine.Point.Y.ValueEMU;
            this.GraphicsPath.AddLine((float) (this.left + this.EMUToLayoutUnitsX(this.lastX)), (float) (this.top + this.EMUToLayoutUnitsY(this.lastY)), (float) (this.left + this.EMUToLayoutUnitsX(valueEMU)), (float) (this.top + this.EMUToLayoutUnitsY(emu)));
            this.lastX = valueEMU;
            this.lastY = emu;
        }

        public void Visit(PathMove pathMove)
        {
            double valueEMU = pathMove.Point.X.ValueEMU;
            double num2 = pathMove.Point.Y.ValueEMU;
            this.lastX = valueEMU;
            this.lastY = num2;
            this.GraphicsPath.StartFigure();
        }

        public void Visit(PathQuadraticBezier value)
        {
        }

        public void Walk()
        {
            this.graphicsPath = new System.Drawing.Drawing2D.GraphicsPath(FillMode.Winding);
            foreach (IPathInstruction instruction in this.path.Instructions)
            {
                instruction.Visit(this);
            }
        }

        public System.Drawing.Drawing2D.GraphicsPath GraphicsPath =>
            this.graphicsPath;
    }
}

