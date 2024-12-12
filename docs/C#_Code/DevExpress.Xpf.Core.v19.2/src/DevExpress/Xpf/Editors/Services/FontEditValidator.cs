namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class FontEditValidator : LookUpEditValidator
    {
        public FontEditValidator(LookUpEditStrategyBase strategy, SelectorPropertiesCoercionHelper coercionHelper, LookUpEditBase editor) : base(strategy, coercionHelper, editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new FontEditStrategyValidator(base.Strategy, base.CoercionHelper, (LookUpEditBase) base.OwnerEdit);
    }
}

