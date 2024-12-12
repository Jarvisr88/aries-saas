namespace DevExpress.Internal
{
    using System;

    public class ShiftedJisContextAnalyzer : JapaneseContextAnalyzer
    {
        protected override int GetOrder(byte[] str, int offset) => 
            ((str[offset] != 130) || ((str[offset + 1] < 0x9f) || (str[offset + 1] > 0xf1))) ? -1 : (str[offset + 1] - 0x9f);
    }
}

