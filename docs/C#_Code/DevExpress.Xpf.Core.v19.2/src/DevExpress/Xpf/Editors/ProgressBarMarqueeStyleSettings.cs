namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using System;

    public class ProgressBarMarqueeStyleSettings : BaseProgressBarStyleSettings
    {
        protected override ProgressBarEditStyleThemeKeyExtension StyleThemeKey
        {
            get
            {
                ProgressBarEditStyleThemeKeyExtension extension1 = new ProgressBarEditStyleThemeKeyExtension();
                extension1.ResourceKey = $"ProgressBarMarqueeStyle{base.editor.Orientation}";
                extension1.ThemeName = ThemeHelper.GetEditorThemeName(base.editor);
                return extension1;
            }
        }

        protected override bool IsIndeterminate =>
            true;
    }
}

