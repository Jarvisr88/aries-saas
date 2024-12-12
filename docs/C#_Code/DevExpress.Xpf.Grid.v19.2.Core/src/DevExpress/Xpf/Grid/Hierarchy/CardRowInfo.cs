namespace DevExpress.Xpf.Grid.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CardRowInfo
    {
        public System.Windows.Size Size { get; set; }

        public System.Windows.Size RenderSize { get; set; }

        public IList<IItem> Elements { get; set; }

        public int Level { get; set; }

        public bool IsItemsContainer { get; set; }

        public bool HasSeparator { get; set; }
    }
}

