namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class CustomizationToolbarButtonBehavior : Behavior<ButtonBase>
    {
        public static readonly DependencyProperty ToolbarProperty;

        static CustomizationToolbarButtonBehavior()
        {
            ToolbarProperty = DependencyProperty.Register("Toolbar", typeof(LayoutItemCustomizationToolbar), typeof(CustomizationToolbarButtonBehavior), new PropertyMetadata(null, (d, e) => OnToolbarChanged(d, e)));
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.Toolbar != null)
            {
                base.AssociatedObject.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.Toolbar.OnButtonPreviewMouseLeftButtonDown);
                base.AssociatedObject.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.Toolbar.OnButtonPreviewMouseLeftButtonUp);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.Toolbar != null)
            {
                base.AssociatedObject.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.Toolbar.OnButtonPreviewMouseLeftButtonDown);
                base.AssociatedObject.PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.Toolbar.OnButtonPreviewMouseLeftButtonUp);
            }
        }

        private static void OnToolbarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomizationToolbarButtonBehavior behavior = d as CustomizationToolbarButtonBehavior;
            LayoutItemCustomizationToolbar newValue = e.NewValue as LayoutItemCustomizationToolbar;
            if ((d != null) && (newValue != null))
            {
                behavior.AssociatedObject.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(newValue.OnButtonPreviewMouseLeftButtonDown);
                behavior.AssociatedObject.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(newValue.OnButtonPreviewMouseLeftButtonUp);
            }
        }

        public LayoutItemCustomizationToolbar Toolbar
        {
            get => 
                (LayoutItemCustomizationToolbar) base.GetValue(ToolbarProperty);
            set => 
                base.SetValue(ToolbarProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomizationToolbarButtonBehavior.<>c <>9 = new CustomizationToolbarButtonBehavior.<>c();

            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CustomizationToolbarButtonBehavior.OnToolbarChanged(d, e);
            }
        }
    }
}

