namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class VirtualDataStackPanelScrollInfo : ScrollInfo
    {
        public static readonly DependencyProperty OrientationProperty;
        private System.Windows.Controls.Orientation orientationCore;

        static VirtualDataStackPanelScrollInfo()
        {
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(VirtualDataStackPanelScrollInfo), new PropertyMetadata(System.Windows.Controls.Orientation.Vertical, (d, e) => ((VirtualDataStackPanelScrollInfo) d).OnOrientationChanged((System.Windows.Controls.Orientation) e.NewValue)));
        }

        public VirtualDataStackPanelScrollInfo(IScrollInfoOwner owner) : base(owner)
        {
            this.orientationCore = System.Windows.Controls.Orientation.Vertical;
        }

        protected override ScrollInfoBase CreateHorizontalScrollInfo() => 
            this.CreateScrollInfo(base.Owner.HorizontalScrollMode, SizeHelperBase.GetDefineSizeHelper(System.Windows.Controls.Orientation.Horizontal));

        private ScrollInfoBase CreateScrollInfo(DataControlScrollMode scrollMode, SizeHelperBase sizeHelper) => 
            (scrollMode != DataControlScrollMode.Pixel) ? ((scrollMode != DataControlScrollMode.Item) ? ((scrollMode != DataControlScrollMode.ItemPixel) ? ((ScrollInfoBase) new ScrollByRowPixelInfo(base.Owner, sizeHelper)) : ((ScrollInfoBase) new ScrollByItemPixelInfo(base.Owner, sizeHelper))) : ((ScrollInfoBase) new ScrollByItemInfo(base.Owner, sizeHelper))) : ((ScrollInfoBase) new ScrollByPixelInfo(base.Owner, sizeHelper));

        protected override ScrollInfoBase CreateVerticalScrollInfo() => 
            this.CreateScrollInfo(base.Owner.VerticalScrollMode, SizeHelperBase.GetDefineSizeHelper(System.Windows.Controls.Orientation.Vertical));

        private void OnOrientationChanged(System.Windows.Controls.Orientation newOrientation)
        {
            this.orientationCore = newOrientation;
            this.ClearScrollInfo();
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                this.orientationCore;
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public override ScrollByItemInfo DefineSizeScrollInfo =>
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? ((ScrollByItemInfo) this.VerticalScrollInfo) : ((ScrollByItemInfo) this.HorizontalScrollInfo);

        public override ScrollByPixelInfo SecondarySizeScrollInfo =>
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? ((ScrollByPixelInfo) this.HorizontalScrollInfo) : ((ScrollByPixelInfo) this.VerticalScrollInfo);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VirtualDataStackPanelScrollInfo.<>c <>9 = new VirtualDataStackPanelScrollInfo.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((VirtualDataStackPanelScrollInfo) d).OnOrientationChanged((Orientation) e.NewValue);
            }
        }
    }
}

