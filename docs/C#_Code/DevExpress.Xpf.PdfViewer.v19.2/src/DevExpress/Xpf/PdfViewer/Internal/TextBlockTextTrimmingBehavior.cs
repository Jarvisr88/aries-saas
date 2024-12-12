namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TextBlockTextTrimmingBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty MaxLengthProperty;

        static TextBlockTextTrimmingBehavior()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBlockTextTrimmingBehavior), new PropertyMetadata((obj, args) => ((TextBlockTextTrimmingBehavior) obj).OnTextChanged()));
            MaxLengthProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBlockTextTrimmingBehavior), new PropertyMetadata((obj, args) => ((TextBlockTextTrimmingBehavior) obj).OnMaxLengthChanged()));
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateTextBlock();
        }

        private void OnMaxLengthChanged()
        {
            this.UpdateTextBlock();
        }

        private void OnTextChanged()
        {
            this.UpdateTextBlock();
        }

        private void UpdateTextBlock()
        {
            if ((base.AssociatedObject != null) && (this.Text != null))
            {
                if (this.Text.Length < this.MaxLength)
                {
                    base.AssociatedObject.Text = this.Text;
                    base.AssociatedObject.ToolTip = null;
                }
                else
                {
                    base.AssociatedObject.Text = this.Text.Substring(0, this.MaxLength) + "...";
                    base.AssociatedObject.ToolTip = this.Text;
                }
            }
        }

        public int MaxLength
        {
            get => 
                (int) base.GetValue(MaxLengthProperty);
            set => 
                base.SetValue(MaxLengthProperty, value);
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextBlockTextTrimmingBehavior.<>c <>9 = new TextBlockTextTrimmingBehavior.<>c();

            internal void <.cctor>b__13_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((TextBlockTextTrimmingBehavior) obj).OnTextChanged();
            }

            internal void <.cctor>b__13_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((TextBlockTextTrimmingBehavior) obj).OnMaxLengthChanged();
            }
        }
    }
}

