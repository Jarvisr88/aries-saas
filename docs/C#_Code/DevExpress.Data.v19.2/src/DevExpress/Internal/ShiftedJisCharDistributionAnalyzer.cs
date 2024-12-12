namespace DevExpress.Internal
{
    using System;

    public class ShiftedJisCharDistributionAnalyzer : JapaneseCharDistributionAnalyzer
    {
        protected override int GetOrder(byte[] str, int offset)
        {
            int num;
            if ((str[offset] >= 0x81) && (str[offset] <= 0x9f))
            {
                num = 0xbc * (str[offset] - 0x81);
            }
            else
            {
                if ((str[offset] < 0xe0) || (str[offset] > 0xef))
                {
                    return -1;
                }
                num = 0xbc * ((str[offset] - 0xe0) + 0x1f);
            }
            num += str[offset + 1] - 0x40;
            if (str[offset + 1] > 0x7f)
            {
                num--;
            }
            return num;
        }
    }
}

