namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;

    internal class x : IBindingList, ITypedList
    {
        internal DataViewManager a;
        private ListChangedEventHandler b;
        private PropertyDescriptorCollection c;

        private event ListChangedEventHandler System.ComponentModel.IBindingList.ListChanged
        {
            add
            {
                this.b += A_0;
            }
            remove
            {
                this.b -= A_0;
            }
        }

        public x(DataViewManager A_0)
        {
            this.a = A_0;
        }

        private bool a(object A_0) => 
            ((IList) this.a).Contains(A_0);

        private string a(PropertyDescriptor[] A_0) => 
            ((ITypedList) this.a).GetListName(A_0);

        private void a(PropertyDescriptor A_0)
        {
            ((IBindingList) this.a).RemoveIndex(A_0);
        }

        private void a(Array A_0, int A_1)
        {
            ((ICollection) this.a).CopyTo(A_0, A_1);
        }

        private void a(PropertyDescriptor A_0, ListSortDirection A_1)
        {
            ((IBindingList) this.a).ApplySort(A_0, A_1);
        }

        private int a(PropertyDescriptor A_0, object A_1) => 
            ((IBindingList) this.a).Find(A_0, A_1);

        internal void a(object A_0, ListChangedEventArgs A_1)
        {
            this.c = null;
            if (this.b != null)
            {
                try
                {
                    this.b(A_0, A_1);
                }
                catch
                {
                }
            }
        }

        private PropertyDescriptorCollection b(PropertyDescriptor[] A_0)
        {
            PropertyDescriptorCollection itemProperties;
            int num;
            PropertyDescriptor[] descriptorArray;
            if ((A_0 == null) || (A_0.Length == 0))
            {
                if (this.c == null)
                {
                    itemProperties = ((ITypedList) this.a).GetItemProperties(A_0);
                    this.c = new PropertyDescriptorCollection(null);
                    for (int i = 0; i < itemProperties.Count; i++)
                    {
                        if (this.a.DataSet.Tables[i] is DbDataTable)
                        {
                            PropertyDescriptor descriptor = new DataViewManagerPropertyDescriptor(itemProperties[i], (DbDataTable) this.a.DataSet.Tables[i]);
                            this.c.Add(descriptor);
                        }
                        else
                        {
                            this.c.Add(itemProperties[i]);
                        }
                    }
                }
                return this.c;
            }
            itemProperties = ((ITypedList) this.a).GetItemProperties(A_0);
            DbDataTable table = ((DbDataSet) this.a.DataSet).a(null, A_0, 0, out num);
            if (table == null)
            {
                return itemProperties;
            }
            if (num >= (A_0.Length - 1))
            {
                descriptorArray = null;
            }
            else
            {
                descriptorArray = new PropertyDescriptor[(A_0.Length - 1) - num];
                for (int i = num + 1; i < A_0.Length; i++)
                {
                    descriptorArray[(i - num) - 1] = A_0[i];
                }
            }
            return table.c(descriptorArray);
        }

        private void b(PropertyDescriptor A_0)
        {
            ((IBindingList) this.a).AddIndex(A_0);
        }

        private void b(int A_0)
        {
            ((IList) this.a).RemoveAt(A_0);
        }

        private int b(object A_0) => 
            ((IList) this.a).IndexOf(A_0);

        private void b(int A_0, object A_1)
        {
            ((IList) this.a).Insert(A_0, A_1);
        }

        private int c(object A_0) => 
            ((IList) this.a).Add(A_0);

        private void d(object A_0)
        {
            ((IList) this.a).Remove(A_0);
        }

        private IEnumerator e() => 
            ((IEnumerable) this.a).GetEnumerator();

        private object j() => 
            ((IBindingList) this.a).AddNew();

        private void n()
        {
            ((IList) this.a).Clear();
        }

        private void o()
        {
            ((IBindingList) this.a).RemoveSort();
        }

        [__DynamicallyInvokable]
        private int System.Collections.ICollection.Count =>
            ((ICollection) this.a).Count;

        [__DynamicallyInvokable]
        private bool System.Collections.ICollection.IsSynchronized =>
            ((ICollection) this.a).IsSynchronized;

        [__DynamicallyInvokable]
        private object System.Collections.ICollection.SyncRoot =>
            ((ICollection) this.a).SyncRoot;

        [__DynamicallyInvokable]
        private bool System.Collections.IList.IsFixedSize =>
            ((IList) this.a).IsFixedSize;

        [__DynamicallyInvokable]
        private bool System.Collections.IList.IsReadOnly =>
            ((IList) this.a).IsReadOnly;

        [__DynamicallyInvokable]
        private object this[int index]
        {
            get => 
                ((IList) this.a)[A_0];
            set => 
                ((IList) this.a)[A_0] = value;
        }

        private bool System.ComponentModel.IBindingList.AllowEdit =>
            ((IBindingList) this.a).AllowEdit;

        private bool System.ComponentModel.IBindingList.AllowNew =>
            ((IBindingList) this.a).AllowNew;

        private bool System.ComponentModel.IBindingList.AllowRemove =>
            ((IBindingList) this.a).AllowRemove;

        private bool System.ComponentModel.IBindingList.IsSorted =>
            ((IBindingList) this.a).IsSorted;

        private ListSortDirection System.ComponentModel.IBindingList.SortDirection =>
            ((IBindingList) this.a).SortDirection;

        private PropertyDescriptor System.ComponentModel.IBindingList.SortProperty =>
            ((IBindingList) this.a).SortProperty;

        private bool System.ComponentModel.IBindingList.SupportsChangeNotification =>
            ((IBindingList) this.a).SupportsChangeNotification;

        private bool System.ComponentModel.IBindingList.SupportsSearching =>
            ((IBindingList) this.a).SupportsSearching;

        private bool System.ComponentModel.IBindingList.SupportsSorting =>
            ((IBindingList) this.a).SupportsSorting;
    }
}

