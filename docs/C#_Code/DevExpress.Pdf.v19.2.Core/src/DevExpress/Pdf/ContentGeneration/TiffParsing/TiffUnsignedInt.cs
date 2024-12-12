namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;

    public class TiffUnsignedInt : ITiffValue
    {
        private readonly long value;

        public TiffUnsignedInt(long value)
        {
            this.value = value;
        }

        public double AsDouble()
        {
            throw new NotImplementedException();
        }

        public int AsInt() => 
            (int) this.value;

        public long AsUint() => 
            this.value;
    }
}

