namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;

    public class GetUniqueValuesEventArgs : HandledEventArgs
    {
        public readonly CriteriaOperator ValuesExpression;
        public readonly int MaxCount;
        public readonly CriteriaOperator FilterExpression;

        public GetUniqueValuesEventArgs(CriteriaOperator valuesExpression, CriteriaOperator filterExpression, int maxCount);

        public string ValuesPropertyName { get; }
    }
}

