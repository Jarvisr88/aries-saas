namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class TabHeaderTransformPanel : LayoutTransformPanel
    {
        public static readonly DependencyProperty CaptionLocationProperty;
        public static readonly DependencyProperty CaptionOrientationProperty;

        static TabHeaderTransformPanel()
        {
            DependencyPropertyRegistrator<TabHeaderTransformPanel> registrator = new DependencyPropertyRegistrator<TabHeaderTransformPanel>();
            registrator.Register<DevExpress.Xpf.Docking.CaptionLocation>("CaptionLocation", ref CaptionLocationProperty, DevExpress.Xpf.Docking.CaptionLocation.Default, (dObj, ea) => ((TabHeaderTransformPanel) dObj).OnCaptionLocationChanged(ea.NewValue), null);
            registrator.Register<Orientation>("CaptionOrientation", ref CaptionOrientationProperty, Orientation.Horizontal, (dObj, ea) => ((TabHeaderTransformPanel) dObj).OnCaptionOrientationChanged(ea.NewValue), null);
        }

        public TabHeaderTransformPanel()
        {
            base.UseLayoutRounding = true;
        }

        protected virtual void OnCaptionLocationChanged(object newValue)
        {
            this.UpdateProperties();
        }

        protected virtual void OnCaptionOrientationChanged(object newValue)
        {
            this.UpdateProperties();
        }

        private void UpdateProperties()
        {
            this.Orientation = (this.CaptionOrientation == Orientation.Horizontal) ? Orientation.Vertical : Orientation.Horizontal;
            if (this.CaptionOrientation == Orientation.Vertical)
            {
                this.Clockwise = (this.CaptionLocation == DevExpress.Xpf.Docking.CaptionLocation.Right) || (this.CaptionLocation == DevExpress.Xpf.Docking.CaptionLocation.Bottom);
            }
        }

        public Orientation CaptionOrientation
        {
            get => 
                (Orientation) base.GetValue(CaptionOrientationProperty);
            set => 
                base.SetValue(CaptionOrientationProperty, value);
        }

        public DevExpress.Xpf.Docking.CaptionLocation CaptionLocation
        {
            get => 
                (DevExpress.Xpf.Docking.CaptionLocation) base.GetValue(CaptionLocationProperty);
            set => 
                base.SetValue(CaptionLocationProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabHeaderTransformPanel.<>c <>9 = new TabHeaderTransformPanel.<>c();

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderTransformPanel) dObj).OnCaptionLocationChanged(ea.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderTransformPanel) dObj).OnCaptionOrientationChanged(ea.NewValue);
            }
        }
    }
}

