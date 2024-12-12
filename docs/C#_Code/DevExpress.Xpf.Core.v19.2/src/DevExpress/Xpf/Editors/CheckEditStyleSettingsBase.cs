namespace DevExpress.Xpf.Editors
{
    using System;

    public abstract class CheckEditStyleSettingsBase : BaseEditStyleSettings
    {
        protected CheckEditStyleSettingsBase()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((CheckEdit) editor).DisplayMode = this.GetDisplayMode();
        }

        protected internal abstract CheckEditDisplayMode GetDisplayMode();
    }
}

