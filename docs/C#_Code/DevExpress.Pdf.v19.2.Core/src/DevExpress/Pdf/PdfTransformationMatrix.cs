namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class PdfTransformationMatrix
    {
        private static PdfTransformationMatrix identity = new PdfTransformationMatrix();
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private readonly double d;
        private readonly double e;
        private readonly double f;

        public PdfTransformationMatrix() : this(1.0, 0.0, 0.0, 1.0, 0.0, 0.0)
        {
        }

        internal PdfTransformationMatrix(PdfStack operands)
        {
            this.f = operands.PopDouble();
            this.e = operands.PopDouble();
            this.d = operands.PopDouble();
            this.c = operands.PopDouble();
            this.b = operands.PopDouble();
            this.a = operands.PopDouble();
        }

        internal PdfTransformationMatrix(IList<object> array)
        {
            if ((array != null) && (array.Count == 6))
            {
                this.a = PdfDocumentReader.ConvertToDouble(array[0]);
                this.b = PdfDocumentReader.ConvertToDouble(array[1]);
                this.c = PdfDocumentReader.ConvertToDouble(array[2]);
                this.d = PdfDocumentReader.ConvertToDouble(array[3]);
                this.e = PdfDocumentReader.ConvertToDouble(array[4]);
                this.f = PdfDocumentReader.ConvertToDouble(array[5]);
            }
            else
            {
                this.a = 1.0;
                this.b = 0.0;
                this.c = 0.0;
                this.d = 1.0;
                this.e = 0.0;
                this.f = 0.0;
            }
        }

        public PdfTransformationMatrix(double a, double b, double c, double d, double e, double f)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }

        public PdfTransformationMatrix Clone() => 
            new PdfTransformationMatrix(this.a, this.b, this.c, this.d, this.e, this.f);

        internal static PdfTransformationMatrix CreateRotate(double degree)
        {
            double a = degree / 57.295779513082323;
            double b = Math.Sin(a);
            double num3 = Math.Cos(a);
            return new PdfTransformationMatrix(num3, b, -b, num3, 0.0, 0.0);
        }

        internal static PdfTransformationMatrix CreateScale(double scaleX, double scaleY) => 
            new PdfTransformationMatrix(scaleX, 0.0, 0.0, scaleY, 0.0, 0.0);

        internal bool Equals(PdfTransformationMatrix matrix) => 
            (this.a == matrix.a) && ((this.b == matrix.b) && ((this.c == matrix.c) && ((this.d == matrix.d) && ((this.e == matrix.e) && (this.f == matrix.f)))));

        public override bool Equals(object obj)
        {
            PdfTransformationMatrix matrix = obj as PdfTransformationMatrix;
            return ((matrix != null) && this.Equals(matrix));
        }

        public override int GetHashCode() => 
            HashCodeHelper.Calculate(this.a.GetHashCode(), this.b.GetHashCode(), this.c.GetHashCode(), this.d.GetHashCode(), this.e.GetHashCode(), this.f.GetHashCode());

        internal static PdfTransformationMatrix Invert(PdfTransformationMatrix matrix)
        {
            double determinant = matrix.Determinant;
            return new PdfTransformationMatrix(matrix.d / determinant, -matrix.b / determinant, -matrix.c / determinant, matrix.a / determinant, ((matrix.c * matrix.f) - (matrix.e * matrix.d)) / determinant, ((matrix.b * matrix.e) - (matrix.f * matrix.a)) / determinant);
        }

        private static bool IsZeroComponent(double component) => 
            Math.Abs(component) < 1E-06;

        public static PdfTransformationMatrix Multiply(PdfTransformationMatrix matrix1, PdfTransformationMatrix matrix2)
        {
            double a = matrix1.a;
            double b = matrix1.b;
            double c = matrix1.c;
            double d = matrix1.d;
            double e = matrix1.e;
            double f = matrix1.f;
            double num7 = matrix2.a;
            double num8 = matrix2.b;
            double num9 = matrix2.c;
            double num10 = matrix2.d;
            return new PdfTransformationMatrix((a * num7) + (b * num9), (a * num8) + (b * num10), (c * num7) + (d * num9), (c * num8) + (d * num10), ((e * num7) + (f * num9)) + matrix2.e, ((e * num8) + (f * num10)) + matrix2.f);
        }

        internal static PdfTransformationMatrix Rotate(PdfTransformationMatrix matrix, double degree) => 
            Multiply(CreateRotate(degree), matrix);

        public static PdfTransformationMatrix Scale(PdfTransformationMatrix matrix, double scaleX, double scaleY) => 
            new PdfTransformationMatrix(matrix.a * scaleX, matrix.b * scaleY, matrix.c * scaleX, matrix.d * scaleY, matrix.e * scaleX, matrix.f * scaleY);

        public PdfPoint Transform(PdfPoint point) => 
            this.Transform(point.X, point.Y);

        public PdfPoint[] Transform(IList<PdfPoint> points)
        {
            int count = points.Count;
            PdfPoint[] pointArray = new PdfPoint[count];
            for (int i = 0; i < count; i++)
            {
                pointArray[i] = this.Transform(points[i]);
            }
            return pointArray;
        }

        internal PdfPoint Transform(double x, double y) => 
            new PdfPoint(((x * this.a) + (y * this.c)) + this.e, ((x * this.b) + (y * this.d)) + this.f);

        internal PdfRectangle TransformBoundingBox(PdfRectangle boundingBox)
        {
            PdfPoint[] points = new PdfPoint[] { boundingBox.BottomLeft, boundingBox.TopLeft, boundingBox.TopRight, boundingBox.BottomRight };
            return PdfRectangle.CreateBoundingBox(this.Transform(points));
        }

        public static PdfTransformationMatrix Translate(PdfTransformationMatrix matrix, double translateX, double translateY) => 
            new PdfTransformationMatrix(matrix.a, matrix.b, matrix.c, matrix.d, matrix.e + translateX, matrix.f + translateY);

        internal void Write(PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.a);
            writer.WriteSpace();
            writer.WriteDouble(this.b);
            writer.WriteSpace();
            writer.WriteDouble(this.c);
            writer.WriteSpace();
            writer.WriteDouble(this.d);
            writer.WriteSpace();
            writer.WriteDouble(this.e);
            writer.WriteSpace();
            writer.WriteDouble(this.f);
        }

        internal static PdfTransformationMatrix Identity =>
            identity;

        public double A =>
            this.a;

        public double B =>
            this.b;

        public double C =>
            this.c;

        public double D =>
            this.d;

        public double E =>
            this.e;

        public double F =>
            this.f;

        internal bool IsDefault =>
            (this.a == 1.0) && ((this.b == 0.0) && ((this.c == 0.0) && ((this.d == 1.0) && ((this.e == 0.0) && (this.f == 0.0)))));

        internal IList<object> Data
        {
            get
            {
                List<object> list1 = new List<object>();
                list1.Add(this.a);
                list1.Add(this.b);
                list1.Add(this.c);
                list1.Add(this.d);
                list1.Add(this.e);
                list1.Add(this.f);
                return list1;
            }
        }

        internal bool IsInvertable
        {
            get
            {
                double num = PdfMathUtils.Max(PdfMathUtils.Max(PdfMathUtils.Max(Math.Abs((double) ((this.c * this.f) - (this.e * this.d))), Math.Abs((double) ((this.b * this.e) - (this.f * this.a)))), PdfMathUtils.Max(Math.Abs(this.a), Math.Abs(this.b))), PdfMathUtils.Max(Math.Abs(this.c), Math.Abs(this.d)));
                double determinant = this.Determinant;
                return (!double.IsInfinity(determinant) && !((num + determinant) == num));
            }
        }

        internal bool IsNotRotated =>
            (!IsZeroComponent(this.a) || !IsZeroComponent(this.d)) ? (IsZeroComponent(this.b) && IsZeroComponent(this.c)) : true;

        internal double Determinant =>
            (this.a * this.d) - (this.b * this.c);

        internal double Sx =>
            new PdfPoint(this.a, this.b).Length();

        internal double Sy =>
            new PdfPoint(this.c, this.d).Length();
    }
}

