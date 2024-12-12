namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class PredefinedFormatConditionFilterModel : FilterModelBase
    {
        private readonly PredefinedFormatConditionType? DefaultConditionType;
        private double value;

        internal PredefinedFormatConditionFilterModel(FilterModelClient client, PredefinedFormatConditionType? conditionType = new PredefinedFormatConditionType?()) : base(client)
        {
            this.value = 10.0;
            this.DefaultConditionType = this.ConditionType = conditionType;
        }

        internal override CriteriaOperator BuildFilter() => 
            (this.ConditionType != null) ? this.BuildFilter(this.Value, this.ValueType) : null;

        internal CriteriaOperator BuildFilter(double value, TopBottomValueType valueType) => 
            BuildFilter(base.PropertyName, value, this.ConditionType, valueType);

        internal static CriteriaOperator BuildFilter(string propertyName, double value, PredefinedFormatConditionType? conditionType, TopBottomValueType valueType)
        {
            FormatConditionFilterInfo filter = new FormatConditionFilterInfo(propertyName, conditionType.Value.ToConditionFilterType(valueType), (valueType == TopBottomValueType.Items) ? Convert.ToInt32(value) : value, null);
            return FormatConditionFiltersHelper.CreateFilter(propertyName, filter, false, TopBottomFilterKind.Regular);
        }

        internal override bool CanBuildFilterCore() => 
            (this.ConditionType != null) && base.Column.GetFilterRestrictions().Allow(this.ConditionType.Value);

        internal static bool IsConstant(PredefinedFormatConditionType? type)
        {
            PredefinedFormatConditionType? nullable = type;
            PredefinedFormatConditionType top = PredefinedFormatConditionType.Top;
            if (!((((PredefinedFormatConditionType) nullable.GetValueOrDefault()) == top) ? (nullable == null) : true))
            {
                return false;
            }
            nullable = type;
            top = PredefinedFormatConditionType.Bottom;
            return ((((PredefinedFormatConditionType) nullable.GetValueOrDefault()) == top) ? (nullable == null) : true);
        }

        private void OnValueTypeChanged()
        {
            if ((this.ValueType == TopBottomValueType.Percent) && (this.Value > 100.0))
            {
                this.Value = 100.0;
            }
            else
            {
                base.ApplyFilter();
            }
        }

        internal override Task UpdateCoreAsync()
        {
            int num1;
            FunctionOperator filter = base.Filter as FunctionOperator;
            FormatConditionFilterInfo info = !filter.ReferenceEqualsNull() ? FormatConditionFiltersHelper.GetTopBottomFilterInfo(filter) : null;
            this.ConditionType = (info != null) ? new PredefinedFormatConditionType?(info.Type.ToPredefinedFormatConditionType()) : this.DefaultConditionType;
            this.Value = ((info == null) || (info.Value1 == null)) ? 10.0 : Convert.ToDouble(info?.Value1);
            if (((info != null) && (info.Type == ConditionFilterType.BottomPercent)) || ((info != null) && (info.Type == ConditionFilterType.TopPercent)))
            {
                num1 = 1;
            }
            else
            {
                num1 = 0;
            }
            this.ValueType = (TopBottomValueType) num1;
            return FilteringUIExtensions.CompletedTask;
        }

        internal PredefinedFormatConditionType? ConditionType
        {
            get => 
                base.GetValue<PredefinedFormatConditionType?>("ConditionType");
            set => 
                base.SetValue<PredefinedFormatConditionType?>(value, new Action(this.ApplyFilter), "ConditionType");
        }

        public double Value
        {
            get => 
                this.value;
            set => 
                base.SetProperty<double>(ref this.value, value, "Value", new Action(this.ApplyFilter));
        }

        public TopBottomValueType ValueType
        {
            get => 
                base.GetValue<TopBottomValueType>("ValueType");
            set => 
                base.SetValue<TopBottomValueType>(value, new Action(this.OnValueTypeChanged), "ValueType");
        }
    }
}

