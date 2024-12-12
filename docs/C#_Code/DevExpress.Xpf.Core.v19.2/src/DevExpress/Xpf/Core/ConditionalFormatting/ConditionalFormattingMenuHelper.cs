namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class ConditionalFormattingMenuHelper
    {
        [IteratorStateMachine(typeof(<GetAvailableHighlightItems>d__5))]
        public static IEnumerable<FormatConditionDialogType> GetAvailableHighlightItems(Type type, bool allowUniqueDuplicate)
        {
            type = GetDataType(type);
            yield return FormatConditionDialogType.GreaterThan;
            yield return FormatConditionDialogType.LessThan;
            yield return FormatConditionDialogType.Between;
            yield return FormatConditionDialogType.EqualTo;
            yield return FormatConditionDialogType.TextThatContains;
            if (allowUniqueDuplicate)
            {
                yield return FormatConditionDialogType.UniqueDuplicate;
            }
            if (type == typeof(DateTime))
            {
                yield return FormatConditionDialogType.ADateOccurring;
            }
            yield return FormatConditionDialogType.CustomCondition;
        }

        [IteratorStateMachine(typeof(<GetAvailableTopBottomRuleItems>d__4))]
        public static IEnumerable<FormatConditionDialogType> GetAvailableTopBottomRuleItems(Type type, bool isServerMode)
        {
            type = GetDataType(type);
            if (!typeof(IComparable).IsAssignableFrom(type))
            {
            }
            TypeCode typeCode = Type.GetTypeCode(type);
            if (!isServerMode)
            {
                yield return FormatConditionDialogType.Top10Items;
                yield return FormatConditionDialogType.Top10Percent;
                yield return FormatConditionDialogType.Bottom10Items;
                yield return FormatConditionDialogType.Bottom10Percent;
            }
            if (IndicatorFormatBase.IsNumericTypeCode(typeCode) || ((typeCode == TypeCode.DateTime) && !isServerMode))
            {
                yield return FormatConditionDialogType.AboveAverage;
                yield return FormatConditionDialogType.BelowAverage;
            }
        }

        private static Type GetDataType(Type type) => 
            Nullable.GetUnderlyingType(type) ?? type;

        public static bool ShowColorScaleMenu(Type type) => 
            ShowIndicatorMenu(type, new Func<TypeCode, bool>(IndicatorFormatBase.IsNumericOrDateTimeTypeCode));

        public static bool ShowDatBarMenu(Type type) => 
            ShowIndicatorMenu(type, new Func<TypeCode, bool>(IndicatorFormatBase.IsNumericTypeCode));

        public static bool ShowIconSetMenu(Type type) => 
            ShowIndicatorMenu(type, new Func<TypeCode, bool>(IndicatorFormatBase.IsNumericOrDateTimeTypeCode));

        private static bool ShowIndicatorMenu(Type type, Func<TypeCode, bool> typeCodeCheck)
        {
            type = GetDataType(type);
            TypeCode typeCode = Type.GetTypeCode(type);
            return typeCodeCheck(typeCode);
        }


    }
}

