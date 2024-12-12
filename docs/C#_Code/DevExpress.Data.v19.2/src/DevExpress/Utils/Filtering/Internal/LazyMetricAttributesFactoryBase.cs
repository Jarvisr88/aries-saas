namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    internal abstract class LazyMetricAttributesFactoryBase : ILazyMetricAttributesFactory
    {
        protected static readonly string[] EmptyRangeMembers = new string[3];
        private static readonly string[] EmptyLookupMembers = new string[3];
        private static readonly string[] EmptyBooleanChoiceMembers = new string[1];

        protected LazyMetricAttributesFactoryBase()
        {
        }

        protected virtual IMetricAttributes CreateBooleanChoiceCore(Type type)
        {
            bool? defaultValue = null;
            return MetricAttributes.CreateBooleanChoice(type, defaultValue, null, null, null, BooleanUIEditorType.Default, EmptyBooleanChoiceMembers);
        }

        protected virtual IMetricAttributes CreateDateTimeRangeCore(Type type)
        {
            object min = null;
            object max = null;
            MetricAttributes.CheckDataTimeRangeCore(type, ref min, ref max);
            return MetricAttributes.CreateRange(type, min, max, null, null, null, DateTimeRangeUIEditorType.Default, EmptyRangeMembers);
        }

        protected virtual IMetricAttributes CreateEnumChoiceCore(Type type, Type enumDataType)
        {
            bool? useFlags = null;
            useFlags = null;
            return MetricAttributes.CreateEnumChoice(type, enumDataType, LookupUIEditorType.Default, useFlags, FlagComparisonRule.Default, ValueSelectionMode.Default, useFlags, null, null, FilterAttribute.EmptyMembers);
        }

        protected virtual IMetricAttributes CreateGroupCore(Type type)
        {
            bool? useSelectAll = null;
            return MetricAttributes.CreateGroup(type, GroupUIEditorType.Default, null, ValueSelectionMode.Default, useSelectAll, null, null, FilterAttribute.EmptyMembers);
        }

        protected virtual IMetricAttributes CreateLookupCore(Type type)
        {
            int? top = null;
            top = null;
            bool? useSelectAll = null;
            useSelectAll = null;
            return MetricAttributes.CreateLookup(type, null, null, null, top, top, LookupUIEditorType.Default, ValueSelectionMode.Default, useSelectAll, null, null, useSelectAll, null, EmptyLookupMembers);
        }

        protected virtual IMetricAttributes CreateNumericRangeCore(Type type)
        {
            object min = null;
            object max = null;
            MetricAttributes.CheckNumericRange(type, ref min, ref max);
            return MetricAttributes.CreateRange(type, min, max, null, null, null, null, RangeUIEditorType.Text, EmptyRangeMembers);
        }

        IMetricAttributes ILazyMetricAttributesFactory.CreateBooleanChoice(Type type) => 
            this.CreateBooleanChoiceCore(type);

        IMetricAttributes ILazyMetricAttributesFactory.CreateEnumChoice(Type type, Type enumDataType) => 
            this.CreateEnumChoiceCore(type, enumDataType);

        IMetricAttributes ILazyMetricAttributesFactory.CreateGroup(Type type) => 
            this.CreateGroupCore(type);

        IMetricAttributes ILazyMetricAttributesFactory.CreateLookup(Type type) => 
            this.CreateLookupCore(type);

        IMetricAttributes ILazyMetricAttributesFactory.CreateRange(Type type) => 
            MetricAttributes.IsDateTimeRange(type) ? this.CreateDateTimeRangeCore(type) : this.CreateNumericRangeCore(type);
    }
}

