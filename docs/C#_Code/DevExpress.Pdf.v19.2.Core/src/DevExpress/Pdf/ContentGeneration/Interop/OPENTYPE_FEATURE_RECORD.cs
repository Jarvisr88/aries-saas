namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct OPENTYPE_FEATURE_RECORD
    {
        private readonly OPENTYPE_TAG tagFeature;
        private readonly int lParameter;
        public OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG tagFeature, bool value) : this(tagFeature, value ? 1 : 0)
        {
        }

        private OPENTYPE_FEATURE_RECORD(OPENTYPE_TAG tagFeature, int lParameter)
        {
            this.tagFeature = tagFeature;
            this.lParameter = lParameter;
        }
    }
}

