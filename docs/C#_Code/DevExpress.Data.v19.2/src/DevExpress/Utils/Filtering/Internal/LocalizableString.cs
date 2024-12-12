namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class LocalizableString
    {
        private string propertyName;
        private string propertyValue;
        private Type resourceType;
        private Func<string> accessor;

        public LocalizableString(Expression<Func<string>> propertyExpression) : this(ExpressionHelper.GetPropertyName<string>(propertyExpression))
        {
        }

        public LocalizableString(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string GetLocalizableValue()
        {
            if (this.accessor == null)
            {
                if (this.resourceType == null)
                {
                    this.accessor = () => this.propertyValue;
                }
                else
                {
                    string propertyValue = this.propertyValue;
                    if (this.propertyValue == null)
                    {
                        string local1 = this.propertyValue;
                        propertyValue = this.propertyName;
                    }
                    PropertyInfo property = this.resourceType.GetProperty(propertyValue);
                    if (!this.IsBadlyConfigured(property))
                    {
                        this.accessor = () => (string) property.GetValue(null, null);
                    }
                    else
                    {
                        LocalizableStringBadlyConfiguredException exception = new LocalizableStringBadlyConfiguredException(this.propertyName, this.resourceType, this.propertyValue);
                        this.accessor = delegate {
                            throw exception;
                        };
                    }
                }
            }
            return this.accessor();
        }

        private bool IsBadlyConfigured(PropertyInfo property)
        {
            if (!this.resourceType.IsVisible || ((property == null) || (property.PropertyType != typeof(string))))
            {
                return true;
            }
            MethodInfo getMethod = property.GetGetMethod();
            return ((getMethod == null) || (!getMethod.IsPublic || !getMethod.IsStatic));
        }

        private void ResetAccessor()
        {
            this.accessor = null;
        }

        public string Value
        {
            get => 
                this.propertyValue;
            set
            {
                if (this.propertyValue != value)
                {
                    this.propertyValue = value;
                    this.ResetAccessor();
                }
            }
        }

        public Type ResourceType
        {
            get => 
                this.resourceType;
            set
            {
                if (this.resourceType != value)
                {
                    this.resourceType = value;
                    this.ResetAccessor();
                }
            }
        }

        private class LocalizableStringBadlyConfiguredException : InvalidOperationException
        {
            private const string LocalizableString_LocalizationFailed = "Cannot retrieve property '{0}' because localization failed.  Type '{1}' is not public or does not contain a public static string property with the name '{2}'.";

            public LocalizableStringBadlyConfiguredException(string propertyName, Type resourceType, string propertyValue) : this(string.Format(CultureInfo.CurrentCulture, "Cannot retrieve property '{0}' because localization failed.  Type '{1}' is not public or does not contain a public static string property with the name '{2}'.", objArray2))
            {
                object[] objArray1 = new object[3];
                objArray1[0] = propertyName;
                objArray1[1] = resourceType.FullName;
                object[] objArray2 = objArray1;
                string text1 = propertyValue;
                if (propertyValue == null)
                {
                    string local1 = propertyValue;
                    text1 = propertyName;
                }
                objArray2[2] = text1;
            }
        }
    }
}

