namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TextBlockBindableInlinesBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty InlineListProperty;

        static TextBlockBindableInlinesBehavior()
        {
            InlineListProperty = DependencyProperty.Register("InlineList", typeof(IEnumerable<Inline>), typeof(TextBlockBindableInlinesBehavior), new FrameworkPropertyMetadata(null, (d, e) => ((TextBlockBindableInlinesBehavior) d).OnInlinesChanged()));
        }

        private void OnInlinesChanged()
        {
            if (this.InlineList != null)
            {
                base.AssociatedObject.Inlines.Clear();
                base.AssociatedObject.Inlines.AddRange(this.InlineList);
            }
        }

        public IEnumerable<Inline> InlineList
        {
            get => 
                (IEnumerable<Inline>) base.GetValue(InlineListProperty);
            set => 
                base.SetValue(InlineListProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextBlockBindableInlinesBehavior.<>c <>9 = new TextBlockBindableInlinesBehavior.<>c();

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextBlockBindableInlinesBehavior) d).OnInlinesChanged();
            }
        }
    }
}

