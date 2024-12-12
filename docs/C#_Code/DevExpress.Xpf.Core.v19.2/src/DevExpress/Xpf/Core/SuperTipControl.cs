namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Automation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("SuperTip")]
    public class SuperTipControl : ItemsControl
    {
        public static readonly DependencyProperty SuperTipProperty = DependencyPropertyManager.Register("SuperTip", typeof(DevExpress.Xpf.Core.SuperTip), typeof(SuperTipControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(SuperTipControl.OnSuperTipPropertyChanged)));

        public SuperTipControl() : this(null)
        {
        }

        public SuperTipControl(DevExpress.Xpf.Core.SuperTip superTip) : this(superTip, true)
        {
        }

        public SuperTipControl(DevExpress.Xpf.Core.SuperTip superTip, bool patchDataContext)
        {
            this.PatchDataContext = patchDataContext;
            if (superTip != null)
            {
                this.SuperTip = superTip;
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            !(this.CurrentItem is SuperTipItemSeparator) ? (!(this.CurrentItem is SuperTipHeaderItem) ? (!(this.CurrentItem is SuperTipItem) ? base.GetContainerForItemOverride() : new SuperTipItemControl()) : new SuperTipHeaderItemControl()) : new SuperTipItemControlSeparator();

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            this.CurrentItem = item as SuperTipItemBase;
            return (item is SuperTipItemControl);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new SuperTipControlAutomationPeer(this);

        protected virtual void OnSuperTipChanged(DependencyPropertyChangedEventArgs e)
        {
            base.ItemsSource = null;
            DevExpress.Xpf.Core.SuperTip oldValue = e.OldValue as DevExpress.Xpf.Core.SuperTip;
            if ((oldValue != null) && this.PatchDataContext)
            {
                oldValue.ClearValue(FrameworkContentElement.DataContextProperty);
            }
            if (this.SuperTip != null)
            {
                if (this.PatchDataContext)
                {
                    Binding binding = new Binding();
                    binding.Source = this;
                    binding.Path = new PropertyPath(FrameworkElement.DataContextProperty);
                    BindingOperations.SetBinding(this.SuperTip, FrameworkContentElement.DataContextProperty, binding);
                }
                base.ItemsSource = this.SuperTip.Items;
            }
        }

        protected static void OnSuperTipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperTipControl) d).OnSuperTipChanged(e);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            (element as SuperTipItemControlBase).Item = item as SuperTipItemBase;
            base.PrepareContainerForItemOverride(element, item);
        }

        public bool PatchDataContext { get; set; }

        public DevExpress.Xpf.Core.SuperTip SuperTip
        {
            get => 
                (DevExpress.Xpf.Core.SuperTip) base.GetValue(SuperTipProperty);
            set => 
                base.SetValue(SuperTipProperty, value);
        }

        private SuperTipItemBase CurrentItem { get; set; }
    }
}

