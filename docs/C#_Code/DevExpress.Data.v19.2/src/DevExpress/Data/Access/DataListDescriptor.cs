namespace DevExpress.Data.Access
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DataListDescriptor
    {
        public static bool SkipSetNullToValueProps;
        public static int NullDisagreeDiagExceptionLevel;
        private static readonly ConcurrentDictionary<Tuple<Type, string>, Tuple<Func<object, object>, Action<object, object>>> accessorsCache;

        static DataListDescriptor();
        public static PropertyDescriptorCollection GetFastProperties(PropertyDescriptorCollection sourceCollection);
        public static PropertyDescriptorCollection GetFastProperties(Type type);
        public static PropertyDescriptor GetFastProperty(PropertyDescriptor source);
        private static Tuple<Func<object, object>, Action<object, object>> TryCreateAccessors(Type rowType, string propertyName);
        private static Func<object, object> TryCreateFastGetter(PropertyInfo property);
        private static DataListDescriptor.FastPropertyDescriptor TryCreateFastProperty(PropertyDescriptor source);
        private static Action<object, object> TryCreateFastSetter(PropertyInfo property, Func<object, object> diagGetter);
        private static Func<object, object> TryCreateSecureGetter(PropertyInfo property);
        private static Action<object, object> TryCreateSecureSetter(PropertyInfo property, Func<object, object> diagGetter);
        private static Action<object, object> WrapNullAssignementForValueTypes(Action<object, object> assignment, PropertyInfo pi, Func<object, object> diagGetter);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataListDescriptor.<>c <>9;
            public static Func<Tuple<Type, string>, Tuple<Func<object, object>, Action<object, object>>> <>9__13_0;

            static <>c();
            internal Tuple<Func<object, object>, Action<object, object>> <TryCreateFastProperty>b__13_0(Tuple<Type, string> k);
        }

        private class FastPropertyDescriptor : PropertyDescriptor, PropertyDescriptorCriteriaCompilationSupport.IHelper, IHasDefaultValue
        {
            private PropertyDescriptor source;
            private Action<object, object> setter;
            private Func<object, object> getter;
            private Type componentType;
            private Type propertyType;
            private bool? hasDefaultValue;
            private object defaultValue;

            public FastPropertyDescriptor(PropertyDescriptor source, Func<object, object> getter, Action<object, object> setter);
            public override bool CanResetValue(object component);
            Delegate PropertyDescriptorCriteriaCompilationSupport.IHelper.TryGetFastGetter(out Type rowType, out Type valueType);
            Expression PropertyDescriptorCriteriaCompilationSupport.IHelper.TryMakeFastExpression(Expression baseExpression);
            public override object GetValue(object component);
            public override void ResetValue(object component);
            public override void SetValue(object component, object value);
            public override bool ShouldSerializeValue(object component);
            private bool TryGetDefaultValue(out object value);

            protected PropertyDescriptor Source { get; }

            public bool HasDefaultValue { get; }

            public object DefaultValue { get; }

            public override bool IsReadOnly { get; }

            public override string DisplayName { get; }

            public override string Category { get; }

            public override Type PropertyType { get; }

            public override Type ComponentType { get; }
        }
    }
}

