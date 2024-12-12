namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;

    public class PdfFontUtils
    {
        private const string errorStr = "Error when reading font file";
        public static string Subname = "DEVEXP";

        private static string AddPostfix(string fontName, bool bold, bool italic)
        {
            string str = fontName;
            string fontPostfix = GetFontPostfix(bold, italic);
            if (fontPostfix != string.Empty)
            {
                str = str + "," + fontPostfix;
            }
            return str;
        }

        internal static byte[] CreateTTFFile(Font font)
        {
            bool flag;
            return CreateTTFFile(font, out flag);
        }

        [SecuritySafeCritical]
        internal static byte[] CreateTTFFile(Font font, out bool isTTC)
        {
            byte[] buffer2;
            using (Graphics graphics = GraphicsHelper.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                IntPtr zero = IntPtr.Zero;
                IntPtr hgdiobj = font.ToHfont();
                try
                {
                    zero = GDIFunctions.SelectObject(hdc, hgdiobj);
                    uint cbData = GDIFunctions.GetFontData(hdc, Ttcf, 0, null, 0);
                    uint dwTable = (cbData != uint.MaxValue) ? Ttcf : 0;
                    if (cbData == uint.MaxValue)
                    {
                        cbData = GDIFunctions.GetFontData(hdc, 0, 0, null, 0);
                    }
                    if (cbData == uint.MaxValue)
                    {
                        throw new PdfException("Error when reading font file");
                    }
                    byte[] lpvBuffer = new byte[cbData];
                    if (GDIFunctions.GetFontData(hdc, dwTable, 0, lpvBuffer, cbData) != lpvBuffer.Length)
                    {
                        throw new PdfException("Error when reading font file");
                    }
                    isTTC = dwTable == Ttcf;
                    buffer2 = lpvBuffer;
                }
                finally
                {
                    GDIFunctions.SelectObject(hdc, zero);
                    GDIFunctions.DeleteObject(hgdiobj);
                    graphics.ReleaseHdc(hdc);
                }
            }
            return buffer2;
        }

        public static bool FontEquals(Font font1, Font font2) => 
            (font1 != null) && ((font2 != null) && (GetTrueTypeFontName(font1, false) == GetTrueTypeFontName(font2, false)));

        public static string GetFontName(Font font) => 
            GetFontName(font.Name, font.Bold, font.Italic);

        public static string GetFontName(string familyName, bool bold, bool italic) => 
            AddPostfix(PdfStringUtils.ClearSpaces(familyName), bold, italic);

        private static string GetFontPostfix(bool bold, bool italic)
        {
            string str = string.Empty;
            if (bold)
            {
                str = str + "Bold";
            }
            if (italic)
            {
                str = str + "Italic";
            }
            return str;
        }

        public static string GetTrueTypeFontName(Font font, bool useSubname) => 
            AddPostfix(GetTrueTypeFontName0(font.Name, useSubname), font.Bold, font.Italic);

        public static string GetTrueTypeFontName0(string fontName, bool useSubname)
        {
            string str = PdfStringUtils.ClearSpaces(fontName);
            return (useSubname ? (Subname + "+" + str) : str);
        }

        private static uint Ttcf
        {
            get
            {
                string str = "ttcf";
                return (uint) (((str[0] | (str[1] << 8)) | (str[2] << 0x10)) | (str[3] << 0x18));
            }
        }
    }
}

