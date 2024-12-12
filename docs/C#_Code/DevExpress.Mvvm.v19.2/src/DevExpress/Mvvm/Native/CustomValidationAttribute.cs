namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.All, Inherited=true, AllowMultiple=true)]
    internal class CustomValidationAttribute : DXValidationAttribute
    {
        private readonly Func<object, bool> isValidFunction;
        private readonly Type valueType;

        public CustomValidationAttribute(Type valueType, Func<object, bool> isValidFunction, Func<object, string> errorMessageAccessor) : this(errorMessageAccessor, func1)
        {
            Func<string> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = () => DataAnnotationsResourcesResolver.CustomValidationAttribute_ValidationError;
            }
            this.valueType = valueType;
            this.isValidFunction = isValidFunction;
        }

        protected override bool IsValid(object value) => 
            !IsValueTypeAndNull(this.valueType, value) ? this.isValidFunction(value) : true;

        internal static bool IsValueTypeAndNull(Type valueType, object value)
        {
            bool isValueType = valueType.IsValueType;
            bool flag2 = (isValueType && valueType.IsGenericType) && (valueType.GetGenericTypeDefinition() == typeof(Nullable<>));
            return (isValueType && (!flag2 && (value == null)));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomValidationAttribute.<>c <>9 = new CustomValidationAttribute.<>c();
            public static Func<string> <>9__2_0;

            internal string <.ctor>b__2_0() => 
                DataAnnotationsResourcesResolver.CustomValidationAttribute_ValidationError;
        }
    }
}

