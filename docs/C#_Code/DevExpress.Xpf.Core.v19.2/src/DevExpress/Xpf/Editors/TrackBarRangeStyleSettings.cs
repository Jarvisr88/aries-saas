namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Windows.Controls;

    public class TrackBarRangeStyleSettings : TrackBarStyleSettings
    {
        protected override ThemeKeyExtensionGeneric GetStyleThemeKey(TrackBarEdit editor)
        {
            string str = (editor.Orientation == Orientation.Vertical) ? "Vertical" : "Horizontal";
            TrackBarEditStyleThemeKeyExtension extension1 = new TrackBarEditStyleThemeKeyExtension();
            extension1.ResourceKey = "RangeTrackBarEditPanel" + str + "Style";
            extension1.ThemeName = ThemeHelper.GetEditorThemeName(editor);
            return extension1;
        }

        protected override bool IsRange =>
            true;
    }
}

