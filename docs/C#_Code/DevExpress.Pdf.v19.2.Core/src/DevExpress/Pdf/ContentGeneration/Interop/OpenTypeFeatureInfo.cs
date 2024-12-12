namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class OpenTypeFeatureInfo
    {
        static OpenTypeFeatureInfo()
        {
            OPENTYPE_FEATURE_RECORD[] records = new OPENTYPE_FEATURE_RECORD[] { new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.Kerning, false), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.RequiredLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.StandardLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.ContextualLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.HistoricalLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.DiscretionaryLigatures, true) };
            <KerningOffProperties>k__BackingField = TextRangeProperties.Create(records);
            OPENTYPE_FEATURE_RECORD[] opentype_feature_recordArray2 = new OPENTYPE_FEATURE_RECORD[] { new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.Kerning, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.RequiredLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.StandardLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.ContextualLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.HistoricalLigatures, true), new OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG.DiscretionaryLigatures, true) };
            <KerningOnProperties>k__BackingField = TextRangeProperties.Create(opentype_feature_recordArray2);
        }

        private OpenTypeFeatureInfo(int[] rangeChars, IntPtr rangeProperties, int rangesCount)
        {
            this.<RangeChars>k__BackingField = rangeChars;
            this.RangeProperties = rangeProperties;
            this.<RangesCount>k__BackingField = rangesCount;
        }

        [SecuritySafeCritical]
        public static OpenTypeFeatureInfo CreateKerningInfo(int textLength, bool useKerning)
        {
            IntPtr rangeProperties = useKerning ? KerningOnProperties.RangeProperties : KerningOffProperties.RangeProperties;
            int[] rangeChars = new int[] { textLength };
            return new OpenTypeFeatureInfo(rangeChars, rangeProperties, 1);
        }

        private static TextRangeProperties KerningOnProperties { get; }

        private static TextRangeProperties KerningOffProperties { get; }

        public int[] RangeChars { get; }

        public IntPtr RangeProperties { get; private set; }

        public int RangesCount { get; }

        private class TextRangeProperties : IDisposable
        {
            private TextRangeProperties(IntPtr rangeProperties)
            {
                this.RangeProperties = rangeProperties;
            }

            [SecuritySafeCritical]
            public static OpenTypeFeatureInfo.TextRangeProperties Create(OPENTYPE_FEATURE_RECORD[] records)
            {
                int num = Marshal.SizeOf<IntPtr>();
                int num2 = Marshal.SizeOf<TEXTRANGE_PROPERTIES>();
                int num3 = Marshal.SizeOf<OPENTYPE_FEATURE_RECORD>();
                int length = records.Length;
                IntPtr ptr = Marshal.AllocHGlobal((int) ((num + num2) + (num3 * length)));
                IntPtr val = ptr + num;
                Marshal.WriteIntPtr(ptr, val);
                IntPtr potfRecords = val + num2;
                TEXTRANGE_PROPERTIES structure = new TEXTRANGE_PROPERTIES(potfRecords, length);
                Marshal.StructureToPtr<TEXTRANGE_PROPERTIES>(structure, val, false);
                for (int i = 0; i < length; i++)
                {
                    Marshal.StructureToPtr<OPENTYPE_FEATURE_RECORD>(records[i], potfRecords, false);
                    potfRecords += num3;
                }
                return new OpenTypeFeatureInfo.TextRangeProperties(ptr);
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            [SecuritySafeCritical]
            protected virtual void Dispose(bool disposing)
            {
                if (this.RangeProperties != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.RangeProperties);
                    this.RangeProperties = IntPtr.Zero;
                }
            }

            ~TextRangeProperties()
            {
                this.Dispose(false);
            }

            public IntPtr RangeProperties { get; private set; }
        }
    }
}

