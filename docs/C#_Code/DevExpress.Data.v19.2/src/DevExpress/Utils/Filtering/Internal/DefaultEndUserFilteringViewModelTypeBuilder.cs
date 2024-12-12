namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class DefaultEndUserFilteringViewModelTypeBuilder : IEndUserFilteringViewModelTypeBuilder
    {
        internal static readonly IEndUserFilteringViewModelTypeBuilder Instance;
        private IDictionary<int, Type> typesCache;
        internal const string FilteringViewModel = "FilteringViewModel";
        private static OpCode[] args;
        private IDictionary<Type, IList<FieldInfo>> backingFields;
        private IDictionary<Type, IList<ConstructorInfo>> backingFieldConstructors;
        private static MethodInfo getItemMethodInfo;
        private static MethodInfo getValueMethodInfo;
        private const string Error_ValuesFieldNotFound = "Class already supports IEndUserFilteringViewModel, but field for values is not found: {0}.";
        private const string Error_PropertyShouldMatchPropertyType = "Property {0}.{1} should match property type: {2}.";
        private const string Error_NestedPropertyShouldMatchNestedType = "Property {0}.{1} should match nested type: {2}.";

        static DefaultEndUserFilteringViewModelTypeBuilder();
        private DefaultEndUserFilteringViewModelTypeBuilder();
        private FieldBuilder BuildBackingField(TypeBuilder typebuilder, string propertyName, Type propertyType);
        private void BuildBackingFieldsInitialization(TypeBuilder typeBuilder, ILGenerator generator);
        private void BuildBackingFieldsInitializationForNestedType(TypeBuilder typeBuilder, ILGenerator generator);
        private void BuildConstructors(Type baseType, TypeBuilder typeBuilder, params FieldInfo[] fields);
        private MethodBuilder BuildInitializeMethod(TypeBuilder typeBuilder, FieldInfo valuesField);
        private static PropertyBuilder BuildNestedProperty(FieldInfo backingField, TypeBuilder typebuilder, string propertyName, Type propertyType);
        private static void BuildNestedPropertyAttributes(PropertyBuilder propertyBuilder);
        private static MethodBuilder BuildNestedPropertyGetter(FieldInfo backingField, TypeBuilder typeBuilder, string propertyName, Type propertyType);
        private void BuildProperties(IEndUserFilteringViewModelProperties properties, IEndUserFilteringViewModelPropertyValues values, FieldInfo valuesField, TypeBuilder typebuilder, Type baseType, string rootPath = null);
        private static PropertyBuilder BuildProperty(FieldInfo valuesField, TypeBuilder typebuilder, string propertyName, Type propertyType, string valuePath);
        private static void BuildPropertyAttributes(PropertyBuilder propertyBuilder, IEndUserFilteringMetric metric);
        private static MethodBuilder BuildPropertyGetter(FieldInfo valuesField, TypeBuilder typeBuilder, string propertyName, Type propertyType, string valuePath);
        private void CreateConstructor(ConstructorInfo cInfo, TypeBuilder typeBuilder, FieldInfo[] fields = null);
        private static FieldInfo DefineValuesField(TypeBuilder typeBuilder, Type baseType);
        Type IEndUserFilteringViewModelTypeBuilder.Create(Type baseType, IEndUserFilteringViewModelProperties properties, IEndUserFilteringViewModelPropertyValues values);
        private static void EmitLdargs(Array parameters, ILGenerator generator, int start = 0);
        private static void EmitLdargsAndStfld(FieldInfo[] fields, ILGenerator generator);
        private static string GetDynamicTypeName(string typeName, int hash);
        private static string GetDynamicTypeName(Type baseType, int hash);
        internal static int GetHash(Type baseType, IEnumerable<KeyValuePair<string, Type>> properties);
        internal static int GetHash(Type baseType, IEnumerable<KeyValuePair<string, Type>> properties, string rootPath);
        private static Type[] GetParameterTypes(MethodBase method, FieldInfo[] fields);
        private static string GetPropertyPath(ref string rootPath, KeyValuePair<string, Type> property);
        private static TypeBuilder GetTypeBuilder(Type baseType, int hash);
        private Type GetTypeOrCache(int hash, Func<int, Type> createType);
        private void ImplementIEndUserFilteringViewModel(TypeBuilder typeBuilder, FieldInfo valuesField);
        private Assembly OnTypeResolve(object sender, ResolveEventArgs args);
        private void RegisterBackingField(TypeBuilder typebuilder, FieldBuilder backingField);
        private void RegisterBackingFieldConstructor(TypeBuilder typeBuilder, ConstructorBuilder ctorBuilder);
        private static bool Throw(string format, Type type);
        private static bool Throw(string format, params object[] parameters);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultEndUserFilteringViewModelTypeBuilder.<>c <>9;
            public static Func<FieldInfo, Type> <>9__13_0;
            public static Func<ParameterInfo, Type> <>9__14_0;
            public static Func<FieldInfo, Type> <>9__14_1;
            public static Func<FieldInfo, bool> <>9__19_0;

            static <>c();
            internal Type <CreateConstructor>b__13_0(FieldInfo f);
            internal bool <DefineValuesField>b__19_0(FieldInfo f);
            internal Type <GetParameterTypes>b__14_0(ParameterInfo p);
            internal Type <GetParameterTypes>b__14_1(FieldInfo f);
        }

        private class EndUserFilteringViewModelTypeBuilderException : NotSupportedException
        {
            public EndUserFilteringViewModelTypeBuilderException(string message);
        }

        private static class NestedPropertiesHelper
        {
            internal static string GetRootPath(ref string path);
            internal static bool HasRootPath(string path);
        }
    }
}

