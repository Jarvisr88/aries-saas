namespace DevExpress.Emf
{
    using System;

    public class DXColorBlend
    {
        private double[] positions;
        private ARGBColor[] colors;

        public DXColorBlend()
        {
        }

        public DXColorBlend(int count)
        {
            this.positions = new double[count];
            this.colors = new ARGBColor[count];
        }

        public double[] Positions
        {
            get => 
                this.positions;
            set => 
                this.positions = value;
        }

        public ARGBColor[] Colors
        {
            get => 
                this.colors;
            set => 
                this.colors = value;
        }
    }
}

