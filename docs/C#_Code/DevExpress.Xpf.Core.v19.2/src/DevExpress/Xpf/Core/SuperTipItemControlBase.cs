namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Automation;
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class SuperTipItemControlBase : Control
    {
        public static readonly DependencyProperty ItemProperty = DependencyPropertyManager.Register("Item", typeof(SuperTipItemBase), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItemControlBase.OnItemPropertyChanged)));
        public static readonly DependencyProperty ActualLayoutStyleProperty = DependencyPropertyManager.Register("ActualLayoutStyle", typeof(Style), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty LayoutStyleProperty = DependencyPropertyManager.Register("LayoutStyle", typeof(Style), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItemControlBase.OnLayoutStylePropertyChanged)));
        public static readonly DependencyProperty UserLayoutStyleProperty = DependencyPropertyManager.Register("UserLayoutStyle", typeof(Style), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItemControlBase.OnUserLayoutStylePropertyChanged)));
        public static readonly DependencyProperty ContentStyleProperty = DependencyPropertyManager.Register("ContentStyle", typeof(Style), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ContentTemplateProperty = DependencyPropertyManager.Register("ContentTemplate", typeof(DataTemplate), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItemControlBase.OnContentTemplatePropertyChanged)));
        public static readonly DependencyProperty UserContentTemplateProperty = DependencyPropertyManager.Register("UserContentTemplate", typeof(DataTemplate), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipItemControlBase.OnUserContentTemplatePropertyChanged)));
        public static readonly DependencyProperty ActualContentTemplateProperty = DependencyPropertyManager.Register("ActualContentTemplate", typeof(DataTemplate), typeof(SuperTipItemControlBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public SuperTipItemControlBase()
        {
            base.DefaultStyleKey = base.GetType();
        }

        protected virtual void OnContentTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateActualContentTemplate();
        }

        protected static void OnContentTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItemControlBase) d).OnContentTemplateChanged(e);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new SuperTipItemControlAutomationPeer(this);

        protected virtual void OnItemChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateItemBinding();
        }

        protected static void OnItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItemControlBase) d).OnItemChanged(e);
        }

        protected virtual void OnLayoutStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateActualLayout();
        }

        protected static void OnLayoutStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItemControlBase) d).OnLayoutStyleChanged(e);
        }

        protected virtual void OnUserContentTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateActualContentTemplate();
        }

        protected static void OnUserContentTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItemControlBase) d).OnUserContentTemplateChanged(e);
        }

        protected virtual void OnUserLayoutStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateActualLayout();
        }

        protected static void OnUserLayoutStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipItemControlBase) d).OnUserLayoutStyleChanged(e);
        }

        protected virtual void UpdateActualContentTemplate()
        {
            this.ActualContentTemplate = (this.UserContentTemplate == null) ? this.ContentTemplate : this.UserContentTemplate;
        }

        protected virtual void UpdateActualLayout()
        {
            this.ActualLayoutStyle = (this.UserLayoutStyle == null) ? this.LayoutStyle : this.UserLayoutStyle;
        }

        protected virtual void UpdateItemBinding()
        {
            BindingOperations.ClearBinding(this, UserLayoutStyleProperty);
            BindingOperations.ClearBinding(this, UserContentTemplateProperty);
            if (this.Item is SuperTipItem)
            {
                Binding binding = new Binding("LayoutStyle") {
                    Source = this.Item,
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(this, UserLayoutStyleProperty, binding);
                binding = new Binding("ContentTemplate") {
                    Source = this.Item,
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(this, UserContentTemplateProperty, binding);
            }
        }

        public SuperTipItemBase Item
        {
            get => 
                (SuperTipItemBase) base.GetValue(ItemProperty);
            set => 
                base.SetValue(ItemProperty, value);
        }

        public Style LayoutStyle
        {
            get => 
                (Style) base.GetValue(LayoutStyleProperty);
            set => 
                base.SetValue(LayoutStyleProperty, value);
        }

        public Style UserLayoutStyle
        {
            get => 
                (Style) base.GetValue(UserLayoutStyleProperty);
            set => 
                base.SetValue(UserLayoutStyleProperty, value);
        }

        public Style ActualLayoutStyle
        {
            get => 
                (Style) base.GetValue(ActualLayoutStyleProperty);
            set => 
                base.SetValue(ActualLayoutStyleProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        public DataTemplate UserContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(UserContentTemplateProperty);
            set => 
                base.SetValue(UserContentTemplateProperty, value);
        }

        public DataTemplate ActualContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualContentTemplateProperty);
            set => 
                base.SetValue(ActualContentTemplateProperty, value);
        }

        public Style ContentStyle
        {
            get => 
                (Style) base.GetValue(ContentStyleProperty);
            set => 
                base.SetValue(ContentStyleProperty, value);
        }
    }
}

