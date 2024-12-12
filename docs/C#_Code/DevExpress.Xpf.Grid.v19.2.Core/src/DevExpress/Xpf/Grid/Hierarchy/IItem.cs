namespace DevExpress.Xpf.Grid.Hierarchy
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public interface IItem : ISupportVisibleIndex
    {
        FrameworkElement Element { get; }

        FrameworkElement AdditionalElement { get; }

        double AdditionalElementWidth { get; }

        double AdditionalElementOffset { get; }

        double GridAreaWidth { get; }

        IItemsContainer ItemsContainer { get; }

        bool IsFixedItem { get; }

        bool IsItemsContainer { get; }

        bool IsRowVisible { get; }

        DevExpress.Xpf.Grid.FixedRowPosition FixedRowPosition { get; }
    }
}

