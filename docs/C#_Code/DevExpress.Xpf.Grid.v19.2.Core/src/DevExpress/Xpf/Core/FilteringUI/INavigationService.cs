namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;

    internal interface INavigationService
    {
        IReadOnlyCollection<NavigationPath> GetPaths();
        void SetIsFocused(NavigationPath path, bool isFocused);
    }
}

