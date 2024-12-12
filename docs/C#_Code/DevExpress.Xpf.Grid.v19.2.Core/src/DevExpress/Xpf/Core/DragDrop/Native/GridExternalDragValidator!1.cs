namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;

    internal class GridExternalDragValidator<T> : GridDragValidatorBase<T> where T: DataViewBase
    {
        public GridExternalDragValidator(T view) : base(view)
        {
        }

        public override MoveValidationState Validate(DropPointer dropPointer) => 
            (dropPointer.Position != DropPosition.Append) ? base.ValidateSortedDataDragDropRestrictions(false) : MoveValidationState.Valid;
    }
}

