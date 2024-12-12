namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class CloseHeaderItemControlTemplateProvider : HeaderItemControlTemplateProvider
    {
        public static readonly DependencyProperty CloseGlyphTemplateProperty = DependencyProperty.Register("CloseGlyphTemplate", typeof(DataTemplate), typeof(CloseHeaderItemControlTemplateProvider), new PropertyMetadata(null));

        public DataTemplate CloseGlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CloseGlyphTemplateProperty);
            set => 
                base.SetValue(CloseGlyphTemplateProperty, value);
        }
    }
}

