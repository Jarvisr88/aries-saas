namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class EF7PropertyInfo : IEdmPropertyInfo
    {
        private readonly PropertyInfo property;

        public EF7PropertyInfo(PropertyInfo property)
        {
            this.property = property;
        }

        public IEdmPropertyInfo AddAttributes(IEnumerable<Attribute> newAttributes)
        {
            throw new NotSupportedException();
        }

        public string Name =>
            this.property.Name;

        public string DisplayName =>
            this.property.Name;

        public Type PropertyType =>
            this.property.PropertyType;

        public bool IsForeignKey
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public DataColumnAttributes Attributes
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public object ContextObject
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool IsNavigationProperty
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}

