namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    public class FlowLayoutItemDragAndDropController : LayoutItemDragAndDropControllerBase
    {
        public FlowLayoutItemDragAndDropController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint, FrameworkElement dragControl) : base(controller, startDragPoint, dragControl)
        {
            if (ElementPositionsAnimation != null)
            {
                ElementPositionsAnimation.Stop();
                ElementPositionsAnimation = null;
            }
        }

        public override void DragAndDrop(Point p)
        {
            base.DragAndDrop(p);
            if ((ElementPositionsAnimation == null) || !ElementPositionsAnimation.IsActive)
            {
                FlowBreakKind kind;
                int visibleIndex = this.Controller.LayoutProvider.GetItemPlaceIndex(base.DragControl, this.Controller.ClientBounds, this.GetItemPlacePoint(p), out kind);
                if (visibleIndex != -1)
                {
                    int itemIndex = this.Controller.LayoutProvider.GetItemIndex(base.DragControlParent.Children, visibleIndex);
                    if ((kind == FlowBreakKind.New) && !this.Controller.ILayoutControl.AllowAddFlowBreaksDuringItemMoving)
                    {
                        kind = (visibleIndex == 0) ? FlowBreakKind.Existing : FlowBreakKind.None;
                    }
                    bool flag = kind != FlowBreakKind.None;
                    FrameworkElements layoutItems = this.Controller.LayoutProvider.LayoutItems;
                    int index = layoutItems.IndexOf(base.DragControlPlaceHolder);
                    int num4 = base.DragControlParent.Children.IndexOf(base.DragControlPlaceHolder);
                    bool flag2 = (index == 0) || FlowLayoutControl.GetIsFlowBreak(base.DragControlPlaceHolder);
                    bool flag3 = flag2 && ((index == (layoutItems.Count - 1)) || FlowLayoutControl.GetIsFlowBreak(layoutItems[index + 1]));
                    if (itemIndex > num4)
                    {
                        itemIndex--;
                    }
                    if (((num4 != itemIndex) || ((((index + 1) == visibleIndex) & flag) && (!flag2 || (kind == FlowBreakKind.Existing)))) || ((visibleIndex == index) && ((!flag & flag2) || ((kind == FlowBreakKind.New) && !flag3))))
                    {
                        if (this.Controller.ILayoutControl.AnimateItemMoving)
                        {
                            ElementPositionsAnimation = new ElementBoundsAnimation(layoutItems);
                            ElementPositionsAnimation.StoreOldElementBounds(null);
                        }
                        if (flag2)
                        {
                            if (flag3)
                            {
                                this.OnGroupFirstItemChanged(base.DragControlPlaceHolder, null);
                            }
                            else
                            {
                                this.OnGroupFirstItemChanged(base.DragControlPlaceHolder, layoutItems[index + 1]);
                            }
                        }
                        if (kind == FlowBreakKind.Existing)
                        {
                            this.OnGroupFirstItemChanged(layoutItems[visibleIndex], base.DragControlPlaceHolder);
                        }
                        base.DragControlParent.Children.RemoveAt(num4);
                        base.DragControlParent.Children.Insert(itemIndex, base.DragControlPlaceHolder);
                        if ((flag2 && ((index > 0) || (visibleIndex == 0))) && (index < (layoutItems.Count - 1)))
                        {
                            this.SetIsFlowBreakAndStoreOriginalValue(layoutItems[index + 1], true);
                        }
                        if (flag && (visibleIndex < layoutItems.Count))
                        {
                            this.SetIsFlowBreakAndStoreOriginalValue(layoutItems[visibleIndex], kind == FlowBreakKind.New);
                        }
                        FlowLayoutControl.SetIsFlowBreak(base.DragControlPlaceHolder, flag && (visibleIndex > 0));
                        if (this.Controller.ILayoutControl.AnimateItemMoving)
                        {
                            this.Controller.Control.UpdateLayout();
                            ElementPositionsAnimation.StoreNewElementBounds(null);
                            ElementPositionsAnimation.Begin(this.Controller.ILayoutControl.ItemMovingAnimationDuration, null, null);
                        }
                    }
                }
            }
        }

        public override void EndDragAndDrop(bool accept)
        {
            if (ElementPositionsAnimation != null)
            {
                ElementPositionsAnimation.Stop();
                ElementPositionsAnimation = null;
            }
            if (this.Controller.ILayoutControl.AnimateItemMoving)
            {
                FrameworkElements elements = new FrameworkElements();
                elements.Add(base.DragControl);
                ElementPositionsAnimation = new ElementBoundsAnimation(elements);
                ElementPositionsAnimation.StoreOldElementBounds(this.Controller.Control);
            }
            int index = base.DragControlParent.Children.IndexOf(base.DragControlPlaceHolder);
            bool isFlowBreak = FlowLayoutControl.GetIsFlowBreak(base.DragControlPlaceHolder);
            base.EndDragAndDrop(accept);
            if (!accept)
            {
                this.RestoreIsFlowBreakOriginalValues();
            }
            else
            {
                if ((base.DragControlIndex != index) || (FlowLayoutControl.GetIsFlowBreak(base.DragControl) != isFlowBreak))
                {
                    int oldPosition = this.Controller.ILayoutControl.GetLogicalChildren(false).IndexOf(base.DragControl);
                    base.DragControlParent.Children.Remove(base.DragControl);
                    base.DragControlParent.Children.Insert(index, base.DragControl);
                    FlowLayoutControl.SetIsFlowBreak(base.DragControl, isFlowBreak);
                    this.Controller.ILayoutControl.OnItemPositionChanged(oldPosition, this.Controller.ILayoutControl.GetLogicalChildren(false).IndexOf(base.DragControl));
                }
                this.SendIsFlowBreakChangeNotifications();
            }
            if (this.Controller.ILayoutControl.AnimateItemMoving)
            {
                object storedDragControlOpacity = base.DragControl.StorePropertyValue(UIElement.OpacityProperty);
                base.DragControl.Opacity = 0.0;
                this.Controller.Control.UpdateLayout();
                ElementPositionsAnimation.StoreNewElementBounds(null);
                object storedDragControlZIndex = base.DragControl.StorePropertyValue(Panel.ZIndexProperty);
                base.DragControl.SetZIndex(0x3e8);
                object storedDragControlIsHitTestVisible = base.DragControl.StorePropertyValue(UIElement.IsHitTestVisibleProperty);
                base.DragControl.IsHitTestVisible = false;
                ExponentialEase easingFunction = new ExponentialEase();
                easingFunction.Exponent = 5.0;
                ElementPositionsAnimation.Begin(FlowLayoutControl.ItemDropAnimationDuration, easingFunction, delegate {
                    this.DragControl.RestorePropertyValue(Panel.ZIndexProperty, storedDragControlZIndex);
                    this.DragControl.RestorePropertyValue(UIElement.IsHitTestVisibleProperty, storedDragControlIsHitTestVisible);
                    ElementPositionsAnimation = null;
                });
                Dispatcher.CurrentDispatcher.BeginInvoke(() => this.DragControl.RestorePropertyValue(UIElement.OpacityProperty, storedDragControlOpacity), DispatcherPriority.Render, new object[0]);
            }
        }

        protected virtual Point GetItemPlacePoint(Point p) => 
            p;

        protected virtual void OnGroupFirstItemChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
        }

        protected void RestoreIsFlowBreakOriginalValues()
        {
            if (this.IsFlowBreakOriginalValues != null)
            {
                foreach (KeyValuePair<UIElement, bool> pair in this.IsFlowBreakOriginalValues)
                {
                    FlowLayoutControl.SetIsFlowBreak(pair.Key, pair.Value);
                }
            }
        }

        protected void SendIsFlowBreakChangeNotifications()
        {
            if (this.IsFlowBreakOriginalValues != null)
            {
                FrameworkElements logicalChildren = this.Controller.ILayoutControl.GetLogicalChildren(false);
                foreach (KeyValuePair<UIElement, bool> pair in this.IsFlowBreakOriginalValues)
                {
                    if (FlowLayoutControl.GetIsFlowBreak(pair.Key) != pair.Value)
                    {
                        int index = logicalChildren.IndexOf((FrameworkElement) pair.Key);
                        this.Controller.ILayoutControl.OnItemPositionChanged(index, index);
                    }
                }
            }
        }

        protected void SetIsFlowBreakAndStoreOriginalValue(UIElement element, bool value)
        {
            if (FlowLayoutControl.GetIsFlowBreak(element) != value)
            {
                this.IsFlowBreakOriginalValues ??= new Dictionary<UIElement, bool>();
                if (!this.IsFlowBreakOriginalValues.ContainsKey(element))
                {
                    this.IsFlowBreakOriginalValues.Add(element, FlowLayoutControl.GetIsFlowBreak(element));
                }
                FlowLayoutControl.SetIsFlowBreak(element, value);
            }
        }

        protected static ElementBoundsAnimation ElementPositionsAnimation { get; set; }

        protected FlowLayoutController Controller =>
            (FlowLayoutController) base.Controller;

        private Dictionary<UIElement, bool> IsFlowBreakOriginalValues { get; set; }
    }
}

