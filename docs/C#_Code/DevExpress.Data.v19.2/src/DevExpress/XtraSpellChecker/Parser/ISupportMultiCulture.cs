namespace DevExpress.XtraSpellChecker.Parser
{
    using System;
    using System.Globalization;

    public interface ISupportMultiCulture
    {
        CultureInfo GetCulture(Position start, Position end);
        bool ShouldCheckWord(Position start, Position end);
    }
}

