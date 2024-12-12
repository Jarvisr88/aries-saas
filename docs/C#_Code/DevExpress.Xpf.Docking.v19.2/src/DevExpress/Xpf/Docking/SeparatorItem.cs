namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SeparatorItem : FixedItem
    {
        internal static int SeparatorHeight = 8;
        internal static int SeparatorWidth = 8;
        public static readonly DependencyProperty OrientationProperty;
        internal static readonly DependencyPropertyKey OrientationPropertyKey;

        static SeparatorItem()
        {
            DependencyPropertyRegistrator<SeparatorItem> registrator = new DependencyPropertyRegistrator<SeparatorItem>();
            GridLength defValue = new GridLength();
            registrator.OverrideMetadata<GridLength>(BaseLayoutItem.ItemWidthProperty, defValue, null, null);
            defValue = new GridLength();
            registrator.OverrideMetadata<GridLength>(BaseLayoutItem.ItemHeightProperty, defValue, null, null);
            registrator.RegisterReadonly<System.Windows.Controls.Orientation>("Orientation", ref OrientationPropertyKey, ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, null, (dObj, value) => ((SeparatorItem) dObj).CoerceOrientation((System.Windows.Controls.Orientation) value));
        }

        protected override Size CalcMaxSizeValue(Size value) => 
            ((base.Parent == null) || (base.Parent.Orientation != System.Windows.Controls.Orientation.Horizontal)) ? new Size(value.Width, (double) SeparatorHeight) : new Size((double) SeparatorWidth, value.Height);

        protected virtual object CoerceOrientation(System.Windows.Controls.Orientation value) => 
            (base.Parent != null) ? base.Parent.Orientation : value;

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.Separator;

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            base.CoerceValue(OrientationProperty);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            internal set => 
                base.SetValue(OrientationPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SeparatorItem.<>c <>9 = new SeparatorItem.<>c();

            internal object <.cctor>b__4_0(DependencyObject dObj, object value) => 
                ((SeparatorItem) dObj).CoerceOrientation((Orientation) value);
        }
    }
}

