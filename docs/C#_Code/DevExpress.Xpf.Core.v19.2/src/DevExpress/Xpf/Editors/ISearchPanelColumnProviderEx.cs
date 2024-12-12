namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface ISearchPanelColumnProviderEx : ISearchPanelColumnProviderBase
    {
        IList Columns { get; }

        bool IsServerMode { get; }

        IList ColumnsForceWithoutPrefix { get; }

        IList<CustomFilterColumn> CustomFilterColumns { get; }
    }
}

