namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class LookUpEditValidator : EditorSpecificValidator
    {
        public LookUpEditValidator(LookUpEditStrategyBase strategy, SelectorPropertiesCoercionHelper coercionHelper, LookUpEditBase editor) : base(editor)
        {
            this.Strategy = strategy;
            this.CoercionHelper = coercionHelper;
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new LookUpEditStrategyValidator(this.Strategy, this.CoercionHelper, (LookUpEditBase) base.OwnerEdit);

        protected LookUpEditStrategyBase Strategy { get; private set; }

        protected SelectorPropertiesCoercionHelper CoercionHelper { get; private set; }
    }
}

