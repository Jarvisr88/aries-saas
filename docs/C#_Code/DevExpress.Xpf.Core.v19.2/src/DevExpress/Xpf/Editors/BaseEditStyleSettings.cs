namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public abstract class BaseEditStyleSettings : FrameworkElement, IHighlighterAppearance
    {
        protected BaseEditStyleSettings()
        {
        }

        public virtual void ApplyToEdit(BaseEdit editor)
        {
        }

        protected internal virtual Inline CreateInlineForHighlighting(FrameworkElement editor, string text)
        {
            System.Windows.Documents.Run run1 = new System.Windows.Documents.Run();
            run1.Text = text;
            System.Windows.Documents.Run run = run1;
            Brush brush = this.HighlightedTextBackground ?? this.GetDefaultHighlightedTextBackground();
            if (brush != null)
            {
                run.Background = brush;
            }
            Brush brush2 = this.HighlightedTextForeground ?? this.GetDefaultHighlightedTextForeground();
            if (brush2 != null)
            {
                run.Foreground = brush2;
            }
            if (this.HighlightedFontFamily != null)
            {
                run.FontFamily = this.HighlightedFontFamily;
            }
            if (this.HighlightedFontSize != null)
            {
                run.FontSize = this.HighlightedFontSize.Value;
            }
            if (this.HighlightedFontStyle != null)
            {
                run.FontStyle = this.HighlightedFontStyle.Value;
            }
            if (this.HighlightedFontWeight != null)
            {
                run.FontWeight = this.HighlightedFontWeight.Value;
            }
            return run;
        }

        Inline IHighlighterAppearance.CreateInlineForHighlighting(FrameworkElement editor, string text) => 
            this.CreateInlineForHighlighting(editor, text);

        protected virtual Brush GetDefaultHighlightedTextBackground() => 
            Brushes.Yellow;

        protected virtual Brush GetDefaultHighlightedTextForeground() => 
            Brushes.Black;

        [DefaultValue((string) null)]
        public FontFamily HighlightedFontFamily { get; set; }

        [DefaultValue((string) null)]
        public double? HighlightedFontSize { get; set; }

        [DefaultValue((string) null)]
        public FontStyle? HighlightedFontStyle { get; set; }

        [DefaultValue((string) null)]
        public FontWeight? HighlightedFontWeight { get; set; }

        public Brush HighlightedTextBackground { get; set; }

        public Brush HighlightedTextForeground { get; set; }
    }
}

