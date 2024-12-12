namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Runtime.CompilerServices;

    public class RangeEditScrollerStyleSettings : TrackBarScrollableRangeStyleSettings
    {
        protected override ThemeKeyExtensionGeneric GetStyleThemeKey(TrackBarEdit editor)
        {
            RangeEditThemeKeys keys = (RangeEditThemeKeys) Enum.Parse(typeof(RangeEditThemeKeys), this.PanelStyle);
            RangeEditThemeKeyExtension extension1 = new RangeEditThemeKeyExtension();
            extension1.ResourceKey = keys;
            extension1.ThemeName = ThemeHelper.GetEditorThemeName(editor);
            return extension1;
        }

        public string PanelStyle { get; set; }
    }
}

