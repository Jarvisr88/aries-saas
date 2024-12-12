namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;

    public class FontEditStrategyValidator : LookUpEditStrategyValidator
    {
        public FontEditStrategyValidator(LookUpEditStrategyBase strategy, SelectorPropertiesCoercionHelper selectorUpdater, LookUpEditBase editor) : base(strategy, selectorUpdater, editor)
        {
        }

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource)
        {
            LookUpEditableItem item = value as LookUpEditableItem;
            return (((item == null) || this.Strategy.IsInLookUpMode) ? value : ((!this.Strategy.ShouldConfirm(item.DisplayValue, UpdateEditorSource.LostFocus) || !this.Strategy.Confirm(item.DisplayValue)) ? item.EditValue : item.DisplayValue));
        }

        protected FontEditStrategy Strategy =>
            base.Strategy as FontEditStrategy;
    }
}

