namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public class TabHeaderContainer : DockDependentDecorator
    {
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty TopSelectedMarginProperty;
        public static readonly DependencyProperty BottomSelectedMarginProperty;
        public static readonly DependencyProperty LeftSelectedMarginProperty;
        public static readonly DependencyProperty RightSelectedMarginProperty;

        static TabHeaderContainer()
        {
            DependencyPropertyRegistrator<TabHeaderContainer> registrator = new DependencyPropertyRegistrator<TabHeaderContainer>();
            registrator.Register<bool>("IsSelected", ref IsSelectedProperty, false, (dObj, ea) => ((TabHeaderContainer) dObj).UpdateChildMargin(), null);
            Thickness defValue = new Thickness();
            registrator.Register<Thickness>("TopSelectedMargin", ref TopSelectedMarginProperty, defValue, (dObj, ea) => ((TabHeaderContainer) dObj).UpdateChildMargin(), null);
            defValue = new Thickness();
            registrator.Register<Thickness>("BottomSelectedMargin", ref BottomSelectedMarginProperty, defValue, (dObj, ea) => ((TabHeaderContainer) dObj).UpdateChildMargin(), null);
            defValue = new Thickness();
            registrator.Register<Thickness>("LeftSelectedMargin", ref LeftSelectedMarginProperty, defValue, (dObj, ea) => ((TabHeaderContainer) dObj).UpdateChildMargin(), null);
            defValue = new Thickness();
            registrator.Register<Thickness>("RightSelectedMargin", ref RightSelectedMarginProperty, defValue, (dObj, ea) => ((TabHeaderContainer) dObj).UpdateChildMargin(), null);
        }

        protected override Thickness GetActualChildMargin() => 
            this.IsSelected ? this.GetSelectedChildMargin() : base.GetActualChildMargin();

        protected virtual Thickness GetSelectedChildMargin()
        {
            switch (base.CaptionLocation)
            {
                case CaptionLocation.Left:
                    return this.LeftSelectedMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

                case CaptionLocation.Top:
                    return this.TopSelectedMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

                case CaptionLocation.Right:
                    return this.RightSelectedMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

                case CaptionLocation.Bottom:
                    return this.BottomSelectedMargin.Multiply(ScreenHelper.DpiThicknessCorrection);
            }
            return this.DefaultSelectedMargin.Multiply(ScreenHelper.DpiThicknessCorrection);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        public Thickness TopSelectedMargin
        {
            get => 
                (Thickness) base.GetValue(TopSelectedMarginProperty);
            set => 
                base.SetValue(TopSelectedMarginProperty, value);
        }

        public Thickness BottomSelectedMargin
        {
            get => 
                (Thickness) base.GetValue(BottomSelectedMarginProperty);
            set => 
                base.SetValue(BottomSelectedMarginProperty, value);
        }

        public Thickness LeftSelectedMargin
        {
            get => 
                (Thickness) base.GetValue(LeftSelectedMarginProperty);
            set => 
                base.SetValue(LeftSelectedMarginProperty, value);
        }

        public Thickness RightSelectedMargin
        {
            get => 
                (Thickness) base.GetValue(RightSelectedMarginProperty);
            set => 
                base.SetValue(RightSelectedMarginProperty, value);
        }

        public Thickness DefaultSelectedMargin =>
            this.TopSelectedMargin;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabHeaderContainer.<>c <>9 = new TabHeaderContainer.<>c();

            internal void <.cctor>b__5_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderContainer) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderContainer) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_2(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderContainer) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_3(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderContainer) dObj).UpdateChildMargin();
            }

            internal void <.cctor>b__5_4(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabHeaderContainer) dObj).UpdateChildMargin();
            }
        }
    }
}

