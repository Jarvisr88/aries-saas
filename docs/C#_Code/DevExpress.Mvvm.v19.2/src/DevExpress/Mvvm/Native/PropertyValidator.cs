namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Text;

    [SecuritySafeCritical]
    public class PropertyValidator
    {
        private readonly IEnumerable<ValidationAttribute> attributes;
        private readonly IEnumerable<DXValidationAttribute> dxAttributes;
        private readonly string propertyName;
        private readonly string displayName;

        private PropertyValidator(IEnumerable<ValidationAttribute> attributes, IEnumerable<DXValidationAttribute> dxAttributes, string propertyName, string displayName)
        {
            this.attributes = attributes;
            this.dxAttributes = dxAttributes;
            this.propertyName = propertyName;
            this.displayName = displayName;
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            ValidationContext context1 = new ValidationContext(instance, null, null);
            context1.MemberName = this.propertyName;
            context1.DisplayName = this.displayName;
            return context1;
        }

        public static PropertyValidator FromAttributes(IEnumerable attributes, string propertyName)
        {
            try
            {
                string displayName = DataAnnotationsAttributeHelper.GetDisplayName(attributes.OfType<Attribute>()) ?? propertyName;
                ValidationAttribute[] source = (attributes != null) ? attributes.OfType<ValidationAttribute>().ToArray<ValidationAttribute>() : new ValidationAttribute[0];
                DXValidationAttribute[] attributeArray2 = (attributes != null) ? attributes.OfType<DXValidationAttribute>().ToArray<DXValidationAttribute>() : new DXValidationAttribute[0];
                return ((source.Any<ValidationAttribute>() || attributeArray2.Any<DXValidationAttribute>()) ? new PropertyValidator(source, attributeArray2, propertyName, displayName) : null);
            }
            catch (TypeAccessException)
            {
                return null;
            }
        }

        public IEnumerable<string> GetErrors(object value, object instance)
        {
            Func<string, bool> predicate = <>c.<>9__7_2;
            if (<>c.<>9__7_2 == null)
            {
                Func<string, bool> local1 = <>c.<>9__7_2;
                predicate = <>c.<>9__7_2 = x => !string.IsNullOrEmpty(x);
            }
            return (from x in this.attributes select x.GetValidationResult(value, this.CreateValidationContext(instance))?.ErrorMessage).Concat<string>(this.dxAttributes.Select<DXValidationAttribute, string>(delegate (DXValidationAttribute x) {
                string displayName = this.displayName;
                if (this.displayName == null)
                {
                    string local1 = this.displayName;
                    displayName = this.propertyName;
                }
                return x.GetValidationResult(value, displayName, instance);
            })).Where<string>(predicate);
        }

        public string GetErrorText(object value, object instance)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.GetErrors(value, instance))
            {
                if (builder.Length > 0)
                {
                    builder.Append(' ');
                }
                builder.Append(str);
            }
            return builder.ToString();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyValidator.<>c <>9 = new PropertyValidator.<>c();
            public static Func<string, bool> <>9__7_2;

            internal bool <GetErrors>b__7_2(string x) => 
                !string.IsNullOrEmpty(x);
        }
    }
}

