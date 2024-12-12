namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class DXMinLengthAttribute : DXValidationAttribute
    {
        public DXMinLengthAttribute(int length, Func<object, string> errorMessageAccessor) : this(errorMessageAccessor, func1)
        {
            Func<string> func1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<string> local1 = <>c.<>9__0_0;
                func1 = <>c.<>9__0_0 = () => DataAnnotationsResourcesResolver.MinLengthAttribute_ValidationError;
            }
            this.Length = length;
        }

        protected override string FormatErrorMessage(string error, string name)
        {
            object[] args = new object[] { name, this.Length };
            return string.Format(CultureInfo.CurrentCulture, error, args);
        }

        protected override bool IsValid(object value)
        {
            if (this.Length < 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataAnnotationsResourcesResolver.MinLengthAttribute_InvalidMinLength, new object[0]));
            }
            if (value == null)
            {
                return true;
            }
            string str = value as string;
            return (((str != null) ? str.Length : ((Array) value).Length) >= this.Length);
        }

        public int Length { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXMinLengthAttribute.<>c <>9 = new DXMinLengthAttribute.<>c();
            public static Func<string> <>9__0_0;

            internal string <.ctor>b__0_0() => 
                DataAnnotationsResourcesResolver.MinLengthAttribute_ValidationError;
        }
    }
}

