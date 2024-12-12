namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class CommonHeaderItemControlTemplateProvider : HeaderItemControlTemplateProvider
    {
        public static readonly DependencyProperty MinimizeGlyphTemplateProperty = DependencyProperty.Register("MinimizeGlyphTemplate", typeof(DataTemplate), typeof(CommonHeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty MaximizeGlyphTemplateProperty = DependencyProperty.Register("MaximizeGlyphTemplate", typeof(DataTemplate), typeof(CommonHeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty RestoreGlyphTemplateProperty = DependencyProperty.Register("RestoreGlyphTemplate", typeof(DataTemplate), typeof(CommonHeaderItemControlTemplateProvider), new PropertyMetadata(null));

        public DataTemplate MinimizeGlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MinimizeGlyphTemplateProperty);
            set => 
                base.SetValue(MinimizeGlyphTemplateProperty, value);
        }

        public DataTemplate MaximizeGlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MaximizeGlyphTemplateProperty);
            set => 
                base.SetValue(MaximizeGlyphTemplateProperty, value);
        }

        public DataTemplate RestoreGlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(RestoreGlyphTemplateProperty);
            set => 
                base.SetValue(RestoreGlyphTemplateProperty, value);
        }
    }
}

