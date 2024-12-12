namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FilterAttributes
    {
        private static readonly Func<PropertyDescriptor, AttributeCollection> metadataAttributesProvider;
        private static readonly Func<PropertyDescriptor, FilterAttributes.FilterAttributesProvider> propertyDescriptorAttributesProviderInitializer;
        private static readonly Func<AttributeCollection, FilterAttributes.FilterAttributesProvider> attributeCollectionAttributesProviderInitializer;
        private readonly FilterAttributes.FilterAttributesProvider provider;
        private readonly Type propertyType;
        private readonly Type enumDataType;
        private Type forcedPropertyType;
        private Type forcedEnumDataType;
        private Type metricAttributesTypeDefinition;
        private IMetricAttributes metricAttributes;

        static FilterAttributes();
        public FilterAttributes(PropertyDescriptor property);
        public FilterAttributes(AnnotationAttributes annotationAttributes, Type propertyType);
        private FilterAttributes(FilterAttributes.FilterAttributesProvider provider, Type propertyType);
        public FilterAttributes(IEnumerable<Attribute> attributes, Type propertyType);
        public FilterAttributes(AttributeCollection attributes, Type propertyType);
        public FilterAttributes(IEnumerable<Attribute> attributes, Type propertyType, Type enumDataType);
        public FilterAttributes(AttributeCollection attributes, Type propertyType, Type enumDataType);
        public bool Any();
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        private static void CheckDataAnnotations_ConditionallyAPTCAIssue<TAttrbute>();
        internal bool Force(Type type);
        internal bool Force<TAttribute>(TAttribute attribute = null) where TAttribute: FilterAttribute;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public FilterBooleanEditorSettings GetBooleanEditorSettings();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public FilterEnumEditorSettings GetEnumEditorSettings();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public FilterGroupEditorSettings GetGroupEditorSettings();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public FilterLookupEditorSettings GetLookupEditorSettings();
        internal static AttributeCollection GetMetadataAttributes(PropertyDescriptor descriptor);
        public IMetricAttributes GetMetricAttributes();
        public Type GetMetricAttributesTypeDefinition();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public FilterRangeEditorSettings GetRangeEditorSettings();
        internal bool IsFit<TAttribute>(Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;
        public FilterAttributes Merge(FilterAttributes attributes);
        internal bool Reset<TAttribute>() where TAttribute: FilterAttribute;
        public static void Reset();
        private void SetForcedPropertyType(Type type);
        internal bool Update(AnnotationAttributes annotationAttributes);
        internal bool Update<TAttribute>(TAttribute attribute, Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Type PropertyType { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Type EnumDataType { get; }

        public bool HasEditorAttribute { get; }

        public bool HasFilterPropertyAttribute { get; }

        public bool IsFilterProperty { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterAttributes.<>c <>9;

            static <>c();
            internal AttributeCollection <.cctor>b__3_0(PropertyDescriptor property);
            internal FilterAttributes.FilterAttributesProvider <.cctor>b__3_1(PropertyDescriptor property);
            internal FilterAttributes.FilterAttributesProvider <.cctor>b__3_2(AttributeCollection attributes);
            internal AttributeCollection <.cctor>b__3_3(PropertyDescriptor property);
            internal FilterAttributes.FilterAttributesProvider <.cctor>b__3_4(PropertyDescriptor property);
            internal FilterAttributes.FilterAttributesProvider <.cctor>b__3_5(AttributeCollection attributes);
        }

        private abstract class FilterAttributesProvider
        {
            protected FilterAttributesProvider(AttributeCollection attributes);
            public virtual bool Any();
            public virtual bool Force<TAttribute>(TAttribute attribute = null) where TAttribute: FilterAttribute;
            public virtual IMetricAttributes GetBooleanChoiceAttributes(Type type);
            public virtual FilterBooleanEditorSettings GetBooleanEditorSettings();
            public virtual IMetricAttributes GetDateTimeRangeAttributes(Type type);
            public virtual IMetricAttributes GetEnumChoiceAttributes(Type type);
            public virtual Type GetEnumDataType(Type type);
            public virtual FilterEnumEditorSettings GetEnumEditorSettings();
            public virtual IMetricAttributes GetGroupAttributes(Type type);
            public virtual FilterGroupEditorSettings GetGroupEditorSettings();
            public virtual IMetricAttributes GetLookupAttributes(Type type);
            public virtual FilterLookupEditorSettings GetLookupEditorSettings();
            public virtual IMetricAttributes GetRangeAttributes(Type type);
            public virtual FilterRangeEditorSettings GetRangeEditorSettings();
            public virtual bool IsFit<TAttribute>(Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;
            public virtual bool Reset();
            public virtual bool Reset<TAttribute>() where TAttribute: FilterAttribute;
            public virtual bool Update(AnnotationAttributes annotationAttributes);
            public virtual bool Update<TAttribute>(TAttribute attribute, Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;

            public virtual bool HasGroupAttribute { get; }

            public virtual bool HasRangeAttribute { get; }

            public virtual bool HasDateTimeRangeAttribute { get; }

            public virtual bool HasLookupAttribute { get; }

            public virtual bool HasBooleanChoiceAttribute { get; }

            public virtual bool HasEnumChoiceAttribute { get; }

            public virtual bool HasEditorAttribute { get; }

            public virtual bool HasFilterPropertyAttribute { get; }

            public virtual bool IsFilterProperty { get; }
        }

        private sealed class FilterAttributesProviderEmpty : FilterAttributes.FilterAttributesProvider
        {
            internal static FilterAttributes.FilterAttributesProviderEmpty Instance;

            static FilterAttributesProviderEmpty();
            private FilterAttributesProviderEmpty();
        }

        private sealed class FilterAttributesProviderMerged : FilterAttributes.FilterAttributesProvider
        {
            private readonly IEnumerable<FilterAttributes.FilterAttributesProvider> providers;

            internal FilterAttributesProviderMerged(params FilterAttributes.FilterAttributesProvider[] providers);
            public override bool Any();
            public override bool Force<TAttribute>(TAttribute attribute = null) where TAttribute: FilterAttribute;
            public override IMetricAttributes GetBooleanChoiceAttributes(Type type);
            public override FilterBooleanEditorSettings GetBooleanEditorSettings();
            public override IMetricAttributes GetDateTimeRangeAttributes(Type type);
            public override IMetricAttributes GetEnumChoiceAttributes(Type type);
            public override Type GetEnumDataType(Type type);
            public override FilterEnumEditorSettings GetEnumEditorSettings();
            public override IMetricAttributes GetGroupAttributes(Type type);
            public override FilterGroupEditorSettings GetGroupEditorSettings();
            public override IMetricAttributes GetLookupAttributes(Type type);
            public override FilterLookupEditorSettings GetLookupEditorSettings();
            public override IMetricAttributes GetRangeAttributes(Type type);
            public override FilterRangeEditorSettings GetRangeEditorSettings();
            public override bool IsFit<TAttribute>(Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;
            private TValue Merge<TValue>(Func<FilterAttributes.FilterAttributesProvider, TValue> accessor, TValue defaultValue = null);
            public override bool Reset();
            public override bool Reset<TAttribute>() where TAttribute: FilterAttribute;
            public override bool Update<TAttribute>(TAttribute attribute, Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;

            public override bool HasRangeAttribute { get; }

            public override bool HasDateTimeRangeAttribute { get; }

            public override bool HasLookupAttribute { get; }

            public override bool HasBooleanChoiceAttribute { get; }

            public override bool HasEnumChoiceAttribute { get; }

            public override bool HasGroupAttribute { get; }

            public override bool HasEditorAttribute { get; }

            public override bool HasFilterPropertyAttribute { get; }

            public override bool IsFilterProperty { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly FilterAttributes.FilterAttributesProviderMerged.<>c <>9;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__3_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__6_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__9_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__12_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__15_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__18_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__21_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__24_0;
                public static Func<FilterAttributes.FilterAttributesProvider, FilterRangeEditorSettings> <>9__25_0;
                public static Func<FilterAttributes.FilterAttributesProvider, FilterLookupEditorSettings> <>9__26_0;
                public static Func<FilterAttributes.FilterAttributesProvider, FilterBooleanEditorSettings> <>9__27_0;
                public static Func<FilterAttributes.FilterAttributesProvider, FilterEnumEditorSettings> <>9__28_0;
                public static Func<FilterAttributes.FilterAttributesProvider, FilterGroupEditorSettings> <>9__29_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__31_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__33_0;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__35_0;

                static <>c();
                internal bool <Any>b__3_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasBooleanChoiceAttribute>b__15_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasDateTimeRangeAttribute>b__9_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasEditorAttribute>b__24_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasEnumChoiceAttribute>b__18_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasFilterPropertyAttribute>b__31_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasGroupAttribute>b__21_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasLookupAttribute>b__12_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_HasRangeAttribute>b__6_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <get_IsFilterProperty>b__33_0(FilterAttributes.FilterAttributesProvider x);
                internal FilterBooleanEditorSettings <GetBooleanEditorSettings>b__27_0(FilterAttributes.FilterAttributesProvider x);
                internal FilterEnumEditorSettings <GetEnumEditorSettings>b__28_0(FilterAttributes.FilterAttributesProvider x);
                internal FilterGroupEditorSettings <GetGroupEditorSettings>b__29_0(FilterAttributes.FilterAttributesProvider x);
                internal FilterLookupEditorSettings <GetLookupEditorSettings>b__26_0(FilterAttributes.FilterAttributesProvider x);
                internal FilterRangeEditorSettings <GetRangeEditorSettings>b__25_0(FilterAttributes.FilterAttributesProvider x);
                internal bool <Reset>b__35_0(FilterAttributes.FilterAttributesProvider x);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__36<TAttribute> where TAttribute: FilterAttribute
            {
                public static readonly FilterAttributes.FilterAttributesProviderMerged.<>c__36<TAttribute> <>9;
                public static Func<FilterAttributes.FilterAttributesProvider, bool> <>9__36_0;

                static <>c__36();
                internal bool <Reset>b__36_0(FilterAttributes.FilterAttributesProvider x);
            }
        }

        private sealed class FilterAttributesProviderReal : FilterAttributes.FilterAttributesProvider
        {
            private AttributeCollection attributes;
            private Lazy<FilterRangeAttribute> rangeAttributeValue;
            private Lazy<FilterDateTimeRangeAttribute> dateTimeRangeAttributeValue;
            private Lazy<FilterLookupAttribute> lookupAttributeValue;
            private Lazy<FilterGroupAttribute> groupAttributeValue;
            private readonly Lazy<FilterBooleanChoiceAttribute> booleanChoiceAttributeValue;
            private readonly Lazy<FilterEnumChoiceAttribute> enumChoiceAttributeValue;
            private readonly Lazy<FilterEditorAttribute> filterEditorAttributeValue;
            private readonly Lazy<FilterPropertyAttribute> filterPropertyAttributeValue;
            private readonly Lazy<EnumDataTypeAttribute> enumDataTypeAttributeValue;
            private Type forcedFilterAttributeType;

            public FilterAttributesProviderReal(AttributeCollection attributes);
            public FilterAttributesProviderReal(PropertyDescriptor property);
            public override bool Any();
            public override bool Force<TAttribute>(TAttribute attribute = null) where TAttribute: FilterAttribute;
            public override IMetricAttributes GetBooleanChoiceAttributes(Type type);
            public override FilterBooleanEditorSettings GetBooleanEditorSettings();
            public override IMetricAttributes GetDateTimeRangeAttributes(Type type);
            public override IMetricAttributes GetEnumChoiceAttributes(Type type);
            public override Type GetEnumDataType(Type type);
            public override FilterEnumEditorSettings GetEnumEditorSettings();
            public override IMetricAttributes GetGroupAttributes(Type type);
            public override FilterGroupEditorSettings GetGroupEditorSettings();
            public override IMetricAttributes GetLookupAttributes(Type type);
            public override FilterLookupEditorSettings GetLookupEditorSettings();
            public override IMetricAttributes GetRangeAttributes(Type type);
            public override FilterRangeEditorSettings GetRangeEditorSettings();
            public override bool IsFit<TAttribute>(Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;
            private Lazy<TAttribute> Read<TAttribute>() where TAttribute: Attribute;
            private Lazy<TAttribute> Read<TAttribute>(Func<TAttribute> getDefaultValue) where TAttribute: Attribute;
            private TValue Read<TAttribute, TValue>(Lazy<TAttribute> lazyAttributeValue, Func<TAttribute, TValue> read, TValue defaultValue = null) where TAttribute: Attribute;
            private Lazy<TValue> Read<TAttribute, TValue>(Type attributeType, Func<TAttribute, TValue> reader, Func<TValue> getDefaultValue) where TAttribute: Attribute;
            private Lazy<TValue> Read<TAttribute, TValue>(Type attributeType, Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute;
            public override bool Reset();
            public override bool Reset<TAttribute>() where TAttribute: FilterAttribute;
            public override bool Update(AnnotationAttributes annotationAttributes);
            public override bool Update<TAttribute>(TAttribute attribute, Func<TAttribute, bool> condition) where TAttribute: FilterAttribute;

            public override bool HasRangeAttribute { get; }

            public override bool HasDateTimeRangeAttribute { get; }

            public override bool HasLookupAttribute { get; }

            public override bool HasBooleanChoiceAttribute { get; }

            public override bool HasEnumChoiceAttribute { get; }

            public override bool HasGroupAttribute { get; }

            public override bool HasEditorAttribute { get; }

            public override bool HasFilterPropertyAttribute { get; }

            public override bool IsFilterProperty { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly FilterAttributes.FilterAttributesProviderReal.<>c <>9;
                public static Func<EnumDataTypeAttribute, Type> <>9__13_0;
                public static Func<FilterEditorAttribute, FilterRangeEditorSettings> <>9__41_0;
                public static Func<FilterEditorAttribute, FilterLookupEditorSettings> <>9__42_0;
                public static Func<FilterEditorAttribute, FilterBooleanEditorSettings> <>9__43_0;
                public static Func<FilterEditorAttribute, FilterEnumEditorSettings> <>9__44_0;
                public static Func<FilterEditorAttribute, FilterGroupEditorSettings> <>9__45_0;
                public static Func<FilterPropertyAttribute, bool> <>9__49_0;

                static <>c();
                internal bool <get_IsFilterProperty>b__49_0(FilterPropertyAttribute x);
                internal FilterBooleanEditorSettings <GetBooleanEditorSettings>b__43_0(FilterEditorAttribute x);
                internal Type <GetEnumDataType>b__13_0(EnumDataTypeAttribute x);
                internal FilterEnumEditorSettings <GetEnumEditorSettings>b__44_0(FilterEditorAttribute x);
                internal FilterGroupEditorSettings <GetGroupEditorSettings>b__45_0(FilterEditorAttribute x);
                internal FilterLookupEditorSettings <GetLookupEditorSettings>b__42_0(FilterEditorAttribute x);
                internal FilterRangeEditorSettings <GetRangeEditorSettings>b__41_0(FilterEditorAttribute x);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__15<TAttribute> where TAttribute: FilterAttribute
            {
                public static readonly FilterAttributes.FilterAttributesProviderReal.<>c__15<TAttribute> <>9;
                public static Func<FilterRangeAttribute> <>9__15_0;
                public static Func<FilterDateTimeRangeAttribute> <>9__15_1;
                public static Func<FilterLookupAttribute> <>9__15_2;

                static <>c__15();
                internal FilterRangeAttribute <Force>b__15_0();
                internal FilterDateTimeRangeAttribute <Force>b__15_1();
                internal FilterLookupAttribute <Force>b__15_2();
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__51<TAttribute> where TAttribute: Attribute
            {
                public static readonly FilterAttributes.FilterAttributesProviderReal.<>c__51<TAttribute> <>9;
                public static Func<TAttribute, TAttribute> <>9__51_0;

                static <>c__51();
                internal TAttribute <Read>b__51_0(TAttribute x);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__52<TAttribute> where TAttribute: Attribute
            {
                public static readonly FilterAttributes.FilterAttributesProviderReal.<>c__52<TAttribute> <>9;
                public static Func<TAttribute, TAttribute> <>9__52_0;

                static <>c__52();
                internal TAttribute <Read>b__52_0(TAttribute x);
            }

            internal sealed class MetadataAttributesProvider
            {
                private readonly AttributeCollection attributes;
                private static readonly ConcurrentDictionary<AnnotationAttributesKey, AttributeCollection> attributesCache;
                private static readonly Func<AnnotationAttributesKey, AttributeCollection, AttributeCollection> updateCache;

                static MetadataAttributesProvider();
                public MetadataAttributesProvider(PropertyDescriptor descriptor);
                private AttributeCollection EnsureMetadataAttributes(PropertyDescriptor descriptor);
                internal static void Reset();

                public AttributeCollection Attributes { get; }

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly FilterAttributes.FilterAttributesProviderReal.MetadataAttributesProvider.<>c <>9;
                    public static Func<Attribute, bool> <>9__6_0;
                    public static Func<Attribute, bool> <>9__6_1;
                    public static Func<FilterMetadataTypeAttribute, Type> <>9__6_2;
                    public static Func<MetadataTypeAttribute, Type> <>9__6_3;

                    static <>c();
                    internal AttributeCollection <.cctor>b__8_0(AnnotationAttributesKey key, AttributeCollection value);
                    internal bool <EnsureMetadataAttributes>b__6_0(Attribute a);
                    internal bool <EnsureMetadataAttributes>b__6_1(Attribute a);
                    internal Type <EnsureMetadataAttributes>b__6_2(FilterMetadataTypeAttribute a);
                    internal Type <EnsureMetadataAttributes>b__6_3(MetadataTypeAttribute a);
                }
            }
        }
    }
}

