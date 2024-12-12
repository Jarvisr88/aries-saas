namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;

    public abstract class EvaluatorContextDescriptor
    {
        protected EvaluatorContextDescriptor();
        public abstract IEnumerable GetCollectionContexts(object source, string collectionName);
        public abstract EvaluatorContext GetNestedContext(object source, string propertyPath);
        public abstract object GetPropertyValue(object source, EvaluatorProperty propertyPath);
        public virtual IEnumerable GetQueryContexts(object source, string queryTypeName, CriteriaOperator condition, int top);

        public virtual bool IsTopLevelCollectionSource { get; }
    }
}

