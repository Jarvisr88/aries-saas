﻿namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.InteropServices;

    public abstract class FilteringPropertyMetadataBuilderGeneric<T, TProperty, TBuilder> : PropertyMetadataBuilderBase<T, TProperty, TBuilder> where TBuilder: FilteringPropertyMetadataBuilderGeneric<T, TProperty, TBuilder>
    {
        internal FilteringPropertyMetadataBuilderGeneric(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent) : base(storage, parent)
        {
        }

        public TBuilder AutoGenerated() => 
            base.AutoGeneratedCore();

        public TBuilder DefaultEditor(object templateKey) => 
            base.DefaultEditorCore(templateKey);

        public TBuilder Description(string description) => 
            base.DescriptionCore(description);

        public TBuilder DisplayFormatString(string dataFormatString, bool applyDisplayFormatInEditMode = false) => 
            base.DisplayFormatStringCore(dataFormatString, applyDisplayFormatInEditMode);

        public TBuilder DisplayName(string name) => 
            base.DisplayNameCore(name);

        public TBuilder DisplayShortName(string shortName) => 
            base.DisplayShortNameCore(shortName);

        public TBuilder DoNotConvertEmptyStringToNull() => 
            base.DoNotConvertEmptyStringToNullCore();

        public FilteringMetadataBuilder<T> EndProperty() => 
            (FilteringMetadataBuilder<T>) base.parent;

        public TBuilder FilterBooleanChoice(string defaultName = null, string trueName = null, string falseName = null, FilterBooleanUIEditorType editorType = 0, bool? defaultValue = new bool?(), string defaultValueMember = null)
        {
            FilterBooleanChoiceAttributeProxy attribute = new FilterBooleanChoiceAttributeProxy();
            attribute.DefaultName = defaultName;
            attribute.TrueName = trueName;
            attribute.FalseName = falseName;
            attribute.EditorType = editorType;
            attribute.DefaultValue = defaultValue;
            attribute.DefaultValueMember = defaultValueMember;
            return base.AddOrReplaceAttribute<FilterBooleanChoiceAttributeProxy>(attribute);
        }

        public TBuilder FilterDateTimeRange(object minOrMinMember = null, object maxOrMaxMember = null, string fromName = null, string toName = null, FilterDateTimeRangeUIEditorType editorType = 0)
        {
            FilterDateTimeRangeAttributeProxy attribute = new FilterDateTimeRangeAttributeProxy();
            attribute.MinOrMinMember = minOrMinMember;
            attribute.MaxOrMaxMember = maxOrMaxMember;
            attribute.FromName = fromName;
            attribute.ToName = toName;
            attribute.EditorType = editorType;
            return base.AddOrReplaceAttribute<FilterDateTimeRangeAttributeProxy>(attribute);
        }

        public TBuilder FilterEnumChoice(bool? useSelectAll = new bool?(), string selectAllName = null, bool? useFlags = new bool?(), FilterLookupUIEditorType editorType = 0, Type enumDataType = null)
        {
            FilterEnumChoiceAttributeProxy attribute = new FilterEnumChoiceAttributeProxy();
            attribute.UseFlags = useFlags;
            attribute.UseSelectAll = useSelectAll;
            attribute.SelectAllName = selectAllName;
            attribute.EditorType = editorType;
            TBuilder local = base.AddOrReplaceAttribute<FilterEnumChoiceAttributeProxy>(attribute);
            if (enumDataType != null)
            {
                local.AddOrReplaceAttribute<EnumDataTypeAttribute>(new EnumDataTypeAttribute(enumDataType));
            }
            return local;
        }

        public TBuilder FilterLookup(object dataSourceOrDataSourceMember, string displayMember = null, string valueMember = null, object topOrTopMember = null, object maxCountOrMaxCountMember = null, bool? useSelectAll = new bool?(), string selectAllName = null, bool? useFlags = new bool?(), FilterLookupUIEditorType editorType = 0)
        {
            FilterLookupAttributeProxy attribute = new FilterLookupAttributeProxy();
            attribute.DataSourceOrDataSourceMember = dataSourceOrDataSourceMember;
            attribute.DisplayMember = displayMember;
            attribute.ValueMember = valueMember;
            attribute.TopOrTopMember = topOrTopMember;
            attribute.MaxCountOrMaxCountMember = maxCountOrMaxCountMember;
            attribute.UseSelectAll = useSelectAll;
            attribute.SelectAllName = selectAllName;
            attribute.UseFlags = useFlags;
            attribute.EditorType = editorType;
            return base.AddOrReplaceAttribute<FilterLookupAttributeProxy>(attribute);
        }

        public TBuilder FilterRange(object minOrMinMember = null, object maxOrMaxMember = null, string fromName = null, string toName = null, FilterRangeUIEditorType editorType = 0)
        {
            FilterRangeAttributeProxy attribute = new FilterRangeAttributeProxy();
            attribute.MinOrMinMember = minOrMinMember;
            attribute.MaxOrMaxMember = maxOrMaxMember;
            attribute.FromName = fromName;
            attribute.ToName = toName;
            attribute.EditorType = editorType;
            return base.AddOrReplaceAttribute<FilterRangeAttributeProxy>(attribute);
        }

        public TBuilder GridEditor(object templateKey) => 
            base.GridEditorCore(templateKey);

        public TBuilder LayoutControlEditor(object templateKey) => 
            this.LayoutControlEditor(templateKey);

        public TBuilder LocatedAt(int position, PropertyLocation propertyLocation = 0) => 
            base.LocatedAtCore(position, propertyLocation);

        public TBuilder MaxLength(int maxLength, Func<string> errorMessageAccessor = null) => 
            base.MaxLengthCore(maxLength, GetErrorMessageAccessor(errorMessageAccessor));

        public TBuilder MaxLength(int maxLength, Func<TProperty, string> errorMessageAccessor) => 
            base.MaxLengthCore(maxLength, errorMessageAccessor);

        public TBuilder MinLength(int minLength, Func<string> errorMessageAccessor = null) => 
            base.MinLengthCore(minLength, GetErrorMessageAccessor(errorMessageAccessor));

        public TBuilder MinLength(int minLength, Func<TProperty, string> errorMessageAccessor) => 
            base.MinLengthCore(minLength, errorMessageAccessor);

        public TBuilder NotAutoGenerated() => 
            base.NotAutoGeneratedCore();

        public TBuilder NullDisplayText(string nullDisplayText) => 
            base.NullDisplayTextCore(nullDisplayText);

        public TBuilder PropertyGridEditor(object templateKey) => 
            base.PropertyGridEditorCore(templateKey);
    }
}

