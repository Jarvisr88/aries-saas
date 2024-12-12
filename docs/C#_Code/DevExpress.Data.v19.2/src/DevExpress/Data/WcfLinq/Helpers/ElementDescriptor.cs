namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ElementDescriptor : EvaluatorContextDescriptor
    {
        private readonly Dictionary<string, Func<object, object>> expressionDict;
        private readonly Dictionary<string, PropertyInfo> propertyDict;
        private readonly ElementTypeResolver typeResolver;
        private readonly ElementDescriptorCache cache;

        public ElementDescriptor(Type elementType, ElementDescriptorCache cache);
        public override IEnumerable GetCollectionContexts(object source, string collectionName);
        public Type GetCriteriaType(CriteriaOperator criteria);
        public override EvaluatorContext GetNestedContext(object source, string propertyPath);
        public override object GetPropertyValue(object source, EvaluatorProperty propertyPath);
        public override IEnumerable GetQueryContexts(object source, string queryTypeName, CriteriaOperator condition, int top);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ElementDescriptor.<>c <>9;
            public static Func<KeyValuePair<string, PropertyInfo>, string> <>9__4_0;
            public static Func<KeyValuePair<string, PropertyInfo>, Type> <>9__4_1;

            static <>c();
            internal string <.ctor>b__4_0(KeyValuePair<string, PropertyInfo> pair);
            internal Type <.ctor>b__4_1(KeyValuePair<string, PropertyInfo> pair);
        }
    }
}

