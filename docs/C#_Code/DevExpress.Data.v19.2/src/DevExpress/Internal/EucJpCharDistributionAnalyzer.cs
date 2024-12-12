namespace DevExpress.Internal
{
    using System;

    public class EucJpCharDistributionAnalyzer : JapaneseCharDistributionAnalyzer
    {
        protected override int GetOrder(byte[] str, int offset) => 
            (str[offset] < 160) ? -1 : (((0x5e * (str[offset] - 0xa1)) + str[offset + 1]) - 0xa1);
    }
}

