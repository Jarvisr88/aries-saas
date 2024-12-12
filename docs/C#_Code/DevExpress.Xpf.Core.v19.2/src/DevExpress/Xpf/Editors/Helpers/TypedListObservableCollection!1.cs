namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class TypedListObservableCollection<T> : ObservableCollection<T>, ITypedList
    {
        public TypedListObservableCollection()
        {
        }

        public TypedListObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public TypedListObservableCollection(List<T> list) : base(list)
        {
        }

        private ICustomTypeDescriptor GetCustomTypedItem()
        {
            ICustomTypeDescriptor descriptor2;
            using (IEnumerator<T> enumerator = base.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        ICustomTypeDescriptor descriptor = current as ICustomTypeDescriptor;
                        if (descriptor == null)
                        {
                            continue;
                        }
                        descriptor2 = descriptor;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return descriptor2;
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            ICustomTypeDescriptor customTypedItem = this.GetCustomTypedItem();
            return ((customTypedItem == null) ? TypeDescriptor.GetProperties(typeof(T)) : customTypedItem.GetProperties());
        }

        public string GetListName(PropertyDescriptor[] listAccessors) => 
            null;
    }
}

