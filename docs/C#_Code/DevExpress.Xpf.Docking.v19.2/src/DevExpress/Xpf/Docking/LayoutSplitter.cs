namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutSplitter : FixedItem
    {
        internal static int LayoutSplitterHeight = 8;
        internal static int LayoutSplitterWidth = 8;
        public static readonly DependencyProperty OrientationProperty;
        internal static readonly DependencyPropertyKey OrientationPropertyKey;

        static LayoutSplitter()
        {
            DependencyPropertyRegistrator<LayoutSplitter> registrator = new DependencyPropertyRegistrator<LayoutSplitter>();
            GridLength defValue = new GridLength();
            registrator.OverrideMetadata<GridLength>(BaseLayoutItem.ItemWidthProperty, defValue, null, null);
            defValue = new GridLength();
            registrator.OverrideMetadata<GridLength>(BaseLayoutItem.ItemHeightProperty, defValue, null, null);
            registrator.RegisterReadonly<System.Windows.Controls.Orientation>("Orientation", ref OrientationPropertyKey, ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, (dObj, e) => dObj.CoerceValue(BaseLayoutItem.ActualMaxSizeProperty), (dObj, value) => ((LayoutSplitter) dObj).CoerceOrientation((System.Windows.Controls.Orientation) value));
        }

        protected override Size CalcMaxSizeValue(Size value) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? new Size((double) LayoutSplitterWidth, value.Height) : new Size(value.Width, (double) LayoutSplitterHeight);

        protected virtual bool CoerceIsEnabled(bool value) => 
            ((base.Manager == null) || !base.Manager.IsCustomization) & value;

        protected virtual object CoerceOrientation(System.Windows.Controls.Orientation value) => 
            (base.Parent != null) ? base.Parent.Orientation : value;

        internal bool GetIsCustomization() => 
            (base.Manager != null) && base.Manager.IsCustomization;

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.LayoutSplitter;

        internal override void OnIsCustomizationChanged(bool isCustomization)
        {
            base.OnIsCustomizationChanged(isCustomization);
            base.CoerceValue(UIElement.IsEnabledProperty);
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            base.CoerceValue(BaseLayoutItem.ActualMaxSizeProperty);
            base.CoerceValue(OrientationProperty);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            internal set => 
                base.SetValue(OrientationPropertyKey, value);
        }

        protected override bool IsEnabledCore =>
            this.CoerceIsEnabled(base.IsEnabledCore);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutSplitter.<>c <>9 = new LayoutSplitter.<>c();

            internal void <.cctor>b__4_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                dObj.CoerceValue(BaseLayoutItem.ActualMaxSizeProperty);
            }

            internal object <.cctor>b__4_1(DependencyObject dObj, object value) => 
                ((LayoutSplitter) dObj).CoerceOrientation((Orientation) value);
        }
    }
}

