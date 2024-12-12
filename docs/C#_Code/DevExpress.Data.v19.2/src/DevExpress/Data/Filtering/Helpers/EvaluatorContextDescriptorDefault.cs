namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class EvaluatorContextDescriptorDefault : EvaluatorContextDescriptor
    {
        private readonly Type ReflectionType;
        private readonly PropertyDescriptorCollection Properties;
        public IEvaluatorDataAccess DataAccess;
        private static object noResult;

        static EvaluatorContextDescriptorDefault();
        public EvaluatorContextDescriptorDefault(PropertyDescriptorCollection properties);
        public EvaluatorContextDescriptorDefault(Type reflectionType);
        public EvaluatorContextDescriptorDefault(PropertyDescriptorCollection properties, Type reflectionType);
        public override IEnumerable GetCollectionContexts(object source, string collectionName);
        public override EvaluatorContext GetNestedContext(object source, string propertyPath);
        public override object GetPropertyValue(object source, EvaluatorProperty propertyPath);
        private object GetPropertyValue(object source, string property, bool isPath);
        public override IEnumerable GetQueryContexts(object source, string collectionTypeName, CriteriaOperator condition, int top);
    }
}

