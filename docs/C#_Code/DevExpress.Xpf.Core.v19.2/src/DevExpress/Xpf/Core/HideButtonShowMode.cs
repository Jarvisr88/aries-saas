namespace DevExpress.Xpf.Core
{
    using System;

    [Flags]
    public enum HideButtonShowMode
    {
        NoWhere = 0,
        InAllTabs = 1,
        InActiveTab = 2,
        InHeaderArea = 4,
        InAllTabsAndHeaderArea = 5,
        InActiveTabAndHeaderArea = 6
    }
}

