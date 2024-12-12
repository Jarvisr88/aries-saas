namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MoveValidationStateExtensions
    {
        public static MoveValidationState Validate(this MoveValidationState state, Func<MoveValidationState> validationFunc) => 
            (state == MoveValidationState.Valid) ? validationFunc() : state;
    }
}

