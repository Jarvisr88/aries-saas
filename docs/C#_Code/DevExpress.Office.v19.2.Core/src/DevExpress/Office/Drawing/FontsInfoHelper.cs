namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    internal static class FontsInfoHelper
    {
        private static readonly Dictionary<string, PInvokeSafeNativeMethods.LogFontCharSet> charSets = new Dictionary<string, PInvokeSafeNativeMethods.LogFontCharSet>();
        private static readonly Dictionary<string, FontFamilyIndex> fontFamilies = new Dictionary<string, FontFamilyIndex>();
        public const int FaceSize = 0x20;

        static FontsInfoHelper()
        {
            IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
            try
            {
                PInvokeSafeNativeMethods.LOGFONT structure = new PInvokeSafeNativeMethods.LOGFONT {
                    lfFaceName = "",
                    lfCharSet = PInvokeSafeNativeMethods.LogFontCharSet.DEFAULT
                };
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<PInvokeSafeNativeMethods.LOGFONT>(structure));
                Marshal.StructureToPtr<PInvokeSafeNativeMethods.LOGFONT>(structure, ptr, true);
                EnumFontFamiliesEx(hdc, ptr, new EnumFontExDelegate(FontsInfoHelper.EnumFontFamiliesExProc), IntPtr.Zero, 0);
            }
            finally
            {
                Graphics graphics;
                graphics.ReleaseHdc(hdc);
            }
        }

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        private static extern int EnumFontFamiliesEx(IntPtr hdc, [In] IntPtr lpLogfont, EnumFontExDelegate lpEnumFontFamExProc, IntPtr lParam, uint dwFlags);
        private static int EnumFontFamiliesExProc(ref Enumlogfontex logFontEx, IntPtr lpntme, int fontType, int i)
        {
            if (!charSets.ContainsKey(logFontEx.elfLogFont.lfFaceName))
            {
                charSets.Add(logFontEx.elfLogFont.lfFaceName, logFontEx.elfLogFont.lfCharSet);
                fontFamilies.Add(logFontEx.elfLogFont.lfFaceName, (FontFamilyIndex) ((byte) (logFontEx.elfLogFont.lfPitchAndFamily >> 4)));
            }
            return 1;
        }

        public static PInvokeSafeNativeMethods.LogFontCharSet GetFontCharSet(string fontName)
        {
            PInvokeSafeNativeMethods.LogFontCharSet dEFAULT;
            if (!charSets.TryGetValue(fontName, out dEFAULT))
            {
                dEFAULT = PInvokeSafeNativeMethods.LogFontCharSet.DEFAULT;
            }
            return dEFAULT;
        }

        public static FontFamilyIndex GetFontFamily(string fontName)
        {
            FontFamilyIndex dontCare;
            if (!fontFamilies.TryGetValue(fontName, out dontCare))
            {
                dontCare = FontFamilyIndex.DontCare;
            }
            return dontCare;
        }

        private delegate int EnumFontExDelegate(ref FontsInfoHelper.Enumlogfontex lpelfe, IntPtr lpntme, int fontType, int lParam);

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        private struct Enumlogfontex
        {
            public PInvokeSafeNativeMethods.LOGFONT elfLogFont;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string elfFullName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string elfStyle;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string elfScript;
        }
    }
}

