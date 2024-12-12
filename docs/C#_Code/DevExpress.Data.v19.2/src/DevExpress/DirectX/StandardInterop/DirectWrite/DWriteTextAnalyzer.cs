namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class DWriteTextAnalyzer : ComObject<IDWriteTextAnalyzer>
    {
        private static readonly Dictionary<DWriteShapingFeatures, DWRITE_TYPOGRAPHIC_FEATURES_ARRAY> featureCache = new Dictionary<DWriteShapingFeatures, DWRITE_TYPOGRAPHIC_FEATURES_ARRAY>();

        static DWriteTextAnalyzer()
        {
            DWRITE_TYPOGRAPHIC_FEATURES_BUILDER dwrite_typographic_features_builder = new DWRITE_TYPOGRAPHIC_FEATURES_BUILDER();
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.KERNING, false);
            featureCache.Add(DWriteShapingFeatures.None, dwrite_typographic_features_builder.ToArray());
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.REQUIRED_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.STANDARD_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.CONTEXTUAL_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.HISTORICAL_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.DISCRETIONARY_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.KERNING, false);
            featureCache.Add(DWriteShapingFeatures.Ligatures, dwrite_typographic_features_builder.ToArray());
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.REQUIRED_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.STANDARD_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.CONTEXTUAL_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.HISTORICAL_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.DISCRETIONARY_LIGATURES, true);
            dwrite_typographic_features_builder.AddTag(DWRITE_FONT_FEATURE_TAG.KERNING, true);
            featureCache.Add(DWriteShapingFeatures.Default, dwrite_typographic_features_builder.ToArray());
        }

        internal DWriteTextAnalyzer(IDWriteTextAnalyzer nativeObject) : base(nativeObject)
        {
        }

        public void AnalyzeBidi(IDWriteTextAnalysisSource analysisSource, int textPosition, int textLength, IDWriteTextAnalysisSink analysisSink)
        {
            base.WrappedObject.AnalyzeBidi(analysisSource, textPosition, textLength, analysisSink);
        }

        public void AnalyzeLineBreakpoints(IDWriteTextAnalysisSource analysisSource, int textPosition, int textLength, IDWriteTextAnalysisSink analysisSink)
        {
            base.WrappedObject.AnalyzeLineBreakpoints(analysisSource, textPosition, textLength, analysisSink);
        }

        public void AnalyzeNumberSubstitution(IDWriteTextAnalysisSource analysisSource, int textPosition, int textLength, IDWriteTextAnalysisSink analysisSink)
        {
            base.WrappedObject.AnalyzeNumberSubstitution(analysisSource, textPosition, textLength, analysisSink);
        }

        public void AnalyzeScript(IDWriteTextAnalysisSource analysisSource, int textPosition, int textLength, IDWriteTextAnalysisSink analysisSink)
        {
            base.WrappedObject.AnalyzeScript(analysisSource, textPosition, textLength, analysisSink);
        }

        public void GetGdiCompatibleGlyphPlacements(string textString, short[] clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES[] textProps, short[] glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES[] glyphProps, int glyphCount, IDWriteFontFace fontFace, float fontEmSize, float pixelsPerDip, bool isSideways, bool isRightToLeft, ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis, string localeName, float[] glyphAdvances, DWRITE_GLYPH_OFFSET[] glyphOffsets)
        {
            base.WrappedObject.GetGdiCompatibleGlyphPlacements(textString, clusterMap, textProps, textString.Length, glyphIndices, glyphProps, glyphCount, fontFace, fontEmSize, pixelsPerDip, IntPtr.Zero, true, isSideways, isRightToLeft, ref scriptAnalysis, localeName, IntPtr.Zero, null, 0, glyphAdvances, glyphOffsets);
        }

        public void GetGlyphPlacements(string textString, short[] clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES[] textProps, short[] glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES[] glyphProps, int glyphCount, IDWriteFontFace fontFace, float fontEmSize, bool isSideways, bool isRightToLeft, ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis, string localeName, float[] glyphAdvances, DWRITE_GLYPH_OFFSET[] glyphOffsets)
        {
            this.GetGlyphPlacements(textString, clusterMap, textProps, glyphIndices, glyphProps, glyphCount, fontFace, fontEmSize, isSideways, isRightToLeft, ref scriptAnalysis, localeName, DWriteShapingFeatures.Default, glyphAdvances, glyphOffsets);
        }

        public void GetGlyphPlacements(string textString, short[] clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES[] textProps, short[] glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES[] glyphProps, int glyphCount, IDWriteFontFace fontFace, float fontEmSize, bool isSideways, bool isRightToLeft, ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis, string localeName, DWriteShapingFeatures features, float[] glyphAdvances, DWRITE_GLYPH_OFFSET[] glyphOffsets)
        {
            DWRITE_TYPOGRAPHIC_FEATURES_ARRAY dwrite_typographic_features_array;
            object obj1;
            if (!featureCache.TryGetValue(features, out dwrite_typographic_features_array))
            {
                dwrite_typographic_features_array = null;
            }
            if (dwrite_typographic_features_array == null)
            {
                obj1 = null;
            }
            else
            {
                int[] numArray1 = new int[] { textString.Length };
                obj1 = numArray1;
            }
            int[] featureRangeLengths = (int[]) obj1;
            base.WrappedObject.GetGlyphPlacements(textString, clusterMap, textProps, textString.Length, glyphIndices, glyphProps, glyphCount, fontFace, fontEmSize, isSideways, isRightToLeft, ref scriptAnalysis, localeName, dwrite_typographic_features_array?.ArrayPtr, featureRangeLengths, (featureRangeLengths == null) ? 0 : 1, glyphAdvances, glyphOffsets);
        }

        public void GetGlyphs(string textString, IDWriteFontFace fontFace, bool isSideways, bool isRightToLeft, ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis, string localeName, IDWriteNumberSubstitution numberSubstitution, int maxGlyphCount, short[] clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES[] textProps, short[] glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES[] glyphProps, out int actualGlyphCount)
        {
            this.GetGlyphs(textString, fontFace, isSideways, isRightToLeft, ref scriptAnalysis, localeName, DWriteShapingFeatures.Default, numberSubstitution, maxGlyphCount, clusterMap, textProps, glyphIndices, glyphProps, out actualGlyphCount);
        }

        public void GetGlyphs(string textString, IDWriteFontFace fontFace, bool isSideways, bool isRightToLeft, ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis, string localeName, DWriteShapingFeatures features, IDWriteNumberSubstitution numberSubstitution, int maxGlyphCount, short[] clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES[] textProps, short[] glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES[] glyphProps, out int actualGlyphCount)
        {
            DWRITE_TYPOGRAPHIC_FEATURES_ARRAY dwrite_typographic_features_array;
            object obj1;
            if (!featureCache.TryGetValue(features, out dwrite_typographic_features_array))
            {
                dwrite_typographic_features_array = null;
            }
            if (dwrite_typographic_features_array == null)
            {
                obj1 = null;
            }
            else
            {
                int[] numArray1 = new int[] { textString.Length };
                obj1 = numArray1;
            }
            int[] featureRangeLengths = (int[]) obj1;
            base.WrappedObject.GetGlyphs(textString, textString.Length, fontFace, isSideways, isRightToLeft, ref scriptAnalysis, localeName, null, dwrite_typographic_features_array?.ArrayPtr, featureRangeLengths, (featureRangeLengths == null) ? 0 : 1, maxGlyphCount, clusterMap, textProps, glyphIndices, glyphProps, out actualGlyphCount);
        }

        private class DWRITE_TYPOGRAPHIC_FEATURES_BUILDER
        {
            private readonly IList<DWRITE_FONT_FEATURE> features = new List<DWRITE_FONT_FEATURE>();

            public void AddTag(DWRITE_FONT_FEATURE_TAG tag, bool enable)
            {
                this.features.Add(new DWRITE_FONT_FEATURE(tag, enable ? 1 : 0));
            }

            public DWRITE_TYPOGRAPHIC_FEATURES_ARRAY ToArray()
            {
                DWRITE_TYPOGRAPHIC_FEATURES[] features = new DWRITE_TYPOGRAPHIC_FEATURES[] { new DWRITE_TYPOGRAPHIC_FEATURES(this.features.ToArray<DWRITE_FONT_FEATURE>()) };
                DWRITE_TYPOGRAPHIC_FEATURES_ARRAY dwrite_typographic_features_array = new DWRITE_TYPOGRAPHIC_FEATURES_ARRAY(features);
                this.features.Clear();
                return dwrite_typographic_features_array;
            }
        }
    }
}

