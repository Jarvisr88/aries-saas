namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Globalization;

    internal static class ExpressionHelper
    {
        private static void CheckValue(object obj, ref string str)
        {
            if (!Equals(obj, null))
            {
                double num;
                NumberStyles any = NumberStyles.Any;
                if (!double.TryParse(str, any, CultureInfo.CurrentCulture, out num))
                {
                    str = $"'{obj}'";
                }
            }
        }

        public static string ConvertToExpression(IFormatConditionRuleValue obj, string colName)
        {
            string str = $"[{colName}]";
            switch (obj.Condition)
            {
                case FormatConditions.Equal:
                    str = str + " = ";
                    break;

                case FormatConditions.NotEqual:
                    str = str + " != ";
                    break;

                case FormatConditions.Between:
                    str = str + " between ";
                    break;

                case FormatConditions.NotBetween:
                    str = str + " notbetween ";
                    break;

                case FormatConditions.Less:
                    str = str + " < ";
                    break;

                case FormatConditions.Greater:
                    str = str + " > ";
                    break;

                case FormatConditions.GreaterOrEqual:
                    str = str + " >= ";
                    break;

                case FormatConditions.LessOrEqual:
                    str = str + " <= ";
                    break;

                default:
                    return "";
            }
            return (str + PrepareValues(obj));
        }

        private static string PrepareValues(IFormatConditionRuleValue obj)
        {
            string str = Convert.ToString(obj.Value1);
            string str2 = Convert.ToString(obj.Value2);
            CheckValue(obj.Value1, ref str);
            CheckValue(obj.Value2, ref str2);
            return (((obj.Condition == FormatConditions.Between) || (obj.Condition == FormatConditions.NotBetween)) ? $"({str},{str2})" : ((str != "") ? ((str2 != "''") ? (str + str2) : str) : str2));
        }
    }
}

