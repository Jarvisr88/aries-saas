namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingGeometryPoint
    {
        private DrawingGeometryCoordinate x;
        private DrawingGeometryCoordinate y;

        public DrawingGeometryPoint()
        {
            this.X = new DrawingGeometryCoordinate();
            this.Y = new DrawingGeometryCoordinate();
        }

        public DrawingGeometryPoint(Point p) : this(p.X, p.Y)
        {
        }

        public DrawingGeometryPoint(int x, int y) : this()
        {
            this.X.Value = x;
            this.Y.Value = y;
        }

        public DrawingGeometryCoordinate X
        {
            get => 
                this.x;
            set
            {
                DrawingGeometryCoordinate coordinate1 = value;
                if (value == null)
                {
                    DrawingGeometryCoordinate local1 = value;
                    coordinate1 = new DrawingGeometryCoordinate();
                }
                this.x = coordinate1;
            }
        }

        public DrawingGeometryCoordinate Y
        {
            get => 
                this.y;
            set
            {
                DrawingGeometryCoordinate coordinate1 = value;
                if (value == null)
                {
                    DrawingGeometryCoordinate local1 = value;
                    coordinate1 = new DrawingGeometryCoordinate();
                }
                this.y = coordinate1;
            }
        }
    }
}

