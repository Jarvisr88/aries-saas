namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.ContentGeneration.Interop;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class UniscribeShaper : DXCTLShaper
    {
        private const float measurementFontSize = 72f;
        private readonly Graphics context = Graphics.FromHwnd(IntPtr.Zero);
        private readonly Font font;
        private readonly double contextDpi;
        private readonly Lazy<SCRIPT_PROPERTIES[]> scriptProperties;
        private IntPtr hFont;
        private IntPtr cache;
        private IntPtr hdc;

        [SecuritySafeCritical]
        public UniscribeShaper(Font originalFont)
        {
            this.font = new Font(originalFont.FontFamily, 72f, originalFont.Style);
            this.hFont = this.font.ToHfont();
            this.contextDpi = this.context.DpiX;
            this.hdc = this.context.GetHdc();
            Gdi32Interop.SelectObject(this.hdc, this.hFont);
            this.scriptProperties = new Lazy<SCRIPT_PROPERTIES[]>(new Func<SCRIPT_PROPERTIES[]>(UniscribeInterop.ScriptGetProperties));
        }

        private static DXTextRunCollection<UniscribeTextRun> CreateRuns(string text, bool directionRightToLeft)
        {
            OPENTYPE_TAG[] scriptTags;
            SCRIPT_CONTROL psControl = new SCRIPT_CONTROL();
            SCRIPT_STATE psState = new SCRIPT_STATE {
                BidiLevel = directionRightToLeft ? ((byte) 1) : ((byte) 0)
            };
            IList<SCRIPT_ITEM> items = UniscribeInterop.ScriptItemize(text, ref psControl, ref psState, out scriptTags);
            DXTextRunCollection<UniscribeTextRun> runs = new DXTextRunCollection<UniscribeTextRun>(new UniscribeTextRun(text, 0, text.Length));
            for (int i = 0; i < items.Count; i++)
            {
                int length;
                int iCharPos = items[i].ICharPos;
                if (i == (items.Count - 1))
                {
                    length = text.Length;
                }
                else
                {
                    SCRIPT_ITEM script_item = items[i + 1];
                    length = script_item.ICharPos;
                }
                runs.UpdateRunProperties(iCharPos, length - iCharPos, delegate (UniscribeTextRun run) {
                    run.Script = items[i].Analysis;
                    run.ScriptTag = scriptTags[i];
                });
            }
            return runs;
        }

        public override void Dispose()
        {
            this.Dispose(true);
            base.Dispose();
        }

        [SecuritySafeCritical]
        private void Dispose(bool disposing)
        {
            if (this.hdc != IntPtr.Zero)
            {
                try
                {
                    this.context.ReleaseHdc(this.hdc);
                }
                catch
                {
                }
                this.hdc = IntPtr.Zero;
            }
            if (this.hFont != IntPtr.Zero)
            {
                Gdi32Interop.DeleteObject(this.hFont);
                this.hFont = IntPtr.Zero;
            }
            if (this.cache != IntPtr.Zero)
            {
                UniscribeInterop.ScriptFreeCache(this.cache);
                this.cache = IntPtr.Zero;
            }
            if (disposing)
            {
                this.font.Dispose();
                this.context.Dispose();
            }
        }

        ~UniscribeShaper()
        {
            this.Dispose(false);
        }

        [SecuritySafeCritical]
        public override IList<DXCluster> GetTextRuns(string text, bool directionRightToLeft, float fontSizeInPoints, bool useKerning)
        {
            DXTextRunCollection<UniscribeTextRun> runs = CreateRuns(text, directionRightToLeft);
            SetLineBreaks(text, runs);
            return (from r in runs.Runs select this.ShapeRun(this.hdc, r, fontSizeInPoints, useKerning)).ToList<DXCluster>();
        }

        public override IEnumerable<IDXTextRun> Itemize(string text, bool directionRightToLeft)
        {
            DXTextRunCollection<UniscribeTextRun> runs = CreateRuns(text, directionRightToLeft);
            SetLineBreaks(text, runs);
            return (IEnumerable<IDXTextRun>) runs.Runs;
        }

        private static void SetLineBreaks(string text, DXTextRunCollection<UniscribeTextRun> runs)
        {
            DXLineBreakpoint[] breakpoints = new DXLineBreakpoint[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                breakpoints[i] = new DXLineBreakpoint(DXBreakCondition.MayNotBreak, char.IsWhiteSpace(c), c == '\x00ad');
            }
            bool whiteSpace = false;
            foreach (UniscribeTextRun run in runs.Runs)
            {
                SCRIPT_ANALYSIS script = run.Script;
                SCRIPT_LOGATTR[] script_logattrArray = UniscribeInterop.ScriptBreak(run.Text.GetText(), ref script);
                if (((script_logattrArray.Length != 0) & whiteSpace) && !script_logattrArray[0].WhiteSpace)
                {
                    UpdateBreakpoint(breakpoints, run.Offset - 1);
                }
                for (int j = 0; j < script_logattrArray.Length; j++)
                {
                    if (script_logattrArray[j].SoftBreak)
                    {
                        int index = (run.Offset + j) - 1;
                        if (index >= 0)
                        {
                            UpdateBreakpoint(breakpoints, index);
                        }
                    }
                    whiteSpace = script_logattrArray[j].WhiteSpace;
                }
            }
            foreach (UniscribeTextRun run2 in runs.Runs)
            {
                run2.Breakpoints = breakpoints;
            }
        }

        private static IList<DXCluster> ShapeNonVisualRun(UniscribeTextRun run)
        {
            IList<DXCluster> list = new DXCluster[run.Length];
            int num = run.Offset;
            StringView text = run.Text;
            for (int i = 0; i < run.Length; i++)
            {
                DXGlyphOffset offset = new DXGlyphOffset();
                DXGlyph[] glyphs = new DXGlyph[] { new DXGlyph(0, 0f, offset) };
                list[i] = new DXCluster(glyphs, text.SubView(i, 1), run.Breakpoints[num + i], run.BidiLevel, text[i] == '\t');
            }
            return list;
        }

        private IList<DXCluster> ShapeRun(IntPtr hdc, UniscribeTextRun run, float fontSize, bool useKerning)
        {
            short[] numArray;
            short[] numArray2;
            SCRIPT_CHARPROP[] script_charpropArray;
            SCRIPT_GLYPHPROP[] script_glyphpropArray;
            int num;
            int[] numArray3;
            GOFFSET[] goffsetArray;
            SCRIPT_ANALYSIS script = run.Script;
            if (this.scriptProperties.Value[script.ScriptId].Control)
            {
                return ShapeNonVisualRun(run);
            }
            StringView text = run.Text;
            OPENTYPE_TAG tagLangSys = new OPENTYPE_TAG();
            OpenTypeFeatureInfo featuresInfo = OpenTypeFeatureInfo.CreateKerningInfo(text.Length, useKerning);
            script.LogicalOrder = true;
            string str = text.GetText();
            UniscribeInterop.ScriptShape(hdc, ref this.cache, run.ScriptTag, tagLangSys, featuresInfo, str, ref script, out numArray, out script_charpropArray, out numArray2, out script_glyphpropArray, out num);
            UniscribeInterop.ScriptPlace(hdc, ref this.cache, run.ScriptTag, tagLangSys, featuresInfo, str, numArray, script_charpropArray, numArray2, num, script_glyphpropArray, ref script, out numArray3, out goffsetArray);
            List<IList<DXGlyph>> list = new List<IList<DXGlyph>>();
            List<DXGlyph> item = new List<DXGlyph>(1);
            float num2 = (float) (((double) (fontSize * 72f)) / (this.contextDpi * 72.0));
            for (int i = 0; i < num; i++)
            {
                if (script_glyphpropArray[i].IsClusterStart && (item.Count != 0))
                {
                    list.Add(item);
                    item = new List<DXGlyph>(1);
                }
                GOFFSET goffset = goffsetArray[i];
                float num7 = ((script.State.BidiLevel % 2) != 0) ? -num2 : num2;
                item.Add(new DXGlyph((ushort) numArray2[i], numArray3[i] * num2, new DXGlyphOffset(goffset.HorizontalOffset * num7, goffset.VerticalOffset * num2)));
            }
            if (item.Count != 0)
            {
                list.Add(item);
            }
            List<DXCluster> list3 = new List<DXCluster>();
            int num3 = numArray[0];
            int splitOffset = 0;
            int num5 = 0;
            for (int j = 0; j < text.Length; j++)
            {
                if (numArray[j] != num3)
                {
                    num3 = numArray[j];
                    int num9 = splitOffset;
                    int length = j - splitOffset;
                    list3.Add(new DXCluster(list[num5++], text.SubView(num9, length), run.GetBreakpoint((num9 + length) - 1), run.BidiLevel, false));
                    splitOffset = j;
                }
            }
            if ((text.Length - splitOffset) > 0)
            {
                list3.Add(new DXCluster(list[num5], text.SubView(splitOffset), run.GetBreakpoint(text.Length - 1), run.BidiLevel, false));
            }
            return list3;
        }

        private static void UpdateBreakpoint(DXLineBreakpoint[] breakpoints, int index)
        {
            DXLineBreakpoint breakpoint = breakpoints[index];
            breakpoints[index] = new DXLineBreakpoint(DXBreakCondition.CanBreak, breakpoint.IsWhitespace, breakpoint.IsSoftHyphen);
        }

        private class UniscribeTextRun : IDXTextRun<UniscribeShaper.UniscribeTextRun>, IDXTextRun
        {
            public UniscribeTextRun(string text, int offset, int length)
            {
                this.Text = new StringView(text, offset, length);
                this.<Offset>k__BackingField = offset;
                this.Length = length;
            }

            private UniscribeTextRun(int offset, int length, StringView text, SCRIPT_ANALYSIS script, OPENTYPE_TAG scriptTag, DXLineBreakpoint[] breakpoints)
            {
                this.Text = text;
                this.<Offset>k__BackingField = offset;
                this.Length = length;
                this.Script = script;
                this.Breakpoints = breakpoints;
                this.ScriptTag = scriptTag;
            }

            public DXLineBreakpoint GetBreakpoint(int index) => 
                this.Breakpoints[index + this.Offset];

            public UniscribeShaper.UniscribeTextRun Split(int splitOffset)
            {
                int length = this.Length;
                this.Length = splitOffset - this.Offset;
                StringView text = this.Text;
                this.Text = text.SubView(0, this.Length);
                return new UniscribeShaper.UniscribeTextRun(splitOffset, length - this.Length, text.SubView(splitOffset - this.Offset), this.Script, this.ScriptTag, this.Breakpoints);
            }

            public int Offset { get; }

            public int Length { get; private set; }

            public SCRIPT_ANALYSIS Script { get; set; }

            public OPENTYPE_TAG ScriptTag { get; set; }

            public DXLineBreakpoint[] Breakpoints { get; set; }

            public StringView Text { get; private set; }

            public byte BidiLevel =>
                this.Script.State.BidiLevel;
        }
    }
}

