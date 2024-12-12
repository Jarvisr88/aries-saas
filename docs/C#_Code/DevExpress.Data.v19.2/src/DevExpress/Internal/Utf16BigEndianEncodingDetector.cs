namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class Utf16BigEndianEncodingDetector : Utf16EncodingDetector
    {
        protected internal override float GetConfidenceCore(int oddLowerByteCount, int evenLowerByteCount, float halfByteCount) => 
            (((((float) evenLowerByteCount) / halfByteCount) <= 0.8f) || ((((float) oddLowerByteCount) / halfByteCount) >= 0.2f)) ? 0.01f : (0.9f + Math.Min((float) 0.1f, (float) (((2f * halfByteCount) * 0.1f) / 236f)));

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0x4b1);
    }
}

