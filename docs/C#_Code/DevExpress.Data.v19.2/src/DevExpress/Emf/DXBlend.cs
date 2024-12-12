namespace DevExpress.Emf
{
    using System;

    public class DXBlend
    {
        private readonly double[] positions;
        private readonly double[] factors;

        public DXBlend(double[] positions, double[] factors)
        {
            this.positions = positions;
            this.factors = factors;
        }

        public double[] Positions =>
            this.positions;

        public double[] Factors =>
            this.factors;

        public int Size =>
            4 + (8 * this.factors.Length);
    }
}

