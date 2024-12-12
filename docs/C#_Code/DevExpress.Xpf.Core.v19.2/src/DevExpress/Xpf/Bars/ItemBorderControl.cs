namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ItemBorderControl : ContentControl
    {
        public static readonly DependencyProperty HideBorderSideProperty;
        public static readonly DependencyProperty ArrowHideBorderSideProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty StateProperty;
        public static readonly DependencyProperty LinkControlProperty;
        public static readonly DependencyProperty ItemPositionProperty;
        public static readonly DependencyProperty NormalTemplateProperty;
        public static readonly DependencyProperty HoverTemplateProperty;
        public static readonly DependencyProperty PressedTemplateProperty;
        public static readonly DependencyProperty CustomizationTemplateProperty;

        static ItemBorderControl();
        public ItemBorderControl();
        protected virtual void CreateBindings();
        public override void OnApplyTemplate();
        protected virtual void OnArrowHideBorderSideChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnArrowHideBorderSidePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnCustomizationTemplateChanged();
        protected virtual void OnHideBorderSideChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnHideBorderSidePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnHoverTemplateChanged();
        protected virtual void OnIsActiveChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemPositionChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnItemPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnLinkControlChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnLinkControlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnNormalTemplateChanged();
        protected virtual void OnPressedTemplateChanged();
        protected virtual void OnStateChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void UpdateVisualStateByActive();
        protected virtual void UpdateVisualStateByArrowHideBorderSide();
        protected virtual void UpdateVisualStateByHideBorderSide();
        protected virtual void UpdateVisualStateByItemPosition();
        protected virtual void UpdateVisualStateByState();
        protected virtual void UpdateVisualStates();

        public ControlTemplate NormalTemplate { get; set; }

        public ControlTemplate HoverTemplate { get; set; }

        public ControlTemplate PressedTemplate { get; set; }

        public ControlTemplate CustomizationTemplate { get; set; }

        public HorizontalItemPositionType ItemPosition { get; set; }

        public BarItemLinkControl LinkControl { get; set; }

        public DevExpress.Xpf.Bars.HideBorderSide HideBorderSide { get; set; }

        public DevExpress.Xpf.Bars.HideBorderSide ArrowHideBorderSide { get; set; }

        public bool IsActive { get; set; }

        public BorderState State { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemBorderControl.<>c <>9;

            static <>c();
            internal void <.cctor>b__10_0(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <.cctor>b__10_1(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <.cctor>b__10_2(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <.cctor>b__10_3(DependencyObject o, DependencyPropertyChangedEventArgs args);
        }
    }
}

