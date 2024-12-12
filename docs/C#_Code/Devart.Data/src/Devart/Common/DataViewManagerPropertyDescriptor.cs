namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;

    public class DataViewManagerPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor a;
        private DbDataTable b;

        public DataViewManagerPropertyDescriptor(PropertyDescriptor originalDescriptor, DbDataTable dataTable) : base(originalDescriptor.Name, null)
        {
            this.a = originalDescriptor;
            this.b = dataTable;
        }

        public override bool CanResetValue(object component) => 
            this.a.CanResetValue(component);

        public override object GetValue(object component) => 
            ((IListSource) ((DataView) this.a.GetValue(component)).Table).GetList();

        public override void ResetValue(object component)
        {
            this.a.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            this.a.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component) => 
            this.a.ShouldSerializeValue(component);

        public override Type ComponentType =>
            this.a.ComponentType;

        public override bool IsReadOnly =>
            this.a.IsReadOnly;

        public override Type PropertyType =>
            this.a.PropertyType;

        public override TypeConverter Converter =>
            this.a.Converter;

        internal DbDataTable DataTable =>
            this.b;
    }
}

