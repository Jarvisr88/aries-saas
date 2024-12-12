namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal class RegularExpressionAttribute : DXValidationAttribute
    {
        private readonly string pattern;
        private Regex regex;

        public RegularExpressionAttribute(string pattern, Func<object, string> errorMessageAccessor) : this(errorMessageAccessor, func1)
        {
            Func<string> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = () => DataAnnotationsResourcesResolver.RegexAttribute_ValidationError;
            }
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("pattern");
            }
            this.pattern = pattern;
        }

        protected override string FormatErrorMessage(string error, string name)
        {
            object[] args = new object[] { name, this.pattern };
            return string.Format(CultureInfo.CurrentCulture, error, args);
        }

        protected override bool IsValid(object value)
        {
            this.regex ??= new Regex(this.pattern);
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            Match match = this.regex.Match(str);
            return (match.Success && ((match.Index == 0) && (match.Length == str.Length)));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RegularExpressionAttribute.<>c <>9 = new RegularExpressionAttribute.<>c();
            public static Func<string> <>9__2_0;

            internal string <.ctor>b__2_0() => 
                DataAnnotationsResourcesResolver.RegexAttribute_ValidationError;
        }
    }
}

