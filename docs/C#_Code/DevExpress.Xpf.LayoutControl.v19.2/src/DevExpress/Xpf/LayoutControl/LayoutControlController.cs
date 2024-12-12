namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public class LayoutControlController : LayoutGroupController
    {
        private bool _IsCustomization;

        public event EventHandler IsCustomizationChanged;

        public event EventHandler<LayoutControlModelChangedEventArgs> ModelChanged;

        public LayoutControlController(DevExpress.Xpf.LayoutControl.ILayoutControl control) : base(control)
        {
        }

        [CompilerGenerated, DebuggerHidden]
        private IEnumerable<UIElement> <>n__0() => 
            base.GetInternalElements();

        protected override bool CanItemDragAndDrop() => 
            this.IsCustomization && this.ILayoutControl.AllowItemMoving;

        protected virtual LayoutControlCustomizationController CreateCustomizationController() => 
            new LayoutControlCustomizationController(this);

        protected override DragAndDropController CreateItemDragAndDropControler(Point startDragPoint, FrameworkElement dragControl) => 
            new LayoutItemDragAndDropController(this, startDragPoint, dragControl);

        public void DesignTimeKeyDown(DXKeyEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                base.ProcessKeyDown(e);
            }
        }

        public void DesignTimeKeyUp(DXKeyEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                base.ProcessKeyUp(e);
            }
        }

        public void DesignTimeMouseCaptureCancelled()
        {
            if (this.ForwardDesignTimeInput)
            {
                this.OnMouseCaptureCancelled();
            }
        }

        public void DesignTimeMouseEnter(DXMouseEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                this.OnMouseEnter(e);
            }
        }

        public void DesignTimeMouseLeave(DXMouseEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                this.OnMouseLeave(e);
            }
        }

        public void DesignTimeMouseLeftButtonDown(DXMouseButtonEventArgs e)
        {
            if (!this.ForwardDesignTimeInput)
            {
                this.CustomizationController.ProcessSelection(base.Control, true);
            }
            FrameworkElement selectableItem = this.CustomizationController.GetSelectableItem(e.GetPosition(base.Control));
            if (selectableItem.IsLayoutGroup() && ((ILayoutGroup) selectableItem).DesignTimeClick(e))
            {
                e.Handled = true;
            }
            else if (this.ForwardDesignTimeInput)
            {
                this.OnMouseLeftButtonDown(e);
            }
        }

        public void DesignTimeMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                this.OnMouseLeftButtonUp(e);
            }
        }

        public void DesignTimeMouseMove(DXMouseEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                this.OnMouseMove(e);
            }
        }

        public void DesignTimeMouseRightButtonDown(DXMouseButtonEventArgs e)
        {
            if (this.ForwardDesignTimeInput)
            {
                this.OnMouseRightButtonDown(e);
            }
        }

        public virtual void DropElement(FrameworkElement element, LayoutItemInsertionPoint insertionPoint, LayoutItemInsertionKind insertionKind)
        {
            (insertionPoint.IsInternalInsertion ? ((ILayoutGroup) insertionPoint.Element) : ((ILayoutGroup) insertionPoint.Element.Parent)).InsertElement(element, insertionPoint, insertionKind);
            base.ILayoutGroup.OptimizeLayout(true);
            base.Control.InvalidateMeasure();
            if (!element.IsInVisualTree())
            {
                element = null;
            }
            if (this.IsCustomization)
            {
                this.CustomizationController.OnDropElement(element);
            }
            this.OnModelChanged(new LayoutControlModelStructureChangedEventArgs("Drag & Drop", element));
        }

        protected override void EndDragAndDrop(bool accept)
        {
            base.EndDragAndDrop(accept);
            if (this.IsCustomization)
            {
                this.CustomizationController.OnEndDragAndDrop(accept);
            }
        }

        public LayoutItemInsertionInfo GetInsertionInfo(FrameworkElement parentedElement, Point p)
        {
            FrameworkElement destination = this.GetItem(p, false, base.Control.IsInDesignTool());
            if (destination == null)
            {
                return new LayoutItemInsertionInfo();
            }
            ILayoutGroup group = destination as ILayoutGroup;
            ILayoutGroup parent = destination.Parent as ILayoutGroup;
            p = base.Control.MapPoint(p, destination);
            LayoutItemInsertionKind insertionKind = !ReferenceEquals(destination, base.Control) ? parent.GetInsertionKind(destination, p) : LayoutItemInsertionKind.None;
            return ((ReferenceEquals(destination, base.Control) || (destination.IsLayoutGroup() && (insertionKind == LayoutItemInsertionKind.None))) ? group.GetInsertionInfoForEmptyArea(parentedElement, p) : new LayoutItemInsertionInfo(destination, insertionKind, parent.GetInsertionPoint(parentedElement, destination, insertionKind, p)));
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__1))]
        public override IEnumerable<UIElement> GetInternalElements()
        {
            IEnumerator<UIElement> enumerator = this.<>n__0().GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                UIElement current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                if (this.CustomizationController != null)
                {
                    enumerator = this.CustomizationController.GetInternalElements().GetEnumerator();
                }
            }
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    UIElement current = enumerator.Current;
                    yield return current;
                }
                else
                {
                    enumerator = null;
                }
            }
        }

        public override FrameworkElement GetItem(Point p, bool ignoreLayoutGroups, bool ignoreLocking) => 
            (ignoreLocking || !base.ILayoutGroup.IsLocked) ? base.GetItem(p, ignoreLayoutGroups, ignoreLocking) : null;

        public override bool IsScrollable() => 
            true;

        public override void OnArrange(Size finalSize)
        {
            base.OnArrange(finalSize);
            if (this.IsCustomization)
            {
                this.CustomizationController.OnArrange(finalSize);
            }
        }

        protected internal virtual void OnControlVisibilityChanged(FrameworkElement control)
        {
            if (this.IsCustomization)
            {
                this.CustomizationController.OnControlVisibilityChanged(control);
            }
        }

        protected virtual void OnIsCustomizationChanged()
        {
            if (this.IsCustomizationChanged != null)
            {
                this.IsCustomizationChanged(base.Control, EventArgs.Empty);
            }
        }

        protected override void OnKeyDown(DXKeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!base.Control.IsInDesignTool() && ((e.Key == Key.F2) && (Keyboard2.IsControlPressed && Keyboard2.IsShiftPressed)))
            {
                this.IsCustomization = !this.IsCustomization;
                e.Handled = true;
            }
            else if (this.IsCustomization)
            {
                this.CustomizationController.OnKeyDown(e);
            }
        }

        protected override void OnLayoutUpdated()
        {
            base.OnLayoutUpdated();
            if (this.IsCustomization && base.Control.IsInVisualTree())
            {
                this.CustomizationController.OnLayoutUpdated();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.IsCustomization)
            {
                this.CustomizationController.OnLoaded();
            }
        }

        public override void OnMeasure(Size availableSize)
        {
            base.OnMeasure(availableSize);
            if (this.IsCustomization)
            {
                this.CustomizationController.OnMeasure(availableSize);
            }
        }

        protected internal virtual void OnModelChanged(LayoutControlModelChangedEventArgs args)
        {
            if (base.Control.IsInDesignTool() && this.IsCustomization)
            {
                LayoutControlModelPropertyChangedEventArgs args2 = args as LayoutControlModelPropertyChangedEventArgs;
                if ((args2 != null) && (ReferenceEquals(args2.Property, LayoutGroup.IsCollapsedProperty) && ((LayoutGroup) args2.Object).IsCollapsed))
                {
                    this.CustomizationController.OnGroupCollapsed((ILayoutGroup) args2.Object);
                }
            }
            if (this.ModelChanged != null)
            {
                this.ModelChanged(base.Control, args);
            }
        }

        protected override void OnMouseLeftButtonDown(DXMouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (this.IsCustomization)
            {
                this.CustomizationController.OnMouseLeftButtonDown(e);
            }
        }

        protected override void OnMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            bool isMouseLeftButtonDown = base.IsMouseLeftButtonDown;
            base.OnMouseLeftButtonUp(e);
            if (((e != null) && this.IsCustomization) & isMouseLeftButtonDown)
            {
                this.CustomizationController.OnMouseLeftButtonUp(e);
            }
        }

        protected override void OnMouseMove(DXMouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.IsCustomization)
            {
                this.CustomizationController.OnMouseMove(e);
            }
        }

        protected virtual void OnMouseRightButtonDown(DXMouseButtonEventArgs e)
        {
            if (this.IsCustomization)
            {
                this.CustomizationController.OnMouseRightButtonDown(e);
            }
        }

        protected internal virtual void OnTabClicked(ILayoutGroup group, FrameworkElement selectedTabChild)
        {
            if (this.IsCustomization)
            {
                this.CustomizationController.OnTabClicked(group, selectedTabChild);
            }
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();
            this.IsCustomization = false;
        }

        protected override void StartDragAndDrop(Point p)
        {
            if (this.IsCustomization)
            {
                this.CustomizationController.OnStartDragAndDrop();
            }
            base.StartDragAndDrop(p);
        }

        public void StartItemDragAndDrop(FrameworkElement item, MouseEventArgs mouseEventArgs, FrameworkElement source)
        {
            DragAndDropController controller;
            if (base.WantsItemDragAndDrop(new Point(0.0, 0.0), p => item, out controller))
            {
                Point position = mouseEventArgs.GetPosition(source);
                ((LayoutItemDragAndDropController) controller).StartDragRelativePoint = new Point(position.X / source.ActualWidth, position.Y / source.ActualHeight);
                base.DragAndDropController = controller;
                this.StartDragAndDrop(mouseEventArgs.GetPosition(base.Control));
            }
        }

        public DevExpress.Xpf.LayoutControl.ILayoutControl ILayoutControl =>
            base.ILayoutControl as DevExpress.Xpf.LayoutControl.ILayoutControl;

        protected override bool NeedsUnloadedEvent =>
            this.IsCustomization;

        public LayoutControlCustomizationController CustomizationController { get; private set; }

        public bool IsCustomization
        {
            get => 
                this._IsCustomization;
            set
            {
                if (this.IsCustomization != value)
                {
                    this._IsCustomization = value;
                    if (this.IsCustomization)
                    {
                        this.CustomizationController = this.CreateCustomizationController();
                        this.CustomizationController.BeginCustomization();
                        this.ILayoutControl.InitCustomizationController();
                    }
                    this.ILayoutControl.IsCustomization = this.IsCustomization;
                    this.OnIsCustomizationChanged();
                    if (!this.IsCustomization)
                    {
                        this.CustomizationController.EndCustomization();
                        this.CustomizationController = null;
                    }
                }
            }
        }

        public Style ItemInsertionPointIndicatorStyle { get; set; }

        protected virtual bool ForwardDesignTimeInput =>
            true;

    }
}

