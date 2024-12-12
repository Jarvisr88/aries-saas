namespace DevExpress.Xpf.Core.WizardFramework
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public class WizardPageLayout : Control
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WizardPageLayout), new PropertyMetadata(null));
        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register("Subtitle", typeof(string), typeof(WizardPageLayout), new PropertyMetadata(null));
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(WizardPageLayout), new PropertyMetadata(null));

        public WizardPageLayout()
        {
            base.DefaultStyleKey = typeof(WizardPageLayout);
        }

        public string Title
        {
            get => 
                (string) base.GetValue(TitleProperty);
            set => 
                base.SetValue(TitleProperty, value);
        }

        public string Subtitle
        {
            get => 
                (string) base.GetValue(SubtitleProperty);
            set => 
                base.SetValue(SubtitleProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }
    }
}

