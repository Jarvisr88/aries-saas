namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Windows.Controls;

    public class TrackBarScrollableRangeStyleSettings : TrackBarStyleSettings
    {
        protected override ThemeKeyExtensionGeneric GetStyleThemeKey(TrackBarEdit editor)
        {
            string str = (editor.Orientation == Orientation.Vertical) ? "Vertical" : "Horizontal";
            TrackBarEditStyleThemeKeyExtension extension1 = new TrackBarEditStyleThemeKeyExtension();
            extension1.ResourceKey = "ScrollableRangeTrackBarEditPanel" + str + "Style";
            extension1.ThemeName = ThemeHelper.GetEditorThemeName(editor);
            return extension1;
        }

        public override bool IsMoveToPointEnabled =>
            true;

        protected override bool IsZoom =>
            false;

        protected override bool IsRange =>
            true;
    }
}

