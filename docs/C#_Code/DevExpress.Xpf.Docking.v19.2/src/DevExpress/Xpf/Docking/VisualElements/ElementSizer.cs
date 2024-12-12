namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ElementSizer : Control
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(ElementSizer), new PropertyMetadata(System.Windows.Controls.Orientation.Vertical));
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(ElementSizer), null);
        private const string HorizontalRootElementName = "HorizontalRootElement";
        private const string VerticalRootElementName = "VerticalRootElement";

        static ElementSizer()
        {
            new DependencyPropertyRegistrator<ElementSizer>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.HorizontalRootElement = base.GetTemplateChild("HorizontalRootElement") as FrameworkElement;
            this.VerticalRootElement = base.GetTemplateChild("VerticalRootElement") as FrameworkElement;
            this.UpdateTemplate();
        }

        protected virtual void UpdateTemplate()
        {
            if (this.HorizontalRootElement != null)
            {
                this.HorizontalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Horizontal);
                if (this.HorizontalRootElement.GetVisible())
                {
                    base.Cursor = this.HorizontalRootElement.Cursor;
                }
            }
            if (this.VerticalRootElement != null)
            {
                this.VerticalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Vertical);
                if (this.VerticalRootElement.GetVisible())
                {
                    base.Cursor = this.VerticalRootElement.Cursor;
                }
            }
            base.InvalidateMeasure();
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public double Thickness
        {
            get => 
                (double) base.GetValue(ThicknessProperty);
            set => 
                base.SetValue(ThicknessProperty, value);
        }

        protected FrameworkElement HorizontalRootElement { get; private set; }

        protected FrameworkElement VerticalRootElement { get; private set; }
    }
}

