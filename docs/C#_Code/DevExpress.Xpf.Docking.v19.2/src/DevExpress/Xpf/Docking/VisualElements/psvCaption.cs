namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class psvCaption : ContentControl
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty TextTrimmingProperty;
        public static readonly DependencyProperty TextWrappingProperty;
        public static readonly DependencyProperty RecognizeAccessKeyProperty;
        protected TextBlock PartTextBlock = new TextBlock();
        protected AccessText PartAccessText;

        static psvCaption()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<psvCaption> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<psvCaption>();
            registrator.Register<string>("Text", ref TextProperty, null, null, null);
            registrator.Register<System.Windows.TextTrimming>("TextTrimming", ref TextTrimmingProperty, System.Windows.TextTrimming.None, null, null);
            registrator.Register<System.Windows.TextWrapping>("TextWrapping", ref TextWrappingProperty, System.Windows.TextWrapping.NoWrap, null, null);
            registrator.Register<bool>("RecognizeAccessKey", ref RecognizeAccessKeyProperty, false, (dObj, e) => ((psvCaption) dObj).OnRecognizeAccessKeyChanged((bool) e.OldValue, (bool) e.NewValue), null);
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<psvCaption>.New().OverrideMetadata(Control.IsTabStopProperty, false, null, FrameworkPropertyMetadataOptions.None).OverrideMetadata(UIElement.FocusableProperty, false, null, FrameworkPropertyMetadataOptions.None);
        }

        public psvCaption()
        {
            this.Forward(this.PartTextBlock, TextBlock.TextProperty, "Text", BindingMode.OneWay);
            this.Forward(this.PartTextBlock, TextBlock.TextTrimmingProperty, "TextTrimming", BindingMode.OneWay);
            this.Forward(this.PartTextBlock, TextBlock.TextWrappingProperty, "TextWrapping", BindingMode.OneWay);
            this.PartAccessText = new AccessText();
            this.Forward(this.PartAccessText, AccessText.TextProperty, "Text", BindingMode.OneWay);
            this.Forward(this.PartAccessText, AccessText.TextTrimmingProperty, "TextTrimming", BindingMode.OneWay);
            this.Forward(this.PartAccessText, AccessText.TextWrappingProperty, "TextWrapping", BindingMode.OneWay);
            base.Content = this.ActualContent;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BaseLayoutItem dataContext = base.DataContext as BaseLayoutItem;
            this.RecognizeAccessKey = LayoutItemsHelper.CanRecognizeAccessKey(dataContext);
        }

        protected virtual void OnRecognizeAccessKeyChanged(bool oldValue, bool newValue)
        {
            base.Content = this.ActualContent;
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        public System.Windows.TextTrimming TextTrimming
        {
            get => 
                (System.Windows.TextTrimming) base.GetValue(TextTrimmingProperty);
            set => 
                base.SetValue(TextTrimmingProperty, value);
        }

        public System.Windows.TextWrapping TextWrapping
        {
            get => 
                (System.Windows.TextWrapping) base.GetValue(TextWrappingProperty);
            set => 
                base.SetValue(TextWrappingProperty, value);
        }

        private FrameworkElement ActualContent =>
            this.RecognizeAccessKey ? ((FrameworkElement) this.PartAccessText) : ((FrameworkElement) this.PartTextBlock);

        public bool RecognizeAccessKey
        {
            get => 
                (bool) base.GetValue(RecognizeAccessKeyProperty);
            set => 
                base.SetValue(RecognizeAccessKeyProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvCaption.<>c <>9 = new psvCaption.<>c();

            internal void <.cctor>b__4_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvCaption) dObj).OnRecognizeAccessKeyChanged((bool) e.OldValue, (bool) e.NewValue);
            }
        }
    }
}

