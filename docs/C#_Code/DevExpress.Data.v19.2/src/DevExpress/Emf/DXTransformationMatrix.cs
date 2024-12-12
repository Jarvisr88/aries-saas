namespace DevExpress.Emf
{
    using System;

    public class DXTransformationMatrix
    {
        private readonly float a;
        private readonly float b;
        private readonly float c;
        private readonly float d;
        private readonly float e;
        private readonly float f;

        public DXTransformationMatrix() : this(1f, 0f, 0f, 1f, 0f, 0f)
        {
        }

        public DXTransformationMatrix(float a, float b, float c, float d, float e, float f)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }

        public float A =>
            this.a;

        public float B =>
            this.b;

        public float C =>
            this.c;

        public float D =>
            this.d;

        public float E =>
            this.e;

        public float F =>
            this.f;

        public bool IsIdentity =>
            (this.a == 1f) && ((this.b == 0f) && ((this.c == 0f) && ((this.d == 1f) && ((this.e == 0f) && (this.f == 0f)))));
    }
}

