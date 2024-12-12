namespace DevExpress.Xpf.Office.UI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class OfficeDefaultBarItemDataTemplates : Control
    {
        public static readonly DependencyProperty MarginBarItemContentTemplateProperty = DependencyPropertyManager.Register("MarginBarItemContentTemplate", typeof(DataTemplate), typeof(OfficeDefaultBarItemDataTemplates), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty PaperKindBarItemContentTemplateProperty = DependencyPropertyManager.Register("PaperKindBarItemContentTemplate", typeof(DataTemplate), typeof(OfficeDefaultBarItemDataTemplates), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty CheckEditTemplateProperty = DependencyPropertyManager.Register("CheckEditTemplate", typeof(DataTemplate), typeof(OfficeDefaultBarItemDataTemplates), new FrameworkPropertyMetadata());

        public OfficeDefaultBarItemDataTemplates()
        {
            base.DefaultStyleKey = typeof(OfficeDefaultBarItemDataTemplates);
        }

        public DataTemplate MarginBarItemContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MarginBarItemContentTemplateProperty);
            set => 
                base.SetValue(MarginBarItemContentTemplateProperty, value);
        }

        public DataTemplate PaperKindBarItemContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PaperKindBarItemContentTemplateProperty);
            set => 
                base.SetValue(PaperKindBarItemContentTemplateProperty, value);
        }

        public DataTemplate CheckEditTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CheckEditTemplateProperty);
            set => 
                base.SetValue(CheckEditTemplateProperty, value);
        }
    }
}

