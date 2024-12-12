namespace DevExpress.Office.PInvoke
{
    using DevExpress.Data.Helpers;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms;

    [SuppressUnmanagedCodeSecurity]
    public static class Win32
    {
        private static bool getUserNameFailed;
        public const int WM_IME_STARTCOMPOSITION = 0x10d;
        public const int WM_IME_ENDCOMPOSITION = 270;
        public const int WM_IME_COMPOSITION = 0x10f;
        public const int NI_OPENCANDIDATE = 0x10;
        public const int NI_CLOSECANDIDATE = 0x11;
        public const int NI_SELECTCANDIDATESTR = 0x12;
        public const int NI_CHANGECANDIDATELIST = 0x13;
        public const int NI_FINALIZECONVERSIONRESULT = 20;
        public const int NI_COMPOSITIONSTR = 0x15;
        public const int NI_SETCANDIDATE_PAGESTART = 0x16;
        public const int NI_SETCANDIDATE_PAGESIZE = 0x17;
        private const int SDDL_REVISION = 1;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_CHAR = 0x102;

        [SecuritySafeCritical]
        public static bool CloseClipboard() => 
            PInvokeSafeNativeMethods.CloseClipboard();

        [SecuritySafeCritical]
        public static string ConvertSecurityDescriptorToStringSecurityDescriptor(IntPtr securityDescriptor, SECURITY_INFORMATION securityInformation)
        {
            IntPtr ptr;
            long num;
            if (PInvokeSafeNativeMethods.ConvertSecurityDescriptorToStringSecurityDescriptorA(securityDescriptor, 1, securityInformation, out ptr, out num) == 0)
            {
                return string.Empty;
            }
            if (num <= 0L)
            {
                return string.Empty;
            }
            string str = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);
            return str;
        }

        [SecuritySafeCritical]
        public static void ConvertStringSecurityDescriptorToSecurityDescriptor(string stringSecurityDescriptor, IntPtr ppSecurityDescriptor)
        {
            PInvokeSafeNativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptorW(stringSecurityDescriptor, 1, ppSecurityDescriptor, IntPtr.Zero);
        }

        [SecuritySafeCritical]
        public static bool ConvertStringSidToSid(string stringSid, out IntPtr ptrSid) => 
            PInvokeSafeNativeMethods.ConvertStringSidToSid(stringSid, out ptrSid);

        [SecuritySafeCritical]
        public static bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int width, int height) => 
            PInvokeSafeNativeMethods.CreateCaret(hWnd, hBitmap, width, height);

        [SecuritySafeCritical]
        public static IntPtr CreateHatchBrush(HatchBrushStyle style, Color color) => 
            PInvokeSafeNativeMethods.CreateHatchBrush((int) style, ColorTranslator.ToWin32(color));

        [SecuritySafeCritical]
        public static IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, int crColor) => 
            PInvokeSafeNativeMethods.CreatePen((int) fnPenStyle, nWidth, crColor);

        [SecuritySafeCritical]
        public static bool DeleteEnhMetaFile(IntPtr hEmf) => 
            OSHelper.IsWindows && PInvokeSafeNativeMethods.DeleteEnhMetaFile(hEmf);

        [SecuritySafeCritical]
        public static bool DeleteMetaFile(IntPtr hEmf) => 
            OSHelper.IsWindows && PInvokeSafeNativeMethods.DeleteMetaFile(hEmf);

        [SecuritySafeCritical]
        public static bool DeleteObject(IntPtr hObject) => 
            PInvokeSafeNativeMethods.DeleteObject(hObject);

        [SecuritySafeCritical]
        public static bool DestroyCaret() => 
            PInvokeSafeNativeMethods.DestroyCaret();

        [SecuritySafeCritical]
        public static int DrawTextEx(IntPtr hdc, string text, ref RECT bounds, DrawTextFlags flags) => 
            PInvokeSafeNativeMethods.DrawTextEx(hdc, text, text.Length, ref bounds, (int) flags, IntPtr.Zero);

        [SecuritySafeCritical]
        public static bool EditSecurity(IWin32Window parent, ISecurityInformation securityInformation) => 
            PInvokeSafeNativeMethods.EditSecurity((parent == null) ? IntPtr.Zero : parent.Handle, securityInformation);

        [SecuritySafeCritical]
        public static bool EmptyClipboard() => 
            PInvokeSafeNativeMethods.EmptyClipboard();

        [SecuritySafeCritical]
        public static bool EnumEnhMetaFile(IntPtr hdc, IntPtr hemf, EnumMetaFileDelegate lpMetaFunc, IntPtr lParam, ref RECT lpRect) => 
            PInvokeSafeNativeMethods.EnumEnhMetaFile(hdc, hemf, lpMetaFunc, lParam, ref lpRect);

        [SecuritySafeCritical]
        public static bool EnumMetaFile(IntPtr hdc, IntPtr hmf, EnumMetaFileDelegate lpMetaFunc, IntPtr lParam) => 
            PInvokeSafeNativeMethods.EnumMetaFile(hdc, hmf, lpMetaFunc, lParam);

        [SecuritySafeCritical]
        public static int ExtTextOut(IntPtr hdc, int x, int y, EtoFlags options, ref RECT clip, IntPtr str, int len, IntPtr widths) => 
            PInvokeSafeNativeMethods.ExtTextOut(hdc, x, y, (int) options, ref clip, str, len, widths);

        [SecuritySafeCritical]
        public static int ExtTextOut(IntPtr hdc, int x, int y, EtoFlags options, ref RECT clip, string str, int len, [In, MarshalAs(UnmanagedType.LPArray)] int[] widths) => 
            PInvokeSafeNativeMethods.ExtTextOut(hdc, x, y, (int) options, ref clip, str, len, widths);

        [SecuritySafeCritical]
        public static byte[] GdipEmfToWmfBits(IntPtr hEmf, MapMode mappingMode, EmfToWmfBitsFlags flags)
        {
            if (!OSHelper.IsWindows)
            {
                return null;
            }
            uint bufferSize = PInvokeSafeNativeMethods.GdipEmfToWmfBits(hEmf, 0, null, (int) mappingMode, flags);
            byte[] buffer = new byte[bufferSize];
            PInvokeSafeNativeMethods.GdipEmfToWmfBits(hEmf, bufferSize, buffer, (int) mappingMode, flags);
            return buffer;
        }

        [SecuritySafeCritical]
        public static short GetAsyncKeyState(Keys key) => 
            PInvokeSafeNativeMethods.GetAsyncKeyState(key);

        [SecuritySafeCritical]
        public static int GetCaretBlinkTime() => 
            PInvokeSafeNativeMethods.GetCaretBlinkTime();

        [SecuritySafeCritical]
        internal static int[] GetCharABCWidths(IntPtr hdc, uint firstChar, uint lastChar)
        {
            uint num = (lastChar - firstChar) + 1;
            int[] numArray = new int[num];
            ABC[] widths = new ABC[num];
            bool flag = PInvokeSafeNativeMethods.GetCharABCWidths(hdc, firstChar, lastChar, widths);
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = !flag ? 0 : ((widths[i].abcC < 0) ? -widths[i].abcC : 0);
            }
            return numArray;
        }

        [SecuritySafeCritical]
        public static bool GetCharABCWidthsFloat(IntPtr hdc, char firstChar, char lastChar, ABCFLOAT[] result) => 
            PInvokeSafeNativeMethods.GetCharABCWidthsFloat(hdc, firstChar, lastChar, result);

        [SecuritySafeCritical]
        public static int GetCharacterPlacement(IntPtr hdc, string lpString, int nCount, int nMaxExtent, ref GCP_RESULTS lpResults, GcpFlags dwFlags) => 
            PInvokeSafeNativeMethods.GetCharacterPlacement(hdc, lpString, nCount, nMaxExtent, ref lpResults, (int) dwFlags);

        [SecuritySafeCritical]
        internal static int[] GetCharWidth(IntPtr hdc, uint firstChar, uint lastChar)
        {
            int[] widths = new int[(lastChar - firstChar) + 1];
            bool flag = PInvokeSafeNativeMethods.GetCharWidth(hdc, firstChar, lastChar, widths);
            return widths;
        }

        [SecuritySafeCritical]
        public static IntPtr GetClipboardData(int format) => 
            PInvokeSafeNativeMethods.GetClipboardData(format);

        [SecuritySafeCritical]
        public static byte[] GetEnhMetafileBits(IntPtr hEmf)
        {
            if (!OSHelper.IsWindows)
            {
                return new byte[0];
            }
            uint cbBuffer = PInvokeSafeNativeMethods.GetEnhMetaFileBits(hEmf, 0, null);
            byte[] buffer = new byte[cbBuffer];
            PInvokeSafeNativeMethods.GetEnhMetaFileBits(hEmf, cbBuffer, buffer);
            return buffer;
        }

        [SecuritySafeCritical]
        public static IntPtr GetFocus() => 
            PInvokeSafeNativeMethods.GetFocus();

        [SecuritySafeCritical]
        public static FontCharset GetFontCharset(IntPtr hdc) => 
            (FontCharset) PInvokeSafeNativeMethods.GetTextCharset(hdc);

        [SecuritySafeCritical]
        public static FontCharset GetFontCharsetInfo(IntPtr hdc, ref FONTSIGNATURE lpSig) => 
            (FontCharset) PInvokeSafeNativeMethods.GetTextCharsetInfo(hdc, ref lpSig, 0);

        [SecuritySafeCritical]
        internal static int GetFontSmoothingType()
        {
            int num = 0;
            PInvokeSafeNativeMethods.SystemParametersInfo(0x200a, 0, ref num, 0);
            return num;
        }

        [SecuritySafeCritical]
        public static int GetFontUnicodeRanges(IntPtr hdc, IntPtr lpgs) => 
            (int) PInvokeSafeNativeMethods.GetFontUnicodeRanges(hdc, lpgs);

        [SecuritySafeCritical]
        public static byte[] GetMetaFileBits(IntPtr hEmf)
        {
            if (!OSHelper.IsWindows)
            {
                return new byte[0];
            }
            uint cbBuffer = PInvokeSafeNativeMethods.GetMetaFileBitsEx(hEmf, 0, null);
            byte[] buffer = new byte[cbBuffer];
            PInvokeSafeNativeMethods.GetMetaFileBitsEx(hEmf, cbBuffer, buffer);
            return buffer;
        }

        [SecuritySafeCritical]
        public static string GetShortPathName(string fileName)
        {
            StringBuilder shortPath = new StringBuilder(0xff);
            PInvokeSafeNativeMethods.GetShortPathName(fileName, shortPath, 0xff);
            return shortPath.ToString();
        }

        [SecuritySafeCritical]
        public static IntPtr GetStockObject(StockObject obj) => 
            PInvokeSafeNativeMethods.GetStockObject((int) obj);

        private static uint GetTableIdentifier(string tableName)
        {
            if (tableName.Length != 4)
            {
                throw new ArgumentException();
            }
            return (uint) (((tableName[0] + (tableName[1] << 8)) + (tableName[2] << 0x10)) + (tableName[3] << 0x18));
        }

        [SecuritySafeCritical]
        public static string GetUserName(ExtendedNameFormat nameFormat)
        {
            string str;
            if (getUserNameFailed)
            {
                return string.Empty;
            }
            try
            {
                if (!SecurityHelper.IsPermissionGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode)))
                {
                    str = string.Empty;
                }
                else
                {
                    StringBuilder userName = new StringBuilder(0x3e8);
                    int capacity = userName.Capacity;
                    str = (PInvokeSafeNativeMethods.GetUserNameEx(nameFormat, userName, ref capacity) != 0) ? userName.ToString() : string.Empty;
                }
            }
            catch
            {
                getUserNameFailed = true;
                str = string.Empty;
            }
            return str;
        }

        [SecuritySafeCritical]
        public static IntPtr GetWindowDC(IntPtr hWnd) => 
            PInvokeSafeNativeMethods.GetWindowDC(hWnd);

        [SecuritySafeCritical]
        public static bool GetWindowExtEx(IntPtr hdc, out SIZE lpSize) => 
            PInvokeSafeNativeMethods.GetWindowExtEx(hdc, out lpSize);

        [SecuritySafeCritical]
        public static bool GetWindowOrgEx(IntPtr hdc, out POINT lpPoint) => 
            PInvokeSafeNativeMethods.GetWindowOrgEx(hdc, out lpPoint);

        [SecuritySafeCritical]
        public static IntPtr GlobalLock(IntPtr hMem) => 
            PInvokeSafeNativeMethods.GlobalLock(hMem);

        [SecuritySafeCritical]
        public static IntPtr GlobalSize(IntPtr hMem) => 
            PInvokeSafeNativeMethods.GlobalSize(hMem);

        [SecuritySafeCritical]
        public static bool GlobalUnlock(IntPtr hMem) => 
            PInvokeSafeNativeMethods.GlobalUnlock(hMem);

        [SecuritySafeCritical]
        public static bool HideCaret(IntPtr hWnd) => 
            PInvokeSafeNativeMethods.HideCaret(hWnd);

        [SecuritySafeCritical]
        public static int ImmGetCompositionStringW(IntPtr hIMC, int dwIndex, byte[] lpBuf, int dwBufLen) => 
            PInvokeSafeNativeMethods.ImmGetCompositionStringW(hIMC, (uint) dwIndex, lpBuf, (uint) dwBufLen);

        [SecuritySafeCritical]
        public static IntPtr ImmGetContext(IntPtr hWnd) => 
            PInvokeSafeNativeMethods.ImmGetContext(hWnd);

        [SecuritySafeCritical]
        public static bool ImmNotifyIME(IntPtr hIMC, int dwAction, int dwIndex, int dwValue) => 
            PInvokeSafeNativeMethods.ImmNotifyIME(hIMC, dwAction, dwIndex, dwValue);

        [SecuritySafeCritical]
        public static int ImmReleaseContext(IntPtr hWnd, IntPtr hIMC) => 
            PInvokeSafeNativeMethods.ImmReleaseContext(hWnd, hIMC);

        [SecuritySafeCritical]
        public static bool ImmSetCandidateWindow(IntPtr hIMC, ref CANDIDATEFORM lpCandForm) => 
            PInvokeSafeNativeMethods.ImmSetCandidateWindow(hIMC, ref lpCandForm);

        [SecuritySafeCritical]
        public static bool ImmSetOpenStatus(IntPtr hIMC, bool fOpen) => 
            PInvokeSafeNativeMethods.ImmSetOpenStatus(hIMC, fOpen);

        [SecuritySafeCritical]
        public static bool IsClipboardFormatAvailable(int format) => 
            PInvokeSafeNativeMethods.IsClipboardFormatAvailable(format);

        [SecuritySafeCritical]
        public static bool IsFontTablePresent(IntPtr hdc, string fontTableName) => 
            PInvokeSafeNativeMethods.GetFontData(hdc, GetTableIdentifier(fontTableName), 0, null, 0) > 0;

        [SecuritySafeCritical]
        public static void LineTo(IntPtr hdc, int x, int y)
        {
            PInvokeSafeNativeMethods.LineTo(hdc, x, y);
        }

        [SecuritySafeCritical]
        public static void MapGenericMask(IntPtr mask, ref GENERIC_MAPPING map)
        {
            PInvokeSafeNativeMethods.MapGenericMask(mask, ref map);
        }

        [SecuritySafeCritical]
        public static void MoveTo(IntPtr hdc, int x, int y)
        {
            PInvokeSafeNativeMethods.MoveToEx(hdc, x, y, IntPtr.Zero);
        }

        [SecuritySafeCritical]
        public static bool OpenClipboard(IntPtr hWndNewOwner) => 
            PInvokeSafeNativeMethods.OpenClipboard(hWndNewOwner);

        [SecuritySafeCritical]
        public static bool PatBlt(IntPtr hdc, int x, int y, int width, int height, TernaryRasterOperation rop) => 
            PInvokeSafeNativeMethods.PatBlt(hdc, x, y, width, height, (uint) rop);

        [SecuritySafeCritical]
        public static int PostMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam) => 
            PInvokeSafeNativeMethods.PostMessage(hWnd, (uint) msg, (uint) wParam, lParam);

        [SecuritySafeCritical]
        public static bool PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lParam) => 
            PInvokeSafeNativeMethods.PostMessage(hWnd, msg, wp, lParam);

        [SecuritySafeCritical]
        public static bool RectVisible(IntPtr hdc, ref RECT lprc) => 
            PInvokeSafeNativeMethods.RectVisible(hdc, ref lprc);

        [SecuritySafeCritical]
        public static int RegisterClipboardFormat(string formatName) => 
            PInvokeSafeNativeMethods.RegisterClipboardFormat(formatName);

        [SecuritySafeCritical]
        public static int ReleaseDC(IntPtr hWnd, IntPtr hDC) => 
            PInvokeSafeNativeMethods.ReleaseDC(hWnd, hDC);

        [SecuritySafeCritical]
        public static IntPtr SelectObject(IntPtr hdc, IntPtr hGdiObj) => 
            PInvokeSafeNativeMethods.SelectObject(hdc, hGdiObj);

        [SecuritySafeCritical]
        public static int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam) => 
            PInvokeSafeNativeMethods.SendMessage(hWnd, (uint) msg, (uint) wParam, lParam);

        [SecuritySafeCritical]
        public static BkMode SetBkMode(IntPtr hdc, BkMode iBkMode) => 
            (BkMode) PInvokeSafeNativeMethods.SetBkMode(hdc, (int) iBkMode);

        [SecuritySafeCritical]
        public static bool SetCaretPos(int x, int y) => 
            PInvokeSafeNativeMethods.SetCaretPos(x, y);

        [SecuritySafeCritical]
        public static IntPtr SetClipboardData(int uFormat, [In] IntPtr hMem) => 
            PInvokeSafeNativeMethods.SetClipboardData(uFormat, hMem);

        [SecuritySafeCritical]
        public static IntPtr SetEnhMetaFileBits(int bufferSize, byte[] buffer) => 
            PInvokeSafeNativeMethods.SetEnhMetaFileBits((uint) bufferSize, buffer);

        [SecuritySafeCritical]
        public static IntPtr SetEnhMetaFileBits(long bufferSize, byte[] buffer) => 
            PInvokeSafeNativeMethods.SetEnhMetaFileBits((uint) bufferSize, buffer);

        [SecuritySafeCritical]
        public static IntPtr SetMetaFileBitsEx(int cbBuffer, byte[] buffer) => 
            PInvokeSafeNativeMethods.SetMetaFileBitsEx((uint) cbBuffer, buffer);

        [SecuritySafeCritical]
        public static IntPtr SetMetaFileBitsEx(long cbBuffer, byte[] buffer) => 
            PInvokeSafeNativeMethods.SetMetaFileBitsEx((uint) cbBuffer, buffer);

        [SecuritySafeCritical]
        public static BinaryRasterOperation SetROP2(IntPtr hdc, BinaryRasterOperation rop) => 
            (BinaryRasterOperation) PInvokeSafeNativeMethods.SetROP2(hdc, (int) rop);

        [SecuritySafeCritical]
        public static void SetSecurityDescriptorControl(IntPtr handle, SECURITY_DESCRIPTOR_CONTROL controlBitsOfInterest, SECURITY_DESCRIPTOR_CONTROL controlBitsToSet)
        {
            PInvokeSafeNativeMethods.SetSecurityDescriptorControl(handle, controlBitsOfInterest, controlBitsToSet);
        }

        [SecuritySafeCritical]
        public static bool SetSecurityDescriptorGroup(IntPtr pSecurityDescriptor, IntPtr pOwner, int bOwnerDefaulted) => 
            PInvokeSafeNativeMethods.SetSecurityDescriptorGroup(pSecurityDescriptor, pOwner, bOwnerDefaulted) != 0;

        [SecuritySafeCritical]
        public static bool SetSecurityDescriptorOwner(IntPtr pSecurityDescriptor, IntPtr pOwner, int bOwnerDefaulted) => 
            PInvokeSafeNativeMethods.SetSecurityDescriptorOwner(pSecurityDescriptor, pOwner, bOwnerDefaulted) != 0;

        [SecuritySafeCritical]
        public static int SetTextAlign(IntPtr hdc, StringFormat format)
        {
            PInvokeSafeNativeMethods.TextAlignment alignment;
            switch (format.Alignment)
            {
                case StringAlignment.Center:
                    alignment = PInvokeSafeNativeMethods.TextAlignment.TA_CENTER;
                    break;

                case StringAlignment.Far:
                    alignment = PInvokeSafeNativeMethods.TextAlignment.TA_RIGHT;
                    break;

                default:
                    alignment = PInvokeSafeNativeMethods.TextAlignment.TA_LEFT;
                    break;
            }
            int textAlign = PInvokeSafeNativeMethods.GetTextAlign(hdc);
            if ((textAlign & 6) == alignment)
            {
                return textAlign;
            }
            textAlign = (textAlign & -7) | ((int) alignment);
            return PInvokeSafeNativeMethods.SetTextAlign(hdc, textAlign);
        }

        [SecuritySafeCritical]
        public static int SetTextAlign(IntPtr hdc, int value) => 
            PInvokeSafeNativeMethods.SetTextAlign(hdc, value);

        [SecuritySafeCritical]
        public static void SetTextColor(IntPtr hdc, Color color)
        {
            PInvokeSafeNativeMethods.SetTextColor(hdc, ColorTranslator.ToWin32(color));
        }

        [SecuritySafeCritical]
        public static bool SetWindowExtEx(IntPtr hdc, int nXExtent, int nYExtent, ref SIZE lpSize) => 
            PInvokeSafeNativeMethods.SetWindowExtEx(hdc, nXExtent, nYExtent, ref lpSize);

        [SecuritySafeCritical]
        public static bool SetWindowOrgEx(IntPtr hdc, int x, int y, ref POINT lpPoint) => 
            PInvokeSafeNativeMethods.SetWindowOrgEx(hdc, x, y, ref lpPoint);

        [SecuritySafeCritical]
        public static IntPtr SetWinMetaFileBits(int bufferSize, byte[] buffer, IntPtr hdc, ref METAFILEPICT mfp) => 
            PInvokeSafeNativeMethods.SetWinMetaFileBits((uint) bufferSize, buffer, hdc, ref mfp);

        [SecuritySafeCritical]
        public static IntPtr SetWinMetaFileBits(long bufferSize, byte[] buffer, IntPtr hdc, ref METAFILEPICT mfp) => 
            PInvokeSafeNativeMethods.SetWinMetaFileBits((uint) bufferSize, buffer, hdc, ref mfp);

        [SecuritySafeCritical]
        public static bool ShowCaret(IntPtr hWnd) => 
            PInvokeSafeNativeMethods.ShowCaret(hWnd);

        [SecuritySafeCritical]
        public static short VkKeyScan(char ch) => 
            PInvokeSafeNativeMethods.VkKeyScan(ch);

        [StructLayout(LayoutKind.Sequential)]
        public struct ABCFLOAT
        {
            public float abcA;
            public float abcB;
            public float abcC;
            public float GetWidth() => 
                (this.abcA + this.abcB) + this.abcC;
        }

        public enum BinaryRasterOperation
        {
            R2_BLACK = 1,
            R2_NOTMERGEPEN = 2,
            R2_MASKNOTPEN = 3,
            R2_NOTCOPYPEN = 4,
            R2_MASKPENNOT = 5,
            R2_NOT = 6,
            R2_XORPEN = 7,
            R2_NOTMASKPEN = 8,
            R2_MASKPEN = 9,
            R2_NOTXORPEN = 10,
            R2_NOP = 11,
            R2_MERGENOTPEN = 12,
            R2_COPYPEN = 13,
            R2_MERGEPENNOT = 14,
            R2_MERGEPEN = 15,
            R2_WHITE = 0x10
        }

        public enum BkMode
        {
            TRANSPARENT = 1,
            OPAQUE = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CANDIDATEFORM
        {
            public int dwIndex;
            public int dwStyle;
            public Win32.POINT ptCurrentPos;
            public Win32.RECT rcArea;
        }

        public class CfsFlags
        {
            public const int CFS_DEFAULT = 0;
            public const int CFS_RECT = 1;
            public const int CFS_POINT = 2;
            public const int CFS_FORCE_POSITION = 0x20;
            public const int CFS_CANDIDATEPOS = 0x40;
            public const int CFS_EXCLUDE = 0x80;
        }

        public class CpsFlags
        {
            public const int CPS_COMPLETE = 1;
            public const int CPS_CONVERT = 2;
            public const int CPS_REVERT = 3;
            public const int CPS_CANCEL = 4;
        }

        [Flags]
        public enum DrawTextFlags
        {
            DT_TOP = 0,
            DT_LEFT = 0,
            DT_CENTER = 1,
            DT_RIGHT = 2,
            DT_VCENTER = 4,
            DT_BOTTOM = 8,
            DT_WORDBREAK = 0x10,
            DT_SINGLELINE = 0x20,
            DT_EXPANDTABS = 0x40,
            DT_TABSTOP = 0x80,
            DT_NOCLIP = 0x100,
            DT_EXTERNALLEADING = 0x200,
            DT_CALCRECT = 0x400,
            DT_NOPREFIX = 0x800,
            DT_INTERNAL = 0x1000,
            DT_EDITCONTROL = 0x2000,
            DT_PATH_ELLIPSIS = 0x4000,
            DT_END_ELLIPSIS = 0x8000,
            DT_MODIFYSTRING = 0x10000,
            DT_RTLREADING = 0x20000,
            DT_WORD_ELLIPSIS = 0x40000,
            DT_NOFULLWIDTHCHARBREAK = 0x80000,
            DT_HIDEPREFIX = 0x100000,
            DT_PREFIXONLY = 0x200000
        }

        public enum EmfToWmfBitsFlags
        {
            EmfToWmfBitsFlagsDefault = 0,
            EmfToWmfBitsFlagsEmbedEmf = 1,
            EmfToWmfBitsFlagsIncludePlaceable = 2,
            EmfToWmfBitsFlagsNoXORClip = 4
        }

        public delegate int EnumMetaFileDelegate(IntPtr hdc, IntPtr handleTable, IntPtr metafileRecord, int objectCount, IntPtr clientData);

        [Flags]
        public enum EtoFlags
        {
            ETO_NONE = 0,
            ETO_OPAQUE = 2,
            ETO_CLIPPED = 4,
            ETO_GLYPH_INDEX = 0x10,
            ETO_RTLREADING = 0x80,
            ETO_NUMERICSLOCAL = 0x400,
            ETO_NUMERICSLATIN = 0x800,
            ETO_IGNORELANGUAGE = 0x1000,
            ETO_PDY = 0x2000
        }

        public enum ExtendedNameFormat
        {
            Unknown = 0,
            FullyQualifiedDN = 1,
            SamCompatible = 2,
            Display = 3,
            UniqueId = 6,
            Canonical = 7,
            UserPrincipal = 8,
            CanonicalEx = 9,
            ServicePrincipal = 10,
            DnsDomain = 12
        }

        public enum FontCharset
        {
            Ansi = 0,
            Default = 1,
            Symbol = 2,
            ShiftJis = 0x80,
            Hangeul = 0x81,
            GB2312 = 0x86,
            ChineseBig5 = 0x88,
            Oem = 0xff,
            Johab = 130,
            Hebrew = 0xb1,
            Arabic = 0xb2,
            Greek = 0xa1,
            Turkish = 0xa2,
            Vietnamese = 0xa3,
            Thai = 0xde,
            EastEurope = 0xee,
            Russian = 0xcc,
            Mac = 0x4d,
            Baltic = 0xba
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Size=0x18)]
        public struct FONTSIGNATURE
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public int[] fsUsb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
            public int[] fsCsb;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto), ComVisible(false)]
        public struct GCP_RESULTS
        {
            public int lStructSize;
            public IntPtr lpOutString;
            public IntPtr lpOrder;
            public IntPtr lpDx;
            public IntPtr lpCaretPos;
            public IntPtr lpClass;
            public IntPtr lpGlyphs;
            public int nGlyphs;
            public int nMaxFit;
        }

        [Flags]
        public enum GcpFlags
        {
            GCP_DBCS = 1,
            GCP_REORDER = 2,
            GCP_USEKERNING = 8,
            GCP_GLYPHSHAPE = 0x10,
            GCP_LIGATE = 0x20,
            GCP_DIACRITIC = 0x100,
            GCP_KASHIDA = 0x400,
            GCP_ERROR = 0x8000,
            GCP_JUSTIFY = 0x10000,
            GCP_CLASSIN = 0x80000,
            GCP_MAXEXTENT = 0x100000,
            GCP_JUSTIFYIN = 0x200000,
            GCP_DISPLAYZWG = 0x400000,
            GCP_SYMSWAPOFF = 0x800000,
            GCP_NUMERICOVERRIDE = 0x1000000,
            GCP_NEUTRALOVERRIDE = 0x2000000,
            GCP_NUMERICSLATIN = 0x4000000,
            GCP_NUMERICSLOCAL = 0x8000000
        }

        public class GcsFlags
        {
            public const int GCS_COMPREADSTR = 1;
            public const int GCS_COMPREADATTR = 2;
            public const int GCS_COMPREADCLAUSE = 4;
            public const int GCS_COMPSTR = 8;
            public const int GCS_COMPATTR = 0x10;
            public const int GCS_COMPCLAUSE = 0x20;
            public const int GCS_CURSORPOS = 0x80;
            public const int GCS_DELTASTART = 0x100;
            public const int GCS_RESULTREADSTR = 0x200;
            public const int GCS_RESULTREADCLAUSE = 0x400;
            public const int GCS_RESULTSTR = 0x800;
            public const int GCS_RESULTCLAUSE = 0x1000;
            public const int CS_INSERTCHAR = 0x2000;
            public const int CS_NOMOVECARET = 0x4000;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GENERIC_MAPPING
        {
            public int GenericRead;
            public int GenericWrite;
            public int GenericExecute;
            public int GenericAll;
        }

        public enum HatchBrushStyle
        {
            Horizontal,
            Vertical,
            DownwardDiagonal,
            UpwardDiagonal,
            Cross,
            DiagonalCross
        }

        public enum MapMode
        {
            Text = 1,
            LowMetric = 2,
            HighMetric = 3,
            LowEnglish = 4,
            HighEnglish = 5,
            Twips = 6,
            Isotropic = 7,
            Anisotropic = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct METAFILEPICT
        {
            private int mapMode;
            private int xExt;
            private int yExt;
            private IntPtr hMf;
            public METAFILEPICT(Win32.MapMode mapMode, int xExt, int yExt)
            {
                this.mapMode = (int) mapMode;
                this.xExt = xExt;
                this.yExt = yExt;
                this.hMf = IntPtr.Zero;
            }
        }

        [Flags]
        public enum PenStyle
        {
            PS_SOLID = 0,
            PS_DASH = 1,
            PS_DOT = 2,
            PS_DASHDOT = 3,
            PS_DASHDOTDOT = 4,
            PS_NULL = 5,
            PS_INSIDEFRAME = 6,
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 15,
            PS_ENDCAP_ROUND = 0,
            PS_ENDCAP_SQUARE = 0x100,
            PS_ENDCAP_FLAT = 0x200,
            PS_ENDCAP_MASK = 0xf00,
            PS_JOIN_ROUND = 0,
            PS_JOIN_BEVEL = 0x1000,
            PS_JOIN_MITER = 0x2000,
            PS_JOIN_MASK = 0xf000,
            PS_COSMETIC = 0,
            PS_GEOMETRIC = 0x10000,
            PS_TYPE_MASK = 0xf0000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator Point(Win32.POINT p) => 
                new Point(p.X, p.Y);

            public static implicit operator Win32.POINT(Point p) => 
                new Win32.POINT(p.X, p.Y);

            public static explicit operator Size(Win32.POINT p) => 
                new Size(p.X, p.Y);
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public int Width =>
                this.Right - this.Left;
            public int Height =>
                this.Bottom - this.Top;
            public System.Drawing.Size Size =>
                new System.Drawing.Size(this.Width, this.Height);
            public Point Location =>
                new Point(this.Left, this.Top);
            public Rectangle ToRectangle() => 
                Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);

            public static Win32.RECT FromRectangle(Rectangle rectangle) => 
                new Win32.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

            public override int GetHashCode() => 
                ((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Width << 0x1a) | (this.Width >> 6))) ^ ((this.Height << 7) | (this.Height >> 0x19));

            public static implicit operator Rectangle(Win32.RECT r) => 
                Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);

            public static implicit operator Win32.RECT(Rectangle r) => 
                new Win32.RECT(r.Left, r.Top, r.Right, r.Bottom);
        }

        [Flags]
        public enum SECURITY_DESCRIPTOR_CONTROL
        {
            SE_OWNER_DEFAULTED = 1,
            SE_GROUP_DEFAULTED = 2,
            SE_DACL_PRESENT = 4,
            SE_DACL_DEFAULTED = 8,
            SE_SACL_DEFAULTED = 8,
            SE_SACL_PRESENT = 0x10,
            SE_DACL_AUTO_INHERIT_REQ = 0x100,
            SE_SACL_AUTO_INHERIT_REQ = 0x200,
            SE_DACL_AUTO_INHERITED = 0x400,
            SE_SACL_AUTO_INHERITED = 0x800,
            SE_DACL_PROTECTED = 0x1000,
            SE_SACL_PROTECTED = 0x2000,
            SE_RM_CONTROL_VALID = 0x4000,
            SE_SELF_RELATIVE = 0x8000
        }

        [Flags]
        public enum SECURITY_INFORMATION
        {
            OWNER_SECURITY_INFORMATION = 1,
            GROUP_SECURITY_INFORMATION = 2,
            DACL_SECURITY_INFORMATION = 4,
            SACL_SECURITY_INFORMATION = 8,
            UNPROTECTED_SACL_SECURITY_INFORMATION = 0x10000000,
            UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000,
            PROTECTED_SACL_SECURITY_INFORMATION = 0x40000000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SI_ACCESS
        {
            public IntPtr guidObjectType;
            public int mask;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string szName;
            public int dwFlags;
            public static readonly int SizeOf;
            static SI_ACCESS()
            {
                SizeOf = Marshal.SizeOf(typeof(Win32.SI_ACCESS));
            }
        }

        public enum SI_ACCESS_FLAG
        {
            SI_ACCESS_SPECIFIC = 0x10000,
            SI_ACCESS_GENERAL = 0x20000,
            SI_ACCESS_CONTAINER = 0x40000,
            SI_ACCESS_PROPERTY = 0x80000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SI_INHERIT_TYPE
        {
            public IntPtr guidObjectType;
            public int dwFlags;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string szName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SI_OBJECT_INFO
        {
            public int dwFlags;
            public IntPtr hInstance;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string szServerName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string szObjectName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string szPageTitle;
            public Guid guidObjectType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int Width;
            public int Height;
            public SIZE(int w, int h)
            {
                this.Width = w;
                this.Height = h;
            }

            public static explicit operator Size(Win32.SIZE sz) => 
                new Size(sz.Width, sz.Height);
        }

        public enum StockObject
        {
            WHITE_BRUSH = 0,
            LTGRAY_BRUSH = 1,
            GRAY_BRUSH = 2,
            DKGRAY_BRUSH = 3,
            BLACK_BRUSH = 4,
            NULL_BRUSH = 5,
            HOLLOW_BRUSH = 5,
            WHITE_PEN = 6,
            BLACK_PEN = 7,
            NULL_PEN = 8,
            OEM_FIXED_FONT = 10,
            ANSI_FIXED_FONT = 11,
            ANSI_VAR_FONT = 12,
            SYSTEM_FONT = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE = 15,
            SYSTEM_FIXED_FONT = 0x10,
            DEFAULT_GUI_FONT = 0x11,
            DC_BRUSH = 0x12,
            DC_PEN = 0x13
        }

        public enum TernaryRasterOperation
        {
            SRCCOPY = 0xcc0020,
            SRCPAINT = 0xee0086,
            SRCAND = 0x8800c6,
            SRCINVERT = 0x660046,
            SRCERASE = 0x440328,
            NOTSRCCOPY = 0x330008,
            NOTSRCERASE = 0x1100a6,
            MERGECOPY = 0xc000ca,
            MERGEPAINT = 0xbb0226,
            PATCOPY = 0xf00021,
            PATPAINT = 0xfb0a09,
            PATINVERT = 0x5a0049,
            DSTINVERT = 0x550009,
            BLACKNESS = 0x42,
            WHITENESS = 0xff0062,
            CAPTUREBLT = 0x40000000
        }
    }
}

