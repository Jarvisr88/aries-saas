namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class EditorSpecificValidator : BaseEditBaseService
    {
        private StrategyValidatorBase validator;

        public EditorSpecificValidator(BaseEdit editor) : base(editor)
        {
        }

        protected virtual StrategyValidatorBase CreateValidator() => 
            new StrategyValidatorBase(base.OwnerEdit);

        public bool DoValidate(object value, object convertedValue, UpdateEditorSource source) => 
            this.Validator.DoValidate(value, convertedValue, source);

        public object GetValidationError() => 
            this.Validator.GetValidationError();

        public object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            this.Validator.ProcessConversion(value, updateEditorSource);

        protected StrategyValidatorBase Validator
        {
            get
            {
                StrategyValidatorBase validator = this.validator;
                if (this.validator == null)
                {
                    StrategyValidatorBase local1 = this.validator;
                    validator = this.validator = this.CreateValidator();
                }
                return validator;
            }
        }
    }
}

