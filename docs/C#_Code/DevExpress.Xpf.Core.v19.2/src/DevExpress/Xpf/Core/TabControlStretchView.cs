namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabControlStretchView : TabControlViewBase
    {
        public static readonly DependencyProperty PinModeProperty;
        public static readonly DependencyProperty PinnedTabAllowHideProperty;
        public static readonly DependencyProperty PinnedTabAllowDragProperty;
        public static readonly DependencyProperty PinnedTabSizeProperty;
        public static readonly DependencyProperty TabNormalSizeProperty;
        public static readonly DependencyProperty SelectedTabMinSizeProperty;
        public static readonly DependencyProperty TabMinSizeProperty;
        public static readonly DependencyProperty MoveItemsWhenDragDropProperty;
        public static readonly DependencyProperty DragDropModeProperty;
        public static readonly DependencyProperty DragDropRegionProperty;
        public static readonly DependencyProperty CloseWindowOnSingleTabItemHidingProperty;
        public static readonly DependencyProperty NewWindowStyleProperty;
        public static readonly DependencyProperty NewTabControlStyleProperty;
        internal bool preventFocusRestoring;
        private List<Window> windowsToClose = new List<Window>();
        private bool lockUpdateDragDropControls;
        private TabControlDragWidgetHelper dragWidgetHelper;

        static TabControlStretchView()
        {
            PinModeProperty = DependencyProperty.RegisterAttached("PinMode", typeof(TabPinMode), typeof(TabControlStretchView), new PropertyMetadata(TabPinMode.None, (d, e) => OnPinModeChanged((DXTabItem) d, (TabPinMode) e.OldValue, (TabPinMode) e.NewValue)));
            PinnedTabAllowHideProperty = DependencyProperty.Register("PinnedTabAllowHide", typeof(bool), typeof(TabControlStretchView), new PropertyMetadata(false, (d, e) => ((TabControlStretchView) d).UpdateViewProperties()));
            PinnedTabAllowDragProperty = DependencyProperty.Register("PinnedTabAllowDrag", typeof(bool), typeof(TabControlStretchView), new PropertyMetadata(false));
            PinnedTabSizeProperty = DependencyProperty.Register("PinnedTabSize", typeof(int), typeof(TabControlStretchView), new PropertyMetadata(0));
            TabNormalSizeProperty = DependencyProperty.Register("TabNormalSize", typeof(int), typeof(TabControlStretchView), new PropertyMetadata(160));
            SelectedTabMinSizeProperty = DependencyProperty.Register("SelectedTabMinSize", typeof(int), typeof(TabControlStretchView), new PropertyMetadata((int) 80));
            TabMinSizeProperty = DependencyProperty.Register("TabMinSize", typeof(int), typeof(TabControlStretchView), new PropertyMetadata((int) 0x23));
            MoveItemsWhenDragDropProperty = DependencyProperty.Register("MoveItemsWhenDragDrop", typeof(bool), typeof(TabControlStretchView), new PropertyMetadata(true));
            DragDropModeProperty = DependencyProperty.Register("DragDropMode", typeof(TabControlDragDropMode), typeof(TabControlStretchView), new PropertyMetadata(TabControlDragDropMode.ReorderOnly));
            DragDropRegionProperty = DependencyProperty.Register("DragDropRegion", typeof(string), typeof(TabControlStretchView), new PropertyMetadata((d, e) => ((TabControlStretchView) d).OnDragDropRegionChanged((string) e.OldValue, (string) e.NewValue)));
            CloseWindowOnSingleTabItemHidingProperty = DependencyProperty.Register("CloseWindowOnSingleTabItemHiding", typeof(bool), typeof(TabControlStretchView), new PropertyMetadata(false));
            NewWindowStyleProperty = DependencyProperty.Register("NewWindowStyle", typeof(Style), typeof(TabControlStretchView), new PropertyMetadata(null));
            NewTabControlStyleProperty = DependencyProperty.Register("NewTabControlStyle", typeof(Style), typeof(TabControlStretchView), new PropertyMetadata(null));
        }

        private bool AreAnyTabControls()
        {
            bool flag;
            using (IEnumerator<DXTabControl> enumerator = this.GetDragTabControls(true, true).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DXTabControl current = enumerator.Current;
                        if (current.VisibleItemsCount <= 0)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected internal override bool CanCloseTabItem(DXTabItem item) => 
            ((GetPinMode(item) == TabPinMode.None) || this.PinnedTabAllowHide) ? (!base.CanCloseTabItem(item) ? this.AreAnyTabControls() : true) : false;

        internal bool CanStartDrag(DXTabItem child)
        {
            if ((GetPinMode(child) != TabPinMode.None) && !this.PinnedTabAllowDrag)
            {
                return false;
            }
            if (this.DragDropMode == TabControlDragDropMode.None)
            {
                return false;
            }
            if ((this.DragDropMode == TabControlDragDropMode.ReorderOnly) && (base.Owner.VisibleItemsCount <= 1))
            {
                return false;
            }
            bool flag = !base.Owner.RaiseTabStartDragging(child).Cancel;
            if (flag)
            {
                base.Owner.BeginDragDrop();
            }
            return flag;
        }

        private void CloseWindow(Window wnd, bool closeImmediately)
        {
            if (wnd != null)
            {
                try
                {
                    if (closeImmediately)
                    {
                        this.windowsToClose.Remove(wnd);
                        wnd.Close();
                    }
                    else
                    {
                        this.windowsToClose.Add(wnd);
                        wnd.Hide();
                    }
                }
                catch
                {
                }
            }
        }

        private void CloseWindowsToClose()
        {
            foreach (Window window in this.windowsToClose.ToList<Window>())
            {
                this.CloseWindow(window, true);
            }
        }

        protected internal override int CoerceSelection(int index, NotifyCollectionChangedAction? originativeAction)
        {
            NotifyCollectionChangedAction? nullable = originativeAction;
            NotifyCollectionChangedAction remove = NotifyCollectionChangedAction.Remove;
            if ((((NotifyCollectionChangedAction) nullable.GetValueOrDefault()) == remove) ? (nullable == null) : true)
            {
                return base.CoerceSelection(index, originativeAction);
            }
            if (base.Owner == null)
            {
                return index;
            }
            DXTabItem item = base.Owner.SelectedContainer ?? base.Owner.GetContainer(index);
            if ((item == null) || (index < 0))
            {
                return base.Owner.GetCoercedSelectedIndexCore(base.Owner.GetContainers(), index);
            }
            TabPinMode pinMode = GetPinMode(item);
            List<DXTabItem> all = base.Owner.GetContainers().ToList<DXTabItem>();
            Func<DXTabItem, bool> predicate = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Func<DXTabItem, bool> local3 = <>c.<>9__54_0;
                predicate = <>c.<>9__54_0 = x => GetPinMode(x) == TabPinMode.None;
            }
            List<DXTabItem> arg = all.Where<DXTabItem>(predicate).ToList<DXTabItem>();
            Func<DXTabItem, bool> func4 = <>c.<>9__54_1;
            if (<>c.<>9__54_1 == null)
            {
                Func<DXTabItem, bool> local4 = <>c.<>9__54_1;
                func4 = <>c.<>9__54_1 = x => GetPinMode(x) == TabPinMode.Left;
            }
            List<DXTabItem> list2 = all.Where<DXTabItem>(func4).ToList<DXTabItem>();
            Func<DXTabItem, bool> func5 = <>c.<>9__54_2;
            if (<>c.<>9__54_2 == null)
            {
                Func<DXTabItem, bool> local5 = <>c.<>9__54_2;
                func5 = <>c.<>9__54_2 = x => GetPinMode(x) == TabPinMode.Right;
            }
            List<DXTabItem> list3 = all.Where<DXTabItem>(func5).ToList<DXTabItem>();
            Func<IList<DXTabItem>, int, int> func = delegate (IList<DXTabItem> filteredChildren, int ind) {
                ind = Math.Max(0, ind);
                ind = Math.Min(filteredChildren.Count - 1, ind);
                int coercedSelectedIndexCore = this.Owner.GetCoercedSelectedIndexCore(filteredChildren, ind);
                return all.IndexOf(filteredChildren.ElementAtOrDefault<DXTabItem>(coercedSelectedIndexCore));
            };
            Func<IList<DXTabItem>, int> func2 = filteredChildren => all.IndexOf(filteredChildren.ElementAtOrDefault<DXTabItem>(this.Owner.GetCoercedSelectedIndexCore(filteredChildren, filteredChildren.Count - 1)));
            Func<IList<DXTabItem>, int> func3 = filteredChildren => filteredChildren.Where<DXTabItem>(new Func<DXTabItem, bool>(base.Owner.IsVisibleAndEnabledItem)).Count<DXTabItem>();
            return ((pinMode != TabPinMode.None) ? ((pinMode != TabPinMode.Left) ? ((pinMode != TabPinMode.Right) ? -1 : ((func3(list3) <= 0) ? ((func3(arg) <= 0) ? ((func3(list2) <= 0) ? -1 : func2(list2)) : func2(arg)) : func(list3, (index - arg.Count) - list2.Count))) : ((func3(list2) <= 0) ? ((func3(arg) <= 0) ? ((func3(list3) <= 0) ? -1 : func2(list3)) : func2(arg)) : func(list2, (index - arg.Count) - list3.Count))) : ((func3(arg) <= 0) ? ((func3(list2) <= 0) ? ((func3(list3) <= 0) ? -1 : func2(list3)) : func2(list2)) : func(arg, (index - list2.Count) - list3.Count)));
        }

        protected virtual TabControlDragWidgetHelper CreateDragWidgetHelper() => 
            new TabControlDragWidgetHelper();

        internal void DropOnEmptySpace(DXTabItem child)
        {
            if (this.DragDropMode != TabControlDragDropMode.Full)
            {
                this.SetVisibility(child, Visibility.Visible);
            }
            else
            {
                this.SetVisibility(child, Visibility.Visible);
                if (!base.Owner.RaiseTabDropping(child).Cancel)
                {
                    try
                    {
                        this.preventFocusRestoring = true;
                        this.Remove(child);
                        object underlyingData = child.UnderlyingData;
                        if (child.UnderlyingData == null)
                        {
                            object local1 = child.UnderlyingData;
                            underlyingData = child;
                        }
                        TabControlNewTabbedWindowEventArgs args = base.Owner.RaiseNewTabbedWindow(underlyingData);
                        DXTabControl content = args.NewWindow.Content as DXTabControl;
                        DXTabControl newTabControl = content;
                        if (content == null)
                        {
                            DXTabControl local2 = content;
                            newTabControl = args.NewTabControl;
                        }
                        (newTabControl.View as TabControlStretchView).Do<TabControlStretchView>(x => x.Insert(child, 0));
                        args.NewWindow.Show();
                    }
                    finally
                    {
                        this.preventFocusRestoring = false;
                    }
                }
            }
        }

        private IEnumerable<DXTabControl> GetDragTabControls(bool excludeOwner, bool excludeInvisible)
        {
            IEnumerable<DXTabControl> source = DragDropRegionManager.GetDragDropControls(this.DragDropRegion, base.Dispatcher).OfType<DXTabControl>();
            if (excludeOwner)
            {
                source = from x in source
                    where !ReferenceEquals(x, base.Owner)
                    select x;
            }
            if (excludeInvisible)
            {
                Func<DXTabControl, bool> predicate = <>c.<>9__67_1;
                if (<>c.<>9__67_1 == null)
                {
                    Func<DXTabControl, bool> local1 = <>c.<>9__67_1;
                    predicate = <>c.<>9__67_1 = x => x.IsVisible;
                }
                source = source.Where<DXTabControl>(predicate);
            }
            return source;
        }

        public static TabPinMode GetPinMode(DXTabItem obj) => 
            (TabPinMode) obj.GetValue(PinModeProperty);

        internal void Insert(DXTabItem child, int index)
        {
            child.Insert(base.Owner, index);
        }

        internal void Move(DXTabItem child, int index)
        {
            child.Move(index, this.MoveItemsWhenDragDrop);
        }

        private void OnDragDropRegionChanged(string oldValue, string newValue)
        {
            base.Owner.Do<DXTabControl>(x => DragDropRegionManager.UnregisterDragDropRegion(x, oldValue));
            base.Owner.Do<DXTabControl>(x => DragDropRegionManager.RegisterDragDropRegion(x, newValue));
        }

        internal void OnDragFinished()
        {
            Action<TabControlStretchView> updateAction = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                Action<TabControlStretchView> local1 = <>c.<>9__71_0;
                updateAction = <>c.<>9__71_0 = x => x.CloseWindowsToClose();
            }
            this.UpdateDragDropControls(false, false, updateAction);
            base.Owner.EndDragDrop();
        }

        protected override void OnOwnerChanged(DXTabControl oldValue, DXTabControl newValue)
        {
            base.OnOwnerChanged(oldValue, newValue);
            oldValue.Do<DXTabControl>(delegate (DXTabControl x) {
                x.Loaded -= new RoutedEventHandler(this.OnOwnerLoaded);
            });
            oldValue.Do<DXTabControl>(delegate (DXTabControl x) {
                x.Unloaded -= new RoutedEventHandler(this.OnOwnerUnloaded);
            });
            newValue.Do<DXTabControl>(delegate (DXTabControl x) {
                x.Loaded += new RoutedEventHandler(this.OnOwnerLoaded);
            });
            newValue.Do<DXTabControl>(delegate (DXTabControl x) {
                x.Unloaded += new RoutedEventHandler(this.OnOwnerUnloaded);
            });
            oldValue.Do<DXTabControl>(x => DragDropRegionManager.UnregisterDragDropRegion(x, this.DragDropRegion));
            newValue.Do<DXTabControl>(x => DragDropRegionManager.RegisterDragDropRegion(x, this.DragDropRegion));
        }

        private void OnOwnerLoaded(object sender, EventArgs e)
        {
            DXTabControl input = (DXTabControl) sender;
            input.Do<DXTabControl>(x => DragDropRegionManager.UnregisterDragDropRegion(x, this.DragDropRegion));
            input.Do<DXTabControl>(x => DragDropRegionManager.RegisterDragDropRegion(x, this.DragDropRegion));
        }

        private void OnOwnerUnloaded(object sender, EventArgs e)
        {
            ((DXTabControl) sender).Do<DXTabControl>(x => DragDropRegionManager.UnregisterDragDropRegion(x, this.DragDropRegion));
        }

        private static void OnPinModeChanged(DXTabItem tab, TabPinMode oldValue, TabPinMode newValue)
        {
            Func<DXTabItem, DXTabControl> evaluator = <>c.<>9__53_0;
            if (<>c.<>9__53_0 == null)
            {
                Func<DXTabItem, DXTabControl> local1 = <>c.<>9__53_0;
                evaluator = <>c.<>9__53_0 = x => x.Owner;
            }
            Func<DXTabControl, TabControlStretchView> func2 = <>c.<>9__53_1;
            if (<>c.<>9__53_1 == null)
            {
                Func<DXTabControl, TabControlStretchView> local2 = <>c.<>9__53_1;
                func2 = <>c.<>9__53_1 = x => x.View.StretchView;
            }
            TabControlStretchView input = tab.With<DXTabItem, DXTabControl>(evaluator).With<DXTabControl, TabControlStretchView>(func2);
            Func<TabControlStretchView, TabPanelStretchView> func3 = <>c.<>9__53_2;
            if (<>c.<>9__53_2 == null)
            {
                Func<TabControlStretchView, TabPanelStretchView> local3 = <>c.<>9__53_2;
                func3 = <>c.<>9__53_2 = x => x.StretchPanel;
            }
            Action<TabPanelStretchView> action = <>c.<>9__53_3;
            if (<>c.<>9__53_3 == null)
            {
                Action<TabPanelStretchView> local4 = <>c.<>9__53_3;
                action = <>c.<>9__53_3 = x => x.ForceUpdateLayout();
            }
            input.With<TabControlStretchView, TabPanelStretchView>(func3).Do<TabPanelStretchView>(action);
            Action<TabControlStretchView> action2 = <>c.<>9__53_4;
            if (<>c.<>9__53_4 == null)
            {
                Action<TabControlStretchView> local5 = <>c.<>9__53_4;
                action2 = <>c.<>9__53_4 = x => x.UpdateViewProperties();
            }
            input.Do<TabControlStretchView>(action2);
        }

        protected internal override void OnTabItemClosed(DXTabItem item)
        {
            this.OnTabItemClosedCore(item, true);
        }

        private void OnTabItemClosedCore(DXTabItem item, bool closeOwnerWindowImmediately)
        {
            if ((base.Owner != null) && (base.Owner.VisibleItemsCount <= 0))
            {
                bool flag = true;
                foreach (DXTabControl control in this.GetDragTabControls(true, true))
                {
                    if (control.VisibleItemsCount > 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag && this.CloseWindowOnSingleTabItemHiding)
                {
                    this.CloseWindow(Window.GetWindow(base.Owner), closeOwnerWindowImmediately);
                }
                else if (flag && (base.SingleTabItemHideMode == SingleTabItemHideMode.HideAndShowNewItem))
                {
                    base.Owner.AddNewTabItem();
                }
                else if (flag && ((base.SingleTabItemHideMode == SingleTabItemHideMode.Hide) && this.CloseWindowOnSingleTabItemHiding))
                {
                    this.CloseWindow(Window.GetWindow(base.Owner), closeOwnerWindowImmediately);
                }
            }
        }

        internal void Remove(DXTabItem child)
        {
            try
            {
                this.preventFocusRestoring = true;
                child.Remove();
                this.OnTabItemClosedCore(child, false);
            }
            finally
            {
                this.preventFocusRestoring = false;
            }
        }

        public static void SetPinMode(DXTabItem obj, TabPinMode value)
        {
            obj.SetValue(PinModeProperty, value);
        }

        internal void SetVisibility(DXTabItem child, Visibility visibility)
        {
            if (visibility == Visibility.Visible)
            {
                base.Owner.ShowTabItem(child, false);
            }
            else if (visibility == Visibility.Collapsed)
            {
                base.Owner.HideTabItem(child, false);
            }
            else
            {
                child.Visibility = visibility;
            }
            if ((visibility == Visibility.Collapsed) && ((this.DragDropMode == TabControlDragDropMode.Full) && (base.Owner.VisibleItemsCount == 0)))
            {
                Window.GetWindow(base.Owner).Do<Window>(x => this.CloseWindow(x, false));
            }
        }

        private void UpdateDragDropControls(bool excludeOwner, bool excludeInvisible, Action<TabControlStretchView> updateAction)
        {
            if (!this.lockUpdateDragDropControls)
            {
                Func<DXTabControl, TabControlStretchView> selector = <>c.<>9__69_0;
                if (<>c.<>9__69_0 == null)
                {
                    Func<DXTabControl, TabControlStretchView> local1 = <>c.<>9__69_0;
                    selector = <>c.<>9__69_0 = x => x.View.StretchView;
                }
                IEnumerable<TabControlStretchView> source = this.GetDragTabControls(excludeOwner, excludeInvisible).Select<DXTabControl, TabControlStretchView>(selector);
                Action<TabControlStretchView> action = <>c.<>9__69_1;
                if (<>c.<>9__69_1 == null)
                {
                    Action<TabControlStretchView> local2 = <>c.<>9__69_1;
                    action = <>c.<>9__69_1 = x => x.lockUpdateDragDropControls = true;
                }
                source.ForEach<TabControlStretchView>(action);
                source.ForEach<TabControlStretchView>(x => updateAction(x));
                Action<TabControlStretchView> action2 = <>c.<>9__69_3;
                if (<>c.<>9__69_3 == null)
                {
                    Action<TabControlStretchView> local3 = <>c.<>9__69_3;
                    action2 = <>c.<>9__69_3 = x => x.lockUpdateDragDropControls = false;
                }
                source.ForEach<TabControlStretchView>(action2);
            }
        }

        protected internal override void UpdateViewPropertiesCore()
        {
            if (!this.lockUpdateDragDropControls)
            {
                base.UpdateViewPropertiesCore();
                Action<TabControlStretchView> updateAction = <>c.<>9__55_0;
                if (<>c.<>9__55_0 == null)
                {
                    Action<TabControlStretchView> local1 = <>c.<>9__55_0;
                    updateAction = <>c.<>9__55_0 = x => x.Owner.UpdateViewProperties();
                }
                this.UpdateDragDropControls(true, false, updateAction);
            }
        }

        public bool PinnedTabAllowHide
        {
            get => 
                (bool) base.GetValue(PinnedTabAllowHideProperty);
            set => 
                base.SetValue(PinnedTabAllowHideProperty, value);
        }

        public bool PinnedTabAllowDrag
        {
            get => 
                (bool) base.GetValue(PinnedTabAllowDragProperty);
            set => 
                base.SetValue(PinnedTabAllowDragProperty, value);
        }

        public int PinnedTabSize
        {
            get => 
                (int) base.GetValue(PinnedTabSizeProperty);
            set => 
                base.SetValue(PinnedTabSizeProperty, value);
        }

        public int TabNormalSize
        {
            get => 
                (int) base.GetValue(TabNormalSizeProperty);
            set => 
                base.SetValue(TabNormalSizeProperty, value);
        }

        public int SelectedTabMinSize
        {
            get => 
                (int) base.GetValue(SelectedTabMinSizeProperty);
            set => 
                base.SetValue(SelectedTabMinSizeProperty, value);
        }

        public int TabMinSize
        {
            get => 
                (int) base.GetValue(TabMinSizeProperty);
            set => 
                base.SetValue(TabMinSizeProperty, value);
        }

        public bool MoveItemsWhenDragDrop
        {
            get => 
                (bool) base.GetValue(MoveItemsWhenDragDropProperty);
            set => 
                base.SetValue(MoveItemsWhenDragDropProperty, value);
        }

        public TabControlDragDropMode DragDropMode
        {
            get => 
                (TabControlDragDropMode) base.GetValue(DragDropModeProperty);
            set => 
                base.SetValue(DragDropModeProperty, value);
        }

        public string DragDropRegion
        {
            get => 
                (string) base.GetValue(DragDropRegionProperty);
            set => 
                base.SetValue(DragDropRegionProperty, value);
        }

        public Style NewWindowStyle
        {
            get => 
                (Style) base.GetValue(NewWindowStyleProperty);
            set => 
                base.SetValue(NewWindowStyleProperty, value);
        }

        public Style NewTabControlStyle
        {
            get => 
                (Style) base.GetValue(NewTabControlStyleProperty);
            set => 
                base.SetValue(NewTabControlStyleProperty, value);
        }

        public bool CloseWindowOnSingleTabItemHiding
        {
            get => 
                (bool) base.GetValue(CloseWindowOnSingleTabItemHidingProperty);
            set => 
                base.SetValue(CloseWindowOnSingleTabItemHidingProperty, value);
        }

        protected internal TabPanelStretchView StretchPanel
        {
            get
            {
                Func<DXTabControl, TabPanelContainer> evaluator = <>c.<>9__52_0;
                if (<>c.<>9__52_0 == null)
                {
                    Func<DXTabControl, TabPanelContainer> local1 = <>c.<>9__52_0;
                    evaluator = <>c.<>9__52_0 = x => x.TabPanel;
                }
                Func<TabPanelContainer, TabPanelStretchView> func2 = <>c.<>9__52_1;
                if (<>c.<>9__52_1 == null)
                {
                    Func<TabPanelContainer, TabPanelStretchView> local2 = <>c.<>9__52_1;
                    func2 = <>c.<>9__52_1 = x => x.Panel as TabPanelStretchView;
                }
                return base.Owner.With<DXTabControl, TabPanelContainer>(evaluator).With<TabPanelContainer, TabPanelStretchView>(func2);
            }
        }

        internal TabControlDragWidgetHelper DragWidgetHelper
        {
            get
            {
                TabControlDragWidgetHelper dragWidgetHelper = this.dragWidgetHelper;
                if (this.dragWidgetHelper == null)
                {
                    TabControlDragWidgetHelper local1 = this.dragWidgetHelper;
                    dragWidgetHelper = this.dragWidgetHelper = this.CreateDragWidgetHelper();
                }
                return dragWidgetHelper;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabControlStretchView.<>c <>9 = new TabControlStretchView.<>c();
            public static Func<DXTabControl, TabPanelContainer> <>9__52_0;
            public static Func<TabPanelContainer, TabPanelStretchView> <>9__52_1;
            public static Func<DXTabItem, DXTabControl> <>9__53_0;
            public static Func<DXTabControl, TabControlStretchView> <>9__53_1;
            public static Func<TabControlStretchView, TabPanelStretchView> <>9__53_2;
            public static Action<TabPanelStretchView> <>9__53_3;
            public static Action<TabControlStretchView> <>9__53_4;
            public static Func<DXTabItem, bool> <>9__54_0;
            public static Func<DXTabItem, bool> <>9__54_1;
            public static Func<DXTabItem, bool> <>9__54_2;
            public static Action<TabControlStretchView> <>9__55_0;
            public static Func<DXTabControl, bool> <>9__67_1;
            public static Func<DXTabControl, TabControlStretchView> <>9__69_0;
            public static Action<TabControlStretchView> <>9__69_1;
            public static Action<TabControlStretchView> <>9__69_3;
            public static Action<TabControlStretchView> <>9__71_0;

            internal void <.cctor>b__83_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                TabControlStretchView.OnPinModeChanged((DXTabItem) d, (TabPinMode) e.OldValue, (TabPinMode) e.NewValue);
            }

            internal void <.cctor>b__83_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlStretchView) d).UpdateViewProperties();
            }

            internal void <.cctor>b__83_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlStretchView) d).OnDragDropRegionChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal bool <CoerceSelection>b__54_0(DXTabItem x) => 
                TabControlStretchView.GetPinMode(x) == TabPinMode.None;

            internal bool <CoerceSelection>b__54_1(DXTabItem x) => 
                TabControlStretchView.GetPinMode(x) == TabPinMode.Left;

            internal bool <CoerceSelection>b__54_2(DXTabItem x) => 
                TabControlStretchView.GetPinMode(x) == TabPinMode.Right;

            internal TabPanelContainer <get_StretchPanel>b__52_0(DXTabControl x) => 
                x.TabPanel;

            internal TabPanelStretchView <get_StretchPanel>b__52_1(TabPanelContainer x) => 
                x.Panel as TabPanelStretchView;

            internal bool <GetDragTabControls>b__67_1(DXTabControl x) => 
                x.IsVisible;

            internal void <OnDragFinished>b__71_0(TabControlStretchView x)
            {
                x.CloseWindowsToClose();
            }

            internal DXTabControl <OnPinModeChanged>b__53_0(DXTabItem x) => 
                x.Owner;

            internal TabControlStretchView <OnPinModeChanged>b__53_1(DXTabControl x) => 
                x.View.StretchView;

            internal TabPanelStretchView <OnPinModeChanged>b__53_2(TabControlStretchView x) => 
                x.StretchPanel;

            internal void <OnPinModeChanged>b__53_3(TabPanelStretchView x)
            {
                x.ForceUpdateLayout();
            }

            internal void <OnPinModeChanged>b__53_4(TabControlStretchView x)
            {
                x.UpdateViewProperties();
            }

            internal TabControlStretchView <UpdateDragDropControls>b__69_0(DXTabControl x) => 
                x.View.StretchView;

            internal void <UpdateDragDropControls>b__69_1(TabControlStretchView x)
            {
                x.lockUpdateDragDropControls = true;
            }

            internal void <UpdateDragDropControls>b__69_3(TabControlStretchView x)
            {
                x.lockUpdateDragDropControls = false;
            }

            internal void <UpdateViewPropertiesCore>b__55_0(TabControlStretchView x)
            {
                x.Owner.UpdateViewProperties();
            }
        }
    }
}

