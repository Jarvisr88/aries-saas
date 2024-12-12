namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class ValidationState
    {
        public ValidationState(EditStrategyBase strategy, BaseEdit editor)
        {
            this.<Strategy>k__BackingField = strategy;
            this.<Editor>k__BackingField = editor;
            this.IsEmpty = true;
            this.IsValid = true;
        }

        public void Initialize(bool isValid, BaseValidationError error)
        {
            this.IsEmpty = false;
            this.IsValid = isValid;
            this.EditorValidationError = error;
        }

        public void Reset()
        {
            this.IsEmpty = true;
            this.IsValid = true;
            this.EditorValidationError = null;
        }

        public void TryReset(UpdateEditorSource updateSource)
        {
            if (this.Strategy.ShouldResetValidateState(updateSource))
            {
                this.Reset();
            }
        }

        private BaseEdit Editor { get; }

        public BaseValidationError EditorValidationError { get; private set; }

        public bool IsEmpty { get; private set; }

        public bool IsValid { get; private set; }

        private EditStrategyBase Strategy { get; }
    }
}

