namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Interop;

    public class PopupBorderControl : ContentControl
    {
        public static readonly DependencyProperty IsLeftProperty;
        public static readonly DependencyProperty DropOppositeProperty;
        public static readonly DependencyProperty ContentWidthProperty = DependencyProperty.Register("ContentWidth", typeof(double), typeof(PopupBorderControl), new UIPropertyMetadata(FrameworkElement.WidthProperty.DefaultMetadata.DefaultValue));
        public static readonly DependencyProperty ContentMinWidthProperty = DependencyProperty.Register("ContentMinWidth", typeof(double), typeof(PopupBorderControl), new UIPropertyMetadata(FrameworkElement.MinWidthProperty.DefaultMetadata.DefaultValue));
        public static readonly DependencyProperty ContentMaxWidthProperty = DependencyProperty.Register("ContentMaxWidth", typeof(double), typeof(PopupBorderControl), new UIPropertyMetadata(FrameworkElement.MaxWidthProperty.DefaultMetadata.DefaultValue));
        public static readonly DependencyProperty ContentHeightProperty = DependencyProperty.Register("ContentHeight", typeof(double), typeof(PopupBorderControl), new UIPropertyMetadata(FrameworkElement.HeightProperty.DefaultMetadata.DefaultValue));
        public static readonly DependencyProperty ContentMinHeightProperty = DependencyProperty.Register("ContentMinHeight", typeof(double), typeof(PopupBorderControl), new UIPropertyMetadata(FrameworkElement.MinHeightProperty.DefaultMetadata.DefaultValue));
        public static readonly DependencyProperty ContentMaxHeightProperty = DependencyProperty.Register("ContentMaxHeight", typeof(double), typeof(PopupBorderControl), new UIPropertyMetadata(FrameworkElement.MaxHeightProperty.DefaultMetadata.DefaultValue));
        public static readonly DependencyProperty PopupProperty;
        private Size screenRestrictions = Size.Empty;

        static PopupBorderControl()
        {
            Type ownerType = typeof(PopupBorderControl);
            PopupProperty = DependencyPropertyManager.Register("Popup", typeof(System.Windows.Controls.Primitives.Popup), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            IsLeftProperty = DependencyPropertyManager.Register("IsLeft", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            DropOppositeProperty = DependencyPropertyManager.Register("DropOpposite", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBorderControl), new FrameworkPropertyMetadata(typeof(PopupBorderControl)));
        }

        public PopupBorderControl()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (this.RestrictHeight && !this.ScreenRestrictions.IsEmpty)
            {
                arrangeBounds.Height = Math.Min(arrangeBounds.Height, this.ScreenRestrictions.Height);
            }
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            ClearAutomationEventsHelper.ClearAutomationEvents();
            if (this.RestrictHeight && !this.ScreenRestrictions.IsEmpty)
            {
                constraint.Height = Math.Min(constraint.Height, this.ScreenRestrictions.Height);
            }
            return base.MeasureOverride(constraint);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new FrameworkElementAutomationPeer(this);

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateVisualState();
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ClearAutomationEventsHelper.ClearAutomationEvents();
        }

        protected virtual void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, BrowserInteropHelper.IsBrowserHosted ? "BrowserHosted" : "Standalone", false);
        }

        public double ContentWidth
        {
            get => 
                (double) base.GetValue(ContentWidthProperty);
            set => 
                base.SetValue(ContentWidthProperty, value);
        }

        public double ContentMinWidth
        {
            get => 
                (double) base.GetValue(ContentMinWidthProperty);
            set => 
                base.SetValue(ContentMinWidthProperty, value);
        }

        public double ContentMaxWidth
        {
            get => 
                (double) base.GetValue(ContentMaxWidthProperty);
            set => 
                base.SetValue(ContentMaxWidthProperty, value);
        }

        public double ContentHeight
        {
            get => 
                (double) base.GetValue(ContentHeightProperty);
            set => 
                base.SetValue(ContentHeightProperty, value);
        }

        public double ContentMinHeight
        {
            get => 
                (double) base.GetValue(ContentMinHeightProperty);
            set => 
                base.SetValue(ContentMinHeightProperty, value);
        }

        public double ContentMaxHeight
        {
            get => 
                (double) base.GetValue(ContentMaxHeightProperty);
            set => 
                base.SetValue(ContentMaxHeightProperty, value);
        }

        public bool RestrictHeight { get; set; }

        public Size ScreenRestrictions
        {
            get => 
                this.screenRestrictions;
            set => 
                this.screenRestrictions = value;
        }

        public System.Windows.Controls.Primitives.Popup Popup
        {
            get => 
                (System.Windows.Controls.Primitives.Popup) base.GetValue(PopupProperty);
            set => 
                base.SetValue(PopupProperty, value);
        }

        public bool IsLeft
        {
            get => 
                (bool) base.GetValue(IsLeftProperty);
            set => 
                base.SetValue(IsLeftProperty, value);
        }

        public bool DropOpposite
        {
            get => 
                (bool) base.GetValue(DropOppositeProperty);
            set => 
                base.SetValue(DropOppositeProperty, value);
        }
    }
}

