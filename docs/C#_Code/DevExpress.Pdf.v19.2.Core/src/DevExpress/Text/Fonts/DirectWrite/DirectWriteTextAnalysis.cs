namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class DirectWriteTextAnalysis : IDWriteTextAnalysisSource, IDWriteTextAnalysisSink
    {
        private readonly DWriteTextAnalyzer textAnalyzer;
        private readonly string localeName;
        private readonly DWRITE_READING_DIRECTION readingDirection;
        private readonly string text;
        private DXTextRunCollection<DirectWriteTextRun> runs;
        private DWRITE_LINE_BREAKPOINT[] breakpoints;

        public DirectWriteTextAnalysis(DWriteTextAnalyzer textAnalyzer, string text, bool rtlParagraph)
        {
            this.textAnalyzer = textAnalyzer;
            this.readingDirection = rtlParagraph ? DWRITE_READING_DIRECTION.RIGHT_TO_LEFT : DWRITE_READING_DIRECTION.LEFT_TO_RIGHT;
            this.text = text;
            this.localeName = null;
            this.runs = new DXTextRunCollection<DirectWriteTextRun>(new DirectWriteTextRun(text, 0, this.TextLength));
            this.breakpoints = new DWRITE_LINE_BREAKPOINT[this.TextLength];
        }

        private void AnalyzeBidi()
        {
            this.textAnalyzer.AnalyzeBidi(this, 0, this.TextLength, this);
        }

        private void AnalyzeLineBreakpoints()
        {
            this.textAnalyzer.AnalyzeLineBreakpoints(this, 0, this.TextLength, this);
        }

        private void AnalyzeScript()
        {
            this.textAnalyzer.AnalyzeScript(this, 0, this.TextLength, this);
        }

        void IDWriteTextAnalysisSink.SetBidiLevel(int textPosition, int textLength, byte explicitLevel, byte resolvedLevel)
        {
            this.runs.UpdateRunProperties(textPosition, textLength, run => run.BidiLevel = resolvedLevel);
        }

        void IDWriteTextAnalysisSink.SetLineBreakpoints(int textPosition, int textLength, DWRITE_LINE_BREAKPOINT[] lineBreakpoints)
        {
            for (int i = textPosition; i < lineBreakpoints.Length; i++)
            {
                this.breakpoints[i] = lineBreakpoints[i - textPosition];
            }
        }

        void IDWriteTextAnalysisSink.SetNumberSubstitution(int textPosition, int textLength, IDWriteNumberSubstitution numberSubstitution)
        {
        }

        void IDWriteTextAnalysisSink.SetScriptAnalysis(int textPosition, int textLength, ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis)
        {
            DWRITE_SCRIPT_ANALYSIS script = scriptAnalysis;
            this.runs.UpdateRunProperties(textPosition, textLength, run => run.Script = script);
        }

        void IDWriteTextAnalysisSource.GetLocaleName(int textPosition, out int textLength, out string localeName)
        {
            textLength = this.TextLength - textPosition;
            localeName = this.localeName;
        }

        void IDWriteTextAnalysisSource.GetNumberSubstitution(int textPosition, out int textLength, out IDWriteNumberSubstitution numberSubstitution)
        {
            throw new Exception();
        }

        DWRITE_READING_DIRECTION IDWriteTextAnalysisSource.GetParagraphReadingDirection() => 
            this.readingDirection;

        void IDWriteTextAnalysisSource.GetTextAtPosition(int textPosition, out string text, out int textLength)
        {
            if (textPosition >= this.TextLength)
            {
                text = null;
                textLength = 0;
            }
            else
            {
                if (textPosition > 0)
                {
                    throw new Exception();
                }
                text = this.text;
                textLength = this.TextLength;
            }
        }

        void IDWriteTextAnalysisSource.GetTextBeforePosition(int textPosition, out string text, out int textLength)
        {
            throw new Exception();
        }

        public static LinkedList<DirectWriteTextRun> GetRuns(DWriteTextAnalyzer textAnalyzer, string text, bool rtlParagraph)
        {
            DirectWriteTextAnalysis analysis = new DirectWriteTextAnalysis(textAnalyzer, text, rtlParagraph);
            analysis.AnalyzeLineBreakpoints();
            foreach (DirectWriteTextRun run in analysis.Runs)
            {
                run.Breakpoints = analysis.Breakpoints;
            }
            analysis.AnalyzeBidi();
            analysis.AnalyzeScript();
            return analysis.Runs;
        }

        public LinkedList<DirectWriteTextRun> Runs =>
            this.runs.Runs;

        internal DWRITE_LINE_BREAKPOINT[] Breakpoints =>
            this.breakpoints;

        private int TextLength =>
            this.text.Length;
    }
}

