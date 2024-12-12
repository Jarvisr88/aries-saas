namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class DockDependentDecorator : Decorator
    {
        public static readonly DependencyProperty TopMarginProperty;
        public static readonly DependencyProperty BottomMarginProperty;
        public static readonly DependencyProperty LeftMarginProperty;
        public static readonly DependencyProperty RightMarginProperty;
        public static readonly DependencyProperty CaptionLocationProperty;

        static DockDependentDecorator()
        {
            DependencyPropertyRegistrator<DockDependentDecorator> registrator = new DependencyPropertyRegistrator<DockDependentDecorator>();
            Thickness defValue = new Thickness();
            registrator.Register<Thickness>("TopMargin", ref TopMarginProperty, defValue, (dObj, ea) => ((DockDependentDecorator) dObj).UpdateChildMargin(), null);
            defValue = new Thickness();
            registrator.Register<Thickness>("BottomMargin", ref BottomMarginProperty, defValue, (dObj, ea) => ((DockDependentDecorator) dObj).UpdateChildMargin(), null);
            defValue = new Thickness();
            registrator.Register<Thickness>("LeftMargin", ref LeftMarginProperty, defValue, (dObj, ea) => ((DockDependentDecorator) dObj).UpdateChildMargin(), null);
            defValue = new Thickness();
            registrator.Register<Thickness>("RightMargin", ref RightMarginProperty, defValue, (dObj, ea) => ((DockDependentDecorator) dObj).UpdateChildMargin(), null);
            registrator.Register<DevExpress.Xpf.Docking.CaptionLocation>("CaptionLocation", ref CaptionLocationProperty, DevExpress.Xpf.Docking.CaptionLocation.Default, (dObj, ea) => ((DockDependentDecorator) dObj).UpdateChildMargin(), null);
        }

        protected virtual Thickness GetActualChildMargin()
        {
            switch (this.CaptionLocation)
            {
                case DevExpress.Xpf.Docking.CaptionLocation.Left:
                    return this.LeftMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

                case DevExpress.Xpf.Docking.CaptionLocation.Top:
                    return this.TopMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

                case DevExpress.Xpf.Docking.CaptionLocation.Right:
                    return this.RightMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

                case DevExpress.Xpf.Docking.CaptionLocation.Bottom:
                    return this.BottomMargin.Multiply(ScreenHelper.DpiThicknessCorrection);
            }
            return this.DefaultMargin.Multiply(ScreenHelper.DpiThicknessCorrection);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            this.UpdateChildMargin();
        }

        protected virtual void UpdateChildMargin()
        {
            FrameworkElement child = this.Child as FrameworkElement;
            if (child != null)
            {
                child.Margin = this.GetActualChildMargin();
            }
        }

        public Thickness TopMargin
        {
            get => 
                (Thickness) base.GetValue(TopMarginProperty);
            set => 
                base.SetValue(TopMarginProperty, value);
        }

        public Thickness BottomMargin
        {
            get => 
                (Thickness) base.GetValue(BottomMarginProperty);
            set => 
                base.SetValue(BottomMarginProperty, value);
        }

        public Thickness LeftMargin
        {
            get => 
                (Thickness) base.GetValue(LeftMarginProperty);
            set => 
                base.SetValue(LeftMarginProperty, value);
        }

        public Thickness RightMargin
        {
            get => 
                (Thickness) base.GetValue(RightMarginProperty);
            set => 
                base.SetValue(RightMarginProperty, value);
        }

        protected virtual Thickness DefaultMargin =>
            this.TopMargin;

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
            public static readonly DockDependentDecorator.<>c <>9 = new DockDependentDecorator.<>c();

            internal void <.cctor>b__5_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockDependentDecorator) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockDependentDecorator) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_2(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockDependentDecorator) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_3(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockDependentDecorator) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_4(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockDependentDecorator) dObj).UpdateChildMargin();
            }
        }
    }
}

