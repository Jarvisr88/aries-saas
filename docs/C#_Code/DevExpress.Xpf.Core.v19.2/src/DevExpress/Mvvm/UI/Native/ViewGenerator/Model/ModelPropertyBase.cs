namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public abstract class ModelPropertyBase : IModelProperty
    {
        protected readonly object obj;
        protected readonly PropertyDescriptor property;
        protected readonly EditingContextBase context;
        private readonly IModelItem parent;

        public ModelPropertyBase(EditingContextBase context, object obj, PropertyDescriptor property, IModelItem parent)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNull(property, "property");
            Guard.ArgumentNotNull(parent, "parent");
            this.obj = obj;
            this.property = property;
            this.context = context;
            this.parent = parent;
        }

        void IModelProperty.ClearValue()
        {
            this.context.Trace.ClearModelPropertyValue(this);
            this.property.ResetValue(this.obj);
        }

        IModelItem IModelProperty.SetValue(object value)
        {
            this.context.Trace.SetModelPropertyValue(this, value);
            return this.SetValueCore(value);
        }

        protected virtual object GetComputedValue()
        {
            object element = ((ModelItemBase) this.parent).element;
            return this.property.GetValue(element);
        }

        protected abstract IModelItem GetValue();
        protected virtual bool IsPropertySet() => 
            ((IModelProperty) this).ComputedValue != null;

        protected abstract IModelItem SetValueCore(object value);

        public IModelItem Parent =>
            this.parent;

        string IModelProperty.Name =>
            this.property.Name;

        bool IModelProperty.IsSet =>
            this.IsPropertySet();

        bool IModelProperty.IsReadOnly =>
            this.property.IsReadOnly;

        object IModelProperty.ComputedValue =>
            this.GetComputedValue();

        IModelItemCollection IModelProperty.Collection
        {
            get
            {
                IEnumerable computedValue = this.GetComputedValue() as IEnumerable;
                return ((computedValue == null) ? null : this.context.CreateModelItemCollection(computedValue, this.parent));
            }
        }

        IModelItem IModelProperty.Value =>
            this.GetValue();

        IModelItemDictionary IModelProperty.Dictionary
        {
            get
            {
                IDictionary computedValue = this.GetComputedValue() as IDictionary;
                return ((computedValue == null) ? null : this.context.CreateModelItemDictionary(computedValue));
            }
        }

        Type IModelProperty.PropertyType =>
            this.property.PropertyType;
    }
}

