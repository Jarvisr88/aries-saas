namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class DXMaxLengthAttribute : DXValidationAttribute
    {
        private const int MaxAllowableLength = -1;

        protected DXMaxLengthAttribute()
        {
            throw new NotSupportedException();
        }

        public DXMaxLengthAttribute(int length, Func<object, string> errorMessageAccessor) : this(errorMessageAccessor, func1)
        {
            Func<string> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = () => DataAnnotationsResourcesResolver.MaxLengthAttribute_ValidationError;
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
            if ((this.Length == 0) || (this.Length < -1))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataAnnotationsResourcesResolver.MaxLengthAttribute_InvalidMaxLength, new object[0]));
            }
            if (value == null)
            {
                return true;
            }
            string str = value as string;
            int num = (str != null) ? str.Length : ((Array) value).Length;
            return ((this.Length == -1) || (num <= this.Length));
        }

        public int Length { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXMaxLengthAttribute.<>c <>9 = new DXMaxLengthAttribute.<>c();
            public static Func<string> <>9__2_0;

            internal string <.ctor>b__2_0() => 
                DataAnnotationsResourcesResolver.MaxLengthAttribute_ValidationError;
        }
    }
}

