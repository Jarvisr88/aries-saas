namespace DevExpress.Internal
{
    using System;

    public class EucJpContextAnalyzer : JapaneseContextAnalyzer
    {
        protected override int GetOrder(byte[] str, int offset) => 
            ((str[offset] != 0xa4) || ((str[offset + 1] < 0xa1) || (str[offset + 1] > 0xf3))) ? -1 : (str[offset + 1] - 0xa1);
    }
}

