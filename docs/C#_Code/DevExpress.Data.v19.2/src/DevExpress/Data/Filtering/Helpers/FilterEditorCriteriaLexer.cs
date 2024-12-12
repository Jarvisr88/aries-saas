namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.IO;

    internal class FilterEditorCriteriaLexer : CriteriaLexer
    {
        public FilterEditorCriteriaLexer(TextReader reader);
        public override void YYError(string message, params object[] args);
    }
}

