namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class DXValidationAttribute : Attribute
    {
        private readonly ErrorMessageAccessorDelegate errorMessageAccessor;

        protected DXValidationAttribute()
        {
            throw new NotSupportedException();
        }

        protected DXValidationAttribute(ErrorMessageAccessorDelegate errorMessageAccessor, Func<string> defaultErrorMessageAccessor)
        {
            ErrorMessageAccessorDelegate delegate2 = (defaultErrorMessageAccessor == null) ? null : (x, y) => defaultErrorMessageAccessor();
            this.errorMessageAccessor = errorMessageAccessor ?? delegate2;
        }

        protected DXValidationAttribute(Func<string> errorMessageAccessor, Func<string> defaultErrorMessageAccessor) : this((errorMessageAccessor == null) ? null : (x, y) => errorMessageAccessor(), defaultErrorMessageAccessor)
        {
        }

        protected DXValidationAttribute(Func<object, string> errorMessageAccessor, Func<string> defaultErrorMessageAccessor) : this((errorMessageAccessor == null) ? null : (x, y) => errorMessageAccessor(x), defaultErrorMessageAccessor)
        {
        }

        public static Func<object, string> ErrorMessageAccessor(Func<string> errorMessageAccessor) => 
            (errorMessageAccessor != null) ? x => errorMessageAccessor() : null;

        public static Func<object, string> ErrorMessageAccessor<TProperty>(Func<TProperty, string> errorMessageAccessor) => 
            (errorMessageAccessor != null) ? x => errorMessageAccessor((TProperty) x) : null;

        public static ErrorMessageAccessorDelegate ErrorMessageAccessor<TProperty, TInstance>(Func<TProperty, TInstance, string> errorMessageAccessor) => 
            (errorMessageAccessor != null) ? (x, y) => errorMessageAccessor((TProperty) x, (TInstance) y) : null;

        protected virtual string FormatErrorMessage(string error, string name)
        {
            object[] args = new object[] { name };
            return string.Format(CultureInfo.CurrentCulture, error, args);
        }

        protected string GetErrorMessageString(object value, object instance) => 
            this.errorMessageAccessor(value, instance);

        public string GetValidationResult(object value, string memberName, object instance = null)
        {
            if (memberName == null)
            {
                throw new ArgumentNullException("memberName");
            }
            return ((!this.IsValid(value) || !this.IsInstanceValid(value, instance)) ? this.FormatErrorMessage(this.GetErrorMessageString(value, instance), memberName) : null);
        }

        protected virtual bool IsInstanceValid(object value, object instance) => 
            true;

        protected abstract bool IsValid(object value);

        public delegate string ErrorMessageAccessorDelegate(object value, object instance);
    }
}

