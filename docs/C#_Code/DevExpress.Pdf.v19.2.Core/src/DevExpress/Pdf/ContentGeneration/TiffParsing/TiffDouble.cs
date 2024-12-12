namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;

    public class TiffDouble : ITiffValue
    {
        private readonly double value;

        public TiffDouble(double value)
        {
            this.value = value;
        }

        public double AsDouble() => 
            this.value;

        public int AsInt()
        {
            throw new NotImplementedException();
        }

        public long AsUint()
        {
            throw new NotImplementedException();
        }
    }
}

