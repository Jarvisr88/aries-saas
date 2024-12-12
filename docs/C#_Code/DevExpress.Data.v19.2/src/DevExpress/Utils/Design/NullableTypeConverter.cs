namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    public class NullableTypeConverter : NullableConverter
    {
        private readonly NullableTypeConverterStandardPropertyGridAspect standardPropertyGridAspect;
        private string nullString;
        private string defaultPopupString;
        private PropertyInfo[] properties;
        private string defaultValueString;
        private object defaultValue;
        private bool isStandardValuesSupport;
        private bool getDefaultValuesFromCtor;
        private bool isSimpleProperty;
        private Dictionary<string, object> standardValuesCache;
        private ICollection standardValuesCollection;
        private bool flagFrom;
        private bool flagTo;

        public NullableTypeConverter(Type type) : base(type)
        {
            this.standardPropertyGridAspect = new NullableTypeConverterStandardPropertyGridAspect();
            this.isStandardValuesSupport = true;
            this.nullString = this.GetNullText();
            this.defaultPopupString = this.GetDefaultPopupText();
            this.properties = base.UnderlyingType.GetProperties();
            this.defaultValue = Activator.CreateInstance(base.UnderlyingType);
            this.defaultValueString = this.defaultValue.ToString();
            this.isSimpleProperty = this.properties.Length == 0;
            this.isStandardValuesSupport = this.CanGetStandardValues(this.isSimpleProperty);
            this.InitializeStandardValueCache();
            this.FillStandardValuesCache(this.isStandardValuesSupport);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        private bool CanGetStandardValues(bool isSimpleProperty)
        {
            if (!isSimpleProperty)
            {
                return true;
            }
            this.getDefaultValuesFromCtor = true;
            this.standardValuesCollection = base.GetStandardValues();
            this.getDefaultValuesFromCtor = false;
            return (this.standardValuesCollection != null);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            this.standardPropertyGridAspect.OnConvertFrom(context);
            if (this.flagTo)
            {
                this.flagFrom = true;
            }
            string key = value as string;
            if (key != null)
            {
                if (key.ToUpper().Equals(this.nullString.ToUpper()))
                {
                    return null;
                }
                if (this.isStandardValuesSupport)
                {
                    object obj2;
                    this.standardValuesCache.TryGetValue(key, out obj2);
                    if (obj2 != null)
                    {
                        return obj2;
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            bool flag = false;
            if (!this.flagFrom)
            {
                this.flagTo ??= true;
            }
            if (this.flagFrom && this.flagTo)
            {
                flag = true;
                this.flagFrom = false;
            }
            if (destinationType == typeof(string))
            {
                if (value == null)
                {
                    this.standardPropertyGridAspect.OnConvertTo(context);
                    return this.nullString;
                }
                if (value.ToString().Equals(this.defaultValueString) && (!this.isSimpleProperty && flag))
                {
                    this.standardPropertyGridAspect.OnConvertTo(context);
                    return this.defaultPopupString;
                }
            }
            this.standardPropertyGridAspect.OnConvertTo(context);
            return base.ConvertTo(context, culture, value, destinationType);
        }

        private void FillStandardValuesCache(bool isStandardValuesSupport)
        {
            if (isStandardValuesSupport)
            {
                if (this.standardValuesCollection == null)
                {
                    this.standardValuesCache.Add(this.defaultPopupString, this.defaultValue);
                }
                else
                {
                    foreach (object obj2 in this.standardValuesCollection)
                    {
                        if (obj2 != null)
                        {
                            this.standardValuesCache.Add(obj2.ToString(), obj2);
                        }
                    }
                }
            }
        }

        protected virtual string GetDefaultPopupText() => 
            "Default";

        protected virtual string GetNullText() => 
            "Null";

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => 
            !this.getDefaultValuesFromCtor ? new TypeConverter.StandardValuesCollection(this.standardValuesCache.Values.ToList<object>()) : base.GetStandardValues(context);

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            !this.getDefaultValuesFromCtor ? this.isStandardValuesSupport : true;

        private void InitializeStandardValueCache()
        {
            this.standardValuesCache = new Dictionary<string, object>();
            this.standardValuesCache.Add(this.nullString, null);
        }
    }
}

