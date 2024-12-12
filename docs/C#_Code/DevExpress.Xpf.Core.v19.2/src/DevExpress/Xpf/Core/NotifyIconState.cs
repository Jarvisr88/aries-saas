namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class NotifyIconState : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(NotifyIconState), new FrameworkPropertyMetadata(""));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NotifyIconState), new FrameworkPropertyMetadata(null));

        public string Name
        {
            get => 
                (string) base.GetValue(NameProperty);
            set => 
                base.SetValue(NameProperty, value);
        }

        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }
    }
}

