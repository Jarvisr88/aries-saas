namespace DevExpress.Data.Filtering
{
    using DevExpress.Data;
    using System;

    public abstract class ControlFilterHelper<T>
    {
        private Func<T, bool> filterFitPredicate;
        private readonly SubstituteFilterEventArgs substituteFilterArgs;

        protected ControlFilterHelper();
        protected virtual Func<T, bool> CreateFilterFitPredicate(CriteriaOperator criteria);
        protected abstract CriteriaOperator GetFilterCriteria();
        public bool IsFit(T dataObject);
        protected CriteriaOperator RaiseSubstituteFilter(CriteriaOperator criteria);
        protected virtual void RaiseSubstituteFilter(SubstituteFilterEventArgs args);
        protected void ResetFilterFitPredicate();

        protected Func<T, bool> FilterFitPredicate { get; }
    }
}

