namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Windows;

    public class ValidationErrorTemplateSelector : RenderTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content)
        {
            BaseValidationError error = (BaseValidationError) content;
            if (error == null)
            {
                return null;
            }
            ErrorType errorType = error.ErrorType;
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            IBaseEdit edit = chrome as IBaseEdit;
            return (((edit == null) || (edit.ValidationErrorTemplate == null)) ? ((errorType != ErrorType.Critical) ? ((errorType != ErrorType.Information) ? ((errorType != ErrorType.Warning) ? resourceProvider.GetCriticalErrorTemplate(chrome) : resourceProvider.GetWarningErrorTemplate(chrome)) : resourceProvider.GetInformationErrorTemplate(chrome)) : resourceProvider.GetCriticalErrorTemplate(chrome)) : resourceProvider.GetValidationErrorTemplate(chrome));
        }
    }
}

