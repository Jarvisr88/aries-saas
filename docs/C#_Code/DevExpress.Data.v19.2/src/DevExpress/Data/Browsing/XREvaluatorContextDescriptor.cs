namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class XREvaluatorContextDescriptor : EvaluatorContextDescriptor
    {
        protected readonly DataContext dataContext;
        protected readonly object dataSource;
        protected readonly string dataMember;
        private readonly Dictionary<string, PropertyDescriptor> propertyDescriptors;
        private readonly Dictionary<string, EvaluatorContextDescriptor> contextDescriptors;
        private readonly Dictionary<string, IEvaluatorPropertyHandler> handlers;
        private readonly IEvaluatorPropertyHandler fallbackHandler;

        protected XREvaluatorContextDescriptor(DataContext dataContext, object dataSource, string dataMember);
        public XREvaluatorContextDescriptor(DataContext dataContext, object dataSource, string dataMember, IDictionary<string, IEvaluatorPropertyHandler> handlers);
        public XREvaluatorContextDescriptor(IEnumerable<IParameter> parameters, DataContext dataContext, object dataSource, string dataMember);
        private static IEnumerable GetCollection(object nestedSource);
        public override IEnumerable GetCollectionContexts(object source, string collectionName);
        private EvaluatorContextDescriptor GetContextDescriptor(string propertyName);
        private static object GetFirstItemForCollection(object nestedSource);
        private static object GetFirstItemForEnumerable(IEnumerable enumerable);
        public override EvaluatorContext GetNestedContext(object source, string propertyName);
        internal object GetParameterValue(string name);
        private PropertyDescriptor GetPropertyDescriptor(object source, string propertyName);
        public override object GetPropertyValue(object source, EvaluatorProperty propertyPath);
        private object GetPropertyValue(object source, string propertyName);
        private object GetValueByName(EvaluatorProperty propertyPath);
        private object GetValueFromFallbackHandler(EvaluatorProperty property);

        public override bool IsTopLevelCollectionSource { get; }
    }
}

