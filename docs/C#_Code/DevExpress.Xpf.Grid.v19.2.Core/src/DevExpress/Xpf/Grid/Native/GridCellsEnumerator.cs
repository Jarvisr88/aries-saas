namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Windows;

    public class GridCellsEnumerator : VisualTreeEnumerator
    {
        private readonly FrameworkElement row;

        public GridCellsEnumerator(FrameworkElement row) : base(row, EnumeratorDirection.Forward)
        {
            this.row = row;
        }

        protected override IEnumerator GetNestedObjects(object obj) => 
            (this.CurrentNavigationIndex < 0) ? base.GetNestedObjects(obj) : NestedObjectEnumeratorBase.EmptyEnumerator;

        public override bool MoveNext()
        {
            while (base.MoveNext())
            {
                DataViewBase objA = DataControlBase.FindCurrentView(base.Current);
                if ((this.CurrentNavigationIndex >= 0) && ((objA != null) && ReferenceEquals(objA, this.RowCurrentView)))
                {
                    return true;
                }
            }
            return false;
        }

        internal DataViewBase RowCurrentView =>
            !(this.row is IAdditionalRowElement) ? ((RowDataBase) this.row.DataContext).View : ((IAdditionalRowElement) this.row).RowCurrentView;

        public int CurrentNavigationIndex =>
            ColumnBase.GetNavigationIndex(base.Current);
    }
}

