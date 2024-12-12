namespace DevExpress.Xpf.Grid.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EmptyItemsContainer : IItemsContainer
    {
        public static readonly IItemsContainer Instance = new EmptyItemsContainer();
        private static readonly IItem[] EmptyItems = new IItem[0];

        private EmptyItemsContainer()
        {
        }

        IList<IItem> IItemsContainer.Items =>
            EmptyItems;

        Size IItemsContainer.DesiredSize { get; set; }

        Size IItemsContainer.RenderSize { get; set; }

        double IItemsContainer.AnimationProgress =>
            1.0;
    }
}

