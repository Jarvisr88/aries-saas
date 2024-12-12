namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class TrackBarStyleSettings : BaseEditStyleSettings
    {
        private void ApplyPanelStyle(TrackBarEdit editor)
        {
            editor.Panel.Style = editor.TryFindResource(this.GetStyleThemeKey(editor)) as Style;
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            TrackBarEdit edit = editor as TrackBarEdit;
            if (edit != null)
            {
                edit.IsRange = this.IsRange;
                edit.IsZoom = this.IsZoom;
                if (edit.Panel != null)
                {
                    this.ApplyPanelStyle(edit);
                }
            }
        }

        protected virtual ThemeKeyExtensionGeneric GetStyleThemeKey(TrackBarEdit editor)
        {
            string str = (editor.Orientation == Orientation.Vertical) ? "Vertical" : "Horizontal";
            TrackBarEditStyleThemeKeyExtension extension1 = new TrackBarEditStyleThemeKeyExtension();
            extension1.ResourceKey = "TrackBarEditPanel" + str + "Style";
            extension1.ThemeName = ThemeHelper.GetEditorThemeName(editor);
            return extension1;
        }

        public virtual bool IsMoveToPointEnabled =>
            false;

        protected virtual bool IsZoom =>
            false;

        protected virtual bool IsRange =>
            false;
    }
}

