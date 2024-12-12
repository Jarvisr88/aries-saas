namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Runtime.CompilerServices;

    public class VmlMatrix : ISupportsCopyFrom<VmlMatrix>, ICloneable<VmlMatrix>
    {
        public VmlMatrix()
        {
            this.Sxx = 1f;
            this.Syy = 1f;
        }

        public VmlMatrix Clone()
        {
            VmlMatrix matrix = new VmlMatrix();
            matrix.CopyFrom(this);
            return matrix;
        }

        public void CopyFrom(VmlMatrix source)
        {
            this.Sxx = source.Sxx;
            this.Sxy = source.Sxy;
            this.Syx = source.Syx;
            this.Syy = source.Syy;
            this.Px = source.Px;
            this.Py = source.Py;
        }

        public float Sxx { get; set; }

        public float Sxy { get; set; }

        public float Syx { get; set; }

        public float Syy { get; set; }

        public float Px { get; set; }

        public float Py { get; set; }
    }
}

