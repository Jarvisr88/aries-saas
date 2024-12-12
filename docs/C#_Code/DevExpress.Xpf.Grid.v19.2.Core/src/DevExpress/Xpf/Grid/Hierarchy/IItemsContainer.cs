namespace DevExpress.Xpf.Grid.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public interface IItemsContainer
    {
        IList<IItem> Items { get; }

        Size DesiredSize { get; set; }

        Size RenderSize { get; set; }

        double AnimationProgress { get; }
    }
}

