namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    public class SuperTipItem : SuperTipItemBase
    {
        public static readonly DependencyProperty GlyphProperty = DependencyPropertyManager.Register("Glyph", typeof(ImageSource), typeof(SuperTipItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItem.OnGlyphPropertyChanged)));
        public static readonly DependencyProperty ContentProperty = DependencyPropertyManager.Register("Content", typeof(object), typeof(SuperTipItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItem.OnContentPropertyChanged)));
        public static readonly DependencyProperty ContentTemplateProperty = DependencyPropertyManager.Register("ContentTemplate", typeof(DataTemplate), typeof(SuperTipItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItem.OnContentTemplatePropertyChanged)));
        public static readonly DependencyProperty LayoutStyleProperty = DependencyPropertyManager.Register("LayoutStyle", typeof(Style), typeof(SuperTipItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItem.OnLayoutStylePropertyChanged)));

        protected internal override string GetAutomationName() => 
            !(this.Content is string) ? null : (this.Content as string);

        protected virtual void OnContentChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        protected static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItem) d).OnContentChanged(e);
        }

        protected virtual void OnContentTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        protected static void OnContentTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItem) d).OnContentTemplateChanged(e);
        }

        protected virtual void OnGlyphChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        protected static void OnGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItem) d).OnGlyphChanged(e);
        }

        protected virtual void OnLayoutStyleChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        protected static void OnLayoutStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItem) d).OnLayoutStyleChanged(e);
        }

        [Description("Gets or sets the tooltip item's glyph.This is a dependency property.")]
        public ImageSource Glyph
        {
            get => 
                (ImageSource) base.GetValue(GlyphProperty);
            set => 
                base.SetValue(GlyphProperty, value);
        }

        [Description("Gets or sets the tooltip item's content.This is a dependency property.")]
        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Description("Gets or sets the template used to display the SuperTipItem.Content object.This is a dependency property.")]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Description("Gets or sets the style applied to paint the panel displaying the tooltip item's content and image.This is a dependency property.")]
        public Style LayoutStyle
        {
            get => 
                (Style) base.GetValue(LayoutStyleProperty);
            set => 
                base.SetValue(LayoutStyleProperty, value);
        }
    }
}

