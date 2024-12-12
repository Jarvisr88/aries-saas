namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public abstract class HeaderItemControlTemplateProvider : Freezable
    {
        public static readonly DependencyProperty HeaderItemControlStyleProperty = DependencyProperty.Register("HeaderItemControlStyle", typeof(Style), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty NormalForegroundProperty = DependencyProperty.Register("NormalForeground", typeof(Brush), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty NormalBackgroundTemplateProperty = DependencyProperty.Register("NormalBackgroundTemplate", typeof(ControlTemplate), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty MouseOverBackgroundTemplateProperty = DependencyProperty.Register("MouseOverBackgroundTemplate", typeof(ControlTemplate), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));
        public static readonly DependencyProperty PressedBackgroundTemplateProperty = DependencyProperty.Register("PressedBackgroundTemplate", typeof(ControlTemplate), typeof(HeaderItemControlTemplateProvider), new PropertyMetadata(null));

        protected HeaderItemControlTemplateProvider()
        {
        }

        protected override Freezable CreateInstanceCore() => 
            this;

        public Style HeaderItemControlStyle
        {
            get => 
                (Style) base.GetValue(HeaderItemControlStyleProperty);
            set => 
                base.SetValue(HeaderItemControlStyleProperty, value);
        }

        public Brush NormalForeground
        {
            get => 
                (Brush) base.GetValue(NormalForegroundProperty);
            set => 
                base.SetValue(NormalForegroundProperty, value);
        }

        public Brush MouseOverForeground
        {
            get => 
                (Brush) base.GetValue(MouseOverForegroundProperty);
            set => 
                base.SetValue(MouseOverForegroundProperty, value);
        }

        public Brush PressedForeground
        {
            get => 
                (Brush) base.GetValue(PressedForegroundProperty);
            set => 
                base.SetValue(PressedForegroundProperty, value);
        }

        public ControlTemplate NormalBackgroundTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(NormalBackgroundTemplateProperty);
            set => 
                base.SetValue(NormalBackgroundTemplateProperty, value);
        }

        public ControlTemplate MouseOverBackgroundTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MouseOverBackgroundTemplateProperty);
            set => 
                base.SetValue(MouseOverBackgroundTemplateProperty, value);
        }

        public ControlTemplate PressedBackgroundTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PressedBackgroundTemplateProperty);
            set => 
                base.SetValue(PressedBackgroundTemplateProperty, value);
        }
    }
}

