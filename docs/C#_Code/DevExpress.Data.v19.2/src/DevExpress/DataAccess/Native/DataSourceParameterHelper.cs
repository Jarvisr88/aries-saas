namespace DevExpress.DataAccess.Native
{
    using DevExpress.DataAccess;
    using System;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public static class DataSourceParameterHelper
    {
        public static bool ExtractParameterName(string expressionString, out string parameterName)
        {
            parameterName = string.Empty;
            return (!ExtractParameterNameCore(expressionString, ref parameterName, @"\A\?(?<name>.*?)?\z") ? ExtractParameterNameCore(expressionString, ref parameterName, @"\A[\[]?Parameters\.(?<name>.*?)[\]]?\z") : true);
        }

        private static bool ExtractParameterNameCore(string expressionString, ref string parameterName, string externalParameterPattern)
        {
            Match match = Regex.Match(expressionString, externalParameterPattern);
            if (!match.Success)
            {
                return false;
            }
            parameterName = match.Groups["name"].Value;
            return true;
        }

        public static bool IsBoundToExternalParameter(DataSourceParameterBase parameter)
        {
            string str;
            return IsBoundToExternalParameter(parameter, out str);
        }

        public static bool IsBoundToExternalParameter(DataSourceParameterBase parameter, out string externalParameterName)
        {
            externalParameterName = string.Empty;
            if (!(parameter.Type == typeof(Expression)))
            {
                return false;
            }
            Expression expression = parameter.Value as Expression;
            return ((expression != null) ? ExtractParameterName(expression.ExpressionString, out externalParameterName) : false);
        }
    }
}

