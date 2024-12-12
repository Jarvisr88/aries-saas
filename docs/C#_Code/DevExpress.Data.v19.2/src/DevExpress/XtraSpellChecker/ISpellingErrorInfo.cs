namespace DevExpress.XtraSpellChecker
{
    using DevExpress.XtraSpellChecker.Parser;
    using System;

    public interface ISpellingErrorInfo
    {
        SpellingError Error { get; }

        Position WordStartPosition { get; }

        Position WordEndPosition { get; }

        string Word { get; }
    }
}

