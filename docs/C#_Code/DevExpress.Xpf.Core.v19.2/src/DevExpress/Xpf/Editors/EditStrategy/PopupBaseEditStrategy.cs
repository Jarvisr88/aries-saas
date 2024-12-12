namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Services;
    using System;

    public class PopupBaseEditStrategy : ButtonEditStrategy
    {
        public PopupBaseEditStrategy(PopupBaseEdit editor) : base(editor)
        {
        }

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new PopupBaseEditSettingsService(this.Editor);

        private PopupBaseEdit Editor =>
            base.Editor as PopupBaseEdit;
    }
}

