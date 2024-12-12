namespace DevExpress.XtraSpellChecker
{
    using DevExpress.XtraSpellChecker.Parser;
    using System;

    public interface IIgnoreItem
    {
        Position Start { get; }

        Position End { get; }

        string Word { get; }
    }
}

