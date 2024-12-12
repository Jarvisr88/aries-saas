namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class XPFContentPresenter : ContentPresenter
    {
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(XPFContentPresenter), new PropertyMetadata(null, new PropertyChangedCallback(XPFContentPresenter.OnForegroundPropertyChanged)));

        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextElement.SetForeground(d, e.NewValue as Brush);
        }

        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }
    }
}

