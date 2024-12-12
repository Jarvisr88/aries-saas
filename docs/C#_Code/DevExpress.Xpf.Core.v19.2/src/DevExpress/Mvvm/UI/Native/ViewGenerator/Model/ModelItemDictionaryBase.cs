namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ModelItemDictionaryBase : IModelItemDictionary
    {
        private readonly IDictionary computedValue;
        private readonly EditingContextBase context;

        public ModelItemDictionaryBase(EditingContextBase context, IDictionary computedValue)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNull(computedValue, "computedValue");
            this.computedValue = computedValue;
            this.context = context;
        }

        void IModelItemDictionary.Add(object key, object value)
        {
            this.computedValue.Add(ModelItemBase.GetUnderlyingObject(key), ModelItemBase.GetUnderlyingObject(value));
        }

        IEnumerable<IModelItem> IModelItemDictionary.Keys =>
            from key in this.computedValue.Keys.Cast<object>() select this.context.CreateModelItem(key, null);

        IEnumerable<IModelItem> IModelItemDictionary.Values =>
            from key in this.computedValue.Values.Cast<object>() select this.context.CreateModelItem(key, null);

        IModelItem IModelItemDictionary.this[IModelItem key]
        {
            get => 
                this.context.CreateModelItem(this.computedValue[key.GetCurrentValue()], null);
            set
            {
                throw new NotImplementedException();
            }
        }

        public IModelItem this[object key]
        {
            get => 
                this.context.CreateModelItem(this.computedValue[key], null);
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

