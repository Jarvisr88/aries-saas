namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class ValidationService : BaseEditBaseService
    {
        public ValidationService(BaseEdit editor) : base(editor)
        {
        }

        public virtual bool DoValidate(object editValue, UpdateEditorSource source) => 
            this.TextInputSettings.DoValidate(editValue, source);

        private TextInputSettingsBase TextInputSettings =>
            base.PropertyProvider.GetService<TextInputSettingsBase>();
    }
}

