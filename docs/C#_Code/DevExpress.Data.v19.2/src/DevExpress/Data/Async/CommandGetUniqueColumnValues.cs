namespace DevExpress.Data.Async
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;

    public class CommandGetUniqueColumnValues : Command
    {
        public CriteriaOperator ValuesExpression;
        public int MaxCount;
        public CriteriaOperator FilterExpression;
        public bool IgnoreAppliedFilter;
        public object Values;

        public CommandGetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

