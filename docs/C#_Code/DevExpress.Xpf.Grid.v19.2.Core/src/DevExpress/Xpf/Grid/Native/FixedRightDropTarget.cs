namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public class FixedRightDropTarget : FixedDropTargetEx
    {
        public FixedRightDropTarget(Panel panel) : base(panel)
        {
        }

        protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType) => 
            base.WouldBeFirstFixedNoneColumn(sourceColumn) || (((sourceColumn.Fixed == FixedStyle.Left) && ((base.FixedNoneVisibleColumnsCount == 0) && (base.FixedLeftVisibleColumnsCount == 0))) || (sourceColumn.Fixed == FixedStyle.Right));

        protected override int CorrectDropIndex(int dropIndex) => 
            dropIndex;

        protected override IList<ColumnBase> GetColumnsList() => 
            base.TableView.TableViewBehavior.FixedRightVisibleColumns;

        protected override int DropIndexCorrection =>
            base.FixedLeftColumnsCount + base.FixedNoneColumnsCount;
    }
}

