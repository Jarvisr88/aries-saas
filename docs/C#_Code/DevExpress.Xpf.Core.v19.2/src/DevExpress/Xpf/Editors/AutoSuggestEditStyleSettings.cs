namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class AutoSuggestEditStyleSettings : PopupBaseEditStyleSettings
    {
        protected AutoSuggestEditStyleSettings()
        {
            base.HighlightedFontWeight = new FontWeight?(FontWeights.Bold);
        }

        protected override Brush GetDefaultHighlightedTextBackground() => 
            null;

        protected override Brush GetDefaultHighlightedTextForeground() => 
            null;

        protected virtual SelectionEventMode GetSelectionEventMode() => 
            SelectionEventMode.MouseUp;
    }
}

