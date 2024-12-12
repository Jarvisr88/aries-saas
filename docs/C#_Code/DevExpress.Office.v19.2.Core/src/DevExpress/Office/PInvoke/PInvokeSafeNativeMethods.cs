namespace DevExpress.Office.PInvoke
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Windows.Forms;

    [SuppressUnmanagedCodeSecurity]
    internal static class PInvokeSafeNativeMethods
    {
        public const int TCI_SRCCHARSET = 1;
        public const int TCI_SRCCODEPAGE = 2;
        public const int TCI_SRCFONTSIG = 3;
        public const int TCI_SRCLOCALE = 0x1000;

        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", SetLastError=true)]
        internal static extern bool CloseClipboard();
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll")]
        internal static extern int ConvertSecurityDescriptorToStringSecurityDescriptorA(IntPtr securityDescriptor, int requestedStringSDRevision, Win32.SECURITY_INFORMATION securityInformation, out IntPtr stringSecurityDescriptor, out long stringSecurityDescriptorLen);
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll")]
        internal static extern int ConvertStringSecurityDescriptorToSecurityDescriptorW([MarshalAs(UnmanagedType.LPWStr)] string stringSecurityDescriptor, int stringSDRevision, IntPtr ppSecurityDescriptor, IntPtr securityDescriptorSize);
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll", SetLastError=true)]
        internal static extern bool ConvertStringSidToSid(string stringSid, out IntPtr ptrSid);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, int fdwItalic, int fdwUnderline, int fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern IntPtr CreateHatchBrush(int fnStyle, int clrref);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool DeleteEnhMetaFile(IntPtr hemf);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool DeleteMetaFile(IntPtr hemf);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern bool DestroyCaret();
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", EntryPoint="DrawTextExW", CharSet=CharSet.Unicode)]
        internal static extern int DrawTextEx(IntPtr hdc, string lpchText, int cchText, ref Win32.RECT lprc, int dwDTFormat, IntPtr lpDTParams);
        [SuppressUnmanagedCodeSecurity, DllImport("aclui.dll")]
        internal static extern bool EditSecurity(IntPtr hwnd, ISecurityInformation psi);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError=true)]
        internal static extern bool EmptyClipboard();
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool EnumEnhMetaFile(IntPtr hdc, IntPtr hemf, Win32.EnumMetaFileDelegate lpMetaFunc, IntPtr lParam, ref Win32.RECT lpRect);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool EnumMetaFile(IntPtr hdc, IntPtr hmf, Win32.EnumMetaFileDelegate lpMetaFunc, IntPtr lParam);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll", EntryPoint="ExtTextOutW", CharSet=CharSet.Unicode)]
        internal static extern int ExtTextOut(IntPtr hdc, int x, int y, int options, ref Win32.RECT clip, IntPtr str, int len, IntPtr widths);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll", EntryPoint="ExtTextOutW", CharSet=CharSet.Unicode)]
        internal static extern int ExtTextOut(IntPtr hdc, int x, int y, int options, ref Win32.RECT clip, string str, int len, [In, MarshalAs(UnmanagedType.LPArray)] int[] widths);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipConvertToEmfPlus(IntPtr refGraphics, IntPtr refMetafile, out bool conversionFailureFlag, EmfType emfType, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr outMetafile);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipCreateFromHDC(IntPtr hdc, out IntPtr graphics);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipDeleteGraphics(IntPtr graphics);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipDisposeImage(IntPtr image);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern uint GdipEmfToWmfBits(IntPtr hEmf, uint bufferSize, byte[] buffer, int mappingMode, Win32.EmfToWmfBitsFlags flags);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipGetHemfFromMetafile(IntPtr refMetafile, out IntPtr hEnhMetafile);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipSetSmoothingMode(IntPtr graphics, SmoothingMode smoothingMode);
        [SuppressUnmanagedCodeSecurity, DllImport("gdiplus.dll")]
        internal static extern int GdipSetTextRenderingHint(IntPtr graphics, TextRenderingHint textRenderingHint);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern short GetAsyncKeyState(Keys vKey);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern int GetCaretBlinkTime();
        [DllImport("gdi32.dll", EntryPoint="GetCharABCWidthsW")]
        public static extern bool GetCharABCWidths(IntPtr hdc, uint firstChar, uint lastChar, [Out, MarshalAs(UnmanagedType.LPArray)] ABC[] widths);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool GetCharABCWidthsFloat(IntPtr hdc, uint uFirstChar, uint uLastChar, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPStruct)] Win32.ABCFLOAT[] lpabc);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll", EntryPoint="GetCharacterPlacementW", CharSet=CharSet.Unicode)]
        internal static extern int GetCharacterPlacement(IntPtr hdc, string lpString, int nCount, int nMaxExtent, ref Win32.GCP_RESULTS lpResults, int dwFlags);
        [DllImport("gdi32.dll", EntryPoint="GetCharWidthW")]
        public static extern bool GetCharWidth(IntPtr hdc, uint firstChar, uint lastChar, [Out, MarshalAs(UnmanagedType.LPArray)] int[] widths);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", SetLastError=true)]
        internal static extern IntPtr GetClipboardData(int uFormat);
        [SuppressUnmanagedCodeSecurity, DllImport("Gdi32.dll")]
        internal static extern uint GetEnhMetaFileBits(IntPtr hEmf, uint cbBuffer, byte[] buffer);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        public static extern IntPtr GetFocus();
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern int GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, byte[] lpvBuffer, uint cbData);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern uint GetFontUnicodeRanges(IntPtr hdc, IntPtr lpgs);
        [SuppressUnmanagedCodeSecurity, DllImport("Gdi32.dll")]
        internal static extern uint GetMetaFileBitsEx(IntPtr hEmf, uint cbBuffer, byte[] buffer);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll", EntryPoint="GetOutlineTextMetricsA")]
        public static extern uint GetOutlineTextMetrics(IntPtr hdc, uint cbData, IntPtr ptrZero);
        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        internal static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string path, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder shortPath, int shortPathLength);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern IntPtr GetStockObject(int fnObject);
        [DllImport("gdi32.dll")]
        internal static extern int GetTextAlign(IntPtr hdc);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern int GetTextCharset(IntPtr hdc);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern int GetTextCharsetInfo(IntPtr hdc, [In, Out] ref Win32.FONTSIGNATURE lpSig, int dwFlags);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRICUnicode lptm);
        [SuppressUnmanagedCodeSecurity, DllImport("secur32.dll", CharSet=CharSet.Auto)]
        internal static extern int GetUserNameEx(Win32.ExtendedNameFormat nameFormat, StringBuilder userName, ref int userNameSize);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool GetWindowExtEx(IntPtr hdc, out Win32.SIZE lpSize);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool GetWindowOrgEx(IntPtr hdc, out Win32.POINT lpPoint);
        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError=true)]
        internal static extern IntPtr GlobalLock(IntPtr hMem);
        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError=true)]
        internal static extern IntPtr GlobalSize(IntPtr hMem);
        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError=true)]
        internal static extern bool GlobalUnlock(IntPtr hMem);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern bool HideCaret(IntPtr hWnd);
        [SuppressUnmanagedCodeSecurity, DllImport("imm32.dll", CharSet=CharSet.Unicode)]
        internal static extern int ImmGetCompositionStringW(IntPtr hIMC, uint dwIndex, byte[] lpBuf, uint dwBufLen);
        [SuppressUnmanagedCodeSecurity, DllImport("imm32.dll")]
        internal static extern IntPtr ImmGetContext(IntPtr hWnd);
        [SuppressUnmanagedCodeSecurity, DllImport("imm32.dll")]
        internal static extern bool ImmNotifyIME(IntPtr hIMC, int dwAction, int dwIndex, int dwValue);
        [SuppressUnmanagedCodeSecurity, DllImport("imm32.dll")]
        internal static extern int ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
        [SuppressUnmanagedCodeSecurity, DllImport("imm32.dll")]
        internal static extern bool ImmSetCandidateWindow(IntPtr hIMC, ref Win32.CANDIDATEFORM lpCandForm);
        [SuppressUnmanagedCodeSecurity, DllImport("imm32.dll")]
        internal static extern bool ImmSetOpenStatus(IntPtr hIMC, bool fOpen);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", SetLastError=true)]
        internal static extern bool IsClipboardFormatAvailable(int format);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll")]
        internal static extern void MapGenericMask(IntPtr mask, ref Win32.GENERIC_MAPPING map);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", SetLastError=true)]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, uint dwRop);
        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        internal static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [SuppressUnmanagedCodeSecurity, DllImport("User32.dll", CharSet=CharSet.Auto)]
        internal static extern int PostMessage(IntPtr hWnd, uint msg, uint wParam, IntPtr lParam);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool RectVisible(IntPtr hdc, [In] ref Win32.RECT lprc);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", SetLastError=true)]
        internal static extern int RegisterClipboardFormat(string lpszFormat);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hGdiObj);
        [SuppressUnmanagedCodeSecurity, DllImport("User32.dll", CharSet=CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, uint msg, uint wParam, IntPtr lParam);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern int SetBkMode(IntPtr hdc, int iBkMode);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern bool SetCaretPos(int X, int Y);
        [DllImport("user32.dll", SetLastError=true)]
        internal static extern IntPtr SetClipboardData(int uFormat, [In] IntPtr hMem);
        [SuppressUnmanagedCodeSecurity, DllImport("Gdi32.dll")]
        internal static extern IntPtr SetEnhMetaFileBits(uint bufferSize, byte[] buffer);
        [SuppressUnmanagedCodeSecurity, DllImport("Gdi32.dll")]
        internal static extern IntPtr SetMetaFileBitsEx(uint cbBuffer, byte[] buffer);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern int SetROP2(IntPtr hdc, int fnDrawMode);
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll")]
        internal static extern void SetSecurityDescriptorControl(IntPtr handle, Win32.SECURITY_DESCRIPTOR_CONTROL controlBitsOfInterest, Win32.SECURITY_DESCRIPTOR_CONTROL controlBitsToSet);
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int SetSecurityDescriptorGroup(IntPtr pSecurityDescriptor, IntPtr pOwner, int bOwnerDefaulted);
        [SuppressUnmanagedCodeSecurity, DllImport("advapi32.dll", SetLastError=true)]
        internal static extern int SetSecurityDescriptorOwner(IntPtr pSecurityDescriptor, IntPtr pOwner, int bOwnerDefaulted);
        [DllImport("gdi32.dll")]
        internal static extern int SetTextAlign(IntPtr hdc, int fMode);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern int SetTextColor(IntPtr hdc, int color);
        [return: MarshalAs(UnmanagedType.Bool)]
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool SetWindowExtEx(IntPtr hdc, int nXExtent, int nYExtent, ref Win32.SIZE lpSize);
        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        internal static extern bool SetWindowOrgEx(IntPtr hdc, int x, int y, ref Win32.POINT lpPoint);
        [SuppressUnmanagedCodeSecurity, DllImport("Gdi32.dll")]
        internal static extern IntPtr SetWinMetaFileBits(uint bufferSize, byte[] buffer, IntPtr hdc, ref Win32.METAFILEPICT mfp);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll")]
        internal static extern bool ShowCaret(IntPtr hWnd);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref int value, int ignore);
        [SuppressUnmanagedCodeSecurity, DllImport("Gdi32.dll")]
        public static extern int TranslateCharsetInfo([In, Out] IntPtr pSrc, [In, Out] ref CHARSETINFO lpSc, [In] int dwFlags);
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet=CharSet.Unicode)]
        internal static extern short VkKeyScan(char ch);

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public struct CHARSETINFO
        {
            public int ciCharset;
            public int ciACP;
            [MarshalAs(UnmanagedType.Struct)]
            public Win32.FONTSIGNATURE fSig;
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
            public PInvokeSafeNativeMethods.LogFontCharSet lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string lfFaceName;
        }

        public enum LogFontCharSet : byte
        {
            ANSI = 0,
            DEFAULT = 1,
            SYMBOL = 2,
            SHIFTJIS = 0x80,
            HANGEUL = 0x81,
            HANGUL = 0x81,
            GB2312 = 0x86,
            CHINESEBIG5 = 0x88,
            OEM = 0xff,
            JOHAB = 130,
            HEBREW = 0xb1,
            ARABIC = 0xb2,
            GREEK = 0xa1,
            TURKISH = 0xa2,
            VIETNAMESE = 0xa3,
            THAI = 0xde,
            EASTEUROPE = 0xee,
            RUSSIAN = 0xcc,
            MAC = 0x4d,
            BALTIC = 0xba
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OUTLINETEXTMETRIC
        {
            public uint otmSize;
            public PInvokeSafeNativeMethods.TEXTMETRIC otmTextMetrics;
            public byte otmFiller;
            public PInvokeSafeNativeMethods.PANOSE otmPanoseNumber;
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
            public Win32.RECT otmrcFontBox;
            public int otmMacAscent;
            public int otmMacDescent;
            public uint otmMacLineGap;
            public uint otmusMinimumPPEM;
            public Win32.POINT otmptSubscriptSize;
            public Win32.POINT otmptSubscriptOffset;
            public Win32.POINT otmptSuperscriptSize;
            public Win32.POINT otmptSuperscriptOffset;
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
            public byte[] ToByteArray()
            {
                byte[] buffer1 = new byte[10];
                buffer1[0] = this.bFamilyType;
                buffer1[1] = this.bSerifStyle;
                buffer1[2] = this.bWeight;
                buffer1[3] = this.bProportion;
                buffer1[4] = this.bContrast;
                buffer1[5] = this.bStrokeVariation;
                buffer1[6] = this.bArmStyle;
                buffer1[7] = this.bLetterform;
                buffer1[8] = this.bMidline;
                buffer1[9] = this.bXHeight;
                return buffer1;
            }
        }

        [Flags]
        internal enum TextAlignment
        {
            TA_NOUPDATECP = 0,
            TA_UPDATECP = 1,
            TA_LEFT = 0,
            TA_RIGHT = 2,
            TA_CENTER = 6,
            TA_HORZ_ALIGNMENT_MASK = 6,
            TA_TOP = 0,
            TA_BOTTOM = 8,
            TA_BASELINE = 0x18,
            TA_RTLREADING = 0x100,
            TA_MASK = 0x11f,
            VTA_BASELINE = 0x18,
            VTA_LEFT = 8,
            VTA_RIGHT = 0,
            VTA_CENTER = 6,
            VTA_BOTTOM = 2,
            VTA_TOP = 0
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct TEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public byte tmFirstChar;
            public byte tmLastChar;
            public byte tmDefaultChar;
            public byte tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public struct TEXTMETRICUnicode
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }
    }
}

