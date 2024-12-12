namespace DevExpress.XtraPrinting.Native
{
    using System;

    public abstract class WordFormatterAlgorithm
    {
        protected WordFormatter formatter;

        public WordFormatterAlgorithm(WordFormatter formatter);
        public abstract bool ApplyWordToLines(Word word);
        public abstract void FormatLastLine(Word word);
        public abstract void SetNextMode(Word word);
        public abstract bool SupportsMode(WordFormatMode mode);
    }
}

