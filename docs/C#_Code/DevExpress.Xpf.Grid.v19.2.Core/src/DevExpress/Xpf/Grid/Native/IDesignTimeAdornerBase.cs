namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public interface IDesignTimeAdornerBase
    {
        IModelItem CreateModelItem(object obj, IModelItem parent);
        Type GetDefaultColumnType(ColumnBase column);
        DataViewBase GetDefaultView(DataControlBase dataControl);
        void InvalidateDataSource();
        bool IsSelectGridArea(Point point);
        void OnColumnHeaderClick();
        void OnColumnMoved();
        void OnColumnResized();
        void OnColumnsLayoutChanged();
        void RemoveGeneratedColumns(DataControlBase dataControl);
        void SelectModelItem(IModelItem item);
        void ShowDialogContent(FrameworkElement content, FrameworkElement root, Size size, FloatingContainerParameters containerParameters);
        void UpdateDesignTimeInfo();
        void UpdateVisibleIndexes(DataControlBase dataControl);

        bool ForceAllowUseColumnInFilterControl { get; }

        bool IsDesignTime { get; }

        bool SkipColumnXamlGenerationProperties { get; set; }

        IModelItem DataControlModelItem { get; }
    }
}

