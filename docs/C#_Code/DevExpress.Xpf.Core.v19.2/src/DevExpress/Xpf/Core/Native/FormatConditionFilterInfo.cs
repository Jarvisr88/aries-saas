namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class FormatConditionFilterInfo
    {
        public FormatConditionFilterInfo(string propertyName, ConditionFilterType type, object value1, object value2);

        public string PropertyName { get; }

        public ConditionFilterType Type { get; }

        public object Value1 { get; }

        public object Value2 { get; }
    }
}

