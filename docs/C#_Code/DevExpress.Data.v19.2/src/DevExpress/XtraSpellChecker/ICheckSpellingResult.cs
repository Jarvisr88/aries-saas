namespace DevExpress.XtraSpellChecker
{
    using System;

    public interface ICheckSpellingResult
    {
        string Text { get; }

        bool HasError { get; }

        int Index { get; }

        int Length { get; }

        string Value { get; }

        CheckSpellingResultType Result { get; }
    }
}

