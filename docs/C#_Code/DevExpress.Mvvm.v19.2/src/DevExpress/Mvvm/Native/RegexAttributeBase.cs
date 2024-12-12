namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Text.RegularExpressions;

    internal abstract class RegexAttributeBase : DXDataTypeAttribute
    {
        protected const RegexOptions DefaultRegexOptions = (RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        private readonly Regex regex;

        public RegexAttributeBase(Regex regex, Func<string> errorMessageAccessor, Func<string> defaultErrorMessageAccessor, PropertyDataType dataType) : base(errorMessageAccessor, defaultErrorMessageAccessor, dataType)
        {
            this.regex = regex;
        }

        protected sealed override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            string input = value as string;
            return ((input != null) && (this.regex.Match(input).Length > 0));
        }
    }
}

