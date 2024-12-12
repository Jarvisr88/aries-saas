namespace DevExpress.Xpo
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    [TypeConverter(typeof(SortProperty.SortPropertyConverter))]
    public sealed class SortProperty
    {
        private CriteriaOperator property;
        private SortingDirection direction;

        public SortProperty() : this((CriteriaOperator) null, SortingDirection.Ascending)
        {
        }

        public SortProperty(CriteriaOperator property, SortingDirection direction)
        {
            this.property = property;
            this.direction = direction;
        }

        public SortProperty(string propertyName, SortingDirection sorting) : this(CriteriaOperator.Parse(propertyName, new object[0]), sorting)
        {
        }

        [DefaultValue(""), Browsable(false)]
        public string PropertyName
        {
            get => 
                (this.property == null) ? string.Empty : this.property.ToString();
            set => 
                this.property = string.IsNullOrEmpty(value) ? null : CriteriaOperator.Parse(value, new object[0]);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CriteriaOperator Property
        {
            get => 
                this.property;
            set => 
                this.property = value;
        }

        [DefaultValue(0)]
        public SortingDirection Direction
        {
            get => 
                this.direction;
            set => 
                this.direction = value;
        }

        internal class SortPropertyConverter : TypeConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
                !(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object val, Type destinationType)
            {
                if ((destinationType == typeof(InstanceDescriptor)) && (val is SortProperty))
                {
                    Type[] types = new Type[] { typeof(string), typeof(SortingDirection) };
                    ConstructorInfo constructor = typeof(SortProperty).GetConstructor(types);
                    if (constructor != null)
                    {
                        object[] arguments = new object[] { ((SortProperty) val).PropertyName, ((SortProperty) val).Direction };
                        return new InstanceDescriptor(constructor, arguments);
                    }
                }
                return base.ConvertTo(context, culture, val, destinationType);
            }
        }
    }
}

