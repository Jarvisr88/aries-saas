namespace DevExpress.Utils.Zip
{
    using System;

    [CLSCompliant(false)]
    public class Crc32CheckSumCalculator : ICheckSumCalculator<uint>
    {
        private static Crc32CheckSumCalculator instance;

        public uint GetFinalCheckSum(uint value) => 
            value ^ uint.MaxValue;

        public uint UpdateCheckSum(uint value, byte[] buffer, int offset, int count) => 
            Crc32CheckSum.Update(value, buffer, offset, count);

        public static Crc32CheckSumCalculator Instance
        {
            get
            {
                instance ??= new Crc32CheckSumCalculator();
                return instance;
            }
        }

        public uint InitialCheckSumValue =>
            uint.MaxValue;
    }
}

