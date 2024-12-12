namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ServerModeCoreExtendable : ServerModeCore
    {
        protected readonly ServerModeCoreExtender Extender;

        public event EventHandler<CustomGetUniqueValuesEventArgs> CustomGetUniqueValues;

        protected ServerModeCoreExtendable(CriteriaOperator[] key, ServerModeCoreExtender extender);
        protected sealed override ServerModeCache CreateCacheCore();
        protected abstract ServerModeKeyedCacheExtendable CreateCacheCoreExtendable();
        protected sealed override object[] GetUniqueValues(CriteriaOperator expression, int maxCount, CriteriaOperator filter);
        protected abstract object[] GetUniqueValuesInternal(CriteriaOperator expression, int maxCount, CriteriaOperator filter);
    }
}

