namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ValueEditingProcessor
    {
        private NullableContainer displayText = new NullableContainer();

        public ValueEditingProcessor()
        {
            this.EditingActions = new List<ValueEditingActionBase>();
            this.EditingData = new ValueEditingData();
        }

        public void Register(ValueEditingActionBase valueEditingAction)
        {
            this.EditingActions.Add(valueEditingAction);
        }

        public void SetDisplayText(string text)
        {
            this.displayText.SetValue(text);
        }

        private List<ValueEditingActionBase> EditingActions { get; set; }

        private ValueEditingActionBase CurrentAction { get; set; }

        private ValueEditingData EditingData { get; set; }

        public string DisplayText =>
            this.displayText.HasValue ? ((string) this.displayText.Value) : string.Empty;
    }
}

