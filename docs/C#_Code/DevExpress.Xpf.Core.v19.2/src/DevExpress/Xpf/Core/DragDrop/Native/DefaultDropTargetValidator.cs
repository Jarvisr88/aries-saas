namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    internal sealed class DefaultDropTargetValidator : IDropTargetValidator
    {
        public MoveValidationState Validate(DropPointer dropPointer) => 
            MoveValidationState.Valid;
    }
}

