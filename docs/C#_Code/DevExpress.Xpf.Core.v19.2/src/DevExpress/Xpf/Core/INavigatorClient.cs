namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface INavigatorClient : INotifyPropertyChanged
    {
        IEnumerable<INavigationItem> Items { get; }

        IList<IBarManagerControllerAction> MenuActions { get; }

        INavigationItem SelectedItem { get; set; }

        bool AcceptsCompactNavigation { get; }

        bool Compact { get; set; }

        int PeekFormShowDelay { get; }

        int PeekFormHideDelay { get; }

        bool IsAttached { get; set; }

        int MaxVisibleItems { set; }
    }
}

