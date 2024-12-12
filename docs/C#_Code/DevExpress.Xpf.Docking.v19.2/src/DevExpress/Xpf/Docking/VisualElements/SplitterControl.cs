namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_HorizontalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="PART_VerticalRootElement", Type=typeof(FrameworkElement))]
    public class SplitterControl : BaseSplitterControl
    {
        public static readonly DependencyProperty LayoutItemProperty;
        private const string HorizontalRootElementName = "PART_HorizontalRootElement";
        private const string VerticalRootElementName = "PART_VerticalRootElement";

        static SplitterControl()
        {
            DependencyPropertyRegistrator<SplitterControl> registrator = new DependencyPropertyRegistrator<SplitterControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, e) => ((SplitterControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue), null);
        }

        public SplitterControl() : base(null)
        {
            base.SplitterWidth = LayoutSplitter.LayoutSplitterWidth;
            base.SplitterHeight = LayoutSplitter.LayoutSplitterHeight;
        }

        protected override int GetCurrent(bool isColumns) => 
            (int) (this.LayoutItem ?? this).GetValue(isColumns ? Grid.ColumnProperty : Grid.RowProperty);

        protected override int GetNext(int current)
        {
            if ((base.LayoutGroup != null) && (this.LayoutItem != null))
            {
                for (int i = base.LayoutGroup.Items.IndexOf(this.LayoutItem); i < base.LayoutGroup.Items.Count; i++)
                {
                    BaseLayoutItem item = base.LayoutGroup.Items[i];
                    if (IsResizableItem(item, base.IsHorizontal))
                    {
                        return base.LayoutGroup.ItemsInternal.IndexOf(item);
                    }
                }
            }
            return current;
        }

        protected override Grid GetParentGrid() => 
            LayoutHelper.FindParentObject<Grid>(this);

        protected override int GetPrev(int current)
        {
            if ((base.LayoutGroup != null) && (this.LayoutItem != null))
            {
                for (int i = base.LayoutGroup.Items.IndexOf(this.LayoutItem); i >= 0; i--)
                {
                    BaseLayoutItem item = base.LayoutGroup.Items[i];
                    if (IsResizableItem(item, base.IsHorizontal))
                    {
                        return base.LayoutGroup.ItemsInternal.IndexOf(item);
                    }
                }
            }
            return current;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DockLayoutManager.Ensure(this, false);
            this.PartHorizontalRootElement = base.GetTemplateChild("PART_HorizontalRootElement") as FrameworkElement;
            this.PartVerticalRootElement = base.GetTemplateChild("PART_VerticalRootElement") as FrameworkElement;
            this.UpdateTemplateParts();
            if (base.LayoutGroup != null)
            {
                if (!base.IsActivated)
                {
                    this.Activate();
                }
                bool horizontal = base.LayoutGroup.Orientation == Orientation.Horizontal;
                base.InitSplitThumb(horizontal);
            }
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem item)
        {
            if (item != null)
            {
                base.LayoutGroup = item.Parent;
            }
            else
            {
                base.LayoutGroup = null;
            }
        }

        protected override void OnOrientationChanged(Orientation orientation)
        {
            base.OnOrientationChanged(orientation);
            this.UpdateTemplateParts();
        }

        private void UpdateTemplateParts()
        {
            if (this.PartHorizontalRootElement != null)
            {
                this.PartHorizontalRootElement.Visibility = VisibilityHelper.Convert(base.Orientation == Orientation.Vertical, Visibility.Collapsed);
            }
            if (this.PartVerticalRootElement != null)
            {
                this.PartVerticalRootElement.Visibility = VisibilityHelper.Convert(base.Orientation == Orientation.Horizontal, Visibility.Collapsed);
            }
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        protected FrameworkElement PartHorizontalRootElement { get; set; }

        protected FrameworkElement PartVerticalRootElement { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SplitterControl.<>c <>9 = new SplitterControl.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((SplitterControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue);
            }
        }
    }
}

