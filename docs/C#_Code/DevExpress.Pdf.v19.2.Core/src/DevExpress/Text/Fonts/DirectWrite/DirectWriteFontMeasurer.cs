namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DirectWriteFontMeasurer : IDisposable
    {
        private readonly DWriteFactory factory;
        private readonly DWriteTextAnalyzer textAnalyzer;
        private const int MaxGlyphCount = 0x1000;

        public DirectWriteFontMeasurer(DWriteFactory factory)
        {
            this.factory = factory;
            this.textAnalyzer = factory.CreateTextAnalyzer();
        }

        public void Dispose()
        {
            this.textAnalyzer.Dispose();
        }

        [SecuritySafeCritical]
        private static void DoActionWithPinnedObject(object o, Action<IntPtr> pinnedObjectPointerAction)
        {
            GCHandle handle = GCHandle.Alloc(o, GCHandleType.Pinned);
            try
            {
                pinnedObjectPointerAction(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public float MeasureCharacterWidth(char character, DirectWriteFont font, float fontSizeInPoints)
        {
            short[] o = new short[1];
            DWriteFontFace fontFace = font.FontFace;
            DoActionWithPinnedObject(o, delegate (IntPtr pointer) {
                int[] codePoints = new int[] { character };
                fontFace.GetGlyphIndices(codePoints, 1, pointer);
            });
            return ((((float) fontFace.GetDesignGlyphMetrics(o, false)[0].AdvanceWidth) / ((float) font.Metrics.UnitsPerEm)) * fontSizeInPoints);
        }

        public DXSizeF MeasureString(string text, DirectWriteFont font, float fontSizeInPoints)
        {
            DXSizeF ef;
            using (DWriteTextFormat format = font.CreateTextFormat(fontSizeInPoints))
            {
                using (DWriteTextLayout layout = this.factory.CreateTextLayout(text, format, float.MaxValue, float.MaxValue))
                {
                    DXFontMetrics metrics = font.Metrics;
                    ef = new DXSizeF(layout.GetWidth(text.Length), (((float) metrics.EmLineSpacing) / ((float) metrics.UnitsPerEm)) * fontSizeInPoints);
                }
            }
            return ef;
        }

        private static T[] Shrink<T>(T[] source, int count)
        {
            T[] destinationArray = new T[count];
            Array.Copy(source, destinationArray, count);
            return destinationArray;
        }

        public DWriteTextAnalyzer TextAnalyzer =>
            this.textAnalyzer;
    }
}

