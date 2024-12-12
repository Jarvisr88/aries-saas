namespace DevExpress.XtraReports.Native.Parameters
{
    using DevExpress.Data.Filtering;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Text.RegularExpressions;

    public class ParametersReplacer : ClientCriteriaVisitorBase
    {
        private static readonly string parameterPrefix;
        private static readonly Regex inOperandRegex;

        static ParametersReplacer();
        private static CriteriaOperator[] CreateConcatParams(string prefix, string parameterName, string suffix);
        internal static string GetParameterFormattedName(string name);
        internal static string GetParameterFullName(string name);
        internal static string GetParameterName(string s);
        public static string UpgradeFilterString(string filterString);
        public override CriteriaOperator Visit(OperandProperty theOperand);
        public override CriteriaOperator Visit(OperandValue theOperand);
    }
}

