namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public class LabelItem : FixedItem
    {
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;
        public static readonly DependencyProperty HasContentProperty;
        internal static readonly DependencyPropertyKey HasContentPropertyKey;
        public static readonly DependencyProperty ContentHorizontalAlignmentProperty;
        public static readonly DependencyProperty ContentVerticalAlignmentProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty DesiredSizeInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HasDesiredSizeProperty;
        internal static readonly DependencyPropertyKey HasDesiredSizePropertyKey;
        private Size desiredSizeCore;

        static LabelItem()
        {
            DependencyPropertyRegistrator<LabelItem> registrator = new DependencyPropertyRegistrator<LabelItem>();
            registrator.OverrideMetadata<GridLength>(BaseLayoutItem.ItemHeightProperty, new GridLength(1.0, GridUnitType.Auto), null, null);
            registrator.Register<object>("Content", ref ContentProperty, null, (dObj, e) => ((LabelItem) dObj).OnContentChanged(e.NewValue), null);
            registrator.Register<DataTemplate>("ContentTemplate", ref ContentTemplateProperty, null, null, null);
            registrator.Register<DataTemplateSelector>("ContentTemplateSelector", ref ContentTemplateSelectorProperty, null, null, null);
            registrator.RegisterReadonly<bool>("HasContent", ref HasContentPropertyKey, ref HasContentProperty, false, null, null);
            registrator.Register<HorizontalAlignment>("ContentHorizontalAlignment", ref ContentHorizontalAlignmentProperty, HorizontalAlignment.Stretch, null, null);
            registrator.Register<VerticalAlignment>("ContentVerticalAlignment", ref ContentVerticalAlignmentProperty, VerticalAlignment.Stretch, null, null);
            registrator.Register<Size>("DesiredSizeInternal", ref DesiredSizeInternalProperty, Size.Empty, (dObj, e) => ((LabelItem) dObj).OnDesiredSizeChanged((Size) e.NewValue), (dObj, value) => ((LabelItem) dObj).CoerceDesiredSize((Size) value));
            registrator.RegisterReadonly<bool>("HasDesiredSize", ref HasDesiredSizePropertyKey, ref HasDesiredSizeProperty, false, null, null);
        }

        protected override Size CalcMinSizeValue(Size value)
        {
            if (!this.HasDesiredSize)
            {
                return base.CalcMinSizeValue(value);
            }
            Size[] minSizes = new Size[] { this.DesiredSizeInternal, value };
            return MathHelper.MeasureMinSize(minSizes);
        }

        protected virtual object CoerceDesiredSize(Size value) => 
            !this.HasDesiredSize ? value : this.desiredSizeCore;

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.Label;

        protected virtual void OnContentChanged(object content)
        {
            base.SetValue(HasContentPropertyKey, this.Content != null);
        }

        protected virtual void OnDesiredSizeChanged(Size value)
        {
            this.SetDesiredSize(value);
            base.SetValue(HasDesiredSizePropertyKey, !value.IsEmpty);
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        private void SetDesiredSize(Size value)
        {
            if (this.desiredSizeCore != value)
            {
                this.desiredSizeCore = value;
            }
        }

        [Description("Gets or sets a LabelItem's caption. This is a dependency property."), XtraSerializableProperty, Category("Content")]
        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Description("Gets or sets a DataTemplate object to visualize a LabelItem.Content object.This is a dependency property.")]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a LabelItem.ContentTemplate used to visualize objects defined as a Label Item's LabelItem.Content. This is a dependency property.")]
        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }

        [Description("Gets if the current LabelItem has a caption. This is a dependency property.")]
        public bool HasContent =>
            (bool) base.GetValue(HasContentProperty);

        [Description("Gets or sets a horizontal alignment for a Label Item's LabelItem.Content. This is a dependency property."), XtraSerializableProperty, Category("Content")]
        public HorizontalAlignment ContentHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(ContentHorizontalAlignmentProperty);
            set => 
                base.SetValue(ContentHorizontalAlignmentProperty, value);
        }

        [Description("Gets or sets a vertical alignment for a Label Item's LabelItem.Content. This is a dependency property."), XtraSerializableProperty, Category("Content")]
        public VerticalAlignment ContentVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(ContentVerticalAlignmentProperty);
            set => 
                base.SetValue(ContentVerticalAlignmentProperty, value);
        }

        internal Size DesiredSizeInternal
        {
            get => 
                (Size) base.GetValue(DesiredSizeInternalProperty);
            set => 
                base.SetValue(DesiredSizeInternalProperty, value);
        }

        internal bool HasDesiredSize =>
            (bool) base.GetValue(HasDesiredSizeProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LabelItem.<>c <>9 = new LabelItem.<>c();

            internal void <.cctor>b__10_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LabelItem) dObj).OnContentChanged(e.NewValue);
            }

            internal void <.cctor>b__10_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LabelItem) dObj).OnDesiredSizeChanged((Size) e.NewValue);
            }

            internal object <.cctor>b__10_2(DependencyObject dObj, object value) => 
                ((LabelItem) dObj).CoerceDesiredSize((Size) value);
        }
    }
}

