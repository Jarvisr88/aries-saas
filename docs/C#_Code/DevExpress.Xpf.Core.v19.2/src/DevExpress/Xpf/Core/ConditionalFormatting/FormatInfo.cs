namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class FormatInfo : Freezable
    {
        public static readonly DependencyProperty FormatNameProperty = DependencyProperty.Register("FormatName", typeof(string), typeof(FormatInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register("DisplayName", typeof(string), typeof(FormatInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(object), typeof(FormatInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(FormatInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(FormatInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register("GroupName", typeof(string), typeof(FormatInfo), new PropertyMetadata(null));

        protected override Freezable CreateInstanceCore() => 
            new FormatInfo();

        public override string ToString() => 
            this.DisplayName;

        public string FormatName
        {
            get => 
                (string) base.GetValue(FormatNameProperty);
            set => 
                base.SetValue(FormatNameProperty, value);
        }

        public string DisplayName
        {
            get => 
                (string) base.GetValue(DisplayNameProperty);
            set => 
                base.SetValue(DisplayNameProperty, value);
        }

        public object Format
        {
            get => 
                base.GetValue(FormatProperty);
            set => 
                base.SetValue(FormatProperty, value);
        }

        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        public string Description
        {
            get => 
                (string) base.GetValue(DescriptionProperty);
            set => 
                base.SetValue(DescriptionProperty, value);
        }

        public string GroupName
        {
            get => 
                (string) base.GetValue(GroupNameProperty);
            set => 
                base.SetValue(GroupNameProperty, value);
        }

        internal bool IsCustom =>
            string.IsNullOrEmpty(this.FormatName);
    }
}

