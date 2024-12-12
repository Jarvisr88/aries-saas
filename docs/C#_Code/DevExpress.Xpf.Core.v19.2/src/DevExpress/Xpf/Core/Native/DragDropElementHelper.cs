namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class DragDropElementHelper
    {
        private BaseLocationStrategy locationStrategy;
        protected bool IsMouseDown;
        private bool isDragging;
        private bool isRelative;
        private Point mouseDownPosition;
        private Point mouseDownPositionCorrection;
        private Point mousePositionToDragElement;
        private readonly BaseDragDropStrategy strategy;
        private UIElement keyboardEventSource;
        internal FrameworkElement topVisual;
        private IndependentMouseEventArgs startDragEventArgs;
        private Point startSourceElementLocation;
        private bool isProcessingOnPreviewMouseMove;
        private Point mouseLocationRelative;
        private Point lastPos;
        private Point sourceElementLocation;
        private Point startDragPoint;

        public DragDropElementHelper(ISupportDragDrop supportDragDrop, bool isRelativeMode = true);
        public virtual void CancelDragging();
        private void ChangeTouchEnabled(bool isPopupContainer);
        private void CollectIgnoredWindowsPointers(List<IntPtr> pointers);
        protected virtual Point CorrectDragElementLocation(Point newPos);
        protected virtual BaseDragDropStrategy CreateDragDropStrategy();
        protected virtual IDragElement CreateDragElement(Point offset);
        public static IDropTarget CreateDropTarget(UIElement dropTargetElement);
        private IDropTarget CreateDropTargetCore(UIElement dropTargetElement);
        protected virtual IDropTarget CreateEmptyDropTarget();
        private static IDropTarget CreateNotEmptyDropTarget(UIElement dropTargetElement, UIElement sourceElement);
        public virtual void Destroy();
        public void Drop(IDropTarget dropTarget, Point position);
        protected virtual void EndDragging(IndependentMouseButtonEventArgs e);
        private bool GetCanStartDrag(object sender, IndependentMouseButtonEventArgs e);
        protected virtual FrameworkElement GetDragSourceElement();
        private IDropTarget GetDropTarget(UIElement dropTargetElement);
        public UIElement GetDropTargetByHitElement(DependencyObject element);
        [SecuritySafeCritical]
        public DependencyObject GetHitTestFactoryElement(Func<UIElement, Point> positionProvider);
        protected Point GetPosition(IndependentMouseEventArgs e, FrameworkElement relativeTo);
        protected virtual Func<UIElement, Point> GetPositionProvider(IndependentMouseEventArgs e);
        private UIElement GetRelativeElement(UIElement dropTargetElement);
        protected virtual Point GetStartDraggingOffset(IndependentMouseEventArgs e, FrameworkElement sourceElement);
        protected virtual UIElement GetTopLevelDropContainer(UIElement relativeTo);
        protected virtual IEnumerable<UIElement> GetTopLevelDropContainers();
        protected virtual bool IsCompatibleDropTargetFactory(IDropTargetFactory factory, UIElement dropTargetElement);
        private bool IsDragGesture(Point point);
        protected virtual bool IsElementVisible(UIElement elem);
        private bool IsSkipHitTestVisibleCheck();
        private void OnApplicationDeactivated(object sender, EventArgs e);
        public void OnDragLeave(IDropTarget dropTarget);
        private void OnDragOver();
        public void OnDragOver(IDropTarget dropTarget, Point position);
        protected virtual void OnLostMouseCapture(object sender, MouseEventArgs e);
        public void OnMouseLeftButtonDownIndependent(object sender, IndependentMouseButtonEventArgs e);
        public void OnMouseLeftButtonUpIndependent(object sender, IndependentMouseButtonEventArgs e);
        public void OnMouseMoveIndependent(object sender, IndependentMouseEventArgs e);
        private void OnMouseMoveIndependentCore(object sender, IndependentMouseEventArgs e, bool isLeave = false);
        public void OnPreviewKeyDown(object sender, KeyEventArgs e);
        public void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e);
        public void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e);
        public void OnPreviewMouseMove(object sender, MouseEventArgs e);
        public void OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e);
        protected virtual void OnTimer(object sender, EventArgs e);
        private Point PointFromScreen(Visual visual, Point point);
        private Point PointToScreen(Visual visual, Point point);
        protected virtual void RemoveDragElement();
        protected internal virtual void StartDragging(Point offset, IndependentMouseEventArgs e);
        protected virtual void SubscribeEvent(FrameworkElement target, RoutedEvent routedEvent, Delegate handler);
        protected virtual void SubscribeEvents();
        protected virtual void UnsubscribeEvent(FrameworkElement target, RoutedEvent routedEvent, Delegate handler);
        protected virtual void UnsubscribeEvents();
        private void UpdateCurrentDropTarget(UIElement dropTargetElement);
        protected virtual void UpdateDragElementLocation(Point newPos);

        protected BaseDragDropStrategy Strategy { get; }

        public IDropTarget CurrentDropTarget { get; set; }

        protected UIElement CurrentDropTargetElement { get; set; }

        protected UIElement CurrentRelativeElement { get; set; }

        public bool IsDragging { get; }

        public IDragElement DragElement { get; set; }

        protected ISupportDragDrop SupportDragDrop { get; set; }

        public FrameworkElement SourceElement { get; set; }

        protected DispatcherTimer Timer { get; set; }

        protected System.Windows.Input.ModifierKeys ModifierKeys { get; set; }

        protected Point DragElementLocation { get; }

        private bool IsTouchEnabled { get; set; }

        protected internal FrameworkElement TopVisual { get; }

        protected Point MouseDownPositionCorrection { get; }
    }
}

