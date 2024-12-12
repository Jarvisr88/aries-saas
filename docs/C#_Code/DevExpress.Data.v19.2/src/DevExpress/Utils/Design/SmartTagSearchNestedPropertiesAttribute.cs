namespace DevExpress.Utils.Design
{
    using System;
    using System.Runtime.CompilerServices;

    public class SmartTagSearchNestedPropertiesAttribute : Attribute
    {
        public SmartTagSearchNestedPropertiesAttribute()
        {
        }

        public SmartTagSearchNestedPropertiesAttribute(string Category)
        {
            this.Category = Category;
        }

        public string Category { get; set; }
    }
}

