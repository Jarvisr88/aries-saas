namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class WordFormatter : IDisposable
    {
        private const string ellipsis = "...";
        private bool isDisposed;
        private float width;
        private float height;
        private System.Drawing.Font font;
        private FontMetrics fontMetrics;
        private GraphicsUnit pageUnit;
        private System.Drawing.StringFormat stringFormat;
        private int additionalLineCount;
        private string line;
        private WordFormatMode mode;
        private List<string> lines;
        private Measurer measurer;
        private bool disposeGDIResources;

        public WordFormatter(float width, float height, System.Drawing.Font font, System.Drawing.StringFormat stringFormat, GraphicsUnit pageUnit);
        public WordFormatter(float width, float height, System.Drawing.Font font, System.Drawing.StringFormat stringFormat, GraphicsUnit pageUnit, Measurer measurer, int additionalLineCount);
        public WordFormatter(float width, float height, System.Drawing.Font font, System.Drawing.StringFormat stringFormat, GraphicsUnit pageUnit, Measurer measurer, int additionalLineCount, bool disposeGDIResources);
        private string AppendWord(string line, string word);
        internal static int CompareDoubles(double number1, double number2);
        public virtual WordFormatterAlgorithm CreateAlgorithm();
        public void Dispose();
        private static bool FirstLessOrEqualSecond(double number1, double number2);
        private bool FormatAllLines(List<Word> words);
        private bool FormatFirstLineOnly(List<Word> words);
        protected internal virtual void FormatLastWordOfLastLine(Word lastWord);
        public bool FormatWords(List<Word> words, int totalTextLineCount);
        public string GetLine(Word word);
        internal static bool HasTrimmedByEllipsis(string text);
        private bool IsTextWrapped(string text, float baseWidth);
        protected internal virtual bool LinesOutOfHeight(int lineCount);
        private float MeasureTextWidth(string text);
        private static float MeasureTextWidth(string text, Measurer measurer, System.Drawing.Font font, System.Drawing.StringFormat stringFormat, GraphicsUnit pageUnit);
        public void SetLine(string text);
        private string TrimByEllipsisCharacter(string text);
        internal static string TrimByEllipsisCharacter(string text, Measurer measurer, System.Drawing.Font font, System.Drawing.StringFormat stringFormat, GraphicsUnit pageUnit, float width);
        public void UpdateLines();

        private bool TrimByCharacter { get; }

        internal bool IsDisposed { get; }

        internal float Width { get; }

        internal float Height { get; }

        internal System.Drawing.Font Font { get; }

        internal GraphicsUnit PageUnit { get; }

        internal System.Drawing.StringFormat StringFormat { get; }

        internal int AdditionalLineCount { get; }

        internal bool LineLimit { get; }

        internal string Line { get; set; }

        internal WordFormatMode Mode { get; set; }

        internal List<string> LinesForTest { get; }

        public string[] Lines { get; }

        private string LastLine { get; set; }

        private bool NextLineOutOfHeight { get; }

        protected class WordFormatterCharAlgorithm : WordFormatterAlgorithm
        {
            private string text;
            private int charIndex;

            public WordFormatterCharAlgorithm(WordFormatter formatter);
            private void ApplyChar(char ch);
            public override bool ApplyWordToLines(Word word);
            public override void FormatLastLine(Word word);
            public override void SetNextMode(Word word);
            public override bool SupportsMode(WordFormatMode mode);
        }

        protected class WordFormatterLineAlgorithm : WordFormatterAlgorithm
        {
            public WordFormatterLineAlgorithm(WordFormatter formatter);
            public override bool ApplyWordToLines(Word word);
            public override void FormatLastLine(Word word);
            private WordFormatMode GetNextMode(Word word, WordFormatMode prevMode);
            protected virtual bool GotoNewLineNeeded(Word word, WordFormatMode prevMode);
            public override void SetNextMode(Word word);
            public override bool SupportsMode(WordFormatMode mode);
            private bool TextExceedWidth(string text);
            protected virtual bool TryFormatByCharNeeded(Word word, WordFormatMode prevMode);
        }
    }
}

