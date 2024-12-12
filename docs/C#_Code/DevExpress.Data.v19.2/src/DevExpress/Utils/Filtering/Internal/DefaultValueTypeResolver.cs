namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    internal sealed class DefaultValueTypeResolver : IValueTypeResolver
    {
        internal static readonly DefaultValueTypeResolver Instance;
        private static readonly IDictionary<Type, Type> attributeTypesMapping;
        private static readonly IDictionary<Type, Type> viewModelTypesMapping;
        private static readonly IDictionary<Type, Type> valueBoxTypesMapping;
        private static readonly Type ValueType;

        static DefaultValueTypeResolver();
        private DefaultValueTypeResolver();
        Type IValueTypeResolver.GetAttributesType(Type metricTypeDefinition, Type valueType);
        Type IValueTypeResolver.GetValueBoxType(Type metricTypeDefinition, Type valueType);
        Type IValueTypeResolver.GetValueViewModelType(Type metricTypeDefinition, Type valueType);
        private static Type EnsureGenericArgumentType(Type valueType, Type type);
        private static Type GetType(Type typeDefinition, Type valueType, IDictionary<Type, Type> mapping);
        private static bool IsArgumentConstrainedByStruct(Type type);
    }
}

