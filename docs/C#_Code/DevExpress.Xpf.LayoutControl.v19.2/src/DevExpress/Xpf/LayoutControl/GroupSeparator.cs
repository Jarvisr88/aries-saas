namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="HorizontalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="VerticalRootElement", Type=typeof(FrameworkElement))]
    public class GroupSeparator : ControlBase
    {
        public static readonly DependencyProperty OrientationProperty;
        private const string HorizontalRootElementName = "HorizontalRootElement";
        private const string VerticalRootElementName = "VerticalRootElement";

        static GroupSeparator()
        {
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(GroupSeparator), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal, (o, e) => ((GroupSeparator) o).OnOrientationChanged()));
        }

        public GroupSeparator()
        {
            base.DefaultStyleKey = typeof(GroupSeparator);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.HorizontalRootElement = base.GetTemplateChild("HorizontalRootElement") as FrameworkElement;
            this.VerticalRootElement = base.GetTemplateChild("VerticalRootElement") as FrameworkElement;
            this.UpdateTemplate();
        }

        protected virtual void OnOrientationChanged()
        {
            this.UpdateTemplate();
        }

        protected virtual void UpdateTemplate()
        {
            if (this.HorizontalRootElement != null)
            {
                this.HorizontalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Horizontal);
            }
            if (this.VerticalRootElement != null)
            {
                this.VerticalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Vertical);
            }
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        protected FrameworkElement HorizontalRootElement { get; private set; }

        protected FrameworkElement VerticalRootElement { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupSeparator.<>c <>9 = new GroupSeparator.<>c();

            internal void <.cctor>b__18_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((GroupSeparator) o).OnOrientationChanged();
            }
        }
    }
}

