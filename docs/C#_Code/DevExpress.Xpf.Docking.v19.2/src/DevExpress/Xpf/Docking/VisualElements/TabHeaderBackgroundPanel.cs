namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class TabHeaderBackgroundPanel : RenderTransformPanel
    {
        public static readonly DependencyProperty CaptionLocationProperty;
        public static readonly DependencyProperty CaptionOrientationProperty;

        static TabHeaderBackgroundPanel()
        {
            DependencyPropertyRegistrator<TabHeaderBackgroundPanel> registrator = new DependencyPropertyRegistrator<TabHeaderBackgroundPanel>();
            registrator.Register<DevExpress.Xpf.Docking.CaptionLocation>("CaptionLocation", ref CaptionLocationProperty, DevExpress.Xpf.Docking.CaptionLocation.Default, (dObj, ea) => ((TabHeaderBackgroundPanel) dObj).OnCaptionLocationChanged(ea.NewValue), null);
            registrator.Register<Orientation>("CaptionOrientation", ref CaptionOrientationProperty, Orientation.Horizontal, (dObj, ea) => ((TabHeaderBackgroundPanel) dObj).OnCaptionOrientationChanged(ea.NewValue), null);
        }

        public TabHeaderBackgroundPanel()
        {
            this.UpdateProperties();
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
            base.Dock = this.CaptionLocation.ToDock(Dock.Top);
            if (this.CaptionOrientation != Orientation.Horizontal)
            {
                if ((base.Dock == Dock.Left) || (base.Dock == Dock.Right))
                {
                    base.Orientation = Orientation.Vertical;
                }
            }
            else if ((base.Dock == Dock.Top) || (base.Dock == Dock.Bottom))
            {
                base.Orientation = Orientation.Horizontal;
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
            public static readonly TabHeaderBackgroundPanel.<>c <>9 = new TabHeaderBackgroundPanel.<>c();

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderBackgroundPanel) dObj).OnCaptionLocationChanged(ea.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderBackgroundPanel) dObj).OnCaptionOrientationChanged(ea.NewValue);
            }
        }
    }
}

