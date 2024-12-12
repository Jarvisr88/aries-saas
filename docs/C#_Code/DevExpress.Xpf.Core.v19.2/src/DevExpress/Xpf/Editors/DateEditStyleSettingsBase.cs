namespace DevExpress.Xpf.Editors
{
    using System;

    public abstract class DateEditStyleSettingsBase : PopupBaseEditStyleSettings
    {
        protected DateEditStyleSettingsBase()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((DateEdit) editor).DateEditPopupContentType = this.GetPopupContentType();
        }

        protected abstract DateEditPopupContentType GetPopupContentType();
    }
}

