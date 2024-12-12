namespace DevExpress.Utils.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public class FullTrustCharsetAndCodePageTranslator : DXCharsetAndCodePageTranslator
    {
        private const int TCI_SRCCHARSET = 1;
        private const int TCI_SRCCODEPAGE = 2;
        private const int TCI_SRCFONTSIG = 3;
        private const int TCI_SRCLOCALE = 0x1000;
        private static Dictionary<int, int> charsetToCodePage = InitializeCharsetTable();
        private static Dictionary<int, int> codePageToCharset = InitializeCodePageTable();

        public override int CharsetFromCodePage(int codePage)
        {
            int num;
            if (!FullTrustCharsetAndCodePageTranslator.codePageToCharset.TryGetValue(codePage, out num))
            {
                num = this.CharsetFromCodePageCore(codePage);
                Dictionary<int, int> codePageToCharset = FullTrustCharsetAndCodePageTranslator.codePageToCharset;
                lock (codePageToCharset)
                {
                    FullTrustCharsetAndCodePageTranslator.codePageToCharset[codePage] = num;
                }
            }
            return num;
        }

        [SecuritySafeCritical]
        private int CharsetFromCodePageCore(int codePage)
        {
            IntPtr pSrc = new IntPtr(codePage);
            CHARSETINFO lpSc = new CHARSETINFO();
            if (TranslateCharsetInfo(pSrc, ref lpSc, 2) == 0)
            {
                return 0;
            }
            pSrc = IntPtr.Zero;
            return lpSc.ciCharset;
        }

        public override int CodePageFromCharset(int charset)
        {
            int num;
            if (!FullTrustCharsetAndCodePageTranslator.charsetToCodePage.TryGetValue(charset, out num))
            {
                num = this.CodePageFromCharsetCore(charset);
                Dictionary<int, int> charsetToCodePage = FullTrustCharsetAndCodePageTranslator.charsetToCodePage;
                lock (charsetToCodePage)
                {
                    FullTrustCharsetAndCodePageTranslator.charsetToCodePage[charset] = num;
                }
            }
            return num;
        }

        [SecuritySafeCritical]
        private int CodePageFromCharsetCore(int charset)
        {
            IntPtr pSrc = new IntPtr(charset);
            CHARSETINFO lpSc = new CHARSETINFO();
            if (TranslateCharsetInfo(pSrc, ref lpSc, 1) == 0)
            {
                return Encoding.Default.CodePage;
            }
            pSrc = IntPtr.Zero;
            return lpSc.ciACP;
        }

        private static Dictionary<int, int> InitializeCharsetTable() => 
            new Dictionary<int, int> { 
                { 
                    0x4d,
                    0x2710
                },
                { 
                    0x4e,
                    0x2711
                },
                { 
                    0x4f,
                    0x2713
                },
                { 
                    80,
                    0x2718
                },
                { 
                    0x51,
                    0x2712
                },
                { 
                    0x53,
                    0x2715
                },
                { 
                    0x54,
                    0x2714
                },
                { 
                    0x55,
                    0x2716
                },
                { 
                    0x56,
                    0x2761
                },
                { 
                    0x57,
                    0x2725
                },
                { 
                    0x58,
                    0x272d
                },
                { 
                    0x59,
                    0x2717
                }
            };

        private static Dictionary<int, int> InitializeCodePageTable()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            if (charsetToCodePage != null)
            {
                foreach (int num in charsetToCodePage.Keys)
                {
                    dictionary[charsetToCodePage[num]] = num;
                }
            }
            return dictionary;
        }

        [DllImport("Gdi32.dll")]
        private static extern int TranslateCharsetInfo([In, Out] IntPtr pSrc, [In, Out] ref CHARSETINFO lpSc, [In] int dwFlags);

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        private struct CHARSETINFO
        {
            public int ciCharset;
            public int ciACP;
            [MarshalAs(UnmanagedType.Struct)]
            public FullTrustCharsetAndCodePageTranslator.FONTSIGNATURE fSig;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Size=0x18)]
        private struct FONTSIGNATURE
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public int[] fsUsb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
            public int[] fsCsb;
        }
    }
}

