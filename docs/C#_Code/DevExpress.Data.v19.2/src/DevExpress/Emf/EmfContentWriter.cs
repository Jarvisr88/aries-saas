namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public class EmfContentWriter : BinaryWriter
    {
        public EmfContentWriter(Stream stream) : base(stream)
        {
        }

        public void Write(ARGBColor color)
        {
            this.Write(color.ToArgb());
        }

        public void Write(DXBlend blend)
        {
            double[] positions = blend.Positions;
            this.Write(positions.Length);
            this.Write(positions);
            this.Write(blend.Factors);
        }

        public void Write(DXColorBlend colorBlend)
        {
            double[] positions = colorBlend.Positions;
            this.Write(positions.Length);
            this.Write(positions);
            foreach (ARGBColor color in colorBlend.Colors)
            {
                this.Write(color);
            }
        }

        public void Write(DXRectangleF rectangle)
        {
            this.Write(rectangle.X);
            this.Write(rectangle.Y);
            this.Write(rectangle.Width);
            this.Write(rectangle.Height);
        }

        public void Write(DXTransformationMatrix transformationMatrix)
        {
            this.Write(transformationMatrix.A);
            this.Write(transformationMatrix.B);
            this.Write(transformationMatrix.C);
            this.Write(transformationMatrix.D);
            this.Write(transformationMatrix.E);
            this.Write(transformationMatrix.F);
        }

        public void Write(EmfRectL rect)
        {
            this.Write(rect.Left);
            this.Write(rect.Top);
            this.Write(rect.Right);
            this.Write(rect.Bottom);
        }

        public void Write(EmfSizeL size)
        {
            this.Write(size.Cx);
            this.Write(size.Cy);
        }

        public void Write(DXPointF[] points)
        {
            foreach (DXPointF tf in points)
            {
                this.Write(tf);
            }
        }

        public void Write(DXPointF point)
        {
            this.Write(point.X);
            this.Write(point.Y);
        }

        private void Write(double[] array)
        {
            foreach (double num2 in array)
            {
                this.Write((float) num2);
            }
        }
    }
}

