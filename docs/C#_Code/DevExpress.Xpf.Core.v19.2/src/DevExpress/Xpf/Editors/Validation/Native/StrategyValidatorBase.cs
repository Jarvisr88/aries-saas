namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class StrategyValidatorBase
    {
        public StrategyValidatorBase(BaseEdit editor)
        {
            this.Editor = editor;
        }

        public bool DoValidate(object value, object convertedValue, UpdateEditorSource updateSource) => 
            this.DoValidateInternal(value, convertedValue, updateSource);

        protected virtual bool DoValidateInternal(object value, object convertedValue, UpdateEditorSource updateSource) => 
            this.IsValidValue(value, convertedValue);

        public virtual string GetValidationError() => 
            "Invalid value";

        protected virtual bool IsValidValue(object value, object convertedValue) => 
            true;

        public virtual object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            !(value is InvalidInputValue) ? this.PropertyProvider.ValueTypeConverter.ConvertBack(value) : ((InvalidInputValue) value).EditValue;

        protected BaseEdit Editor { get; private set; }

        protected ActualPropertyProvider PropertyProvider =>
            this.Editor.PropertyProvider;
    }
}

