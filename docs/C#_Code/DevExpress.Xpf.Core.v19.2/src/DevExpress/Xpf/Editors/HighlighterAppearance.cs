namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class HighlighterAppearance : IHighlighterAppearance
    {
        public HighlighterAppearance()
        {
            this.HighlightedTextForeground = Brushes.Black;
            this.HighlightedTextBackground = Brushes.Yellow;
        }

        public virtual void ApplyToEdit(BaseEdit editor)
        {
        }

        public virtual Inline CreateInlineForHighlighting(FrameworkElement editor, string text)
        {
            System.Windows.Documents.Run run1 = new System.Windows.Documents.Run();
            run1.Text = text;
            run1.Background = this.HighlightedTextBackground;
            System.Windows.Documents.Run run = run1;
            if (this.HighlightedTextForeground != null)
            {
                run.Foreground = this.HighlightedTextForeground;
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

