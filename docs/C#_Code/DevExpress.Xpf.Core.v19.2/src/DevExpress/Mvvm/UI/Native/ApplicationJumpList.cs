namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ApplicationJumpList : IApplicationJumpList, IList<ApplicationJumpItemInfo>, ICollection<ApplicationJumpItemInfo>, IEnumerable<ApplicationJumpItemInfo>, IEnumerable
    {
        private IApplicationJumpListImplementation implementation;

        public ApplicationJumpList(IApplicationJumpListImplementation implementation)
        {
            if (implementation == null)
            {
                throw new ArgumentNullException("implementation");
            }
            this.implementation = implementation;
        }

        bool IApplicationJumpList.AddOrReplace(ApplicationJumpTaskInfo jumpTask) => 
            this.implementation.AddOrReplace(jumpTask);

        ApplicationJumpTaskInfo IApplicationJumpList.Find(string commandId) => 
            this.implementation.Find(commandId);

        void ICollection<ApplicationJumpItemInfo>.Add(ApplicationJumpItemInfo item)
        {
            this.implementation.AddItem(item);
        }

        void ICollection<ApplicationJumpItemInfo>.Clear()
        {
            this.implementation.ClearItems();
        }

        bool ICollection<ApplicationJumpItemInfo>.Contains(ApplicationJumpItemInfo item) => 
            this.implementation.ContainsItem(item);

        void ICollection<ApplicationJumpItemInfo>.CopyTo(ApplicationJumpItemInfo[] array, int arrayIndex)
        {
            foreach (ApplicationJumpItemInfo info in this.implementation.GetItems())
            {
                array[arrayIndex] = info;
                arrayIndex++;
            }
        }

        bool ICollection<ApplicationJumpItemInfo>.Remove(ApplicationJumpItemInfo item) => 
            this.implementation.RemoveItem(item);

        IEnumerator<ApplicationJumpItemInfo> IEnumerable<ApplicationJumpItemInfo>.GetEnumerator() => 
            this.implementation.GetItems().GetEnumerator();

        int IList<ApplicationJumpItemInfo>.IndexOf(ApplicationJumpItemInfo item) => 
            this.implementation.IndexOfItem(item);

        void IList<ApplicationJumpItemInfo>.Insert(int index, ApplicationJumpItemInfo item)
        {
            this.implementation.InsertItem(index, item);
        }

        void IList<ApplicationJumpItemInfo>.RemoveAt(int index)
        {
            this.implementation.RemoveItemAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.implementation.GetItems().GetEnumerator();

        ApplicationJumpItemInfo IList<ApplicationJumpItemInfo>.this[int index]
        {
            get => 
                this.implementation.GetItem(index);
            set => 
                this.implementation.SetItem(index, value);
        }

        int ICollection<ApplicationJumpItemInfo>.Count =>
            this.implementation.ItemsCount();

        bool ICollection<ApplicationJumpItemInfo>.IsReadOnly =>
            false;
    }
}

