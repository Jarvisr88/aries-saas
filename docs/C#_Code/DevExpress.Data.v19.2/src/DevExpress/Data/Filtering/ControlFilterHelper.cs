namespace DevExpress.Data.Filtering
{
    using System;

    public abstract class ControlFilterHelper : ControlFilterHelper<object>
    {
        protected ControlFilterHelper();
        protected sealed override Func<object, bool> CreateFilterFitPredicate(CriteriaOperator criteria);
        protected abstract IControlFilterColumnsProvider GetFilterColumnsProvider();
    }
}

