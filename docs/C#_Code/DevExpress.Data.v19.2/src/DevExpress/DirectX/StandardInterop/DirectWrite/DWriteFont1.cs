namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using System;

    public class DWriteFont1 : DWriteFont
    {
        private readonly IDWriteFont1 nativeObject;

        internal DWriteFont1(IDWriteFont1 nativeObject) : base((IDWriteFont) nativeObject)
        {
            this.nativeObject = nativeObject;
        }

        public DWRITE_FONT_METRICS1 GetMetrics1()
        {
            DWRITE_FONT_METRICS1 dwrite_font_metrics;
            this.nativeObject.GetMetrics(out dwrite_font_metrics);
            return dwrite_font_metrics;
        }

        public byte[] GetPanose()
        {
            byte[] panose = new byte[10];
            this.nativeObject.GetPanose(panose);
            return panose;
        }

        public DWRITE_UNICODE_RANGE[] GetUnicodeRanges()
        {
            int maxRangeCount = 0x400;
            while (true)
            {
                int num;
                DWRITE_UNICODE_RANGE[] unicodeRanges = new DWRITE_UNICODE_RANGE[maxRangeCount];
                int hResult = this.nativeObject.GetUnicodeRanges(maxRangeCount, unicodeRanges, out num);
                if (hResult != -2147024774)
                {
                    InteropHelpers.CheckHResult(hResult);
                    DWRITE_UNICODE_RANGE[] destinationArray = new DWRITE_UNICODE_RANGE[num];
                    Array.Copy(unicodeRanges, destinationArray, num);
                    return destinationArray;
                }
                maxRangeCount += 0x400;
            }
        }
    }
}

