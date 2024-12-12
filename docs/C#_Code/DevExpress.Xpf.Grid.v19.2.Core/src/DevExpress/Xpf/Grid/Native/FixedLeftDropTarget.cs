namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public class FixedLeftDropTarget : FixedDropTargetEx
    {
        public FixedLeftDropTarget(Panel panel) : base(panel)
        {
        }

        protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType) => 
            base.WouldBeFirstFixedNoneColumn(sourceColumn) || (((sourceColumn.Fixed == FixedStyle.Right) && ((base.FixedNoneVisibleColumnsCount == 0) && (base.FixedRightVisibleColumnsCount == 0))) || (sourceColumn.Fixed == FixedStyle.Left));

        protected override int CorrectDropIndex(int dropIndex) => 
            (!base.TableView.IsCheckBoxSelectorColumnVisible || (dropIndex != 0)) ? dropIndex : -1;

        protected override IList<ColumnBase> GetColumnsList() => 
            base.TableView.TableViewBehavior.FixedLeftVisibleColumns;

        protected override int DropIndexCorrection =>
            0;
    }
}

