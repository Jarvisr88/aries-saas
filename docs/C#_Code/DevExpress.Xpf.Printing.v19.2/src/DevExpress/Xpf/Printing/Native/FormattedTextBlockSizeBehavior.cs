namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class FormattedTextBlockSizeBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty TextProperty;

        static FormattedTextBlockSizeBehavior()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FormattedTextBlockSizeBehavior), new FrameworkPropertyMetadata(null, (d, e) => ((FormattedTextBlockSizeBehavior) d).OnTextChanged()));
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Binding binding = new Binding("Text");
            binding.Source = base.AssociatedObject;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(this, TextProperty, binding);
        }

        protected override void OnDetaching()
        {
            BindingOperations.ClearBinding(this, TextProperty);
            base.OnDetaching();
        }

        private void OnTextChanged()
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                FormattedText text = new FormattedText(this.Text, CultureInfo.CurrentCulture, base.AssociatedObject.FlowDirection, new Typeface(base.AssociatedObject.FontFamily, base.AssociatedObject.FontStyle, FontWeights.Bold, base.AssociatedObject.FontStretch), base.AssociatedObject.FontSize, Brushes.Black);
                base.AssociatedObject.Width = text.Width;
            }
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
            public static readonly FormattedTextBlockSizeBehavior.<>c <>9 = new FormattedTextBlockSizeBehavior.<>c();

            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormattedTextBlockSizeBehavior) d).OnTextChanged();
            }
        }
    }
}

