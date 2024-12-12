namespace DevExpress.Xpf.Core.DragDrop.Native
{
    public interface IDropTargetValidator
    {
        MoveValidationState Validate(DropPointer dropPointer);
    }
}

