namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class OperandValuesRecord
    {
        private OperandValuesRecord(ValueData binary = null, Tuple<ValueData, ValueData> ternary = null, ValueData[] variable = null, CriteriaOperator customFilter = null, CriteriaOperator dateTimeOperatorsFilter = null, CriteriaOperator formatConditionFilter = null, Tuple<double, TopBottomValueType> predefinedFormatConditionFilter = null, bool isEmpty = false)
        {
            this.<Binary>k__BackingField = binary;
            Tuple<ValueData, ValueData> tuple1 = ternary;
            if (ternary == null)
            {
                Tuple<ValueData, ValueData> local1 = ternary;
                tuple1 = new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue);
            }
            this.<Ternary>k__BackingField = tuple1;
            ValueData[] dataArray2 = variable;
            if (variable == null)
            {
                ValueData[] local2 = variable;
                dataArray2 = new ValueData[] { ValueData.NullValue };
            }
            this.<Variable>k__BackingField = dataArray2;
            this.<CustomFilter>k__BackingField = customFilter;
            this.<DateTimeOperatorsFilter>k__BackingField = dateTimeOperatorsFilter;
            this.<FormatConditionFilter>k__BackingField = formatConditionFilter;
            this.<PredefinedFormatConditionFilter>k__BackingField = predefinedFormatConditionFilter;
            this.<IsEmpty>k__BackingField = isEmpty;
        }

        public static OperandValuesRecord CreateBinary(ValueData value) => 
            new OperandValuesRecord(value, null, null, null, null, null, null, false);

        public static OperandValuesRecord CreateCustom(CriteriaOperator value) => 
            new OperandValuesRecord(null, null, null, value, null, null, null, false);

        public static OperandValuesRecord CreateDateTimeOperators(CriteriaOperator value) => 
            new OperandValuesRecord(null, null, null, null, value, null, null, false);

        public static OperandValuesRecord CreateEmpty() => 
            new OperandValuesRecord(null, null, null, null, null, null, null, true);

        public static OperandValuesRecord CreateFormatCondition(CriteriaOperator value) => 
            new OperandValuesRecord(null, null, null, null, null, value, null, false);

        public static OperandValuesRecord CreatePredefinedFormatCondition(PredefinedFormatConditionFilterModel model) => 
            new OperandValuesRecord(null, null, null, null, null, null, new Tuple<double, TopBottomValueType>(model.Value, model.ValueType), false);

        public static OperandValuesRecord CreateTernary(Tuple<ValueData, ValueData> value) => 
            new OperandValuesRecord(null, value, null, null, null, null, null, false);

        public static OperandValuesRecord CreateVariable(ValueData[] value) => 
            new OperandValuesRecord(null, null, value, null, null, null, null, false);

        public ValueData Binary { get; }

        public Tuple<ValueData, ValueData> Ternary { get; }

        public ValueData[] Variable { get; }

        public CriteriaOperator CustomFilter { get; }

        public CriteriaOperator DateTimeOperatorsFilter { get; }

        public CriteriaOperator FormatConditionFilter { get; }

        public Tuple<double, TopBottomValueType> PredefinedFormatConditionFilter { get; }

        public bool IsEmpty { get; }
    }
}

