namespace DevExpress.Xpf.Editors.Validation
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ValidationErrorExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new BaseValidationError(null, null, this.ErrorType);

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; set; }
    }
}

