namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Windows;

    public class LookupItemsProvider : IWeakEventListener
    {
        private IItemsProviderOwner owner;
        private IList itemSource;

        public LookupItemsProvider(IItemsProviderOwner owner)
        {
            this.owner = owner;
            this.itemSource = this.Owner.Items;
            this.Reset();
        }

        public static bool AreEqual(IList list1, IList list2)
        {
            if ((list1 != null) || (list2 != null))
            {
                if ((list1 == null) && (list2 != null))
                {
                    return (list2.Count == 0);
                }
                if ((list2 == null) && (list1 != null))
                {
                    return (list1.Count == 0);
                }
                if (list1.Count != list2.Count)
                {
                    return false;
                }
                for (int i = 0; i < list1.Count; i++)
                {
                    if (!list2.Contains(list1[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public object FindItem(string description, bool toLower) => 
            null;

        public virtual object GetDisplayValue(object value) => 
            null;

        private object GetDisplayValueCore(object value) => 
            null;

        public virtual object GetDisplayValueFromItem(object item) => 
            null;

        public object GetItem(object value) => 
            null;

        protected internal virtual object GetValueFromItem(object itemValue) => 
            null;

        public int IndexOf(object item) => 
            -1;

        public int IndexOfValue(object editValue) => 
            -1;

        private void OnListChanged()
        {
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(ListChangedEventManager)))
            {
                return false;
            }
            this.OnListChanged();
            return true;
        }

        public void Reset()
        {
        }

        protected IItemsProviderOwner Owner =>
            this.owner;

        public int Count =>
            this.itemSource.Count;

        public object this[int index] =>
            null;
    }
}

