namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public static class LayoutHelper
    {
        public static Size ArrangeElementWithSingleChild(FrameworkElement element, Size arrangeSize);
        public static Size ArrangeElementWithSingleChild(FrameworkElement element, Size arrangeSize, Point position);
        private static bool CheckIsDesignTimeRoot(DependencyObject d);
        public static T FindAmongParents<T>(DependencyObject o, DependencyObject stopObject) where T: DependencyObject;
        public static FrameworkElement FindElement(FrameworkElement treeRoot, Predicate<FrameworkElement> predicate);
        public static FrameworkElement FindElementByName(FrameworkElement treeRoot, string name);
        public static T FindElementByType<T>(FrameworkElement treeRoot) where T: FrameworkElement;
        public static FrameworkElement FindElementByType(FrameworkElement treeRoot, Type type);
        internal static DependencyObject FindElementCore(DependencyObject treeRoot, Predicate<DependencyObject> predicate);
        public static T FindLayoutOrVisualParentObject<T>(DependencyObject child, bool useLogicalTree = false, DependencyObject stopSearchNode = null) where T: class;
        public static DependencyObject FindLayoutOrVisualParentObject(DependencyObject child, Predicate<DependencyObject> predicate, bool useLogicalTree = false, DependencyObject stopSearchNode = null);
        public static DependencyObject FindLayoutOrVisualParentObject(DependencyObject child, Type parentType, bool useLogicalTree = false, DependencyObject stopSearchNode = null);
        private static DependencyObject FindLayoutOrVisualParentObjectCore(DependencyObject child, Predicate<DependencyObject> predicate, bool useLogicalTree, DependencyObject stopSearchNode = null);
        public static T FindParentObject<T>(DependencyObject child) where T: class;
        public static DependencyObject FindRoot(DependencyObject d, bool useLogicalTree = false);
        public static void ForEachElement(FrameworkElement treeRoot, LayoutHelper.ElementHandler elementHandler);
        public static DependencyObject GetParent(DependencyObject d, bool useLogicalTree = false);
        private static DependencyObject GetParentCore(DependencyObject d, bool useLogicalTree = false);
        public static Rect GetRelativeElementRect(UIElement element, UIElement parent);
        public static FrameworkElement GetRoot(FrameworkElement element);
        [IteratorStateMachine(typeof(LayoutHelper.<GetRootPath>d__18))]
        public static IEnumerable GetRootPath(DependencyObject root, DependencyObject element);
        public static Rect GetScreenRect(FrameworkElement element);
        private static Rect GetScreenRectCore(Window window, FrameworkElement element);
        private static Rect GetStandardWindowRect(Window window);
        public static UIElement GetTopContainerWithAdornerLayer(UIElement element);
        public static FrameworkElement GetTopLevelVisual(DependencyObject d);
        public static UIElement HitTest(UIElement element, Point point);
        public static bool IsChildElement(DependencyObject root, DependencyObject element);
        public static bool IsChildElement(DependencyObject root, DependencyObject element, bool useLogicalTree);
        public static bool IsChildElementEx(DependencyObject root, DependencyObject element, bool useLogicalTree = false);
        public static bool IsElementLoaded(FrameworkElement element);
        private static bool IsElementLoadedCore(FrameworkElement element);
        public static bool IsPointInsideElementBounds(Point position, FrameworkElement element, Thickness margin);
        public static bool IsVisibleInTree(UIElement element, bool visualTreeOnly = false);
        private static bool IsVisibleInTreeCore(UIElement element, bool visualTreeOnly = false);
        public static Size MeasureElementWithSingleChild(FrameworkElement element, Size constraint);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutHelper.<>c <>9;
            public static Predicate<FrameworkElement> <>9__0_0;
            public static HitTestFilterCallback <>9__8_0;

            static <>c();
            internal bool <GetTopContainerWithAdornerLayer>b__0_0(FrameworkElement e);
            internal HitTestFilterBehavior <HitTest>b__8_0(DependencyObject e);
        }

        [CompilerGenerated]
        private sealed class <GetRootPath>d__18 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private DependencyObject element;
            public DependencyObject <>3__element;
            private DependencyObject <parent>5__1;
            private DependencyObject root;
            public DependencyObject <>3__root;

            [DebuggerHidden]
            public <GetRootPath>d__18(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        public delegate void ElementHandler(FrameworkElement e);
    }
}

