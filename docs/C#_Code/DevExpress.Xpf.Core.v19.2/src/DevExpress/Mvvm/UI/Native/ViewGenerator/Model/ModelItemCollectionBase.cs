namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ModelItemCollectionBase : IModelItemCollection, IEnumerable<IModelItem>, IEnumerable
    {
        protected readonly IEnumerable computedValue;
        protected readonly EditingContextBase context;
        protected readonly IModelItem parent;

        public ModelItemCollectionBase(EditingContextBase context, IEnumerable computedValue, IModelItem parent)
        {
            Guard.ArgumentNotNull(computedValue, "computedValue");
            Guard.ArgumentNotNull(context, "context");
            this.computedValue = computedValue;
            this.context = context;
            this.parent = parent;
        }

        public void Clear()
        {
            this.context.Trace.ClearModelItemCollection(this);
            IList computedValue = this.computedValue as IList;
            if (computedValue == null)
            {
                throw new InvalidOperationException();
            }
            computedValue.Clear();
        }

        void IModelItemCollection.Add(IModelItem value)
        {
            ((IModelItemCollection) this).Add(value.GetCurrentValue());
        }

        IModelItem IModelItemCollection.Add(object value)
        {
            this.context.Trace.AddModelItem(this, value);
            IList computedValue = this.computedValue as IList;
            if (computedValue == null)
            {
                throw new InvalidOperationException();
            }
            object currentValue = value;
            IModelItem item = value as IModelItem;
            if (item != null)
            {
                currentValue = item.GetCurrentValue();
            }
            else
            {
                item = this.context.CreateModelItem(currentValue, this.parent);
            }
            computedValue.Add(currentValue);
            this.OnChanged();
            return item;
        }

        bool IModelItemCollection.Remove(IModelItem item) => 
            ((IModelItemCollection) this).Remove(item.GetCurrentValue());

        bool IModelItemCollection.Remove(object item)
        {
            IList computedValue = this.computedValue as IList;
            if (computedValue == null)
            {
                throw new InvalidOperationException();
            }
            object obj2 = (item is IModelItem) ? ((IModelItem) item).GetCurrentValue() : item;
            if (!computedValue.Contains(obj2))
            {
                return false;
            }
            computedValue.Remove(obj2);
            this.OnChanged();
            return true;
        }

        private IEnumerator<IModelItem> GetEnumerator() => 
            (from o in this.computedValue.Cast<object>() select this.context.CreateModelItem(o, this.parent)).GetEnumerator();

        public int IndexOf(IModelItem item)
        {
            IList computedValue = this.computedValue as IList;
            if (computedValue == null)
            {
                throw new InvalidOperationException();
            }
            return computedValue.IndexOf(item.GetCurrentValue());
        }

        public void Insert(int index, IModelItem valueItem)
        {
            this.Insert(index, valueItem.GetCurrentValue());
        }

        public void Insert(int index, object value)
        {
            IList computedValue = this.computedValue as IList;
            if (computedValue == null)
            {
                throw new InvalidOperationException();
            }
            computedValue.Insert(index, value);
        }

        protected virtual void OnChanged()
        {
        }

        public void RemoveAt(int index)
        {
            IList computedValue = this.computedValue as IList;
            if (computedValue == null)
            {
                throw new InvalidOperationException();
            }
            computedValue.RemoveAt(index);
        }

        IEnumerator<IModelItem> IEnumerable<IModelItem>.GetEnumerator() => 
            this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public IModelItem this[int index]
        {
            get
            {
                IList computedValue = this.computedValue as IList;
                if (computedValue == null)
                {
                    throw new InvalidOperationException();
                }
                return this.context.CreateModelItem(computedValue[index], this.parent);
            }
            set
            {
                IList computedValue = this.computedValue as IList;
                if (computedValue == null)
                {
                    throw new InvalidOperationException();
                }
                computedValue[index] = value.GetCurrentValue();
            }
        }
    }
}

