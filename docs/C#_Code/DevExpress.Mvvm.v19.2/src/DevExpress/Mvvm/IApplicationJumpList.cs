namespace DevExpress.Mvvm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IApplicationJumpList : IList<ApplicationJumpItemInfo>, ICollection<ApplicationJumpItemInfo>, IEnumerable<ApplicationJumpItemInfo>, IEnumerable
    {
        bool AddOrReplace(ApplicationJumpTaskInfo jumpTask);
        ApplicationJumpTaskInfo Find(string commandId);
    }
}

