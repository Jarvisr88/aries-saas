namespace DevExpress.Utils.Zip
{
    using System;

    public class ByteCountCheckSumCalculator : ICheckSumCalculator<int>
    {
        private static ByteCountCheckSumCalculator instance;

        public int GetFinalCheckSum(int value) => 
            value;

        public int UpdateCheckSum(int value, byte[] buffer, int offset, int count) => 
            value + count;

        public static ByteCountCheckSumCalculator Instance
        {
            get
            {
                instance ??= new ByteCountCheckSumCalculator();
                return instance;
            }
        }

        public int InitialCheckSumValue =>
            0;
    }
}

