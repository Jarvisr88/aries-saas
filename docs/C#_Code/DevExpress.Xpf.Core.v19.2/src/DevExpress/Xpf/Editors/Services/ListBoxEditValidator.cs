namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class ListBoxEditValidator : EditorSpecificValidator
    {
        public ListBoxEditValidator(ListBoxEditStrategy strategy, SelectorPropertiesCoercionHelper selectorUpdater, ListBoxEdit editor) : base(editor)
        {
            this.Strategy = strategy;
            this.SelectorUpdater = selectorUpdater;
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new ListBoxEditStrategyValidator(this.Strategy, this.SelectorUpdater, (ListBoxEdit) base.OwnerEdit);

        private ListBoxEditStrategy Strategy { get; set; }

        private SelectorPropertiesCoercionHelper SelectorUpdater { get; set; }
    }
}

