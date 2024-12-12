namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;

    public class TiffSignedInt : ITiffValue
    {
        private readonly int value;

        public TiffSignedInt(int value)
        {
            this.value = value;
        }

        public double AsDouble()
        {
            throw new NotImplementedException();
        }

        public int AsInt() => 
            this.value;

        public long AsUint() => 
            (long) ((ulong) this.value);
    }
}

