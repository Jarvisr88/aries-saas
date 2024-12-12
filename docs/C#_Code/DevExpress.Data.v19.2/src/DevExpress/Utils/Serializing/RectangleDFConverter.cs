namespace DevExpress.Utils.Serializing
{
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class RectangleDFConverter : StructDoubleConverter
    {
        public static readonly RectangleDFConverter Instance = new RectangleDFConverter();

        private RectangleDFConverter()
        {
        }

        protected override object CreateObject(double[] values) => 
            new RectangleDF(values[0], values[1], (float) values[2], (float) values[3]);

        protected override double[] GetValues(object obj)
        {
            RectangleDF edf = (RectangleDF) obj;
            return new double[] { edf.X, edf.Y, edf.Width, edf.Height };
        }

        public override System.Type Type =>
            typeof(RectangleDF);
    }
}

