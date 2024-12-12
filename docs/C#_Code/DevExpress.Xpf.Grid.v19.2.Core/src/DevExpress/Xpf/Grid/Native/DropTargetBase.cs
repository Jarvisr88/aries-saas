namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class DropTargetBase : ColumnHeaderDropTargetBase
    {
        public DropTargetBase(Panel panel) : base(panel)
        {
        }

        protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType) => 
            true;

        protected override bool ContainsColumn(FrameworkElement element, ColumnBase column) => 
            element.DataContext == column;

        protected override bool DenyDropIfGroupingIsNotAllowed(HeaderPresenterType sourceType) => 
            sourceType == HeaderPresenterType.GroupPanel;

        protected override int DropIndexCorrection =>
            0;
    }
}

