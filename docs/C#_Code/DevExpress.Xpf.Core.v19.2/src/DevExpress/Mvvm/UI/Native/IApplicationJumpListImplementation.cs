namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.Generic;

    public interface IApplicationJumpListImplementation
    {
        void AddItem(ApplicationJumpItemInfo item);
        bool AddOrReplace(ApplicationJumpTaskInfo jumpTask);
        void ClearItems();
        bool ContainsItem(ApplicationJumpItemInfo item);
        ApplicationJumpTaskInfo Find(string commandId);
        ApplicationJumpItemInfo GetItem(int index);
        IEnumerable<ApplicationJumpItemInfo> GetItems();
        int IndexOfItem(ApplicationJumpItemInfo item);
        void InsertItem(int index, ApplicationJumpItemInfo item);
        int ItemsCount();
        bool RemoveItem(ApplicationJumpItemInfo item);
        void RemoveItemAt(int index);
        void SetItem(int index, ApplicationJumpItemInfo item);
    }
}

