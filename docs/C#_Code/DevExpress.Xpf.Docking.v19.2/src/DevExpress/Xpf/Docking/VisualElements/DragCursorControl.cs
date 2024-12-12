namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_CaptionControl", Type=typeof(DevExpress.Xpf.Docking.VisualElements.CaptionControl))]
    public class DragCursorControl : psvControl, IControlHost
    {
        public static readonly DependencyProperty CursorTypeProperty;
        private BaseLayoutItem dragItem;

        static DragCursorControl()
        {
            DependencyPropertyRegistrator<DragCursorControl> registrator = new DependencyPropertyRegistrator<DragCursorControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DragCursorType>("CursorType", ref CursorTypeProperty, DragCursorType.Item, (dObj, ea) => ((DragCursorControl) dObj).OnCursorTypeChanged((DragCursorType) ea.NewValue), null);
        }

        public FrameworkElement[] GetChildren() => 
            new FrameworkElement[] { this.CaptionControl };

        private DragCursorType GetDragCursorType()
        {
            if (this.DragItem.IsItemWithRestrictedFloating() && this.DragItem.AllowFloat)
            {
                return DragCursorType.Window;
            }
            LayoutItemType[] source = new LayoutItemType[] { LayoutItemType.Panel, LayoutItemType.Document, LayoutItemType.TabPanelGroup, LayoutItemType.FloatGroup };
            return (source.Contains<LayoutItemType>(this.DragItem.ItemType) ? DragCursorType.Panel : DragCursorType.Item);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CaptionControl = base.GetTemplateChild("PART_CaptionControl") as TemplatedCaptionControl;
            this.UpdateVisualState();
        }

        protected virtual void OnCursorTypeChanged(DragCursorType type)
        {
            this.UpdateVisualState();
        }

        private void OnDragItemChanged()
        {
            if (this.DragItem == null)
            {
                base.ClearValue(FrameworkElement.DataContextProperty);
                DockLayoutManager.SetLayoutItem(this, null);
            }
            else
            {
                BaseLayoutItem dragItem = this.DragItem;
                TabbedGroup group = this.DragItem as TabbedGroup;
                if ((group != null) && ((group.CaptionImage == null) && ((group.CaptionTemplate == null) && ((group.Caption == null) && (string.IsNullOrEmpty(group.ActualCaption) && (group.SelectedItem != null))))))
                {
                    dragItem = group.SelectedItem;
                }
                base.DataContext = dragItem;
                DockLayoutManager.SetLayoutItem(this, dragItem);
                this.CursorType = this.GetDragCursorType();
            }
        }

        private void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, (this.CursorType == DragCursorType.Item) ? "CursorTypeItem" : "CursorTypePanel", false);
        }

        public DragCursorType CursorType
        {
            get => 
                (DragCursorType) base.GetValue(CursorTypeProperty);
            set => 
                base.SetValue(CursorTypeProperty, value);
        }

        internal BaseLayoutItem DragItem
        {
            get => 
                this.dragItem;
            set
            {
                if (!ReferenceEquals(this.dragItem, value))
                {
                    this.dragItem = value;
                    this.OnDragItemChanged();
                }
            }
        }

        protected TemplatedCaptionControl CaptionControl { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragCursorControl.<>c <>9 = new DragCursorControl.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DragCursorControl) dObj).OnCursorTypeChanged((DragCursorType) ea.NewValue);
            }
        }
    }
}

