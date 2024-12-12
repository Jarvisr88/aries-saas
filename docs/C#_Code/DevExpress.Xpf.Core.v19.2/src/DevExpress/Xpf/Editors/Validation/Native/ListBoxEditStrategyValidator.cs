namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;
    using System.Runtime.CompilerServices;

    public class ListBoxEditStrategyValidator : StrategyValidatorBase
    {
        public ListBoxEditStrategyValidator(ListBoxEditStrategy strategy, SelectorPropertiesCoercionHelper selectorUpdater, ListBoxEdit editor) : base(editor)
        {
            this.SelectorUpdater = selectorUpdater;
            this.Strategy = strategy;
        }

        public override string GetValidationError() => 
            "hidden error";

        private bool IndexFromItemsSource(int index) => 
            index > -1;

        protected override bool IsValidValue(object value, object convertedValue)
        {
            LookUpEditableItem item = value as LookUpEditableItem;
            return (((item == null) || (item.Completed || !item.AsyncLoading)) ? base.IsValidValue(value, convertedValue) : false);
        }

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            this.ProcessSingleConversion(value, updateEditorSource);

        private object ProcessSingleConversion(object value, UpdateEditorSource updateEditorSource)
        {
            LookUpEditableItem item = value as LookUpEditableItem;
            if (item == null)
            {
                return base.ProcessConversion(value, updateEditorSource);
            }
            object editValue = item.EditValue;
            if (item.Completed)
            {
                return editValue;
            }
            if (!this.Strategy.IsInLookUpMode)
            {
                return base.ProcessConversion(editValue, updateEditorSource);
            }
            int indexFromEditValue = this.SelectorUpdater.GetIndexFromEditValue(item.EditValue);
            return (!this.IndexFromItemsSource(indexFromEditValue) ? editValue : this.SelectorUpdater.GetEditValueFromSelectedIndex(indexFromEditValue));
        }

        protected ListBoxEdit Editor =>
            base.Editor as ListBoxEdit;

        private ListBoxEditStrategy Strategy { get; set; }

        private SelectorPropertiesCoercionHelper SelectorUpdater { get; set; }
    }
}

