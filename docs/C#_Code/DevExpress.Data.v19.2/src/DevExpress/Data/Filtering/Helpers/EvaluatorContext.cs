namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;

    public class EvaluatorContext
    {
        public readonly EvaluatorContextDescriptor Descriptor;
        public readonly object Source;

        public EvaluatorContext(EvaluatorContextDescriptor descriptor, object source);
        public IEnumerable GetCollectionContexts(string collectionName);
        public EvaluatorContext GetNestedContext(string propertyPath);
        public object GetPropertyValue(EvaluatorProperty propertyPath);
        public IEnumerable GetQueryContexts(string queryTypeName, CriteriaOperator condition, int top);
    }
}

