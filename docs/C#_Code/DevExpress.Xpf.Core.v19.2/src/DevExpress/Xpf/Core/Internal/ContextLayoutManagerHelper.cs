namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    public static class ContextLayoutManagerHelper
    {
        private static readonly object lObject = new object();
        private static volatile Type clmType;
        private static Type lelType;
        private static Func<object, object> clmGetter;
        private static Action<object> clmUpdateLayout;
        private static Func<object, object, object> lelAdd;
        private static Action<object, object> lelRemove;
        private static Func<object, object> clmGetLel;
        internal static readonly Action<Visual, Visual> PropagateResumeLayout;
        internal static readonly Action<Visual> PropagateSuspendLayout;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutUpdatedListItemsProperty = DependencyPropertyManager.RegisterAttached("LayoutUpdatedListItems", typeof(Hashtable), typeof(ContextLayoutManagerHelper), new FrameworkPropertyMetadata(null));

        static ContextLayoutManagerHelper()
        {
            int? parametersCount = null;
            PropagateSuspendLayout = ReflectionHelper.CreateInstanceMethodHandler<Action<Visual>>(null, "PropagateSuspendLayout", BindingFlags.NonPublic | BindingFlags.Static, typeof(UIElement), parametersCount, null, true);
            parametersCount = null;
            PropagateResumeLayout = ReflectionHelper.CreateInstanceMethodHandler<Action<Visual, Visual>>(null, "PropagateResumeLayout", BindingFlags.NonPublic | BindingFlags.Static, typeof(UIElement), parametersCount, null, true);
        }

        public static void AddLayoutUpdatedHandler(EventHandler handler)
        {
            object contextLayoutManager = ContextLayoutManager;
            if (contextLayoutManager != null)
            {
                object obj3 = clmGetLel(contextLayoutManager);
                lelAdd(obj3, handler);
            }
        }

        public static void AddLayoutUpdatedHandler(DependencyObject dObj, EventHandler handler)
        {
            if (dObj is UIElement)
            {
                ((UIElement) dObj).LayoutUpdated += handler;
            }
            else
            {
                object contextLayoutManager = ContextLayoutManager;
                if (contextLayoutManager != null)
                {
                    object obj3 = clmGetLel(contextLayoutManager);
                    object obj4 = lelAdd(obj3, handler);
                    Hashtable layoutUpdatedListItems = GetLayoutUpdatedListItems(dObj);
                    if (layoutUpdatedListItems == null)
                    {
                        layoutUpdatedListItems = new Hashtable(2);
                        SetLayoutUpdatedListItems(dObj, layoutUpdatedListItems);
                    }
                    layoutUpdatedListItems.Add(handler, obj4);
                }
            }
        }

        private static object GetLayoutManagerInstance()
        {
            if (clmType == null)
            {
                object lObject = ContextLayoutManagerHelper.lObject;
                lock (lObject)
                {
                    if (clmType == null)
                    {
                        Type instanceType = typeof(UIElement).Assembly.GetType("System.Windows.ContextLayoutManager");
                        int? parametersCount = null;
                        clmGetter = ReflectionHelper.CreateInstanceMethodHandler<Func<object, object>>(null, "From", BindingFlags.NonPublic | BindingFlags.Static, instanceType, parametersCount, null, true);
                        parametersCount = null;
                        clmUpdateLayout = ReflectionHelper.CreateInstanceMethodHandler<Action<object>>(null, "UpdateLayout", BindingFlags.NonPublic | BindingFlags.Instance, instanceType, parametersCount, null, true);
                        parametersCount = null;
                        clmGetLel = ReflectionHelper.CreateInstanceMethodHandler<Func<object, object>>(null, "get_LayoutEvents", BindingFlags.NonPublic | BindingFlags.Instance, instanceType, parametersCount, null, true);
                        lelType = typeof(UIElement).Assembly.GetType("System.Windows.LayoutEventList");
                        parametersCount = null;
                        lelAdd = ReflectionHelper.CreateInstanceMethodHandler<Func<object, object, object>>(null, "Add", BindingFlags.NonPublic | BindingFlags.Instance, lelType, parametersCount, null, true);
                        parametersCount = null;
                        lelRemove = ReflectionHelper.CreateInstanceMethodHandler<Action<object, object>>(null, "Remove", BindingFlags.NonPublic | BindingFlags.Instance, lelType, parametersCount, null, true);
                        clmType = instanceType;
                    }
                }
            }
            return clmGetter(Dispatcher.CurrentDispatcher);
        }

        private static Hashtable GetLayoutUpdatedListItems(DependencyObject obj) => 
            (Hashtable) obj.GetValue(LayoutUpdatedListItemsProperty);

        public static void RemoveLayoutUpdatedHandler(DependencyObject dObj, EventHandler handler)
        {
            if (dObj is UIElement)
            {
                ((UIElement) dObj).LayoutUpdated -= handler;
            }
            else
            {
                Hashtable layoutUpdatedListItems = GetLayoutUpdatedListItems(dObj);
                if (layoutUpdatedListItems != null)
                {
                    object contextLayoutManager = ContextLayoutManager;
                    if (contextLayoutManager != null)
                    {
                        object obj3 = clmGetLel(contextLayoutManager);
                        object obj4 = layoutUpdatedListItems[handler];
                        if (obj4 != null)
                        {
                            layoutUpdatedListItems.Remove(handler);
                            lelRemove(obj3, obj4);
                        }
                    }
                }
            }
        }

        private static void SetLayoutUpdatedListItems(DependencyObject obj, Hashtable value)
        {
            obj.SetValue(LayoutUpdatedListItemsProperty, value);
        }

        public static IDisposable SuspendLayout(UIElement v) => 
            new SuspendLayoutData(v);

        public static void UpdateLayout()
        {
            object contextLayoutManager = ContextLayoutManager;
            if (contextLayoutManager != null)
            {
                clmUpdateLayout(contextLayoutManager);
            }
        }

        private static object ContextLayoutManager =>
            GetLayoutManagerInstance();

        private class SuspendLayoutData : IDisposable
        {
            private readonly UIElement visual;
            private static Func<UIElement, object> get_ArrangeRequest = ReflectionHelper.CreateFieldGetter<UIElement, object>(typeof(UIElement), "ArrangeRequest", BindingFlags.NonPublic | BindingFlags.Instance);
            private static Func<UIElement, object> get_MeasureRequest = ReflectionHelper.CreateFieldGetter<UIElement, object>(typeof(UIElement), "MeasureRequest", BindingFlags.NonPublic | BindingFlags.Instance);

            public SuspendLayoutData(UIElement visual)
            {
                this.visual = visual;
                ContextLayoutManagerHelper.PropagateSuspendLayout(visual);
            }

            public void Dispose()
            {
                UIElement parent = VisualTreeHelper.GetParent(this.visual) as UIElement;
                ContextLayoutManagerHelper.PropagateResumeLayout(parent, this.visual);
                if (parent != null)
                {
                    bool flag = UIElementFlagsHelper.ReadFlag(this.visual, UIElementFlags.NeverMeasured) && (get_MeasureRequest(this.visual) == null);
                    bool flag2 = UIElementFlagsHelper.ReadFlag(this.visual, UIElementFlags.NeverArranged) && (get_ArrangeRequest(this.visual) == null);
                    if (flag)
                    {
                        parent.InvalidateMeasure();
                    }
                    if (flag2)
                    {
                        parent.InvalidateArrange();
                    }
                }
                ContextLayoutManagerHelper.UpdateLayout();
            }
        }
    }
}

