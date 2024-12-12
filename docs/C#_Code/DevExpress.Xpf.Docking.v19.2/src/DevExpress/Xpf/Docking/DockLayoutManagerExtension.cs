namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.ThemeKeys;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public static class DockLayoutManagerExtension
    {
        public static void AddToLogicalTree(this DockLayoutManager manager, FloatingContainer container, object content)
        {
            if (manager != null)
            {
                DockLayoutManager.AddLogicalChild(manager, container);
                DockLayoutManager.AddLogicalChild(manager, content as FrameworkElement);
                if (content is IControlHost)
                {
                    FrameworkElement[] children = ((IControlHost) content).GetChildren();
                    for (int i = 0; i < children.Length; i++)
                    {
                        if (children[i] != null)
                        {
                            DockLayoutManager.AddLogicalChild(manager, children[i]);
                        }
                    }
                }
            }
        }

        public static void BeginUpdate(this DockLayoutManager manager)
        {
            Action<ISupportBatchUpdate> action = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Action<ISupportBatchUpdate> local1 = <>c.<>9__0_0;
                action = <>c.<>9__0_0 = x => x.BeginUpdate();
            }
            manager.Do<ISupportBatchUpdate>(action);
        }

        public static LayoutElementHitInfo CalcHitInfo(this DockLayoutManager container, Point point)
        {
            if (container == null)
            {
                return LayoutElementHitInfo.Empty;
            }
            LayoutElementHitInfo empty = LayoutElementHitInfo.Empty;
            IView host = container.ViewAdapter.GetView(point);
            return ((host == null) ? empty : host.Adapter.CalcHitInfo(host, host.ScreenToClient(point)));
        }

        internal static bool CanExecuteContextAction(this DockLayoutManager container, ContextAction action, BaseLayoutItem context)
        {
            if (context == null)
            {
                return false;
            }
            DockLayoutManager actualViewOwner = container.GetActualViewOwner(context);
            IView view = actualViewOwner.GetView(context.GetRoot());
            return ((view != null) ? actualViewOwner.ViewAdapter.ContextActionService.CanExecute(view, GetViewElementFromUIElements(actualViewOwner, context) ?? GetViewElementFromUIElements(actualViewOwner, context.GetRoot()), action) : false);
        }

        internal static Rect CorrectBoundsInAdorner(this DockLayoutManager container, Rect bounds) => 
            GeometryHelper.CorrectBounds(bounds, CoordinateHelper.GetAvailableAdornerRect(container, bounds), WindowHelper.GetActualThreshold(container));

        internal static void Deactivate(this DockLayoutManager container, BaseLayoutItem item)
        {
            if (!container.IsActivationLocked)
            {
                if (ReferenceEquals(container.ActiveDockItem, item))
                {
                    container.ActiveDockItem = null;
                }
                if (ReferenceEquals(container.ActiveMDIItem, item))
                {
                    container.ActiveMDIItem = null;
                }
                if (ReferenceEquals(container.ActiveLayoutItem, item))
                {
                    container.ActiveLayoutItem = null;
                }
            }
        }

        public static void EndUpdate(this DockLayoutManager manager)
        {
            Action<ISupportBatchUpdate> action = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Action<ISupportBatchUpdate> local1 = <>c.<>9__1_0;
                action = <>c.<>9__1_0 = x => x.EndUpdate();
            }
            manager.Do<ISupportBatchUpdate>(action);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static void EnsureCustomizationRoot(this DockLayoutManager manager)
        {
            manager.CustomizationController.CustomizationRoot ??= manager.LayoutRoot;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static void EnsureCustomizationRoot(this DockLayoutManager manager, LayoutGroup root)
        {
            manager.CustomizationController.CustomizationRoot = root;
        }

        internal static void ExecuteContextAction(this DockLayoutManager container, ContextAction action, BaseLayoutItem context)
        {
            if (context != null)
            {
                DockLayoutManager actualViewOwner = container.GetActualViewOwner(context);
                IView view = actualViewOwner.GetView(context.GetRoot());
                actualViewOwner.ViewAdapter.ContextActionService.Execute(view, GetViewElementFromUIElements(actualViewOwner, context) ?? GetViewElementFromUIElements(actualViewOwner, context.GetRoot()), action);
            }
        }

        public static T FindName<T>(this DockLayoutManager manager, string name) where T: class => 
            LayoutHelper.FindElementByName(manager, name) as T;

        internal static IUIElement FindUIScope(this DependencyObject dObj)
        {
            switch (dObj)
            {
                case (DockLayoutManager _):
                    break;

                case (AutoHideTray _):
                    return ((IUIElement) dObj).Scope;
                    break;

                case (FloatPanePresenter _):
                    return ((IUIElement) dObj).Scope;
                    break;
            }
            return null;
        }

        internal static DockLayoutManager GetActualViewOwner(this DockLayoutManager container, BaseLayoutItem item)
        {
            LayoutGroup root = item.GetRoot();
            if (root != null)
            {
                IView view = container.GetView(root);
                if (view == null)
                {
                    foreach (DockLayoutManager manager in container.Linked)
                    {
                        view = manager.GetView(root);
                        if (view != null)
                        {
                            container = manager;
                            break;
                        }
                    }
                }
            }
            return container;
        }

        internal static IEnumerable<Window> GetAffectedWindows(this DockLayoutManager manager) => 
            manager.GetApplicationWindows() ?? manager.GetOwnedWindows();

        internal static List<Window> GetAllAffectedWindows(this DockLayoutManager manager)
        {
            List<Window> applicationWindows = manager.GetApplicationWindows();
            return ((applicationWindows == null) ? manager.GetOwnedWindows().Concat<Window>(manager.Linked.SelectMany<DockLayoutManager, Window>(new Func<DockLayoutManager, IEnumerable<Window>>(DockLayoutManagerExtension.GetOwnedWindows))).Distinct<Window>().ToList<Window>() : applicationWindows);
        }

        private static List<Window> GetApplicationWindows(this DockLayoutManager manager)
        {
            Func<Application, bool> func1 = <>c.<>9__59_0;
            if (<>c.<>9__59_0 == null)
            {
                Func<Application, bool> local1 = <>c.<>9__59_0;
                func1 = <>c.<>9__59_0 = x => x.Dispatcher.CheckAccess();
            }
            return (((Application) func1).Return<Application, bool>(((Func<Application, bool>) (<>c.<>9__59_1 ??= () => false)), (<>c.<>9__59_1 ??= () => false)) ? Application.Current.Windows.OfType<Window>().ToList<Window>() : null);
        }

        internal static Rect GetAutoHideResizeBounds(this DockLayoutManager container)
        {
            FrameworkElement autoHideLayer = container.AutoHideLayer;
            if ((autoHideLayer == null) || ((autoHideLayer.ActualWidth == 0.0) || (autoHideLayer.ActualHeight == 0.0)))
            {
                return Rect.Empty;
            }
            if (container.AutoHideMode == AutoHideMode.Inline)
            {
                Point elementPoint = new Point();
                Point location = CoordinateHelper.PointToScreen(container, autoHideLayer, elementPoint);
                return new Rect(location, new Size(autoHideLayer.ActualWidth, autoHideLayer.ActualHeight));
            }
            IView host = container.GetView(container.LayoutRoot);
            if (host == null)
            {
                return Rect.Empty;
            }
            if (host.LayoutRoot == null)
            {
                host.EnsureLayoutRoot();
            }
            if ((host.LayoutRoot.IsReady && (host.LayoutRoot.Size.Height == 0.0)) || (host.LayoutRoot.Size.Width == 0.0))
            {
                host.LayoutRoot.Invalidate();
            }
            host.LayoutRoot.EnsureBounds();
            Rect screenRect = ElementHelper.GetScreenRect(host);
            RectHelper.Inflate(ref screenRect, (double) -25.0, -12.5);
            return screenRect;
        }

        internal static double GetAvailableAutoHideSize(this DockLayoutManager container, bool horz)
        {
            Rect autoHideResizeBounds = container.GetAutoHideResizeBounds();
            return (autoHideResizeBounds.IsEmpty ? double.NaN : (horz ? autoHideResizeBounds.Width : autoHideResizeBounds.Height));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ICustomizationController GetCustomizationController(this DockLayoutManager container) => 
            container.CustomizationController;

        internal static Point GetDragOffset(this DockLayoutManager manager)
        {
            FloatPaneElementsThemeKeyExtension key = new FloatPaneElementsThemeKeyExtension();
            key.ResourceKey = FloatPaneElements.WindowDraggingOffset;
            key.ThemeName = GetThemeName(manager);
            object obj2 = manager.FindResource(key);
            if (obj2 != null)
            {
                return (Point) obj2;
            }
            return new Point();
        }

        internal static Comparison<FloatGroup> GetFloatGroupComparison(this DockLayoutManager container) => 
            delegate (FloatGroup g1, FloatGroup g2) {
                if (ReferenceEquals(g1, g2))
                {
                    return 0;
                }
                IView view = container.GetView(g1.UIElements.GetElement<FloatPanePresenter>());
                IView view2 = container.GetView(g2.UIElements.GetElement<FloatPanePresenter>());
                return ((view == null) || (view2 == null)) ? 0 : view2.ZOrder.CompareTo(view.ZOrder);
            };

        public static BaseLayoutItem GetItem(this DockLayoutManager container, string name) => 
            BaseLayoutItemCollection.FindItem(container.GetItems(), name);

        public static BaseLayoutItem[] GetItems(this DockLayoutManager container)
        {
            if (container == null)
            {
                return new BaseLayoutItem[0];
            }
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            if (container.LayoutRoot != null)
            {
                container.LayoutRoot.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
            }
            if (container.FloatGroups != null)
            {
                container.FloatGroups.Accept<FloatGroup>(delegate (FloatGroup fGroup) {
                    fGroup.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
                });
            }
            if (container.AutoHideGroups != null)
            {
                container.AutoHideGroups.Accept<AutoHideGroup>(delegate (AutoHideGroup ahGroup) {
                    ahGroup.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
                });
            }
            if (container.ClosedPanels != null)
            {
                container.ClosedPanels.Accept<LayoutPanel>(new VisitDelegate<LayoutPanel>(items.Add));
            }
            if (container.DecomposedItems != null)
            {
                container.DecomposedItems.Accept<LayoutGroup>(new VisitDelegate<LayoutGroup>(items.Add));
            }
            if (container.HiddenItems != null)
            {
                container.HiddenItems.Accept<BaseLayoutItem>(delegate (BaseLayoutItem hiddenItem) {
                    hiddenItem.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
                });
            }
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            foreach (BaseLayoutItem item in items)
            {
                list.Add(item);
                LayoutPanel panel = item as LayoutPanel;
                if ((panel != null) && (panel.Layout != null))
                {
                    panel.Layout.Accept(new VisitDelegate<BaseLayoutItem>(list.Add));
                }
            }
            return list.ToArray();
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static UIElement GetLastUIElement(this DockLayoutManager container, BaseLayoutItem item)
        {
            if (item == null)
            {
                return null;
            }
            IUIElement[] elements = item.UIElements.GetElements();
            return (((elements == null) || (elements.Length == 0)) ? null : (elements[elements.Length - 1] as UIElement));
        }

        private static DockLayoutManager GetManager(this LayoutGroup group)
        {
            while (group != null)
            {
                if (group.Manager != null)
                {
                    return group.Manager;
                }
                group = group.Parent;
            }
            return null;
        }

        internal static IUIElement GetManager(this IUIElement element)
        {
            while ((element != null) && !(element is DockLayoutManager))
            {
                element = (element.Scope != null) ? element.Scope : (element as DependencyObject).GetParentIUIElement();
            }
            return element;
        }

        private static List<Window> GetOwnedWindows(this DockLayoutManager manager)
        {
            List<Window> windows = new List<Window>();
            if ((manager != null) && manager.Dispatcher.CheckAccess())
            {
                (LayoutHelper.FindRoot(manager, false) as Window).Do<Window>(delegate (Window x) {
                    windows.Add(x);
                });
                foreach (FloatGroup group in manager.FloatGroups)
                {
                    Action<FloatingPaneWindow> <>9__2;
                    Func<FloatGroup, FloatingPaneWindow> evaluator = <>c.<>9__60_1;
                    if (<>c.<>9__60_1 == null)
                    {
                        Func<FloatGroup, FloatingPaneWindow> local1 = <>c.<>9__60_1;
                        evaluator = <>c.<>9__60_1 = x => x.Window;
                    }
                    Action<FloatingPaneWindow> action = <>9__2;
                    if (<>9__2 == null)
                    {
                        Action<FloatingPaneWindow> local2 = <>9__2;
                        action = <>9__2 = delegate (FloatingPaneWindow x) {
                            windows.Add(x);
                        };
                    }
                    group.With<FloatGroup, FloatingPaneWindow>(evaluator).Do<FloatingPaneWindow>(action);
                }
            }
            return windows;
        }

        internal static IUIElement GetParentIUIElement(this DependencyObject dObj)
        {
            if (dObj != null)
            {
                do
                {
                    dObj = GetVisualParent(dObj);
                    if (dObj is IUIElement)
                    {
                        return (dObj as IUIElement);
                    }
                }
                while (dObj != null);
            }
            return null;
        }

        internal static IUIElement GetRootUIScope(this IUIElement element)
        {
            while ((element != null) && !(element.Scope is DockLayoutManager))
            {
                element = element.Scope;
            }
            return element;
        }

        internal static string GetThemeName(DockLayoutManager manager) => 
            BarManagerHelper.GetThemeName(manager);

        public static UIElement GetUIElement(this DockLayoutManager container, BaseLayoutItem item) => 
            (item != null) ? (item.GetUIElement<IUIElement>() as UIElement) : null;

        public static T GetUIElement<T>(this DockLayoutManager container, BaseLayoutItem item) where T: class
        {
            if (item != null)
            {
                return item.GetUIElement<T>();
            }
            return default(T);
        }

        internal static IView GetView(this DockLayoutManager container, BaseLayoutItem item) => 
            container.GetView(item.GetRoot());

        public static IView GetView(this DockLayoutManager container, LayoutGroup group)
        {
            if (group == null)
            {
                return null;
            }
            IUIElement uIElement = group.GetUIElement<IUIElement>();
            return container.GetView(uIElement.GetRootUIScope());
        }

        public static IView GetView(this DockLayoutManager container, IUIElement element)
        {
            if ((element != null) && ((container != null) && (container.ViewAdapter != null)))
            {
                using (IEnumerator<IView> enumerator = container.ViewAdapter.Views.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        IView current = enumerator.Current;
                        if (current.RootKey == element)
                        {
                            return current;
                        }
                    }
                }
            }
            return null;
        }

        public static ILayoutElement GetViewElement(this DockLayoutManager container, IUIElement element) => 
            container.GetView(element.GetRootUIScope())?.GetElement(element);

        private static ILayoutElement GetViewElementFromUIElements(DockLayoutManager container, BaseLayoutItem context) => 
            (context != null) ? (container.GetViewElement(context) ?? container.GetViewElement(context.GetUIElement<IUIElement>(true))) : null;

        private static DependencyObject GetVisualOrLogicalParent(DependencyObject dObj)
        {
            DependencyObject obj2;
            DependencyObject parent = (dObj is Visual) ? VisualTreeHelper.GetParent(dObj) : null;
            if (obj2 == null)
            {
                DependencyObject local1 = (dObj is Visual) ? VisualTreeHelper.GetParent(dObj) : null;
                parent = LogicalTreeHelper.GetParent(dObj);
            }
            return parent;
        }

        private static DependencyObject GetVisualParent(DependencyObject child)
        {
            if (child is FloatPanePresenter.FloatingContentPresenter)
            {
                child = ((FloatPanePresenter.FloatingContentPresenter) child).Container;
            }
            return VisualTreeHelper.GetParent(child);
        }

        public static void HideView(this DockLayoutManager container, LayoutGroup group)
        {
            container.HideView(group, true);
        }

        public static void HideView(this DockLayoutManager container, LayoutGroup group, bool immediately)
        {
            if (group != null)
            {
                IUIElement element = group.GetUIElement<IUIElement>() ?? group;
                BaseLayoutItem[] items = new BaseLayoutItem[] { group };
                using (new LogicalTreeLocker(container, items))
                {
                    IView view = container.GetView(element.GetRootUIScope());
                    if ((view != null) && (view.Type == HostType.AutoHide))
                    {
                        container.ViewAdapter.ActionService.Hide(view, immediately);
                    }
                }
            }
        }

        public static void InvalidateView(this DockLayoutManager container, LayoutGroup group)
        {
            if (group != null)
            {
                IUIElement element = group.UIElements.GetElement<IUIElement>() ?? group;
                IView view = container.GetView(element.GetRootUIScope());
                if (view != null)
                {
                    view.Invalidate();
                }
            }
        }

        internal static bool IsInContainer(DependencyObject dObj) => 
            LayoutHelper.FindParentObject<DockLayoutManager>(dObj) != null;

        internal static bool IsInFloatingContainer(DependencyObject dObj) => 
            LayoutHelper.FindParentObject<FloatPanePresenter.FloatingContentPresenter>(dObj) != null;

        internal static bool IsViewCreated(this DockLayoutManager container, LayoutGroup group)
        {
            if (group == null)
            {
                return false;
            }
            IUIElement element = group.GetUIElement<IUIElement>() ?? group;
            return (container.GetView(element) != null);
        }

        internal static bool ProcessPanelNavigation(this DockLayoutManager manager, BaseLayoutItem activeItem, bool forward)
        {
            if ((activeItem != null) && !(activeItem is LayoutPanel))
            {
                activeItem = activeItem.GetRoot().ParentPanel;
            }
            if (activeItem != null)
            {
                ObservableCollection<BaseLayoutItem> observables = new ObservableCollection<BaseLayoutItem>();
                BaseLayoutItem[] items = manager.GetItems();
                int index = 0;
                while (true)
                {
                    if (index >= items.Length)
                    {
                        if (observables.Count <= 0)
                        {
                            break;
                        }
                        int num2 = observables.IndexOf(activeItem) + (forward ? 1 : -1);
                        if (num2 >= observables.Count)
                        {
                            num2 = 0;
                        }
                        if (num2 < 0)
                        {
                            num2 = observables.Count - 1;
                        }
                        manager.Activate(observables[num2]);
                        return true;
                    }
                    BaseLayoutItem item = items[index];
                    if (!item.IsClosed && ((item.ItemType == LayoutItemType.Document) || (item.ItemType == LayoutItemType.Panel)))
                    {
                        observables.Add(item);
                    }
                    index++;
                }
            }
            return false;
        }

        internal static bool RaiseBeforeItemAddedEvent(this DockLayoutManager container, object item, BaseLayoutItem target, out BaseLayoutItem actualTarget)
        {
            BeforeItemAddedEventArgs e = new BeforeItemAddedEventArgs(item, target);
            container.RaiseEvent(e);
            actualTarget = e.Cancel ? null : e.Target;
            return e.Cancel;
        }

        internal static bool RaiseDockItemDraggingEvent(this DockLayoutManager container, BaseLayoutItem item, Point screenLocation)
        {
            DockItemDraggingEventArgs e = new DockItemDraggingEventArgs(screenLocation, item);
            container.RaiseEvent(e);
            return e.Cancel;
        }

        internal static void RaiseDockOperationCompletedEvent(this DockLayoutManager container, DockOperation dockOperation, BaseLayoutItem item)
        {
            DockOperationCompletedEventArgs args1 = new DockOperationCompletedEventArgs(item, dockOperation);
            args1.Source = container;
            DockOperationCompletedEventArgs e = args1;
            container.RaiseEvent(e);
        }

        internal static bool RaiseDockOperationStartingEvent(this DockLayoutManager container, DockOperation dockOperation, BaseLayoutItem item, BaseLayoutItem target = null)
        {
            DockOperationStartingEventArgs args1 = new DockOperationStartingEventArgs(item, target, dockOperation);
            args1.Source = container;
            DockOperationStartingEventArgs e = args1;
            container.RaiseEvent(e);
            return e.Cancel;
        }

        internal static bool RaiseItemCancelEvent(this DockLayoutManager container, BaseLayoutItem item, RoutedEvent routedEvent)
        {
            ItemCancelEventArgs args1 = new ItemCancelEventArgs(item);
            args1.RoutedEvent = routedEvent;
            args1.Source = container;
            ItemCancelEventArgs e = args1;
            container.RaiseEvent(e);
            return e.Cancel;
        }

        internal static bool RaiseItemDockingEvent(this DockLayoutManager container, RoutedEvent routedEvent, BaseLayoutItem item, Point pt, BaseLayoutItem target, DockType type, bool isHiding)
        {
            DockItemDockingEventArgs args1 = new DockItemDockingEventArgs(item, pt, target, type, isHiding);
            args1.RoutedEvent = routedEvent;
            args1.Source = container;
            DockItemDockingEventArgs e = args1;
            container.RaiseEvent(e);
            return e.Cancel;
        }

        internal static void RaiseItemEvent(this DockLayoutManager container, BaseLayoutItem item, RoutedEvent routedEvent)
        {
            ItemEventArgs args1 = new ItemEventArgs(item);
            args1.RoutedEvent = routedEvent;
            args1.Source = container;
            ItemEventArgs e = args1;
            container.RaiseEvent(e);
        }

        internal static void RaiseItemSelectionChangedEvent(this DockLayoutManager container, BaseLayoutItem item, bool selected)
        {
            LayoutItemSelectionChangedEventArgs e = new LayoutItemSelectionChangedEventArgs(item, selected);
            e.Source = container;
            container.RaiseEvent(e);
        }

        internal static bool RaiseItemSelectionChangingEvent(this DockLayoutManager container, BaseLayoutItem item, bool selected)
        {
            LayoutItemSelectionChangingEventArgs args1 = new LayoutItemSelectionChangingEventArgs(item, selected);
            args1.Source = container;
            LayoutItemSelectionChangingEventArgs e = args1;
            container.RaiseEvent(e);
            return e.Cancel;
        }

        internal static void RaiseItemSizeChangedEvent(this DockLayoutManager container, BaseLayoutItem item, bool isWidth, GridLength value, GridLength prevValue)
        {
            LayoutItemSizeChangedEventArgs e = new LayoutItemSizeChangedEventArgs(item, isWidth, value, prevValue);
            e.Source = container;
            container.RaiseEvent(e);
        }

        internal static void RaiseShowingDockHintsEvent(this DockLayoutManager container, BaseLayoutItem item, BaseLayoutItem target, DockHintsConfiguration state = null)
        {
            ShowingDockHintsEventArgs args1 = new ShowingDockHintsEventArgs(item, target);
            DockHintsConfiguration configuration1 = state;
            if (state == null)
            {
                DockHintsConfiguration local1 = state;
                configuration1 = new DockHintsConfiguration();
            }
            args1.DockHintsConfiguration = configuration1;
            ShowingDockHintsEventArgs e = args1;
            container.RaiseEvent(e);
        }

        internal static bool RaiseShowingTabHintsEvent(this DockLayoutManager container, BaseLayoutItem item, BaseLayoutItem target, int insertIndex, Rect tab, Rect tabHeader)
        {
            LayoutGroup group = target as LayoutGroup;
            if ((group != null) && group.Items.IsValidIndex<BaseLayoutItem>(insertIndex))
            {
                target = group[insertIndex];
            }
            ShowingDockHintsEventArgs args1 = new ShowingDockHintsEventArgs(item, target);
            args1.DockHintsConfiguration = new DockHintsConfiguration();
            args1.Tab = tab;
            args1.TabHeader = tabHeader;
            args1.TabIndex = insertIndex;
            ShowingDockHintsEventArgs e = args1;
            container.RaiseEvent(e);
            return e.DockHintsConfiguration.GetIsVisible(DockHint.TabHeader);
        }

        internal static void RebuildLayoutAndUpdate(this LayoutGroup group, bool shouldUpdateLayout = true)
        {
            if ((group.ItemType == LayoutItemType.Group) && (group.GroupBorderStyle != GroupBorderStyle.Tabbed))
            {
                Action<GroupPanel> action = <>c.<>9__10_0;
                if (<>c.<>9__10_0 == null)
                {
                    Action<GroupPanel> local1 = <>c.<>9__10_0;
                    action = <>c.<>9__10_0 = x => x.RebuildLayout();
                }
                LayoutTreeHelper.GetVisualChildren(group).OfType<GroupPanel>().FirstOrDefault<GroupPanel>().Do<GroupPanel>(action);
            }
            group.Update(shouldUpdateLayout);
        }

        public static void RemoveFromLogicalTree(this DockLayoutManager manager, FloatingContainer container, object content)
        {
            if (manager != null)
            {
                DockLayoutManager.RemoveLogicalChild(manager, container);
                DockLayoutManager.RemoveLogicalChild(manager, content as FrameworkElement);
                if (content is IControlHost)
                {
                    FrameworkElement[] children = ((IControlHost) content).GetChildren();
                    for (int i = 0; i < children.Length; i++)
                    {
                        if (children[i] != null)
                        {
                            DockLayoutManager.RemoveLogicalChild(manager, children[i]);
                        }
                    }
                }
            }
        }

        public static void Update(this DockLayoutManager manager)
        {
            if (manager != null)
            {
                manager.Update();
            }
        }

        internal static void Update(this LayoutGroup group)
        {
            DockLayoutManager manager = group.GetManager();
            if ((manager != null) && !manager.IsDisposing)
            {
                manager.Update();
            }
        }

        internal static void Update(this LayoutGroup group, bool shouldUpdateLayout)
        {
            DockLayoutManager manager = group.GetManager();
            if ((manager != null) && !manager.IsDisposing)
            {
                manager.Update(shouldUpdateLayout);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockLayoutManagerExtension.<>c <>9 = new DockLayoutManagerExtension.<>c();
            public static Action<ISupportBatchUpdate> <>9__0_0;
            public static Action<ISupportBatchUpdate> <>9__1_0;
            public static Action<GroupPanel> <>9__10_0;
            public static Func<Application, bool> <>9__59_0;
            public static Func<bool> <>9__59_1;
            public static Func<FloatGroup, FloatingPaneWindow> <>9__60_1;

            internal void <BeginUpdate>b__0_0(ISupportBatchUpdate x)
            {
                x.BeginUpdate();
            }

            internal void <EndUpdate>b__1_0(ISupportBatchUpdate x)
            {
                x.EndUpdate();
            }

            internal bool <GetApplicationWindows>b__59_0(Application x) => 
                x.Dispatcher.CheckAccess();

            internal bool <GetApplicationWindows>b__59_1() => 
                false;

            internal FloatingPaneWindow <GetOwnedWindows>b__60_1(FloatGroup x) => 
                x.Window;

            internal void <RebuildLayoutAndUpdate>b__10_0(GroupPanel x)
            {
                x.RebuildLayout();
            }
        }
    }
}

