namespace DevExpress.Data.Browsing.HierarchicalData
{
    using DevExpress.Data.Browsing;
    using System;
    using System.Collections.Generic;

    internal interface IHierarchicalListBrowser : IListBrowser
    {
        IList<int> Levels { get; }

        int CurrentLevel { get; }
    }
}

