namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;

    public class StringDirection : IDisposable
    {
        private const int GCP_REORDER = 2;
        private const uint GCP_LIGATE = 0x20;
        private const uint LAYOUT_RTL = 1;
        private GCHandle orderHandle;
        private GCHandle dxHandle;
        private GCHandle caretPosHandle;
        private GCHandle classHandle;
        private GCHandle glyphsHandle;
        private Graphics gr;
        private IntPtr hdc;
        private Font font;
        private IntPtr fontH;
        private IntPtr oldFont;

        public StringDirection(Font font)
        {
            this.Initialize(font, false);
        }

        public StringDirection(Font font, bool rtl)
        {
            this.Initialize(font, rtl);
        }

        private static void AssignMap(Dictionary<ushort, char[]> result, ushort glyph, List<char> chars)
        {
            if (!IsWritingOrderControl(chars[0]))
            {
                result[glyph] = chars.ToArray();
            }
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            GDIFunctions.SelectObject(this.hdc, this.oldFont);
            GDIFunctions.DeleteObject(this.fontH);
            this.font.Dispose();
            this.gr.ReleaseHdc(this.hdc);
            this.gr.Dispose();
        }

        [SecuritySafeCritical]
        private void FreeHandles()
        {
            this.orderHandle.Free();
            this.dxHandle.Free();
            this.caretPosHandle.Free();
            this.classHandle.Free();
            this.glyphsHandle.Free();
        }

        [SecuritySafeCritical]
        private void Initialize(Font originalFont, bool rtl)
        {
            this.gr = Graphics.FromHwnd(IntPtr.Zero);
            this.hdc = this.gr.GetHdc();
            try
            {
                this.font = (Font) originalFont.Clone();
                this.fontH = this.font.ToHfont();
                this.oldFont = GDIFunctions.SelectObject(this.hdc, this.fontH);
                if (rtl)
                {
                    GDIFunctions.SetLayout(this.hdc, 1);
                }
            }
            catch
            {
                this.gr.ReleaseHdc(this.hdc);
                this.gr.Dispose();
                throw;
            }
        }

        [SecuritySafeCritical]
        private void InitializeHandles(int strLength)
        {
            this.orderHandle = GCHandle.Alloc(new int[strLength], GCHandleType.Pinned);
            this.dxHandle = GCHandle.Alloc(new int[strLength], GCHandleType.Pinned);
            this.caretPosHandle = GCHandle.Alloc(new int[strLength], GCHandleType.Pinned);
            this.classHandle = GCHandle.Alloc(new byte[strLength], GCHandleType.Pinned);
            this.glyphsHandle = GCHandle.Alloc(new ushort[strLength], GCHandleType.Pinned);
        }

        private static bool IsWritingOrderControl(char c) => 
            (c >= '‪') && (c <= '‮');

        private static Dictionary<ushort, char[]> PrepareCharMap(string orignialString, int[] order, ushort[] glyphs)
        {
            Dictionary<ushort, char[]> result = new Dictionary<ushort, char[]>();
            List<char> chars = new List<char>(3) {
                orignialString[0]
            };
            int index = order[0];
            for (int i = 1; i < order.Length; i++)
            {
                if (index == order[i])
                {
                    chars.Add(orignialString[i]);
                }
                else
                {
                    AssignMap(result, glyphs[index], chars);
                    chars.Clear();
                    chars.Add(orignialString[i]);
                    index = order[i];
                }
            }
            AssignMap(result, glyphs[index], chars);
            return result;
        }

        [SecuritySafeCritical]
        private GCP_RESULTS PrepareGCP_RESULTS(int strLength) => 
            new GCP_RESULTS { 
                lStructSize = (uint) Marshal.SizeOf(typeof(GCP_RESULTS)),
                lpOutString = new string('\0', strLength),
                lpOrder = this.orderHandle.AddrOfPinnedObject(),
                lpDx = this.dxHandle.AddrOfPinnedObject(),
                lpCaretPos = this.caretPosHandle.AddrOfPinnedObject(),
                lpClass = this.classHandle.AddrOfPinnedObject(),
                lpGlyphs = this.glyphsHandle.AddrOfPinnedObject(),
                nGlyphs = (uint) strLength,
                nMaxFit = 0
            };

        private static ushort[] PrepareGlyphs(string originalString, uint actualGlyphCount, ushort[] originalGlyphs, int[] order)
        {
            ushort[] numArray;
            List<int> list = new List<int>();
            for (int i = 0; i < order.Length; i++)
            {
                if (IsWritingOrderControl(originalString[i]))
                {
                    list.Add(order[i]);
                }
            }
            if (list.Count == 0)
            {
                numArray = new ushort[actualGlyphCount];
                Array.Copy(originalGlyphs, numArray, (long) actualGlyphCount);
            }
            else
            {
                numArray = new ushort[actualGlyphCount - list.Count];
                list.Sort();
                int num2 = 0;
                int num3 = 0;
                for (int j = 0; j < actualGlyphCount; j++)
                {
                    if ((num2 < list.Count) && (list[num2] == j))
                    {
                        num2++;
                    }
                    else
                    {
                        numArray[num3++] = originalGlyphs[j];
                    }
                }
            }
            return numArray;
        }

        public TextRun ProcessString(string str) => 
            this.ProcessString(str, this.hdc);

        [SecuritySafeCritical]
        private TextRun ProcessString(string str, IntPtr hdc)
        {
            TextRun run2;
            int length = str.Length;
            this.InitializeHandles(length);
            try
            {
                GCP_RESULTS lpResults = this.PrepareGCP_RESULTS(length);
                TextRun run = new TextRun();
                if (GDIFunctions.GetCharacterPlacement(hdc, str, length, 0, ref lpResults, 0x22) == 0)
                {
                    run.Text = str;
                }
                else
                {
                    run.Text = lpResults.lpOutString;
                    run.Glyphs = PrepareGlyphs(str, lpResults.nGlyphs, (ushort[]) this.glyphsHandle.Target, (int[]) this.orderHandle.Target);
                    run.CharMap = PrepareCharMap(str, (int[]) this.orderHandle.Target, (ushort[]) this.glyphsHandle.Target);
                }
                run2 = run;
            }
            finally
            {
                this.FreeHandles();
            }
            return run2;
        }
    }
}

