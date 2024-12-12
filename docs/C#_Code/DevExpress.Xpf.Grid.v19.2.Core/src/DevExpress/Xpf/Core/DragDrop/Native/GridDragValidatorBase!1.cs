namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal abstract class GridDragValidatorBase<T> : IDropTargetValidator where T: DataViewBase
    {
        private readonly T view;

        public GridDragValidatorBase(T view)
        {
            Guard.ArgumentNotNull(view, "view");
            this.view = view;
        }

        private bool HasReadOnlyColumns()
        {
            Func<ColumnBase, bool> predicate = <>c<T>.<>9__8_1;
            if (<>c<T>.<>9__8_1 == null)
            {
                Func<ColumnBase, bool> local1 = <>c<T>.<>9__8_1;
                predicate = <>c<T>.<>9__8_1 = y => y != null;
            }
            Func<ColumnBase, bool> func2 = <>c<T>.<>9__8_2;
            if (<>c<T>.<>9__8_2 == null)
            {
                Func<ColumnBase, bool> local2 = <>c<T>.<>9__8_2;
                func2 = <>c<T>.<>9__8_2 = z => z.ReadOnly;
            }
            return (from x in this.DataControl.ActualSortInfo select base.DataControl.ColumnsCore[x.FieldName]).Where<ColumnBase>(predicate).Any<ColumnBase>(func2);
        }

        public abstract MoveValidationState Validate(DropPointer dropPointer);
        protected MoveValidationState ValidateSortedDataDragDropRestrictions(bool isDragInsideGroup) => 
            ((this.DataControl.ActualSortInfo.Count == 0) || ((this.DataControl.ActualSortInfo.Count == this.DataControl.ActualGroupCountCore) & isDragInsideGroup)) ? MoveValidationState.Valid : (!this.View.AllowSortedDataDragDrop ? MoveValidationState.Fail : (this.HasReadOnlyColumns() ? MoveValidationState.Warning : MoveValidationState.Valid));

        protected internal T View =>
            this.view;

        protected DataControlBase DataControl =>
            this.View.DataControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridDragValidatorBase<T>.<>c <>9;
            public static Func<ColumnBase, bool> <>9__8_1;
            public static Func<ColumnBase, bool> <>9__8_2;

            static <>c()
            {
                GridDragValidatorBase<T>.<>c.<>9 = new GridDragValidatorBase<T>.<>c();
            }

            internal bool <HasReadOnlyColumns>b__8_1(ColumnBase y) => 
                y != null;

            internal bool <HasReadOnlyColumns>b__8_2(ColumnBase z) => 
                z.ReadOnly;
        }
    }
}

