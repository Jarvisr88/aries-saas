namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.HitTest;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public abstract class HeaderDropTargetBase : IDropTarget
    {
        private Popup dragAdorner;

        public HeaderDropTargetBase(System.Windows.Controls.Panel panel);
        protected abstract bool CanDrop(UIElement source, int dropIndex);
        protected virtual bool CanDropCore(UIElement source, Point pt, out object dragAnchor, bool isDrag);
        protected virtual Point CorrectDragIndicatorLocation(Point point);
        private void CreateDragIndicatorAdorner(UIElement sourceElement);
        protected abstract HeaderDropTargetBase.HeaderHitTestResult CreateHitTestResult();
        protected virtual void DestroyDragAdorner();
        void IDropTarget.Drop(UIElement source, Point pt);
        void IDropTarget.OnDragLeave();
        void IDropTarget.OnDragOver(UIElement source, Point pt);
        private void DragIndicator_SizeChanged(object sender, SizeChangedEventArgs e);
        protected int GetChildHeaderIndexByPoint(Point pt, bool useVisibleIndexCorrection, out bool isFarCorner);
        protected FrameworkElement GetColumnHeaderButton(FrameworkElement parentElement);
        protected HeaderDropTargetBase.HeaderHitTestResult GetColumnHeaderHitTestResult(Point pt);
        private int GetColumnVisibleIndexFromDragSource(int oldVisibleIndex, int newVisibleIndex, bool isFarCorner, bool sameSource);
        protected virtual int GetDragIndex(int dropIndex, Point pt);
        protected virtual object GetDragIndicatorDataContext(UIElement sourceElement);
        protected virtual int GetDropIndexFromDragSource(UIElement element, Point pt);
        protected abstract Orientation GetDropPlaceOrientation(DependencyObject element);
        protected virtual object GetHeaderButtonOwner(int index);
        protected virtual bool GetIsFarCorner(HeaderDropTargetBase.HeaderHitTestResult result, Point point);
        protected Point GetLocationDragIndicator(int headerIndex, bool rightTop = false);
        protected abstract int GetRelativeVisibleIndex(UIElement element);
        protected abstract int GetVisibleIndex(DependencyObject obj, bool useVisibleIndexCorrection);
        protected abstract bool IsSameSource(UIElement element);
        protected bool IsVisible(DependencyObject dObj);
        protected abstract void MoveColumnTo(UIElement source, int dropIndex);
        protected virtual void MoveColumnToCore(UIElement source, object dropAnchor);
        private void Panel_Unloaded(object sender, RoutedEventArgs e);
        protected abstract void SetColumnHeaderDragIndicatorSize(DependencyObject element, double value);
        protected abstract void SetDropPlaceOrientation(DependencyObject element, Orientation value);
        protected virtual void UpdateDragAdornerLocation(int headerIndex);
        protected virtual void UpdateDragAdornerLocationCore(UIElement sourceElement, object headerAnchor);
        protected virtual void UpdateIndicator(DependencyObject element, UIElement uiElement);
        protected void UpdatePopupLocation(Point location);

        public Popup DragAdorner { get; }

        public System.Windows.Controls.Panel Panel { get; private set; }

        protected virtual IList Children { get; }

        protected abstract FrameworkElement GridElement { get; }

        protected abstract FrameworkElement DragIndicatorTemplateSource { get; }

        protected abstract string DragIndicatorTemplatePropertyName { get; }

        protected abstract string HeaderButtonTemplateName { get; }

        protected abstract int DropIndexCorrection { get; }

        protected UIElement TopContainer { get; }

        protected virtual int ChildrenCount { get; }

        protected UIElement DragIndicator { get; }

        protected virtual UIElement AdornableElement { get; }

        protected abstract class HeaderHitTestResult
        {
            public HeaderHitTestResult();
            protected abstract DevExpress.Xpf.Core.DropPlace GetDropPlace(DependencyObject visualHit);
            protected abstract DependencyObject GetGridColumn(DependencyObject visualHit);
            public HitTestResultBehavior HitTestCallBack(HitTestResult result);

            public DependencyObject HeaderElement { get; private set; }

            public DevExpress.Xpf.Core.DropPlace DropPlace { get; private set; }
        }
    }
}

