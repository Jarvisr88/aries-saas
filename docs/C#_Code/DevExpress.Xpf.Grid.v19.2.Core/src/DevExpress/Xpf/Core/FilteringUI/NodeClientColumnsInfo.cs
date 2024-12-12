namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class NodeClientColumnsInfo
    {
        public NodeClientColumnsInfo(IList<FieldItem> columns, bool allowSearch)
        {
            IList<FieldItem> list1 = columns;
            if (columns == null)
            {
                IList<FieldItem> local1 = columns;
                list1 = new FieldItem[0];
            }
            this.<Columns>k__BackingField = list1;
            this.<AllowSearch>k__BackingField = allowSearch;
        }

        public IList<FieldItem> Columns { get; }

        public bool AllowSearch { get; }
    }
}

