namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.ThemeKeys;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public static class LayoutItemsHelper
    {
        private static bool AreEqual(PathNode[] path1, PathNode[] path2)
        {
            if ((path1 == null) || (path2 == null))
            {
                return false;
            }
            if (path1.Length != path2.Length)
            {
                return false;
            }
            for (int i = path1.Length - 1; i >= 0; i--)
            {
                if (path1[i].Type != path2[i].Type)
                {
                    return false;
                }
                if (path1[i].Index != path2[i].Index)
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool AreInSameGroup(BaseLayoutItem[] items)
        {
            if ((items == null) || (items.Length == 0))
            {
                return false;
            }
            BaseLayoutItem item = items[0];
            foreach (BaseLayoutItem item2 in items)
            {
                if (!AreInSameGroup(item, item2))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool AreInSameGroup(BaseLayoutItem item1, BaseLayoutItem item2) => 
            (item1 != null) && ((item2 != null) && ((item1.Parent != null) && ((item2.Parent != null) && ReferenceEquals(item1.Parent, item2.Parent))));

        public static bool AreTreePathEqual(BaseLayoutItem item1, BaseLayoutItem item2) => 
            !ReferenceEquals(item1, item2) ? AreEqual(GetPath(item1), GetPath(item2)) : true;

        private static void BeginGridLengthAnimation(BaseLayoutItem layoutItem, DependencyProperty property, GridLength from, GridLength to, TimeSpan duration, EventHandler completed)
        {
            GridLengthAnimation animation = CreateAnimation(from, to, duration);
            animation.Completed += delegate (object sender, EventArgs e) {
                layoutItem.BeginAnimation(property, null);
                layoutItem.SetValue(property, to);
            };
            if (completed != null)
            {
                animation.Completed += completed;
            }
            layoutItem.BeginAnimation(property, animation);
        }

        public static void BeginHeightAnimation(this BaseLayoutItem layoutItem, GridLength from, GridLength to, TimeSpan duration, EventHandler completed = null)
        {
            BeginGridLengthAnimation(layoutItem, BaseLayoutItem.ItemHeightProperty, from, to, duration, completed);
        }

        public static void BeginWidthAnimation(this BaseLayoutItem layoutItem, GridLength from, GridLength to, TimeSpan duration, EventHandler completed = null)
        {
            BeginGridLengthAnimation(layoutItem, BaseLayoutItem.ItemWidthProperty, from, to, duration, completed);
        }

        internal static bool CanRecognizeAccessKey(BaseLayoutItem item) => 
            item is LayoutControlItem;

        internal static bool CanReorder(BaseLayoutItem item, BaseLayoutItem target)
        {
            LayoutPanel panel = item as LayoutPanel;
            LayoutPanel panel2 = target as LayoutPanel;
            return ((panel != null) && ((panel2 != null) && (ReferenceEquals(panel.Parent, panel2.Parent) && (panel.IsPinnedTab == panel2.IsPinnedTab))));
        }

        private static GridLengthAnimation CreateAnimation(GridLength from, GridLength to, TimeSpan duration)
        {
            GridLengthAnimation animation1 = new GridLengthAnimation();
            animation1.From = from;
            animation1.To = to;
            animation1.Duration = new Duration(duration);
            animation1.FillBehavior = FillBehavior.Stop;
            return animation1;
        }

        internal static DockLayoutManager FindDockLayoutManager(this BaseLayoutItem item)
        {
            if (item.Manager != null)
            {
                return item.Manager;
            }
            LayoutGroup root = item.GetRoot();
            if (root == null)
            {
                return null;
            }
            DockLayoutManager manager = root.Manager;
            DockLayoutManager manager2 = manager;
            if (manager == null)
            {
                DockLayoutManager local1 = manager;
                if (root.ParentPanel == null)
                {
                    return null;
                }
                manager2 = root.ParentPanel.FindDockLayoutManager();
            }
            return manager2;
        }

        internal static DependencyObject FindElementInVisualTree(DependencyObject root, Predicate<DependencyObject> filter, Predicate<DependencyObject> result)
        {
            DependencyObject obj3;
            using (VisualTreeEnumeratorWithConditionalStop stop = new VisualTreeEnumeratorWithConditionalStop(root, filter))
            {
                while (true)
                {
                    if (stop.MoveNext())
                    {
                        DependencyObject current = stop.Current;
                        if ((result == null) || !result(current))
                        {
                            continue;
                        }
                        obj3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return obj3;
        }

        internal static int FindSubtreeIndex(LayoutGroup rootElement, BaseLayoutItem subtreeElement)
        {
            while ((subtreeElement.Parent != null) && !ReferenceEquals(subtreeElement.Parent, rootElement))
            {
                subtreeElement = subtreeElement.Parent;
            }
            return rootElement.Items.IndexOf(subtreeElement);
        }

        internal static BaseLayoutItem GetActualLayoutItem(this FloatGroup floatGroup)
        {
            BaseLayoutItem item = floatGroup;
            if (floatGroup.HasSingleItem)
            {
                TabbedGroup group = floatGroup.Items[0] as TabbedGroup;
                item = (group == null) ? floatGroup.Items[0] : (group.SelectedItem ?? floatGroup);
            }
            return item;
        }

        internal static bool GetAllowDockToDocumentGroup(this BaseLayoutItem item) => 
            !(item is LayoutGroup) ? ((item is LayoutPanel) && ((LayoutPanel) item).AllowDockToDocumentGroup) : ((LayoutGroup) item).Items.AllowDockToDocumentGroup();

        internal static Dock? GetAutoHideCenterDock(BaseLayoutItem item)
        {
            Func<Tuple<BaseLayoutItem, BaseLayoutItem>, bool> predicate = <>c.<>9__66_5;
            if (<>c.<>9__66_5 == null)
            {
                Func<Tuple<BaseLayoutItem, BaseLayoutItem>, bool> local1 = <>c.<>9__66_5;
                predicate = <>c.<>9__66_5 = x => x.Item2 != null;
            }
            Tuple<BaseLayoutItem, BaseLayoutItem> tuple = item.Manager.GetItems().Where<BaseLayoutItem>(delegate (BaseLayoutItem x) {
                if (!AutoHideGroup.GetIsAutoHideCenter(x) || !IsActuallyVisibleInTree(x))
                {
                    return false;
                }
                Func<BaseLayoutItem, BaseLayoutItem> parentEvaluator = <>c.<>9__66_2;
                if (<>c.<>9__66_2 == null)
                {
                    Func<BaseLayoutItem, BaseLayoutItem> local1 = <>c.<>9__66_2;
                    parentEvaluator = <>c.<>9__66_2 = y => y.Parent;
                }
                return (item.FindAmongParents<BaseLayoutItem>(y => ReferenceEquals(y, x), parentEvaluator) == null);
            }).Select<BaseLayoutItem, Tuple<BaseLayoutItem, BaseLayoutItem>>(delegate (BaseLayoutItem x) {
                Func<BaseLayoutItem, BaseLayoutItem> parentEvaluator = <>c.<>9__66_4;
                if (<>c.<>9__66_4 == null)
                {
                    Func<BaseLayoutItem, BaseLayoutItem> local1 = <>c.<>9__66_4;
                    parentEvaluator = <>c.<>9__66_4 = y => y.Parent;
                }
                return new Tuple<BaseLayoutItem, BaseLayoutItem>(x, item.FindCommonAncestor<BaseLayoutItem>(x, parentEvaluator));
            }).Where<Tuple<BaseLayoutItem, BaseLayoutItem>>(predicate).OrderBy<Tuple<BaseLayoutItem, BaseLayoutItem>, int>(delegate (Tuple<BaseLayoutItem, BaseLayoutItem> x) {
                Func<BaseLayoutItem, BaseLayoutItem> parentEvaluator = <>c.<>9__66_7;
                if (<>c.<>9__66_7 == null)
                {
                    Func<BaseLayoutItem, BaseLayoutItem> local1 = <>c.<>9__66_7;
                    parentEvaluator = <>c.<>9__66_7 = y => y.Parent;
                }
                return item.FindAncestorIndex<BaseLayoutItem>(x.Item2, parentEvaluator);
            }).FirstOrDefault<Tuple<BaseLayoutItem, BaseLayoutItem>>();
            if (tuple != null)
            {
                BaseLayoutItem subtreeElement = tuple.Item1;
                LayoutGroup rootElement = (tuple.Item2 as LayoutGroup) ?? tuple.Item2.Parent;
                if (rootElement != null)
                {
                    int num = FindSubtreeIndex(rootElement, subtreeElement);
                    int num2 = FindSubtreeIndex(rootElement, item);
                    return new Dock?((rootElement.Orientation != Orientation.Horizontal) ? ((num2 >= num) ? Dock.Bottom : Dock.Top) : ((num2 >= num) ? Dock.Right : Dock.Left));
                }
            }
            return null;
        }

        internal static Size GetAutoHideRenderSize(this BaseLayoutItem item)
        {
            if ((item == null) || !item.IsAutoHidden)
            {
                return Size.Empty;
            }
            AutoHideGroup root = (AutoHideGroup) item.GetRoot();
            AutoHideTray rootUIScope = root.GetRootUIScope() as AutoHideTray;
            Size actualAutoHideSize = root.GetActualAutoHideSize(item);
            bool flag = (root.DockType == Dock.Left) || (root.DockType == Dock.Right);
            return ((rootUIScope != null) ? new Size(flag ? actualAutoHideSize.Width : rootUIScope.RenderSize.Width, flag ? rootUIScope.RenderSize.Height : actualAutoHideSize.Height) : Size.Empty);
        }

        internal static Thickness GetBorderMargin(this BaseLayoutItem item)
        {
            DockLayoutManager dockLayoutManager = item.GetDockLayoutManager();
            if (dockLayoutManager == null)
            {
                return new Thickness();
            }
            string themeName = DockLayoutManagerExtension.GetThemeName(dockLayoutManager);
            FloatPaneElements elements = (item is DocumentPanel) ? FloatPaneElements.FormBorderMargin : FloatPaneElements.BorderMargin;
            FloatPaneElementsThemeKeyExtension key = new FloatPaneElementsThemeKeyExtension();
            key.ResourceKey = elements;
            key.ThemeName = themeName;
            return (Thickness) dockLayoutManager.FindResource(key);
        }

        public static T GetChild<T>(DependencyObject element) where T: DependencyObject
        {
            if ((element != null) && (VisualTreeHelper.GetChildrenCount(element) == 1))
            {
                return (VisualTreeHelper.GetChild(element, 0) as T);
            }
            return default(T);
        }

        public static DockLayoutManager GetDockLayoutManager(this BaseLayoutItem item) => 
            (item != null) ? (item.Manager ?? DockLayoutManager.GetDockLayoutManager(item)) : null;

        public static IEnumerator<BaseLayoutItem> GetEnumerator(LayoutGroup group) => 
            new LayoutEnumerator(group, null);

        public static IEnumerator<BaseLayoutItem> GetEnumerator(LayoutGroup group, Predicate<BaseLayoutItem> filter) => 
            new LayoutEnumerator(group, filter);

        public static IEnumerator<DependencyObject> GetEnumerator(DependencyObject element, Predicate<DependencyObject> filter) => 
            new IUIElementVisualTreeEnumerator(element, filter);

        internal static BaseLayoutItem GetFirstItemInGroup(LayoutGroup group)
        {
            BaseLayoutItem selectedItem = group;
            while (true)
            {
                if ((group != null) && (group.Items.Count > 0))
                {
                    LayoutGroup group2 = group.Items[0] as LayoutGroup;
                    if ((group2 != null) && (group.GroupBorderStyle != GroupBorderStyle.Tabbed))
                    {
                        group = group2;
                        continue;
                    }
                    if (!(group is TabbedGroup) && (group.GroupBorderStyle != GroupBorderStyle.Tabbed))
                    {
                        selectedItem = group.Items[0];
                    }
                    else if (group.SelectedItem != null)
                    {
                        selectedItem = group.SelectedItem;
                    }
                }
                return selectedItem;
            }
        }

        internal static LayoutGroup GetFirstNoBorderParent(BaseLayoutItem item)
        {
            if (item == null)
            {
                return null;
            }
            LayoutGroup parent = item.Parent;
            while ((parent != null) && (parent.GroupBorderStyle != GroupBorderStyle.NoBorder))
            {
                parent = parent.Parent;
            }
            return parent;
        }

        internal static Thickness GetFloatingBorderMargin(this BaseLayoutItem item)
        {
            if (!item.AllowFloat)
            {
                return new Thickness(12.0);
            }
            DockLayoutManager dockLayoutManager = item.GetDockLayoutManager();
            if ((!item.IsFloatingRootItem && !(item is FloatGroup)) || dockLayoutManager.ShowContentWhenDragging)
            {
                return new Thickness(6.0);
            }
            return item.GetBorderMargin();
        }

        internal static int GetIndexFromItem(BaseLayoutItem item) => 
            ((item == null) || (item.Parent == null)) ? -1 : item.Parent.IndexFromItem(item);

        internal static bool GetIsDocumentHost(this BaseLayoutItem item)
        {
            LayoutGroup group = item as LayoutGroup;
            return ((group != null) && group.GetIsDocumentHost());
        }

        public static BaseLayoutItem[] GetItems(this AutoHideGroupCollection collection)
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            collection.Accept<AutoHideGroup>(delegate (AutoHideGroup ahGroup) {
                ahGroup.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
            });
            return items.ToArray();
        }

        public static BaseLayoutItem[] GetItems(this FloatGroupCollection collection)
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            collection.Accept<FloatGroup>(delegate (FloatGroup fGroup) {
                fGroup.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
            });
            return items.ToArray();
        }

        public static BaseLayoutItem[] GetItems(this LayoutGroup group)
        {
            BaseLayoutItem[] array = new BaseLayoutItem[group.Items.Count];
            group.Items.CopyTo(array, 0);
            return array;
        }

        public static IUIElement GetIUIParent(DependencyObject dObj)
        {
            if (dObj == null)
            {
                return null;
            }
            DependencyObject visualParent = GetVisualParent(dObj);
            while ((visualParent != null) && !(visualParent is IUIElement))
            {
                visualParent = GetVisualParent(visualParent);
            }
            return (IUIElement) visualParent;
        }

        internal static IEnumerable<BaseLayoutItem> GetNestedItems(this LayoutGroup group)
        {
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            group.Accept(new VisitDelegate<BaseLayoutItem>(list.Add));
            return list.ToArray();
        }

        internal static BaseLayoutItem GetNextItem(BaseLayoutItem item)
        {
            if ((item == null) || (item.Parent == null))
            {
                return null;
            }
            BaseLayoutItemCollection items = item.Parent.Items;
            BaseLayoutItem item2 = null;
            BaseLayoutItem item3 = null;
            int index = items.IndexOf(item);
            if ((index - 1) >= 0)
            {
                item3 = items[index - 1];
            }
            if ((index + 1) < items.Count)
            {
                item2 = items[index + 1];
            }
            return (item3 ?? item2);
        }

        internal static LayoutGroup GetOwnerGroup(this LayoutGroup group) => 
            LayoutGroup.GetOwnerGroup(group) ?? group;

        private static PathNode[] GetPath(BaseLayoutItem item)
        {
            List<PathNode> list = new List<PathNode>();
            while (item != null)
            {
                PathNode node = PathNode.FromItem(item);
                list.Add(node);
                LayoutGroup parent = item.Parent;
                if (parent != null)
                {
                    node.Index = parent.Items.IndexOf(item);
                }
                else
                {
                    LayoutGroup group2 = item as LayoutGroup;
                    if (group2 != null)
                    {
                        item = group2.ParentPanel;
                        continue;
                    }
                }
                item = parent;
            }
            return list.ToArray();
        }

        internal static Size GetResizingMaxSize(FloatGroup fGroup) => 
            new Size(double.IsNaN(fGroup.ActualMaxSize.Width) ? double.PositiveInfinity : fGroup.ActualMaxSize.Width, double.IsNaN(fGroup.ActualMaxSize.Height) ? double.PositiveInfinity : fGroup.ActualMaxSize.Height);

        internal static Size GetResizingMinSize(FloatGroup fGroup)
        {
            Size size = new Size(82.0, 42.0);
            if (fGroup.Items.Count > 0)
            {
                LayoutGroup group = fGroup[0] as LayoutGroup;
                if ((group != null) && !group.IgnoreOrientation)
                {
                    bool flag = group.Orientation == Orientation.Horizontal;
                    size = new Size(flag ? ((double) ((0x2a * group.Items.Count) + 0x10)) : ((double) 0x52), flag ? ((double) 0x2a) : ((double) ((0x2a * group.Items.Count) + 0x20)));
                }
            }
            Size[] minSizes = new Size[] { size, fGroup.ActualMinSize };
            return MathHelper.MeasureMinSize(minSizes);
        }

        public static LayoutGroup GetRoot(this BaseLayoutItem item)
        {
            if (item == null)
            {
                return null;
            }
            BaseLayoutItem parent = item;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return (parent as LayoutGroup);
        }

        internal static Size GetSize(this BaseLayoutItem item) => 
            item.GetSize(new Size(200.0, 200.0));

        internal static Size GetSize(this BaseLayoutItem item, Size defaultSize)
        {
            double width = item.ItemWidth.IsAbsolute ? item.ItemWidth.Value : defaultSize.Width;
            return MathHelper.MeasureSize(item.ActualMinSize, item.ActualMaxSize, new Size(width, item.ItemHeight.IsAbsolute ? item.ItemHeight.Value : defaultSize.Height));
        }

        internal static int GetTabIndexFromItem(BaseLayoutItem item) => 
            ((item == null) || (item.Parent == null)) ? -1 : item.Parent.TabIndexFromItem(item);

        public static T GetTemplateChild<T>(DependencyObject element) where T: DependencyObject
        {
            T current = default(T);
            using (IEnumerator<DependencyObject> enumerator = GetEnumerator(element, null))
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is T)
                    {
                        current = enumerator.Current;
                        break;
                    }
                }
            }
            return current;
        }

        public static T GetTemplateChild<T>(DependencyObject element, bool acceptRoot) where T: DependencyObject
        {
            T current = default(T);
            using (IEnumerator<DependencyObject> enumerator = GetEnumerator(element, null))
            {
                while (enumerator.MoveNext())
                {
                    if ((enumerator.Current is T) && (acceptRoot || (enumerator.Current != element)))
                    {
                        current = enumerator.Current;
                        break;
                    }
                }
            }
            return current;
        }

        public static T GetTemplateParent<T>(DependencyObject dObj) where T: DependencyObject
        {
            if (dObj == null)
            {
                return default(T);
            }
            DependencyObject visualParent = GetVisualParent(dObj);
            while ((visualParent != null) && (!(visualParent is T) && !(visualParent is IUIElement)))
            {
                visualParent = GetVisualParent(visualParent);
            }
            return (visualParent as T);
        }

        public static UIElement GetUIElement(this BaseLayoutItem item) => 
            !(item is ILayoutContent) ? (!(item is LabelItem) ? null : (((LabelItem) item).Content as UIElement)) : ((ILayoutContent) item).Control;

        public static T GetVisualChild<T>(DependencyObject element) where T: DependencyObject
        {
            T current = default(T);
            VisualTreeEnumerator enumerator = new VisualTreeEnumerator(element);
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    if (!(enumerator.Current is T))
                    {
                        continue;
                    }
                    current = (T) enumerator.Current;
                }
                return current;
            }
        }

        public static DependencyObject GetVisualParent(DependencyObject dObj) => 
            VisualTreeHelper.GetParent(dObj);

        internal static bool IsActuallyVisibleInTree(BaseLayoutItem item)
        {
            if (item == null)
            {
                return false;
            }
            DefinitionBase definition = DefinitionsHelper.GetDefinition(item);
            return ((definition != null) && !DefinitionsHelper.IsZero(definition));
        }

        internal static bool IsDataBound(BaseLayoutItem item)
        {
            LayoutGroup parent = item.Parent;
            return ((parent != null) && ((parent.ItemsSource != null) || BindingOperations.IsDataBound(parent, LayoutGroup.ItemsSourceProperty)));
        }

        public static bool IsDockItem(BaseLayoutItem item)
        {
            LayoutItemType itemType = item.ItemType;
            return ((itemType == LayoutItemType.Panel) || ((itemType == LayoutItemType.Document) || (itemType == LayoutItemType.AutoHidePanel)));
        }

        private static bool IsEmptyGroup(LayoutGroup group) => 
            (group != null) && (!group.HasNotCollapsedItems && ((group.ItemType == LayoutItemType.TabPanelGroup) || ((group.ItemType == LayoutItemType.Group) && (group.GroupBorderStyle == GroupBorderStyle.NoBorder))));

        public static bool IsEmptyLayoutGroup(BaseLayoutItem target)
        {
            LayoutGroup group = target as LayoutGroup;
            return ((group != null) && ((group.Items.Count == 0) && (group.ItemType == LayoutItemType.Group)));
        }

        internal static bool IsFloatingRootItem(BaseLayoutItem item)
        {
            if (item == null)
            {
                return false;
            }
            if (item.IsFloatingRootItem)
            {
                return true;
            }
            Func<TabbedGroup, bool> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<TabbedGroup, bool> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => x.IsFloatingRootItem;
            }
            return (item.Parent as TabbedGroup).Return<TabbedGroup, bool>(evaluator, (<>c.<>9__6_1 ??= () => false));
        }

        public static bool IsInTree(this BaseLayoutItem item)
        {
            LayoutGroup root = item.GetRoot();
            if ((root != null) && (root.ParentPanel != null))
            {
                root = root.ParentPanel.GetRoot();
            }
            return (root.IsRoot() && !root.IsUngroupped);
        }

        internal static bool IsItemWithRestrictedFloating(this BaseLayoutItem item)
        {
            if (!(item is LayoutPanel) && (!(item is TabbedGroup) && !(item is FloatGroup)))
            {
                return false;
            }
            if (!item.AllowFloat)
            {
                return true;
            }
            Func<DockLayoutManager, bool> evaluator = <>c.<>9__63_0;
            if (<>c.<>9__63_0 == null)
            {
                Func<DockLayoutManager, bool> local1 = <>c.<>9__63_0;
                evaluator = <>c.<>9__63_0 = x => x.ShowContentWhenDragging;
            }
            return !item.GetDockLayoutManager().Return<DockLayoutManager, bool>(evaluator, (<>c.<>9__63_1 ??= () => true));
        }

        public static bool IsLayoutItem(BaseLayoutItem item)
        {
            LayoutItemType itemType = item.ItemType;
            if (itemType != LayoutItemType.ControlItem)
            {
                switch (itemType)
                {
                    case LayoutItemType.FixedItem:
                    case LayoutItemType.LayoutSplitter:
                    case LayoutItemType.EmptySpaceItem:
                    case LayoutItemType.Separator:
                    case LayoutItemType.Label:
                        break;

                    default:
                        return false;
                }
            }
            return true;
        }

        internal static bool? IsNextNeighbour(BaseLayoutItem item, BaseLayoutItem next)
        {
            if ((next != null) && ((item != null) && ((next.Parent != null) && (item.Parent != null))))
            {
                if (ReferenceEquals(next, item) || !ReferenceEquals(next.Parent, item.Parent))
                {
                    return null;
                }
                LayoutGroup parent = next.Parent;
                int index = parent.Items.IndexOf(next);
                int num2 = parent.Items.IndexOf(item);
                if (Math.Abs((int) (index - num2)) == 1)
                {
                    return new bool?((index - num2) == 1);
                }
            }
            return null;
        }

        public static bool IsParent(BaseLayoutItem item, BaseLayoutItem parent)
        {
            if ((item != null) && (parent != null))
            {
                for (BaseLayoutItem item2 = item.Parent; item2 != null; item2 = item2.Parent)
                {
                    if (ReferenceEquals(item2, parent))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool IsResizable(BaseLayoutItem item, bool isHorizontal = true)
        {
            bool flag = ((item.AllowSizing && !(item is LayoutSplitter)) && !(item is SeparatorItem)) && (isHorizontal ? !(item.ActualMinSize.Width == item.ActualMaxSize.Width) : !(item.ActualMinSize.Height == item.ActualMaxSize.Height));
            if (flag && (item is LayoutGroup))
            {
                flag = IsResizableGroup((LayoutGroup) item, isHorizontal);
            }
            return flag;
        }

        private static bool IsResizableGroup(LayoutGroup group, bool isHorizontal)
        {
            bool flag;
            if ((group == null) || (IsEmptyGroup(group) || !IsActuallyVisibleInTree(group)))
            {
                return false;
            }
            if ((group is DocumentGroup) && !((DocumentGroup) group).IsTabbed)
            {
                return true;
            }
            if ((group.GroupBorderStyle == GroupBorderStyle.Tabbed) && group.IsControlItemsHost)
            {
                return true;
            }
            if (!isHorizontal && ((group.GroupBorderStyle == GroupBorderStyle.GroupBox) && !group.IsExpanded))
            {
                return false;
            }
            using (IEnumerator<BaseLayoutItem> enumerator = group.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (!IsResizable(current, true))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return !group.HasItems;
                    }
                    break;
                }
            }
            return flag;
        }

        internal static bool IsRoot(this LayoutGroup group) => 
            (group != null) && group.IsRootGroup;

        public static bool IsTemplateChild<T>(DependencyObject dObj, T root) where T: DependencyObject => 
            LayoutHelper.FindParentObject<T>(dObj) == root;

        internal static bool ShouldStayInTree(this LayoutGroup group) => 
            ((group.Manager == null) || !group.Manager.IsContainerHost(group)) ? (group.HasPlaceHolders || (group.ReadLocalValue(LayoutGroup.ItemsSourceProperty) != DependencyProperty.UnsetValue)) : true;

        internal static BaseLayoutItem[] SortByZIndex(BaseLayoutItem[] items)
        {
            Func<BaseLayoutItem, int> keySelector = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Func<BaseLayoutItem, int> local1 = <>c.<>9__20_0;
                keySelector = <>c.<>9__20_0 = item => item.ZIndex;
            }
            return items.OrderBy<BaseLayoutItem, int>(keySelector).ToArray<BaseLayoutItem>();
        }

        public static void UpdateZIndexes(DocumentGroup dGroup)
        {
            BaseLayoutItem[] items = dGroup.GetItems();
            BaseLayoutItem selectedItem = dGroup.SelectedItem;
            if ((items.Length != 0) && (selectedItem != null))
            {
                UpdateZIndexes(items, selectedItem);
            }
        }

        public static void UpdateZIndexes(BaseLayoutItem[] items, BaseLayoutItem item)
        {
            items = SortByZIndex(items);
            int index = Array.IndexOf<BaseLayoutItem>(items, item);
            int num2 = items.Length - 1;
            if (index < num2)
            {
                Array.Copy(items, index + 1, items, index, num2 - index);
                items[num2] = item;
            }
            for (int i = 0; i < items.Length; i++)
            {
                items[i].ZIndex = i;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemsHelper.<>c <>9 = new LayoutItemsHelper.<>c();
            public static Func<TabbedGroup, bool> <>9__6_0;
            public static Func<bool> <>9__6_1;
            public static Func<BaseLayoutItem, int> <>9__20_0;
            public static Func<DockLayoutManager, bool> <>9__63_0;
            public static Func<bool> <>9__63_1;
            public static Func<BaseLayoutItem, BaseLayoutItem> <>9__66_2;
            public static Func<BaseLayoutItem, BaseLayoutItem> <>9__66_4;
            public static Func<Tuple<BaseLayoutItem, BaseLayoutItem>, bool> <>9__66_5;
            public static Func<BaseLayoutItem, BaseLayoutItem> <>9__66_7;

            internal BaseLayoutItem <GetAutoHideCenterDock>b__66_2(BaseLayoutItem y) => 
                y.Parent;

            internal BaseLayoutItem <GetAutoHideCenterDock>b__66_4(BaseLayoutItem y) => 
                y.Parent;

            internal bool <GetAutoHideCenterDock>b__66_5(Tuple<BaseLayoutItem, BaseLayoutItem> x) => 
                x.Item2 != null;

            internal BaseLayoutItem <GetAutoHideCenterDock>b__66_7(BaseLayoutItem y) => 
                y.Parent;

            internal bool <IsFloatingRootItem>b__6_0(TabbedGroup x) => 
                x.IsFloatingRootItem;

            internal bool <IsFloatingRootItem>b__6_1() => 
                false;

            internal bool <IsItemWithRestrictedFloating>b__63_0(DockLayoutManager x) => 
                x.ShowContentWhenDragging;

            internal bool <IsItemWithRestrictedFloating>b__63_1() => 
                true;

            internal int <SortByZIndex>b__20_0(BaseLayoutItem item) => 
                item.ZIndex;
        }

        private class IUIElementVisualTreeEnumerator : IEnumerator<DependencyObject>, IDisposable, IEnumerator
        {
            private DependencyObject Root;
            private Stack<DependencyObject> Stack = new Stack<DependencyObject>(0x10);
            private Predicate<DependencyObject> Filter;
            private DependencyObject current;

            public IUIElementVisualTreeEnumerator(DependencyObject root, Predicate<DependencyObject> filter)
            {
                this.Filter = filter;
                this.Root = root;
            }

            public void Dispose()
            {
                this.Reset();
                this.Stack = null;
                this.Filter = null;
            }

            public bool MoveNext()
            {
                if (this.current == null)
                {
                    this.current = this.Root;
                }
                else
                {
                    int childrenCount = VisualTreeHelper.GetChildrenCount(this.current);
                    DependencyObject[] objArray = new DependencyObject[childrenCount];
                    int index = 0;
                    while (true)
                    {
                        if (index >= childrenCount)
                        {
                            if (objArray.Length != 0)
                            {
                                for (int i = 0; i < objArray.Length; i++)
                                {
                                    DependencyObject obj2 = objArray[(objArray.Length - 1) - i];
                                    if (!(obj2 is IUIElement) && ((this.Filter == null) || this.Filter(obj2)))
                                    {
                                        this.Stack.Push(obj2);
                                    }
                                }
                            }
                            this.current = (this.Stack.Count > 0) ? this.Stack.Pop() : null;
                            break;
                        }
                        objArray[index] = VisualTreeHelper.GetChild(this.current, index);
                        index++;
                    }
                }
                return (this.current != null);
            }

            public void Reset()
            {
                this.Stack.Clear();
                this.current = null;
            }

            object IEnumerator.Current =>
                this.current;

            public DependencyObject Current =>
                this.current;
        }

        private class LayoutEnumerator : IEnumerator<BaseLayoutItem>, IDisposable, IEnumerator
        {
            private LayoutGroup Root;
            private Stack<BaseLayoutItem> Stack = new Stack<BaseLayoutItem>(8);
            private Predicate<BaseLayoutItem> Filter;
            private BaseLayoutItem current;

            public LayoutEnumerator(LayoutGroup rootGroup, Predicate<BaseLayoutItem> filter)
            {
                this.Filter = filter;
                this.Root = rootGroup;
            }

            public void Dispose()
            {
                this.Reset();
                this.Stack = null;
                this.Filter = null;
            }

            public bool MoveNext()
            {
                if (this.current == null)
                {
                    this.current = this.Root;
                }
                else
                {
                    LayoutGroup current = this.current as LayoutGroup;
                    if (current != null)
                    {
                        BaseLayoutItem[] items = current.GetItems();
                        if (items.Length != 0)
                        {
                            for (int i = 0; i < items.Length; i++)
                            {
                                BaseLayoutItem item = items[(items.Length - 1) - i];
                                if ((this.Filter == null) || this.Filter(item))
                                {
                                    this.Stack.Push(item);
                                }
                            }
                        }
                    }
                    this.current = (this.Stack.Count > 0) ? this.Stack.Pop() : null;
                }
                return (this.current != null);
            }

            public void Reset()
            {
                this.Stack.Clear();
                this.current = null;
            }

            object IEnumerator.Current =>
                this.current;

            public BaseLayoutItem Current =>
                this.current;
        }

        private class PathNode
        {
            public int Index = -1;
            public System.Type Type;

            public static LayoutItemsHelper.PathNode FromItem(BaseLayoutItem item)
            {
                LayoutItemsHelper.PathNode node1 = new LayoutItemsHelper.PathNode();
                node1.Type = item.GetType();
                return node1;
            }
        }
    }
}

