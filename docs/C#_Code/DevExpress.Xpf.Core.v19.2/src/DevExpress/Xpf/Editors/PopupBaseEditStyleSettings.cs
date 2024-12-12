namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Windows.Controls;

    public class PopupBaseEditStyleSettings : ButtonEditStyleSettings
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            PopupBaseEdit edit = editor as PopupBaseEdit;
            if (edit != null)
            {
                edit.UpdatePopupElements();
            }
        }

        protected internal virtual PopupCloseMode GetClosePopupOnClickMode(PopupBaseEdit editor) => 
            editor.PopupSettings.GetClosePopupOnClickMode();

        public virtual ControlTemplate GetPopupBottomAreaTemplate(PopupBaseEdit editor)
        {
            PopupBaseEditThemeKeyExtension extension1 = new PopupBaseEditThemeKeyExtension();
            extension1.ResourceKey = PopupBaseEditThemeKeys.PopupBottomAreaTemplate;
            extension1.ThemeName = ThemeHelper.GetEditorThemeName(editor);
            PopupBaseEditThemeKeyExtension key = extension1;
            return (ControlTemplate) editor.FindResource(key);
        }

        public virtual PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            PopupFooterButtons.None;

        public virtual ControlTemplate GetPopupTopAreaTemplate(PopupBaseEdit editor) => 
            null;

        protected internal virtual bool GetShowSizeGrip(PopupBaseEdit editor) => 
            false;

        public virtual bool StaysPopupOpen() => 
            false;

        protected internal virtual bool ShouldFocusPopup =>
            false;

        public virtual bool ShouldCaptureMouseOnPopup =>
            true;
    }
}

