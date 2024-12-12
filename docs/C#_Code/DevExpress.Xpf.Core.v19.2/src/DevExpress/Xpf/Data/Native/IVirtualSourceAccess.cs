namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Data;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public interface IVirtualSourceAccess
    {
        event EventHandler TotalSummariesCalculated;

        void Apply(SortDefinition[] sortOrder = null, CriteriaOperatorValue? filter = new CriteriaOperatorValue?(), SummaryDefinition[] summaries = null);
        object GetTotalSummaryValue(SummaryDefinition summary);
        void GetUniquePropertyValues(string propertyName, CriteriaOperator filter, Action<Either<object[], ValueAndCount[]>, Exception> onRequestCompleted, bool allowThrottle = true);
        void UpdateRow(int index, Action<UpdateRowResult> onRequestCompleted);
    }
}

