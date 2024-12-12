namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_HorizontalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="PART_VerticalRootElement", Type=typeof(FrameworkElement))]
    public class SeparatorControl : psvContentControl
    {
        public static readonly DependencyProperty OrientationProperty;
        private const string HorizontalRootElementName = "PART_HorizontalRootElement";
        private const string VerticalRootElementName = "PART_VerticalRootElement";

        static SeparatorControl()
        {
            DependencyPropertyRegistrator<SeparatorControl> registrator = new DependencyPropertyRegistrator<SeparatorControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, (dObj, e) => ((SeparatorControl) dObj).OnOrientationChanged((System.Windows.Controls.Orientation) e.NewValue), null);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartHorizontalRootElement = base.GetTemplateChild("PART_HorizontalRootElement") as FrameworkElement;
            this.PartVerticalRootElement = base.GetTemplateChild("PART_VerticalRootElement") as FrameworkElement;
            this.UpdateTemplateParts();
        }

        protected override void OnDispose()
        {
            base.ClearValue(DockLayoutManager.DockLayoutManagerProperty);
            base.ClearValue(DockLayoutManager.LayoutItemProperty);
            base.OnDispose();
        }

        public void OnOrientationChanged(System.Windows.Controls.Orientation orientation)
        {
            this.UpdateTemplateParts();
        }

        private void UpdateTemplateParts()
        {
            if (this.PartHorizontalRootElement != null)
            {
                this.PartHorizontalRootElement.Visibility = VisibilityHelper.Convert(this.Orientation == System.Windows.Controls.Orientation.Vertical, Visibility.Collapsed);
            }
            if (this.PartVerticalRootElement != null)
            {
                this.PartVerticalRootElement.Visibility = VisibilityHelper.Convert(this.Orientation == System.Windows.Controls.Orientation.Horizontal, Visibility.Collapsed);
            }
        }

        protected FrameworkElement PartHorizontalRootElement { get; set; }

        protected FrameworkElement PartVerticalRootElement { get; set; }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SeparatorControl.<>c <>9 = new SeparatorControl.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((SeparatorControl) dObj).OnOrientationChanged((Orientation) e.NewValue);
            }
        }
    }
}

