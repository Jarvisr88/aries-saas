namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class RangeAttribute : DXValidationAttribute
    {
        private readonly IComparable maximum;
        private readonly IComparable minimum;

        public RangeAttribute(object minimum, object maximum, Func<object, string> errorMessageAccessor) : this(errorMessageAccessor, func1)
        {
            Func<string> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = () => DataAnnotationsResourcesResolver.RangeAttribute_ValidationError;
            }
            if (!(minimum is IComparable))
            {
                throw new ArgumentException("minimum");
            }
            if (!(maximum is IComparable))
            {
                throw new ArgumentException("maximum");
            }
            this.minimum = minimum as IComparable;
            this.maximum = maximum as IComparable;
        }

        protected override string FormatErrorMessage(string error, string name)
        {
            object[] args = new object[] { name, this.minimum, this.maximum };
            return string.Format(CultureInfo.CurrentCulture, error, args);
        }

        protected override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if ((value as string) == string.Empty)
            {
                return true;
            }
            IComparable comparable = value as IComparable;
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            return ((this.minimum.CompareTo(comparable) <= 0) && (this.maximum.CompareTo(comparable) >= 0));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RangeAttribute.<>c <>9 = new RangeAttribute.<>c();
            public static Func<string> <>9__2_0;

            internal string <.ctor>b__2_0() => 
                DataAnnotationsResourcesResolver.RangeAttribute_ValidationError;
        }
    }
}

