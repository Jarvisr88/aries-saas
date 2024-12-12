namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;

    public class UniscribeInterop
    {
        private const int E_OUTOFMEMORY = -2147024882;
        private const int USP_E_SCRIPT_NOT_IN_FONT = -2147220992;
        private const int SCRIPT_UNDEFINED = 0;
        private const int maxBufferResizesCount = 0x10;

        [SecuritySafeCritical]
        public static SCRIPT_LOGATTR[] ScriptBreak(string text, ref SCRIPT_ANALYSIS psa)
        {
            SCRIPT_LOGATTR[] psla = new SCRIPT_LOGATTR[text.Length];
            InteropHelpers.CheckHResult(ScriptBreak(text, text.Length, ref psa, psla));
            return psla;
        }

        [DllImport("Usp10.dll")]
        private static extern int ScriptBreak([MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, ref SCRIPT_ANALYSIS psa, [Out, MarshalAs(UnmanagedType.LPArray)] SCRIPT_LOGATTR[] psla);
        [DllImport("Usp10.dll")]
        private static extern int ScriptFreeCache(ref IntPtr psc);
        [SecuritySafeCritical]
        public static void ScriptFreeCache(IntPtr cache)
        {
            InteropHelpers.CheckHResult(ScriptFreeCache(ref cache));
        }

        [SecuritySafeCritical]
        public static SCRIPT_PROPERTIES[] ScriptGetProperties()
        {
            IntPtr ptr;
            int num;
            InteropHelpers.CheckHResult(ScriptGetProperties(out ptr, out num));
            ptr = Marshal.ReadIntPtr(ptr);
            SCRIPT_PROPERTIES[] script_propertiesArray = new SCRIPT_PROPERTIES[num];
            for (int i = 0; i < num; i++)
            {
                script_propertiesArray[i] = Marshal.PtrToStructure<SCRIPT_PROPERTIES>(ptr + (Marshal.SizeOf<SCRIPT_PROPERTIES>() * i));
            }
            return script_propertiesArray;
        }

        [DllImport("Usp10.dll")]
        private static extern int ScriptGetProperties(out IntPtr scriptPropertiesData, out int piNumScripts);
        [SecuritySafeCritical]
        public static IList<SCRIPT_ITEM> ScriptItemize(string text, ref SCRIPT_CONTROL psControl, ref SCRIPT_STATE psState, out OPENTYPE_TAG[] scriptTags)
        {
            int cMaxItems = Math.Max(4, text.Length);
            int hResult = 0;
            SCRIPT_ITEM[] pItems = null;
            scriptTags = null;
            int pcItems = 0;
            int num4 = 0;
            while (true)
            {
                if (num4 < 0x10)
                {
                    pItems = new SCRIPT_ITEM[cMaxItems + 1];
                    scriptTags = new OPENTYPE_TAG[cMaxItems];
                    hResult = ScriptItemizeOpenType(text, text.Length, cMaxItems, ref psControl, ref psState, pItems, scriptTags, out pcItems);
                    if (hResult == -2147024882)
                    {
                        cMaxItems *= 2;
                        num4++;
                        continue;
                    }
                }
                InteropHelpers.CheckHResult(hResult);
                return new ArraySegment<SCRIPT_ITEM>(pItems, 0, pcItems);
            }
        }

        [DllImport("Usp10.dll")]
        private static extern int ScriptItemizeOpenType([MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, int cMaxItems, ref SCRIPT_CONTROL psControl, ref SCRIPT_STATE psState, [Out, MarshalAs(UnmanagedType.LPArray)] SCRIPT_ITEM[] pItems, [Out, MarshalAs(UnmanagedType.LPArray)] OPENTYPE_TAG[] pScriptTags, out int pcItems);
        [SecuritySafeCritical]
        public static void ScriptPlace(IntPtr hdc, ref IntPtr cache, OPENTYPE_TAG tagScript, OPENTYPE_TAG tagLangSys, OpenTypeFeatureInfo featuresInfo, string text, short[] clusters, SCRIPT_CHARPROP[] charProps, short[] glyphs, int glyphsCount, SCRIPT_GLYPHPROP[] glyphAttributes, ref SCRIPT_ANALYSIS psa, out int[] advances, out GOFFSET[] offsets)
        {
            ABC abc;
            advances = new int[glyphsCount];
            offsets = new GOFFSET[glyphsCount];
            InteropHelpers.CheckHResult(ScriptPlaceOpenType(hdc, ref cache, ref psa, tagScript, tagLangSys, featuresInfo.RangeChars, featuresInfo.RangeProperties, featuresInfo.RangesCount, text, clusters, charProps, text.Length, glyphs, glyphAttributes, glyphsCount, advances, offsets, out abc));
        }

        [DllImport("Usp10.dll")]
        private static extern int ScriptPlaceOpenType(IntPtr hdc, ref IntPtr psc, ref SCRIPT_ANALYSIS psa, OPENTYPE_TAG tagScript, OPENTYPE_TAG tagLangSys, [MarshalAs(UnmanagedType.LPArray)] int[] rcRangeChars, IntPtr rpRangeProperties, int cRanges, [MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_CHARPROP[] pCharProps, int cChars, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_GLYPHPROP[] pGlyphProps, int cGlyphs, [Out, MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [Out, MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset, out ABC pABC);
        [SecuritySafeCritical]
        public static void ScriptShape(IntPtr hdc, ref IntPtr cache, OPENTYPE_TAG tagScript, OPENTYPE_TAG tagLangSys, OpenTypeFeatureInfo featuresInfo, string text, ref SCRIPT_ANALYSIS psa, out short[] clusters, out SCRIPT_CHARPROP[] charProps, out short[] glyphs, out SCRIPT_GLYPHPROP[] glyphAttributes, out int actualGlyphCount)
        {
            clusters = new short[text.Length];
            charProps = new SCRIPT_CHARPROP[text.Length];
            int cMaxGlyphs = text.Length + 5;
            glyphs = null;
            glyphAttributes = null;
            actualGlyphCount = 0;
            int hResult = 0;
            for (int i = 0; i < 0x10; i++)
            {
                glyphs = new short[cMaxGlyphs];
                glyphAttributes = new SCRIPT_GLYPHPROP[cMaxGlyphs];
                hResult = ScriptShapeOpenType(hdc, ref cache, ref psa, tagScript, tagLangSys, featuresInfo.RangeChars, featuresInfo.RangeProperties, featuresInfo.RangesCount, text, text.Length, cMaxGlyphs, clusters, charProps, glyphs, glyphAttributes, out actualGlyphCount);
                if ((hResult == -2147220992) && (psa.ScriptId != 0))
                {
                    psa.ScriptId = 0;
                }
                else
                {
                    if (hResult != -2147024882)
                    {
                        break;
                    }
                    cMaxGlyphs *= 2;
                }
            }
            InteropHelpers.CheckHResult(hResult);
        }

        [DllImport("Usp10.dll")]
        private static extern int ScriptShapeOpenType(IntPtr hdc, ref IntPtr psc, ref SCRIPT_ANALYSIS psa, OPENTYPE_TAG tagScript, OPENTYPE_TAG tagLangSys, [MarshalAs(UnmanagedType.LPArray)] int[] rcRangeChars, IntPtr rpRangeProperties, int cRanges, [MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cChars, int cMaxGlyphs, [Out, MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [Out, MarshalAs(UnmanagedType.LPArray)] SCRIPT_CHARPROP[] pCharProps, [Out, MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs, [Out, MarshalAs(UnmanagedType.LPArray)] SCRIPT_GLYPHPROP[] pOutGlyphProps, out int pcGlyphs);
    }
}

