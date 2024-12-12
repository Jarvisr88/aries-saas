namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.All, Inherited=true, AllowMultiple=true)]
    internal class CustomInstanceValidationAttribute : DXValidationAttribute
    {
        private readonly Func<object, object, bool> isValidFunction;
        private readonly Type valueType;

        public CustomInstanceValidationAttribute(Type valueType, Func<object, object, bool> isValidFunction, DXValidationAttribute.ErrorMessageAccessorDelegate errorMessageAccessor) : this(errorMessageAccessor, func1)
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

        protected override bool IsInstanceValid(object value, object instance) => 
            !CustomValidationAttribute.IsValueTypeAndNull(this.valueType, value) ? this.isValidFunction(value, instance) : true;

        protected override bool IsValid(object value) => 
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomInstanceValidationAttribute.<>c <>9 = new CustomInstanceValidationAttribute.<>c();
            public static Func<string> <>9__2_0;

            internal string <.ctor>b__2_0() => 
                DataAnnotationsResourcesResolver.CustomValidationAttribute_ValidationError;
        }
    }
}

