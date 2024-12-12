namespace DevExpress.Utils.Zip
{
    using System;

    [CLSCompliant(false)]
    public class Adler32CheckSumCalculator : ICheckSumCalculator<uint>
    {
        private static Adler32CheckSumCalculator instance;
        private readonly Adler32 adler = new Adler32();

        public uint GetFinalCheckSum(uint value) => 
            value;

        public uint UpdateCheckSum(uint value, byte[] buffer, int offset, int count)
        {
            this.adler.Initialize(value);
            this.adler.Add(buffer, offset, count);
            return this.adler.ToUInt();
        }

        public static Adler32CheckSumCalculator Instance
        {
            get
            {
                instance ??= new Adler32CheckSumCalculator();
                return instance;
            }
        }

        public uint InitialCheckSumValue =>
            1;
    }
}

