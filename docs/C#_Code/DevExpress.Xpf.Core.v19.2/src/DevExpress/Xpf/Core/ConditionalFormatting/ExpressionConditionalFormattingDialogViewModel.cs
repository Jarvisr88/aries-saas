namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class ExpressionConditionalFormattingDialogViewModel : ConditionalFormattingDialogViewModel
    {
        protected ExpressionConditionalFormattingDialogViewModel(IFormatsOwner owner, ConditionalFormattingStringId titleId, ConditionalFormattingStringId descriptionId, ConditionalFormattingStringId connectorId) : base(owner, titleId, descriptionId, connectorId)
        {
        }

        protected object ConvertRuleValue(object value) => 
            ManagerHelperBase.ConvertRuleValue(value, base.Context.ColumnInfo.FieldType, this.IsInDesignMode());

        protected override BaseEditUnit CreateEditUnit(string fieldName)
        {
            ConditionEditUnit unit = new ConditionEditUnit();
            if (base.ApplyFormatToWholeRow)
            {
                unit.ApplyToRow = true;
            }
            ConditionRule valueRule = this.GetValueRule();
            if (valueRule != ConditionRule.Expression)
            {
                unit.ValueRule = valueRule;
            }
            else
            {
                unit.Expression = this.GetExpression(fieldName);
            }
            if (this.Value != null)
            {
                unit.Value1 = this.ConvertRuleValue(this.Value);
            }
            return unit;
        }

        public static string CriteriaOperatorToString(CriteriaOperator criteria) => 
            CriteriaOperator.ToString(criteria);

        protected abstract CriteriaOperator GetCriteria(string fieldName);
        protected virtual string GetCriteriaString(string fieldName) => 
            null;

        public string GetExpression(string fieldName) => 
            this.GetCriteriaString(fieldName) ?? CriteriaOperatorToString(this.GetCriteria(fieldName));

        internal override object GetInitialValue()
        {
            Func<object> dateTime = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<object> local1 = <>c.<>9__10_0;
                dateTime = <>c.<>9__10_0 = () => DateTime.Today;
            }
            return this.SelectValue<object>(dateTime, <>c.<>9__10_1 ??= () => 0M, <>c.<>9__10_2 ??= () => null);
        }

        internal virtual ConditionRule GetValueRule() => 
            ConditionRule.Expression;

        protected T SelectValue<T>(Func<T> dateTime, Func<T> numeric, Func<T> text) => 
            SelectValue<T>(base.Context.ColumnInfo.FieldType, dateTime, numeric, text);

        public static T SelectValue<T>(Type type, Func<T> dateTime, Func<T> numeric, Func<T> text)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            return (!(type == typeof(DateTime)) ? (!EditorsSource.NumericIntegerTypes.Concat<Type>(EditorsSource.NumericFloatTypes).Contains<Type>(type) ? text() : numeric()) : dateTime());
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType
        {
            get
            {
                Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> dateTime = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> local1 = <>c.<>9__4_0;
                    dateTime = <>c.<>9__4_0 = () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleDateTime;
                }
                return this.SelectValue<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType>(dateTime, <>c.<>9__4_1 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleNumeric, <>c.<>9__4_2 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleText);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExpressionConditionalFormattingDialogViewModel.<>c <>9 = new ExpressionConditionalFormattingDialogViewModel.<>c();
            public static Func<ConditionValueType> <>9__4_0;
            public static Func<ConditionValueType> <>9__4_1;
            public static Func<ConditionValueType> <>9__4_2;
            public static Func<object> <>9__10_0;
            public static Func<object> <>9__10_1;
            public static Func<object> <>9__10_2;

            internal ConditionValueType <get_ConditionValueType>b__4_0() => 
                ConditionValueType.SingleDateTime;

            internal ConditionValueType <get_ConditionValueType>b__4_1() => 
                ConditionValueType.SingleNumeric;

            internal ConditionValueType <get_ConditionValueType>b__4_2() => 
                ConditionValueType.SingleText;

            internal object <GetInitialValue>b__10_0() => 
                DateTime.Today;

            internal object <GetInitialValue>b__10_1() => 
                0M;

            internal object <GetInitialValue>b__10_2() => 
                null;
        }
    }
}

