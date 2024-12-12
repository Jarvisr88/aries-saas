namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Linq;

    internal class GridInternalDragValidator<T> : GridDragValidatorBase<T> where T: DataViewBase
    {
        private readonly DragInfo activeDragInfo;
        private int sourceParentRowHandle;
        private bool allDraggingRowsAreInsideOneGroup;
        private bool draggingWholeGroup;

        public GridInternalDragValidator(T view, DragInfo activeDragInfo) : base(view)
        {
            Guard.ArgumentNotNull(activeDragInfo, "activeDragInfo");
            this.activeDragInfo = activeDragInfo;
            this.UpdateDragGroupInfo();
        }

        private int GetChildRowCount(int rowHande) => 
            base.DataControl.DataProviderBase.GetChildRowCount(rowHande);

        private int GetParentRowHandle(RowPointer pointer) => 
            base.DataControl.DataProviderBase.GetParentRowHandle(pointer.Handle);

        private int GetRowVisibleIndex(RowPointer pointer) => 
            base.DataControl.GetRowVisibleIndexByHandleCore(pointer.Handle);

        private bool IsDragGroupRowToItsChildren(DropPointer dropPointer) => 
            this.draggingWholeGroup && this.IsDragInsideGroup(dropPointer);

        private bool IsDragInsideGroup(DropPointer pointer) => 
            this.allDraggingRowsAreInsideOneGroup && (this.GetParentRowHandle(pointer.RowPointer) == this.sourceParentRowHandle);

        private bool IsDragRowToItself(DropPointer dropPointer)
        {
            if ((this.ActiveDragInfo.RowPointers == null) || (this.ActiveDragInfo.RowPointers.Length != 1))
            {
                return false;
            }
            RowPointer pointer = this.ActiveDragInfo.RowPointers.Single<RowPointer>();
            RowPointer rowPointer = dropPointer.RowPointer;
            if ((pointer.Handle < 0) || ((rowPointer.Handle < 0) || !pointer.Path.SequenceEqual<int>(rowPointer.Path)))
            {
                return false;
            }
            if ((base.DataControl.ActualGroupCountCore > 0) && (this.GetParentRowHandle(pointer) != this.GetParentRowHandle(rowPointer)))
            {
                return false;
            }
            int rowVisibleIndex = this.GetRowVisibleIndex(this.ActiveDragInfo.RowPointers[0]);
            if (rowVisibleIndex < 0)
            {
                return false;
            }
            int num2 = this.GetRowVisibleIndex(dropPointer.RowPointer);
            if (rowVisibleIndex < 0)
            {
                return false;
            }
            int num3 = num2 - rowVisibleIndex;
            return ((num3 == 0) || (((num3 == 1) && (dropPointer.Position == DropPosition.Before)) || ((num3 == -1) && (dropPointer.Position == DropPosition.After))));
        }

        private bool IsDragRowToItsParentGroupRow(DropPointer dropPointer) => 
            this.allDraggingRowsAreInsideOneGroup && (dropPointer.RowPointer.Handle == this.sourceParentRowHandle);

        private void UpdateDragGroupInfo()
        {
            if (base.DataControl.ActualGroupCountCore != 0)
            {
                RowPointer[] rowPointers = this.ActiveDragInfo.RowPointers;
                if ((rowPointers != null) && (rowPointers.Length != 0))
                {
                    int firstParentRowHandle = this.GetParentRowHandle(rowPointers.First<RowPointer>());
                    if (rowPointers.All<RowPointer>(x => ((GridInternalDragValidator<T>) this).GetParentRowHandle(x) == firstParentRowHandle))
                    {
                        this.sourceParentRowHandle = firstParentRowHandle;
                        this.allDraggingRowsAreInsideOneGroup = true;
                        this.draggingWholeGroup = this.GetChildRowCount(this.sourceParentRowHandle) == rowPointers.Length;
                    }
                }
            }
        }

        public override MoveValidationState Validate(DropPointer dropPointer) => 
            MoveValidationState.Valid.Validate(() => ((GridInternalDragValidator<T>) this).ValidateCriticalInternalDragRestrictions(dropPointer)).Validate(() => ((GridInternalDragValidator<T>) this).ValidateSortedDataDragDropRestrictions(dropPointer)).Validate(() => ((GridInternalDragValidator<T>) this).ValidateOverride(dropPointer));

        private MoveValidationState ValidateCriticalInternalDragRestrictions(DropPointer dropPointer)
        {
            if (((!base.View.AllowDropRecordToItself && this.IsDragRowToItself(dropPointer)) || this.IsDragGroupRowToItsChildren(dropPointer)) || this.IsDragRowToItsParentGroupRow(dropPointer))
            {
                return MoveValidationState.Fail;
            }
            return MoveValidationState.Valid;
        }

        protected virtual MoveValidationState ValidateOverride(DropPointer dropPointer) => 
            MoveValidationState.Valid;

        private MoveValidationState ValidateSortedDataDragDropRestrictions(DropPointer dropPointer) => 
            base.ValidateSortedDataDragDropRestrictions(this.IsDragInsideGroup(dropPointer));

        protected DragInfo ActiveDragInfo =>
            this.activeDragInfo;
    }
}

