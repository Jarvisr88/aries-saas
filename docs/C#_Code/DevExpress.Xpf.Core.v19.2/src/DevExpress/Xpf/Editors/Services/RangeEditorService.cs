namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class RangeEditorService : BaseEditBaseService
    {
        public RangeEditorService(BaseEdit editor) : base(editor)
        {
        }

        public virtual object CorrectToBounds(object maskValue) => 
            maskValue;

        public virtual bool InRange(object maskValue) => 
            true;

        public virtual bool SpinDown(object value, IMaskManagerProvider maskProvider) => 
            this.EditingSettings.AllowSpin && maskProvider.Instance.SpinDown();

        public virtual bool SpinUp(object value, IMaskManagerProvider maskProvider) => 
            this.EditingSettings.AllowSpin && maskProvider.Instance.SpinUp();

        public virtual bool ShouldRoundToBounds =>
            false;

        protected BaseEditingSettingsService EditingSettings =>
            base.PropertyProvider.GetService<BaseEditingSettingsService>();
    }
}

