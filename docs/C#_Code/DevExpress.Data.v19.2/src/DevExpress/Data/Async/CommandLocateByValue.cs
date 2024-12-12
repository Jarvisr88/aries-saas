namespace DevExpress.Data.Async
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;

    public class CommandLocateByValue : Command
    {
        public CriteriaOperator Expression;
        public object Value;
        public int StartIndex;
        public bool SearchUp;
        public int RowIndex;

        public CommandLocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

