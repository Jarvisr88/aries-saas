namespace DevExpress.Data.Utils
{
    using DevExpress.Utils;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AnnotationAttributes
    {
        private Func<IEnumerable<Attribute>> filterAttributesFallback;
        private Lazy<string[]> filterGrouping;
        private Lazy<string> filterGroupingOrigin;
        private static readonly Func<PropertyDescriptor, AnnotationAttributes.AnnotationAttributesProvider> propertyDescriptorAnnotationAttributesProviderInitializer;
        private static readonly Func<AttributeCollection, AnnotationAttributes.AnnotationAttributesProvider> attributeCollectionAnnotationAttributesProviderInitializer;
        private static readonly bool IsConditionallyAPTCAIssueThreat;
        private readonly AnnotationAttributes.AnnotationAttributesProvider provider;
        private readonly object dataSourceNullValue;

        static AnnotationAttributes();
        private AnnotationAttributes(AnnotationAttributes.AnnotationAttributesProvider provider);
        public AnnotationAttributes(IEnumerable<Attribute> attributes);
        public AnnotationAttributes(AttributeCollection attributes);
        public AnnotationAttributes(PropertyDescriptor property);
        public AnnotationAttributes(PropertyDescriptor property, object dataSourceNullValue);
        public bool Any();
        public bool? CheckAddEnumeratorIntegerValues();
        public static bool? CheckAddEnumeratorIntegerValues(AnnotationAttributes annotationAttributes);
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        private static void CheckDataAnnotations_ConditionallyAPTCAIssue<TAttrbute>();
        private object CheckDataSourceNullValue(object value);
        public bool? CheckIsDataColumnAllowNull();
        public bool? CheckIsXpoNullabilityForced();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static void ConditionallyAPTCA(Action unsafeAction);
        internal static bool DenyAttributesCache(PropertyDescriptor descriptor);
        internal static bool DenyExternalAndFluentAttributes();
        private static bool DenyMetadataAttributes();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool EnsureFilterGrouping(out string[] grouping);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool EnsureFilterGroupingOrigin(out string origin);
        public System.ComponentModel.DataAnnotations.DataType? GetActualDataType();
        protected internal static AnnotationAttributes GetAnnotationAttributes(Type filterTypeEnum, Enum id);
        public static bool GetApplyFormatInEditMode(AnnotationAttributes annotationAttributes);
        protected internal AttributeCollection GetAttributes();
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(AnnotationAttributes annotationAttributes);
        public static bool GetAutoGenerateColumn(AnnotationAttributes annotationAttributes);
        public static bool GetAutoGenerateColumnOrFilter(AnnotationAttributes annotationAttributes);
        public static bool GetAutoGenerateFilter(AnnotationAttributes annotationAttributes);
        public static string GetColumnCaption(AnnotationAttributes annotationAttributes);
        public static string GetColumnDescription(AnnotationAttributes annotationAttributes);
        public static int GetColumnIndex(AnnotationAttributes annotationAttributes, int columnIndex = 0);
        public static AnnotationAttributes.ColumnOptions GetColumnOptions(PropertyDescriptor columnDescriptor, int columnIndex, bool readOnly);
        public static AnnotationAttributes.ColumnOptions GetColumnOptions(Type propertyType, AnnotationAttributes columnAttributes, int columnIndex, bool readOnly);
        public static string GetDataFormatString(AnnotationAttributes annotationAttributes);
        public static System.ComponentModel.DataAnnotations.DataType? GetDataType(AnnotationAttributes annotationAttributes);
        public static string GetDescription(AnnotationAttributes annotationAttributes);
        protected internal IEnumerable<Attribute> GetFilterAttributes();
        private IEnumerable<Attribute> GetFilterAttributesCore();
        protected internal FilterGroupAttribute GetFilterGroupAttribute();
        public static string GetGroupName(AnnotationAttributes annotationAttributes);
        public static string GetName(AnnotationAttributes annotationAttributes);
        public static string GetNullDisplayText(AnnotationAttributes annotationAttributes);
        public static string GetShortName(AnnotationAttributes annotationAttributes);
        public AnnotationAttributes Merge(AnnotationAttributes attributes);
        public static void Reset();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetFilterAttributesFallback(Func<IEnumerable<Attribute>> fallback);
        public static bool ShouldHideFieldLabel(AnnotationAttributes annotationAttributes);
        public bool TryValidateValue(object value, out string errorMessage);
        public bool TryValidateValue(object value, object row, out string errorMessage);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string[] FilterGrouping { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string FilterGroupingOrigin { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsNonRootedFilterGroupingOrigin { get; }

        public static DefaultBoolean AllowValidation { get; set; }

        public static DefaultBoolean AllowMetadataAttributes { get; set; }

        public static DefaultBoolean AllowExternalAndFluentAttributes { get; set; }

        public bool HasDisplayAttribute { get; }

        public string Name { get; }

        public string Description { get; }

        public string ShortName { get; }

        public string GroupName { get; }

        public string Prompt { get; }

        public int? Order { get; }

        public bool? AutoGenerateField { get; }

        public bool? AutoGenerateFilter { get; }

        public bool HasDisplayFormatAttribute { get; }

        public bool ApplyFormatInEditMode { get; }

        public bool ConvertEmptyStringToNull { get; }

        public string DataFormatString { get; }

        public string NullDisplayText { get; }

        public bool? IsRequired { get; }

        public bool? AllowEdit { get; }

        public bool? IsReadOnly { get; }

        public System.ComponentModel.DataAnnotations.DataType? DataType { get; }

        public Type EnumType { get; }

        public bool IsKey { get; }

        public string FieldName { get; }

        public AnnotationAttributesKey Key { get; }

        protected bool IsShortNameEmpty { get; }

        protected bool HasShortName { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AnnotationAttributes.<>c <>9;
            public static Func<Attribute, bool> <>9__0_0;
            public static Func<FilterGroupAttribute, bool> <>9__4_0;

            static <>c();
            internal AnnotationAttributes.AnnotationAttributesProvider <.cctor>b__18_0(PropertyDescriptor property);
            internal AnnotationAttributes.AnnotationAttributesProvider <.cctor>b__18_1(AttributeCollection attributes);
            internal AnnotationAttributes.AnnotationAttributesProvider <.cctor>b__18_2(PropertyDescriptor property);
            internal AnnotationAttributes.AnnotationAttributesProvider <.cctor>b__18_3(AttributeCollection attributes);
            internal bool <GetFilterAttributes>b__0_0(Attribute x);
            internal bool <GetFilterGroupAttribute>b__4_0(FilterGroupAttribute a);
        }

        private abstract class AnnotationAttributesProvider
        {
            protected readonly AttributeCollection attributes;
            private Lazy<bool> isKey;

            protected AnnotationAttributesProvider(AttributeCollection attributes);
            public virtual bool Any();
            public virtual bool? CheckAddEnumeratorIntegerValues();
            public virtual bool? CheckIsDataColumnAllowNull();
            public virtual bool? CheckIsXpoNullabilityForced();
            public virtual System.ComponentModel.DataAnnotations.DataType? GetActualDataType();
            public virtual AttributeCollection GetAttributes();
            protected virtual bool GetIsKey();
            public virtual bool TryValidateValue(object value, out string errorMessage);
            public virtual bool TryValidateValue(object value, object row, out string errorMessage);

            public virtual bool HasDisplayAttribute { get; }

            public virtual string Name { get; }

            public virtual string Description { get; }

            public virtual bool IsShortNameEmpty { get; }

            public virtual bool HasShortName { get; }

            public virtual string ShortName { get; }

            public virtual string GroupName { get; }

            public virtual string Prompt { get; }

            public virtual int? Order { get; }

            public virtual bool? AutoGenerateField { get; }

            public virtual bool? AutoGenerateFilter { get; }

            public virtual bool HasDisplayFormatAttribute { get; }

            public virtual bool ApplyFormatInEditMode { get; }

            public virtual bool ConvertEmptyStringToNull { get; }

            public virtual string DataFormatString { get; }

            public virtual string NullDisplayText { get; }

            public virtual bool? IsRequired { get; }

            public virtual bool? AllowEdit { get; }

            public virtual bool? IsReadOnly { get; }

            public virtual System.ComponentModel.DataAnnotations.DataType? DataType { get; }

            public virtual Type EnumType { get; }

            public virtual string FieldName { get; }

            public virtual AnnotationAttributesKey Key { get; }

            public bool IsKey { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly AnnotationAttributes.AnnotationAttributesProvider.<>c <>9;
                public static Func<Attribute, bool> <>9__4_0;

                static <>c();
                internal bool <GetIsKey>b__4_0(Attribute a);
            }
        }

        private sealed class AnnotationAttributesProviderEmpty : AnnotationAttributes.AnnotationAttributesProvider
        {
            internal static AnnotationAttributes.AnnotationAttributesProvider Instance;

            static AnnotationAttributesProviderEmpty();
            private AnnotationAttributesProviderEmpty();
        }

        private sealed class AnnotationAttributesProviderMerged : AnnotationAttributes.AnnotationAttributesProvider
        {
            private readonly IEnumerable<AnnotationAttributes.AnnotationAttributesProvider> providers;

            internal AnnotationAttributesProviderMerged(params AnnotationAttributes.AnnotationAttributesProvider[] providers);
            public override bool Any();
            public override bool? CheckAddEnumeratorIntegerValues();
            public override System.ComponentModel.DataAnnotations.DataType? GetActualDataType();
            public override AttributeCollection GetAttributes();
            private TValue Merge<TValue>(Func<AnnotationAttributes.AnnotationAttributesProvider, TValue> accessor, TValue defaultValue = null);
            public override bool TryValidateValue(object value, out string errorMessage);
            public override bool TryValidateValue(object value, object row, out string errorMessage);

            public override bool HasDisplayAttribute { get; }

            public override string Name { get; }

            public override string Description { get; }

            public override string ShortName { get; }

            public override bool IsShortNameEmpty { get; }

            public override bool HasShortName { get; }

            public override string GroupName { get; }

            public override string Prompt { get; }

            public override int? Order { get; }

            public override bool? AutoGenerateField { get; }

            public override bool? AutoGenerateFilter { get; }

            public override bool HasDisplayFormatAttribute { get; }

            public override bool ApplyFormatInEditMode { get; }

            public override bool ConvertEmptyStringToNull { get; }

            public override string DataFormatString { get; }

            public override string NullDisplayText { get; }

            public override bool? IsRequired { get; }

            public override bool? AllowEdit { get; }

            public override bool? IsReadOnly { get; }

            public override System.ComponentModel.DataAnnotations.DataType? DataType { get; }

            public override Type EnumType { get; }

            public override string FieldName { get; }

            public override AnnotationAttributesKey Key { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly AnnotationAttributes.AnnotationAttributesProviderMerged.<>c <>9;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__5_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, DataType?> <>9__6_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__8_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__10_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__12_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__14_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__16_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__18_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__20_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__22_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, int?> <>9__24_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool?> <>9__26_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool?> <>9__28_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__30_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__32_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool> <>9__34_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__36_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__38_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool?> <>9__40_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool?> <>9__42_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool?> <>9__44_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, DataType?> <>9__46_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, Type> <>9__48_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, string> <>9__50_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, AnnotationAttributesKey> <>9__52_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, bool?> <>9__53_0;
                public static Func<AnnotationAttributes.AnnotationAttributesProvider, AttributeCollection> <>9__54_0;

                static <>c();
                internal bool <Any>b__5_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool? <CheckAddEnumeratorIntegerValues>b__53_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool? <get_AllowEdit>b__42_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool <get_ApplyFormatInEditMode>b__32_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool? <get_AutoGenerateField>b__26_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool? <get_AutoGenerateFilter>b__28_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool <get_ConvertEmptyStringToNull>b__34_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_DataFormatString>b__36_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal DataType? <get_DataType>b__46_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_Description>b__12_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal Type <get_EnumType>b__48_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_FieldName>b__50_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_GroupName>b__20_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool <get_HasDisplayAttribute>b__8_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool <get_HasDisplayFormatAttribute>b__30_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool <get_HasShortName>b__18_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool? <get_IsReadOnly>b__44_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool? <get_IsRequired>b__40_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal bool <get_IsShortNameEmpty>b__16_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal AnnotationAttributesKey <get_Key>b__52_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_Name>b__10_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_NullDisplayText>b__38_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal int? <get_Order>b__24_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_Prompt>b__22_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal string <get_ShortName>b__14_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal DataType? <GetActualDataType>b__6_0(AnnotationAttributes.AnnotationAttributesProvider x);
                internal AttributeCollection <GetAttributes>b__54_0(AnnotationAttributes.AnnotationAttributesProvider x);
            }
        }

        private sealed class AnnotationAttributesProviderReal : AnnotationAttributes.AnnotationAttributesProvider
        {
            private readonly Lazy<DisplayAttribute> displayAttributeValue;
            private readonly Lazy<DisplayNameAttribute> displayNameAttributeValue;
            private readonly Lazy<DescriptionAttribute> descriptionAttributeValue;
            private readonly Lazy<DisplayFormatAttribute> displayFormatAttributeValue;
            private readonly Lazy<bool?> requiredValue;
            private readonly Lazy<bool?> isReadOnlyValue;
            private readonly Lazy<bool?> allowEditValue;
            private readonly Lazy<System.ComponentModel.DataAnnotations.DataType?> dataTypeValue;
            private readonly Lazy<Type> enumTypeValue;
            private readonly Lazy<bool?> enumDataColumnValue;
            private readonly Lazy<bool?> dataColumnAllowNullValue;
            private readonly Lazy<bool?> xpoForcedNullabilityValue;
            private string fieldNameCore;
            private AnnotationAttributesKey keyCore;

            public AnnotationAttributesProviderReal(AttributeCollection attributes);
            public AnnotationAttributesProviderReal(PropertyDescriptor property);
            public override bool Any();
            public override bool? CheckAddEnumeratorIntegerValues();
            public override bool? CheckIsDataColumnAllowNull();
            public override bool? CheckIsXpoNullabilityForced();
            public override System.ComponentModel.DataAnnotations.DataType? GetActualDataType();
            private static string GetFieldName(PropertyDescriptor descriptor);
            private static Lazy<bool?> GetIsDataColumnAllowNull(PropertyDescriptor descriptor);
            private static Lazy<bool?> GetIsEnumDataColumn(PropertyDescriptor descriptor);
            private static Lazy<bool?> GetIsXPONullabilityForced(PropertyDescriptor descriptor);
            private static string GetNonDefaultDescription(DescriptionAttribute attribute);
            private static string GetNonDefaultDisplayName(DisplayNameAttribute attribute);
            private static string GetNonDefaultValue<TAttribute>(TAttribute attribute, Func<TAttribute, string> accessor, TAttribute defaultAttribute) where TAttribute: Attribute;
            private Lazy<TAttribute> Read<TAttribute>() where TAttribute: Attribute;
            private Lazy<TValue> Read<TAttribute, TValue>(Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute;
            private TValue Read<TAttribute, TValue>(Lazy<TAttribute> lazyAttributeValue, Func<TAttribute, TValue> read, TValue defaultValue = null) where TAttribute: Attribute;
            private Lazy<TValue> Read<TAttribute, TValue>(Type attributeType, Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute;
            internal static void Reset();
            private static bool SafeCheck(Func<bool> check, bool defaultResult = true);
            public override bool TryValidateValue(object value, out string errorMessage);
            public override bool TryValidateValue(object value, object row, out string errorMessage);

            public override string FieldName { get; }

            public override AnnotationAttributesKey Key { get; }

            public override bool HasDisplayAttribute { get; }

            public override string Name { get; }

            public override string Description { get; }

            public override string ShortName { get; }

            public override bool IsShortNameEmpty { get; }

            public override bool HasShortName { get; }

            public override string GroupName { get; }

            public override string Prompt { get; }

            public override int? Order { get; }

            public override bool? AutoGenerateField { get; }

            public override bool? AutoGenerateFilter { get; }

            public override bool HasDisplayFormatAttribute { get; }

            public override bool ApplyFormatInEditMode { get; }

            public override bool ConvertEmptyStringToNull { get; }

            public override string DataFormatString { get; }

            public override string NullDisplayText { get; }

            public override bool? IsRequired { get; }

            public override bool? AllowEdit { get; }

            public override bool? IsReadOnly { get; }

            public override System.ComponentModel.DataAnnotations.DataType? DataType { get; }

            public override Type EnumType { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly AnnotationAttributes.AnnotationAttributesProviderReal.<>c <>9;
                public static Func<RequiredAttribute, bool?> <>9__13_0;
                public static Func<ReadOnlyAttribute, bool?> <>9__13_1;
                public static Func<EditableAttribute, bool?> <>9__13_2;
                public static Func<DataTypeAttribute, DataType?> <>9__13_3;
                public static Func<EnumDataTypeAttribute, Type> <>9__13_4;
                public static Func<Attribute, bool> <>9__15_0;
                public static Func<ValidationAttribute, bool> <>9__15_1;
                public static Func<Attribute, bool> <>9__16_0;
                public static Func<ValidationAttribute, bool> <>9__16_1;
                public static Func<ValidationAttribute, bool> <>9__16_2;
                public static Func<ValidationResult, string> <>9__16_4;
                public static Func<DisplayAttribute, string> <>9__35_0;
                public static Func<DisplayNameAttribute, string> <>9__35_1;
                public static Func<DisplayAttribute, string> <>9__37_0;
                public static Func<DescriptionAttribute, string> <>9__37_1;
                public static Func<DisplayAttribute, string> <>9__39_0;
                public static Func<DisplayAttribute, bool> <>9__41_0;
                public static Func<DisplayAttribute, bool> <>9__43_0;
                public static Func<DisplayAttribute, string> <>9__45_0;
                public static Func<DisplayAttribute, string> <>9__47_0;
                public static Func<DisplayAttribute, int?> <>9__49_0;
                public static Func<DisplayAttribute, bool?> <>9__51_0;
                public static Func<DisplayAttribute, bool?> <>9__53_0;
                public static Func<DisplayFormatAttribute, bool> <>9__57_0;
                public static Func<DisplayFormatAttribute, bool> <>9__59_0;
                public static Func<DisplayFormatAttribute, string> <>9__61_0;
                public static Func<DisplayFormatAttribute, string> <>9__63_0;
                public static Func<DisplayNameAttribute, string> <>9__78_0;
                public static Func<DescriptionAttribute, string> <>9__79_0;

                static <>c();
                internal bool? <.ctor>b__13_0(RequiredAttribute x);
                internal bool? <.ctor>b__13_1(ReadOnlyAttribute x);
                internal bool? <.ctor>b__13_2(EditableAttribute x);
                internal DataType? <.ctor>b__13_3(DataTypeAttribute x);
                internal Type <.ctor>b__13_4(EnumDataTypeAttribute x);
                internal bool <get_ApplyFormatInEditMode>b__57_0(DisplayFormatAttribute x);
                internal bool? <get_AutoGenerateField>b__51_0(DisplayAttribute x);
                internal bool? <get_AutoGenerateFilter>b__53_0(DisplayAttribute x);
                internal bool <get_ConvertEmptyStringToNull>b__59_0(DisplayFormatAttribute x);
                internal string <get_DataFormatString>b__61_0(DisplayFormatAttribute x);
                internal string <get_Description>b__37_0(DisplayAttribute x);
                internal string <get_Description>b__37_1(DescriptionAttribute x);
                internal string <get_GroupName>b__45_0(DisplayAttribute x);
                internal bool <get_HasShortName>b__43_0(DisplayAttribute x);
                internal bool <get_IsShortNameEmpty>b__41_0(DisplayAttribute x);
                internal string <get_Name>b__35_0(DisplayAttribute x);
                internal string <get_Name>b__35_1(DisplayNameAttribute x);
                internal string <get_NullDisplayText>b__63_0(DisplayFormatAttribute x);
                internal int? <get_Order>b__49_0(DisplayAttribute x);
                internal string <get_Prompt>b__47_0(DisplayAttribute x);
                internal string <get_ShortName>b__39_0(DisplayAttribute x);
                internal string <GetNonDefaultDescription>b__79_0(DescriptionAttribute a);
                internal string <GetNonDefaultDisplayName>b__78_0(DisplayNameAttribute a);
                internal bool <TryValidateValue>b__15_0(Attribute a);
                internal bool <TryValidateValue>b__15_1(ValidationAttribute a);
                internal bool <TryValidateValue>b__16_0(Attribute a);
                internal bool <TryValidateValue>b__16_1(ValidationAttribute a);
                internal bool <TryValidateValue>b__16_2(ValidationAttribute a);
                internal string <TryValidateValue>b__16_4(ValidationResult r);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__75<TAttribute> where TAttribute: Attribute
            {
                public static readonly AnnotationAttributes.AnnotationAttributesProviderReal.<>c__75<TAttribute> <>9;
                public static Func<TAttribute, TAttribute> <>9__75_0;

                static <>c__75();
                internal TAttribute <Read>b__75_0(TAttribute x);
            }

            private static class DataColumnPropertyDescriptorHelper
            {
                private static Type DataColumnPropertyDescriptorType;
                private static FieldInfo dataColumnFieldInfo;
                private static Type DevartDataColumnPropertyDescriptorType;
                private static FieldInfo devartDataColumnFieldInfo;

                private static DataColumn GetDataColumn(PropertyDescriptor property);
                private static DataColumn GetDevartDataColumn(PropertyDescriptor property);
                internal static Lazy<bool?> GetIsDataColumnAllowNull(PropertyDescriptor property);
                internal static Lazy<bool?> GetIsEnumDataColumn(PropertyDescriptor property);
                private static bool IsDataColumnPropertyDescriptor(Type descriptorType);
                private static bool IsDevartDataColumnPropertyDescriptor(Type descriptorType);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly AnnotationAttributes.AnnotationAttributesProviderReal.DataColumnPropertyDescriptorHelper.<>c <>9;
                    public static Func<FieldInfo, bool> <>9__8_0;

                    static <>c();
                    internal bool <IsDevartDataColumnPropertyDescriptor>b__8_0(FieldInfo x);
                }

                private sealed class WeakClosure
                {
                    private readonly WeakReference dataColumnDescriptor;

                    public WeakClosure(PropertyDescriptor property);
                    internal bool? GetIsDataColumnAllowNull();
                    internal bool? GetIsEnumDataColumn();
                }
            }

            private static class EnterpriseLibraryHelper
            {
                private const string EnterpriseLibraryValidatorTypeName = "Microsoft.Practices.EnterpriseLibrary.Validation.Validators.BaseValidationAttribute";
                private static Type EnterpriseLibraryValidatorType;
                private static Func<Attribute, string> getRulesetRoutine;

                private static bool IsNullOrEmptyRuleset(Attribute attribute, Type attributeType);
                internal static bool IsValidatorWithEmptyOrNullRuleset(Attribute attribute);
            }

            private static class ExternalAndFluentAPIAttributes
            {
                internal static IEnumerable<Attribute> GetAttributes(Type componentType, string memberName);
                private static IEnumerable<Attribute> GetExternalAndFluentAPIAttributes(Type metadataHelperType, Type componentType, string memberName);
                private static Type GetMetadataHelperType();
            }

            private sealed class MetadataAttributesProvider
            {
                private readonly AttributeCollection attributes;
                private static readonly ConcurrentDictionary<AnnotationAttributesKey, AttributeCollection> attributesCache;
                private static readonly Func<AnnotationAttributesKey, AttributeCollection, AttributeCollection> updateCache;

                static MetadataAttributesProvider();
                public MetadataAttributesProvider(PropertyDescriptor descriptor);
                private static AttributeCollection EnsureMetadataAttributes(PropertyDescriptor descriptor);
                internal static void Reset();

                public AttributeCollection Attributes { get; }

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly AnnotationAttributes.AnnotationAttributesProviderReal.MetadataAttributesProvider.<>c <>9;
                    public static Func<Attribute, bool> <>9__6_0;

                    static <>c();
                    internal AttributeCollection <.cctor>b__8_0(AnnotationAttributesKey key, AttributeCollection value);
                    internal bool <EnsureMetadataAttributes>b__6_0(Attribute a);
                }
            }

            private static class ValidationContextHelper
            {
                private static Func<ValidationAttribute, bool> requiresValidationContextRoutine;

                internal static bool CustomRequiresValidationContextCore(CustomValidationAttribute attribute);
                internal static bool RequiresValidationContext(ValidationAttribute attribute);
                internal static bool RequiresValidationContextCore(ValidationAttribute attribute);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly AnnotationAttributes.AnnotationAttributesProviderReal.ValidationContextHelper.<>c <>9;
                    public static Func<ValidationAttribute, bool> <>9__2_0;

                    static <>c();
                    internal bool <RequiresValidationContextCore>b__2_0(ValidationAttribute va);
                }
            }

            private static class XPOPropertyDescriptorHelper
            {
                private static readonly HashSet<string> descriptorsWithForcedNullability;
                private static FieldInfo f_propertyType;

                static XPOPropertyDescriptorHelper();
                internal static Lazy<bool?> GetNullabilityForced(PropertyDescriptor property);
                private static bool IsXPOPropertyDescriptorWithForcedNullability(Type descriptorType);
                private static bool IsXPPropertyDescriptorWithForcedNullability(PropertyDescriptor property);

                private sealed class WeakClosure
                {
                    private readonly WeakReference propertyDescriptor;

                    public WeakClosure(PropertyDescriptor property);
                    internal bool? GetNullabilityForced();
                }
            }
        }

        public sealed class ColumnOptions
        {
            private Lazy<string> dateTimeFormatString;
            private Lazy<bool> isCurrency;

            internal ColumnOptions(bool isReadOnly, int columnIndex);
            public AnnotationAttributes.ColumnOptions Calculate(PropertyDescriptor columnDescriptor);
            internal AnnotationAttributes.ColumnOptions CalculateCore(Type propertyType, AnnotationAttributes attributes);
            private string CalculateDateTimeFormatString();
            private bool CalculateDefaultAlignment(Type propertyType);
            private bool CalculateIsCurrency();
            private bool CheckDateTimeFormatStringByDataType(ref string dataFormatString);
            private void CheckDefaultAlignmentByDataType(ref bool farAlignedByDefault);
            private bool CheckIsCurrency();

            public bool AutoGenerateField { get; private set; }

            public bool AllowEdit { get; private set; }

            public bool AllowFilter { get; private set; }

            public bool ReadOnly { get; private set; }

            public bool IsFarAlignedByDefault { get; private set; }

            public int ColumnIndex { get; private set; }

            public string DateTimeFormatString { get; }

            public bool IsCurrency { get; }

            public AnnotationAttributes Attributes { get; private set; }
        }

        internal static class Reader
        {
            public static TValue[] Read<TAttribute, TValue>(AttributeCollection attributes, Func<TAttribute, TValue> read) where TAttribute: Attribute;
            public static TValue Read<TAttribute, TValue>(Type type, Func<TAttribute, TValue> read, TValue defaultValue = null) where TAttribute: Attribute;
            public static TValue Read<TAttribute, TValue>(Type attributeType, AttributeCollection attributes, Func<TAttribute, TValue> read, TValue defaultValue) where TAttribute: Attribute;
            public static TValue Read<TAttribute, TValue>(Type attributeType, AttributeCollection attributes, Func<TAttribute, TValue> read, Func<TValue> getDefaultValue) where TAttribute: Attribute;

            [Serializable, CompilerGenerated]
            private sealed class <>c__3<TAttribute, TValue> where TAttribute: Attribute
            {
                public static readonly AnnotationAttributes.Reader.<>c__3<TAttribute, TValue> <>9;
                public static Func<Attribute, bool> <>9__3_0;

                static <>c__3();
                internal bool <Read>b__3_0(Attribute x);
            }
        }
    }
}

