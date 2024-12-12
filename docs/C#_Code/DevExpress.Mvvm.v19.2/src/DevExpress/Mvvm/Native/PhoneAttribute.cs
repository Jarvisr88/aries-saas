namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal sealed class PhoneAttribute : RegexAttributeBase
    {
        private static readonly Regex regex = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public PhoneAttribute(Func<string> errorMessageAccessor) : this(regex, errorMessageAccessor, func1, PropertyDataType.PhoneNumber)
        {
            Func<string> func1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<string> local1 = <>c.<>9__1_0;
                func1 = <>c.<>9__1_0 = () => DataAnnotationsResourcesResolver.PhoneAttribute_Invalid;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PhoneAttribute.<>c <>9 = new PhoneAttribute.<>c();
            public static Func<string> <>9__1_0;

            internal string <.ctor>b__1_0() => 
                DataAnnotationsResourcesResolver.PhoneAttribute_Invalid;
        }
    }
}

