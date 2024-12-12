namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    internal class Win32Util
    {
        private const int ETO_CLIPPED = 4;
        public const int MM_ANISOTROPIC = 8;
        public const int TRANSPARENT = 1;
        private const int LOGPIXELSX = 0x58;
        private const int LOGPIXELSY = 90;

        public static IntPtr CreateSolidBrush(Color color) => 
            Win32API.CreateSolidBrush(ColorTranslator.ToWin32(color));

        public static IntPtr CreateSolidBrush(int color) => 
            Win32API.CreateSolidBrush(color);

        public static IntPtr CreateSolidBrush(IntPtr hdc) => 
            Win32API.CreateSolidBrush(GetTextColor(hdc));

        public static void DeleteObject(IntPtr hObject)
        {
            Win32API.DeleteObject(hObject);
        }

        public static void ExtTextOut(IntPtr hdc, int x, int y, bool isCliped, Rectangle clip, string str, int[] spacings)
        {
            ExtTextOut(hdc, x, y, isCliped, clip, str, str.Length, spacings);
        }

        public static void ExtTextOut(IntPtr hdc, int x, int y, bool isClipped, Rectangle clip, string str, int length, int[] spacings)
        {
            RECT rect = GetRect(clip);
            if (!isClipped)
            {
                rect.top = y;
            }
            int options = isClipped ? 4 : 0;
            Win32API.SetBkColor(hdc, 1);
            IntPtr handle = SelectObject(hdc, Win32API.GetStockObject(0));
            Win32API.ExtTextOut(hdc, x, y, options, ref rect, str, length, spacings);
            SelectObject(hdc, handle);
        }

        public static void FillRect(IntPtr hdc, Rectangle bounds, IntPtr hBrush)
        {
            RECT r = GetRect(bounds);
            Win32API.FillRect(hdc, ref r, hBrush);
        }

        public static int[] GetCharABCWidths(IntPtr hdc, uint firstChar, uint lastChar)
        {
            uint num = (lastChar - firstChar) + 1;
            int[] numArray = new int[num];
            ABC[] widths = new ABC[num];
            bool flag = Win32API.GetCharABCWidths(hdc, firstChar, lastChar, widths);
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = !flag ? 0 : ((widths[i].abcC < 0) ? -widths[i].abcC : 0);
            }
            return numArray;
        }

        public static int[] GetCharWidth(IntPtr hdc, uint firstChar, uint lastChar)
        {
            int[] widths = new int[(lastChar - firstChar) + 1];
            bool flag = Win32API.GetCharWidth(hdc, firstChar, lastChar, widths);
            return widths;
        }

        public static int[] GetCharWidth32(IntPtr hdc, uint firstChar, uint lastChar)
        {
            int[] widths = new int[(lastChar - firstChar) + 1];
            bool flag = Win32API.GetCharWidth32(hdc, firstChar, lastChar, widths);
            return widths;
        }

        public static KerningPair[] GetKerningPairs(IntPtr hdc)
        {
            int nNumPairs = Win32API.GetKerningPairs(hdc, 0, null);
            if (nNumPairs == 0)
            {
                return null;
            }
            KerningPair[] kerningPairs = new KerningPair[nNumPairs];
            Win32API.GetKerningPairs(hdc, nNumPairs, kerningPairs);
            return kerningPairs;
        }

        public static int GetLogicPixelPerInchX(IntPtr hdc) => 
            Win32API.GetDeviceCaps(hdc, 0x58);

        public static int GetLogicPixelPerInchY(IntPtr hdc) => 
            Win32API.GetDeviceCaps(hdc, 90);

        public static bool GetOutlineTextMetrics(IntPtr hdc, ref OUTLINETEXTMETRIC lptm)
        {
            uint strSize = Win32API.GetOutlineTextMetrics(hdc, 0, IntPtr.Zero);
            if (strSize == 0)
            {
                return false;
            }
            IntPtr ptr = Marshal.AllocHGlobal((int) strSize);
            bool flag = true;
            try
            {
                flag = Win32API.GetOutlineTextMetrics(hdc, strSize, ptr) != 0;
                if (flag)
                {
                    lptm = (OUTLINETEXTMETRIC) Marshal.PtrToStructure(ptr, typeof(OUTLINETEXTMETRIC));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return flag;
        }

        private static RECT GetRect(Rectangle bounds)
        {
            RECT rect;
            rect.top = bounds.Top;
            rect.left = bounds.Left;
            rect.bottom = bounds.Bottom;
            rect.right = bounds.Right;
            return rect;
        }

        public static int GetTextColor(IntPtr hdc) => 
            Win32API.GetTextColor(hdc);

        public static DevExpress.Utils.Text.TEXTMETRIC GetTextMetrics(IntPtr hdc)
        {
            DevExpress.Utils.Text.TEXTMETRIC textmetric;
            bool textMetrics = Win32API.GetTextMetrics(hdc, out textmetric);
            return textmetric;
        }

        public static IntPtr SelectObject(IntPtr hdc, IntPtr handle) => 
            Win32API.SelectObject(hdc, handle);

        public static void SetBkColor(IntPtr hdc, Color color)
        {
            Win32API.SetBkColor(hdc, ColorTranslator.ToWin32(color));
        }

        public static void SetBkMode(IntPtr hdc, int mode)
        {
            Win32API.SetBkMode(hdc, mode);
        }

        public static void SetTextColor(IntPtr hdc, Color color)
        {
            Win32API.SetTextColor(hdc, ColorTranslator.ToWin32(color));
        }

        public static void SetTextColor(IntPtr hdc, int color)
        {
            Win32API.SetTextColor(hdc, color);
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public struct ABC
        {
            public int abcA;
            public int abcB;
            public int abcC;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public struct KerningPair
        {
            public char wFirst;
            public char wSecond;
            public int iKernAmount;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto), ComVisible(false)]
        public class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string lfFaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OUTLINETEXTMETRIC
        {
            public uint otmSize;
            public DevExpress.Utils.Text.TEXTMETRIC otmTextMetrics;
            public byte otmFiller;
            public Win32Util.PANOSE otmPanoseNumber;
            public uint otmfsSelection;
            public uint otmfsType;
            public int otmsCharSlopeRise;
            public int otmsCharSlopeRun;
            public int otmItalicAngle;
            public uint otmEMSquare;
            public int otmAscent;
            public int otmDescent;
            public uint otmLineGap;
            public uint otmsCapEmHeight;
            public uint otmsXHeight;
            public Win32Util.RECT otmrcFontBox;
            public int otmMacAscent;
            public int otmMacDescent;
            public uint otmMacLineGap;
            public uint otmusMinimumPPEM;
            public Win32Util.POINT otmptSubscriptSize;
            public Win32Util.POINT otmptSubscriptOffset;
            public Win32Util.POINT otmptSuperscriptSize;
            public Win32Util.POINT otmptSuperscriptOffset;
            public uint otmsStrikeoutSize;
            public int otmsStrikeoutPosition;
            public int otmsUnderscoreSize;
            public int otmsUnderscorePosition;
            public uint otmpFamilyName;
            public uint otmpFaceName;
            public uint otmpStyleName;
            public uint otmpFullName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PANOSE
        {
            public byte bFamilyType;
            public byte bSerifStyle;
            public byte bWeight;
            public byte bProportion;
            public byte bContrast;
            public byte bStrokeVariation;
            public byte bArmStyle;
            public byte bLetterform;
            public byte bMidline;
            public byte bXHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx;
            public int cy;
        }

        [SuppressUnmanagedCodeSecurity]
        public class Win32API
        {
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, int fdwItalic, int fdwUnderline, int fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateSolidBrush(int color);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("USER32.dll", CharSet=CharSet.Auto)]
            public static extern int DrawText(IntPtr hdc, string text, int textLen, ref Win32Util.RECT gdiRect, int format);
            [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
            public static extern int ExtTextOut(IntPtr hdc, int x, int y, int options, ref Win32Util.RECT clip, string str, int len, [In, MarshalAs(UnmanagedType.LPArray)] int[] widths);
            [DllImport("User32.dll")]
            public static extern int FillRect(IntPtr hdc, ref Win32Util.RECT r, IntPtr brush);
            [DllImport("gdi32.dll", EntryPoint="GetCharABCWidthsW")]
            public static extern bool GetCharABCWidths(IntPtr hdc, uint firstChar, uint lastChar, [Out, MarshalAs(UnmanagedType.LPArray)] Win32Util.ABC[] widths);
            [DllImport("gdi32.dll", EntryPoint="GetCharWidthW")]
            public static extern bool GetCharWidth(IntPtr hdc, uint firstChar, uint lastChar, [Out, MarshalAs(UnmanagedType.LPArray)] int[] widths);
            [DllImport("gdi32.dll", EntryPoint="GetCharWidth32W")]
            public static extern bool GetCharWidth32(IntPtr hdc, uint firstChar, uint lastChar, [Out, MarshalAs(UnmanagedType.LPArray)] int[] widths);
            [DllImport("gdi32.dll")]
            public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
            [DllImport("Gdi32.dll", CharSet=CharSet.Auto)]
            public static extern int GetKerningPairs(IntPtr hdc, int nNumPairs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] Win32Util.KerningPair[] kerningPairs);
            [DllImport("gdi32.dll")]
            public static extern int GetMapMode(IntPtr hdc);
            [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
            public static extern uint GetOutlineTextMetrics(IntPtr hdc, uint strSize, IntPtr lptm);
            [DllImport("GDI32.dll")]
            public static extern IntPtr GetStockObject(int fnObject);
            [DllImport("gdi32.dll")]
            public static extern int GetTextColor(IntPtr hdc);
            [DllImport("gdi32.dll")]
            public static extern bool GetTextExtentPoint32(IntPtr hdc, string lpString, int cbString, out Win32Util.SIZE lpSize);
            [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
            public static extern bool GetTextMetrics(IntPtr hdc, out DevExpress.Utils.Text.TEXTMETRIC lptm);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiObj);
            [DllImport("gdi32.dll")]
            public static extern int SetBkColor(IntPtr hdc, int color);
            [DllImport("gdi32.dll")]
            public static extern int SetBkMode(IntPtr hdc, int mode);
            [DllImport("gdi32.dll")]
            public static extern int SetMapMode(IntPtr hdc, int mapMode);
            [DllImport("gdi32.dll")]
            public static extern int SetTextColor(IntPtr hdc, int color);
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("gdi32.dll")]
            public static extern bool SetViewportExtEx(IntPtr hdc, int nXExtent, int nYExtent, ref Win32Util.SIZE lpSize);
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("gdi32.dll")]
            public static extern bool SetWindowExtEx(IntPtr hdc, int nXExtent, int nYExtent, ref Win32Util.SIZE lpSize);
        }
    }
}

