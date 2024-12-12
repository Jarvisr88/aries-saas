namespace DevExpress.Data.Mask
{
    using System;

    public abstract class MaskManagerStated<TState> : MaskManager where TState: MaskManagerState
    {
        private TState currentState;
        private TState backupState;
        private MaskManagerStated<TState>.StateChangeType backupType;
        private TState cachedDState;
        private int cachedDCP;
        private int cachedDSA;
        private string cachedDT;

        protected MaskManagerStated(TState initialState);
        protected bool Apply(TState newState, MaskManagerStated<TState>.StateChangeType changeType);
        protected bool Apply(TState newState, MaskManagerStated<TState>.StateChangeType changeType, bool isNeededKeyCheck);
        private void ApplyInternal(TState newState, MaskManagerStated<TState>.StateChangeType changeType);
        public sealed override string GetCurrentEditText();
        public sealed override object GetCurrentEditValue();
        protected abstract int GetCursorPosition(TState state);
        protected abstract string GetDisplayText(TState state);
        protected abstract string GetEditText(TState state);
        protected abstract object GetEditValue(TState state);
        protected abstract int GetSelectionAnchor(TState state);
        protected virtual bool IsValid(TState newState);
        protected void SetInitialState(TState newState);
        public override bool SpinDown();
        public override bool SpinUp();
        public override bool Undo();
        private void VerifyCache();

        protected TState CurrentState { get; }

        public override bool CanUndo { get; }

        public sealed override int DisplayCursorPosition { get; }

        public sealed override int DisplaySelectionAnchor { get; }

        public sealed override string DisplayText { get; }

        protected enum StateChangeType
        {
            public const MaskManagerStated<TState>.StateChangeType Insert = MaskManagerStated<TState>.StateChangeType.Insert;,
            public const MaskManagerStated<TState>.StateChangeType Delete = MaskManagerStated<TState>.StateChangeType.Delete;,
            public const MaskManagerStated<TState>.StateChangeType Terminator = MaskManagerStated<TState>.StateChangeType.Terminator;
        }
    }
}

