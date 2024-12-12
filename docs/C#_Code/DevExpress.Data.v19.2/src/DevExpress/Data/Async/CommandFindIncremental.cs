namespace DevExpress.Data.Async
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;

    public class CommandFindIncremental : Command
    {
        public CriteriaOperator Expression;
        public string Value;
        public int StartIndex;
        public bool SearchUp;
        public bool IgnoreStartRow;
        public bool AllowLoop;
        public int RowIndex;

        public CommandFindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

