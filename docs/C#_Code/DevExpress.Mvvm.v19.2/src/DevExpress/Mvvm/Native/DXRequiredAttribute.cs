namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXRequiredAttribute : DXValidationAttribute
    {
        private readonly bool allowEmptyStrings;

        protected DXRequiredAttribute()
        {
            throw new NotSupportedException();
        }

        public DXRequiredAttribute(bool allowEmptyStrings, Func<string> errorMessageAccessor) : this(errorMessageAccessor, func1)
        {
            Func<string> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = () => DataAnnotationsResourcesResolver.RequiredAttribute_ValidationError;
            }
            this.allowEmptyStrings = allowEmptyStrings;
        }

        protected override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            string str = value as string;
            return ((str == null) || (this.allowEmptyStrings || (str.Trim().Length != 0)));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXRequiredAttribute.<>c <>9 = new DXRequiredAttribute.<>c();
            public static Func<string> <>9__2_0;

            internal string <.ctor>b__2_0() => 
                DataAnnotationsResourcesResolver.RequiredAttribute_ValidationError;
        }
    }
}

