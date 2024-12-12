namespace DevExpress.Utils.Filtering
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class FilterAttributeLocalizable : FilterAttribute
    {
        private readonly Dictionary<string, LocalizableString> localizableStrings = new Dictionary<string, LocalizableString>(StringComparer.Ordinal);
        private Type resourceTypeCore;

        protected FilterAttributeLocalizable()
        {
            foreach (Expression<Func<string>> expression in this.GetLocalizableProperties())
            {
                this.RegisterLocalizableProperty(expression);
            }
        }

        protected abstract IEnumerable<Expression<Func<string>>> GetLocalizableProperties();
        protected string GetLocalizablePropertyValue(Expression<Func<string>> propertySelector) => 
            this.localizableStrings[ExpressionHelper.GetPropertyName<string>(propertySelector)].Value;

        protected string GetLocalizableValue(Expression<Func<string>> propertySelector) => 
            this.localizableStrings[ExpressionHelper.GetPropertyName<string>(propertySelector)].GetLocalizableValue();

        private void OnResourceTypeChanged(Type value)
        {
            foreach (LocalizableString str in this.localizableStrings.Values)
            {
                str.ResourceType = value;
            }
        }

        private void RegisterLocalizableProperty(Expression<Func<string>> propertySelector)
        {
            this.localizableStrings.Add(ExpressionHelper.GetPropertyName<string>(propertySelector), new LocalizableString(propertySelector));
        }

        protected void SetLocalizablePropertyValue(Expression<Func<string>> propertySelector, string value)
        {
            this.localizableStrings[ExpressionHelper.GetPropertyName<string>(propertySelector)].Value = value;
        }

        public Type ResourceType
        {
            get => 
                this.resourceTypeCore;
            set
            {
                if (this.resourceTypeCore != value)
                {
                    this.resourceTypeCore = value;
                    this.OnResourceTypeChanged(value);
                }
            }
        }
    }
}

