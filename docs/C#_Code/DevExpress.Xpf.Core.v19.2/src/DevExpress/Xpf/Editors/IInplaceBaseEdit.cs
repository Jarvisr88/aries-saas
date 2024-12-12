namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    public interface IInplaceBaseEdit : IBaseEdit, IInputElement, IChrome
    {
        IEnumerable<ButtonInfoBase> GetSortedButtons();
        void RaiseEditValueChanged(object oldValue, object newValue);

        IBaseEdit BaseEdit { get; }

        bool ShowBorder { get; set; }

        bool IsNullTextVisible { get; }

        bool HasTextDecorations { get; set; }

        TextDecorationCollection TextDecorations { get; set; }

        System.Windows.TextTrimming TextTrimming { get; set; }

        System.Windows.TextWrapping TextWrapping { get; set; }

        DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria { get; set; }

        string HighlightedText { get; set; }

        bool AllowDefaultButton { get; set; }

        Style ActiveEditorStyle { get; set; }

        bool ShowToolTipForTrimmedText { get; set; }

        bool ShowText { get; set; }

        bool ApplyItemTemplateToSelectedItem { get; set; }

        DevExpress.Xpf.Editors.CheckEditDisplayMode CheckEditDisplayMode { get; set; }

        ImageSource CheckedGlyph { get; set; }

        ImageSource UncheckedGlyph { get; set; }

        ImageSource IndeterminateGlyph { get; set; }

        DataTemplate GlyphTemplate { get; set; }

        TimeSpan? DayDuration { get; set; }
    }
}

