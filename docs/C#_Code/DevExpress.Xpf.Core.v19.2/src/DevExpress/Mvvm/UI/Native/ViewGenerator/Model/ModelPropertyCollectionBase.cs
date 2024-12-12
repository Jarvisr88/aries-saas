namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public abstract class ModelPropertyCollectionBase : IModelPropertyCollection, IEnumerable<IModelProperty>, IEnumerable
    {
        protected readonly object obj;
        protected readonly EditingContextBase context;
        protected readonly IModelItem parent;

        public ModelPropertyCollectionBase(EditingContextBase context, object obj, IModelItem parent)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNull(parent, "parent");
            this.obj = obj;
            this.context = context;
            this.parent = parent;
        }

        IModelProperty IModelPropertyCollection.Find(string propertyName) => 
            this.FindCore(propertyName, null);

        IModelProperty IModelPropertyCollection.Find(Type propertyType, string propertyName)
        {
            if (propertyType == null)
            {
                throw new ArgumentNullException();
            }
            return this.FindCore(propertyName, propertyType);
        }

        protected abstract IModelProperty FindCore(string propertyName, Type type);
        public abstract IEnumerator<IModelProperty> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public override string ToString()
        {
            string str = string.Empty;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this.obj))
            {
                str = str + descriptor.Name + ",";
            }
            return str;
        }

        IModelProperty IModelPropertyCollection.this[string propertyName]
        {
            get
            {
                IModelProperty property = this.FindCore(propertyName, null);
                if (property == null)
                {
                    throw new ArgumentException();
                }
                return property;
            }
        }

        IModelProperty IModelPropertyCollection.this[DXPropertyIdentifier propertyIdentifier]
        {
            get
            {
                IModelProperty property = this.FindCore(propertyIdentifier.Name, propertyIdentifier.DeclaringType);
                if (property == null)
                {
                    throw new ArgumentException();
                }
                return property;
            }
        }
    }
}

