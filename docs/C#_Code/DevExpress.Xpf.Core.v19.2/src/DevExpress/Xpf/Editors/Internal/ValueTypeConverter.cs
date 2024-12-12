namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class ValueTypeConverter : IValueConverter
    {
        public ValueTypeConverter()
        {
            this.ConvertValueCache = new NullableContainer();
            this.ConvertBackValueCache = new NullableContainer();
        }

        public object Convert(object editValue)
        {
            if (!this.ConvertBackValueCache.HasValue || (!Equals(this.ConvertBackValueCache.Value, editValue) || !this.ConvertValueCache.HasValue))
            {
                this.Reset();
                try
                {
                    object obj2 = this.Convert(editValue, editValue?.GetType(), null, CultureInfo.CurrentCulture);
                    this.ConvertValueCache.SetValue(obj2);
                    object obj3 = this.ConvertBack(obj2, obj2?.GetType(), null, CultureInfo.CurrentCulture);
                    this.ConvertBackValueCache.SetValue(obj3);
                }
                catch (Exception exception)
                {
                    this.ResetValues();
                    this.ValidationError = new BaseValidationError(this.GetValidationError(), exception, ErrorType.Critical);
                }
            }
            return this.ConvertValueCache.Value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj2 = value;
            if ((value != null) && (this.TargetType != null))
            {
                obj2 = System.Convert.ChangeType(obj2, this.ToConvertibleType(this.TargetType), culture);
            }
            if (this.Converter != null)
            {
                obj2 = this.Converter.Convert(obj2, targetType, parameter, culture);
            }
            return obj2;
        }

        public object ConvertBack(object editValue)
        {
            if (!this.ConvertValueCache.HasValue || (!Equals(this.ConvertValueCache.Value, editValue) || !this.ConvertBackValueCache.HasValue))
            {
                this.Reset();
                try
                {
                    object obj2 = this.ConvertBack(editValue, editValue?.GetType(), null, CultureInfo.CurrentCulture);
                    this.ConvertBackValueCache.SetValue(obj2);
                    object obj3 = this.Convert(obj2, obj2?.GetType(), null, CultureInfo.CurrentCulture);
                    this.ConvertValueCache.SetValue(obj3);
                }
                catch (Exception exception)
                {
                    this.ResetValues();
                    this.ValidationError = new BaseValidationError(this.GetValidationError(), exception, ErrorType.Critical);
                }
            }
            return this.ConvertBackValueCache.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj2 = value;
            if ((value != null) && (this.TargetType != null))
            {
                obj2 = System.Convert.ChangeType(obj2, this.ToConvertibleType(this.TargetType), culture);
            }
            if (this.Converter != null)
            {
                obj2 = this.Converter.ConvertBack(obj2, targetType, parameter, culture);
            }
            return obj2;
        }

        private string GetValidationError() => 
            EditorLocalizer.GetString(EditorStringId.InvalidValueConversion);

        private void Reset()
        {
            this.ValidationError = null;
            this.ResetValues();
        }

        public void ResetValues()
        {
            this.ConvertValueCache.Reset();
            this.ConvertBackValueCache.Reset();
        }

        private Type ToConvertibleType(Type type) => 
            (type != null) ? (typeof(IConvertible).IsAssignableFrom(type) ? type : typeof(object)) : typeof(object);

        public Type TargetType { get; set; }

        public IValueConverter Converter { get; set; }

        public BaseValidationError ValidationError { get; set; }

        private NullableContainer ConvertValueCache { get; set; }

        private NullableContainer ConvertBackValueCache { get; set; }
    }
}

