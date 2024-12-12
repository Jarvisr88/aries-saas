namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class DocumentGroup : TabbedGroup
    {
        public static readonly DependencyProperty MDIStyleProperty;
        public static readonly DependencyProperty ClosePageButtonShowModeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty LastClosePageButtonShowModeInternalProperty;
        public static readonly DependencyProperty ShowWhenEmptyProperty;
        public static readonly DependencyProperty PinLocationProperty;
        public static readonly DependencyProperty PinnedProperty;
        public static readonly DependencyProperty ShowPinButtonProperty;
        public static readonly DependencyProperty TabbedGroupDisplayModeProperty;

        static DocumentGroup()
        {
            DockingStrategyRegistrator.RegisterDocumentGroupStrategy();
            DependencyPropertyRegistrator<DocumentGroup> registrator = new DependencyPropertyRegistrator<DocumentGroup>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowFloatProperty, false, null, null);
            registrator.Register<DevExpress.Xpf.Docking.MDIStyle>("MDIStyle", ref MDIStyleProperty, DevExpress.Xpf.Docking.MDIStyle.Default, (dObj, e) => ((DocumentGroup) dObj).OnMDIStyleChanged((DevExpress.Xpf.Docking.MDIStyle) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.ClosePageButtonShowMode>("ClosePageButtonShowMode", ref ClosePageButtonShowModeProperty, DevExpress.Xpf.Docking.ClosePageButtonShowMode.Default, (dObj, e) => ((DocumentGroup) dObj).OnClosePageButtonShowModeChanged((DevExpress.Xpf.Docking.ClosePageButtonShowMode) e.NewValue), null);
            registrator.RegisterAttached<DevExpress.Xpf.Docking.ClosePageButtonShowMode>("LastClosePageButtonShowModeInternal", ref LastClosePageButtonShowModeInternalProperty, DevExpress.Xpf.Docking.ClosePageButtonShowMode.Default, null, null);
            registrator.OverrideMetadata<DataTemplateSelector>(LayoutGroup.GroupTemplateSelectorProperty, new DefaultTemplateSelector(), (dObj, e) => ((DocumentGroup) dObj).OnGroupTemplateChanged(), null);
            registrator.Register<bool>("ShowWhenEmpty", ref ShowWhenEmptyProperty, false, (dObj, e) => ((DocumentGroup) dObj).OnShowEmptyChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.TabbedGroupDisplayMode>("TabbedGroupDisplayMode", ref TabbedGroupDisplayModeProperty, DevExpress.Xpf.Docking.TabbedGroupDisplayMode.Default, null, null);
            registrator.RegisterAttached<TabHeaderPinLocation>("PinLocation", ref PinLocationProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TabHeaderPinLocation.Default, null, null);
            registrator.RegisterAttached<bool>("Pinned", ref PinnedProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, false, null, null);
            registrator.RegisterAttached<bool>("ShowPinButton", ref ShowPinButtonProperty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, false, null, null);
        }

        public DocumentGroup() : this(new BaseLayoutItem[0])
        {
        }

        internal DocumentGroup(params BaseLayoutItem[] items) : base(items)
        {
            this.Forward(this, LayoutGroup.TabbedGroupDisplayModeCoreProperty, TabbedGroupDisplayModeProperty, BindingMode.OneWay);
        }

        protected internal override void AfterItemAdded(int index, BaseLayoutItem item)
        {
            base.AfterItemAdded(index, item);
            if (!this.IsTabbed && base.IsMaximized)
            {
                if (item is DocumentPanel)
                {
                    DocumentPanel.SetMDIState(item, MDIState.Maximized);
                }
                LayoutItemsHelper.UpdateZIndexes(this);
            }
        }

        protected internal override void AfterItemRemoved(BaseLayoutItem item)
        {
            base.AfterItemRemoved(item);
            item.SetValue(LastClosePageButtonShowModeInternalProperty, this.ClosePageButtonShowMode);
            DocumentPanel panel = item as DocumentPanel;
            if (panel != null)
            {
                panel.IsMDIChild = false;
            }
            if (base.IsMaximized && !this.ContainsMaximizedDocument())
            {
                base.IsMaximized = false;
            }
        }

        protected internal override void BeforeItemAdded(BaseLayoutItem item)
        {
            base.BeforeItemAdded(item);
            DocumentPanel panel = item as DocumentPanel;
            if (panel != null)
            {
                panel.IsMDIChild = this.MDIStyle == DevExpress.Xpf.Docking.MDIStyle.MDI;
            }
        }

        protected override void BeforeItemRemoved(BaseLayoutItem item)
        {
            if (this.MDIStyle != DevExpress.Xpf.Docking.MDIStyle.MDI)
            {
                base.BeforeItemRemoved(item);
            }
            else
            {
                Func<BaseLayoutItem, int> keySelector = <>c.<>9__73_0;
                if (<>c.<>9__73_0 == null)
                {
                    Func<BaseLayoutItem, int> local1 = <>c.<>9__73_0;
                    keySelector = <>c.<>9__73_0 = x => x.ZIndex;
                }
                BaseLayoutItem[] array = base.Items.OrderBy<BaseLayoutItem, int>(keySelector).ToArray<BaseLayoutItem>();
                item.TabIndexBeforeRemove = Array.IndexOf<BaseLayoutItem>(array, item);
            }
        }

        protected override Size CalcMaxSizeValue(Size value) => 
            this.IsTabbed ? base.CalcMaxSizeValue(value) : value;

        protected override Size CalcMinSizeValue(Size value) => 
            this.IsTabbed ? base.CalcMinSizeValue(value) : value;

        protected bool CanShowCloseButtonForSelectedItem() => 
            (base.SelectedItem != null) && (base.SelectedItem.AllowClose && base.SelectedItem.ShowCloseButton);

        protected bool CanShowRestoreButtonForSelectedItem() => 
            (base.SelectedItem is DocumentPanel) && ((DocumentPanel) base.SelectedItem).ShowRestoreButton;

        protected internal void ChangeMDIStyle()
        {
            if (this.IsTabbed)
            {
                this.MDIStyle = DevExpress.Xpf.Docking.MDIStyle.MDI;
            }
            else
            {
                base.ClearValue(MDIStyleProperty);
            }
            LayoutItemsHelper.UpdateZIndexes(this);
        }

        protected override bool CoerceDestroyOnClosingChildren(bool value) => 
            this.IsTabbed & value;

        protected override bool CoerceIsCloseButtonVisible(bool value)
        {
            if (!this.IsTabbed)
            {
                return (this.ContainsMaximizedDocument() && this.CanShowCloseButtonForSelectedItem());
            }
            switch (this.ClosePageButtonShowMode)
            {
                case DevExpress.Xpf.Docking.ClosePageButtonShowMode.InAllTabPageHeaders:
                case DevExpress.Xpf.Docking.ClosePageButtonShowMode.InActiveTabPageHeader:
                case DevExpress.Xpf.Docking.ClosePageButtonShowMode.NoWhere:
                    return false;
            }
            return this.CanShowCloseButtonForSelectedItem();
        }

        protected override bool CoerceIsDropDownButtonVisible(bool visible) => 
            this.HasTabHeader() && ((base.Items.Count > 0) && this.ShowDropDownButton);

        protected override bool CoerceIsRestoreButtonVisible(bool visible) => 
            !this.IsTabbed && (this.ContainsMaximizedDocument() && (this.CanShowRestoreButtonForSelectedItem() && this.ShowRestoreButton));

        protected override TabHeaderLayoutType CoerceTabHeaderLayoutType(TabHeaderLayoutType value) => 
            (value == TabHeaderLayoutType.Default) ? TabHeaderLayoutType.Scroll : value;

        private bool ContainsMaximizedDocument()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (MDIStateHelper.GetMDIState(current) != MDIState.Maximized)
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

        internal override ContentItem CreateContentItem(object content) => 
            !base.IsControlItemsHost ? ((base.Manager != null) ? base.Manager.CreateDocumentPanel() : new DocumentPanel()) : base.CreateContentItem(content);

        protected override LayoutGroup GetContainerHost(BaseLayoutItem container)
        {
            LayoutGroup group = null;
            if (!base.HasNotCollapsedItems && ((base.Parent != null) && base.Parent.Items.IsDocumentHost(true)))
            {
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__68_0;
                if (<>c.<>9__68_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c.<>9__68_0;
                    predicate = <>c.<>9__68_0 = item => (item is LayoutPanel) && (item.Parent is DocumentGroup);
                }
                Func<BaseLayoutItem, DateTime> keySelector = <>c.<>9__68_1;
                if (<>c.<>9__68_1 == null)
                {
                    Func<BaseLayoutItem, DateTime> local2 = <>c.<>9__68_1;
                    keySelector = <>c.<>9__68_1 = item => ((LayoutPanel) item).LastActivationDateTime;
                }
                BaseLayoutItem item = base.Parent.GetNestedItems().Where<BaseLayoutItem>(predicate).OrderBy<BaseLayoutItem, DateTime>(keySelector).LastOrDefault<BaseLayoutItem>();
                if (item != null)
                {
                    DocumentGroup parent = item.Parent as DocumentGroup;
                    if ((parent != null) && parent.IsAutoGenerated)
                    {
                        group = item.Parent;
                    }
                }
            }
            return (group ?? this);
        }

        protected internal override bool GetIsDocumentHost() => 
            (base.Manager != null) && ((base.Manager.DockingStyle != DockingStyle.Default) && (this.IsTabbed && (base.Parent != null)));

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.DocumentPanelGroup;

        protected override int GetNextTabIndex(BaseLayoutItem item) => 
            (this.MDIStyle != DevExpress.Xpf.Docking.MDIStyle.MDI) ? base.GetNextTabIndex(item) : base.TabIndexFromItem(this.GetNextTabItem(base.Items.ToArray<BaseLayoutItem>(), item));

        private BaseLayoutItem GetNextTabItem(BaseLayoutItem[] items, BaseLayoutItem item)
        {
            Func<BaseLayoutItem, int> keySelector = <>c.<>9__81_0;
            if (<>c.<>9__81_0 == null)
            {
                Func<BaseLayoutItem, int> local1 = <>c.<>9__81_0;
                keySelector = <>c.<>9__81_0 = x => x.ZIndex;
            }
            items = items.Concat<BaseLayoutItem>(item.Yield<BaseLayoutItem>()).OrderBy<BaseLayoutItem, int>(keySelector).ToArray<BaseLayoutItem>();
            int index = Array.IndexOf<BaseLayoutItem>(items, item) - 1;
            return (items.IsValidIndex<BaseLayoutItem>(index) ? items[index] : null);
        }

        [XtraSerializableProperty]
        public static TabHeaderPinLocation GetPinLocation(DependencyObject obj) => 
            (TabHeaderPinLocation) obj.GetValue(PinLocationProperty);

        [XtraSerializableProperty]
        public static bool GetPinned(DependencyObject obj) => 
            (bool) obj.GetValue(PinnedProperty);

        public static bool GetShowPinButton(DependencyObject obj) => 
            (bool) obj.GetValue(ShowPinButtonProperty);

        protected override bool HasTabHeader() => 
            this.IsTabbed;

        protected override bool IsItemItsOwnContainer(object item) => 
            item is LayoutPanel;

        protected virtual void OnClosePageButtonShowModeChanged(DevExpress.Xpf.Docking.ClosePageButtonShowMode style)
        {
            base.OnLayoutChanged();
            foreach (BaseLayoutItem item in base.Items)
            {
                item.CoerceValue(BaseLayoutItem.IsCloseButtonVisibleProperty);
            }
        }

        protected override void OnIsMaximizedChanged(bool maximized)
        {
            base.CoerceValue(LayoutGroup.SelectedItemProperty);
        }

        protected virtual void OnMDIStyleChanged(DevExpress.Xpf.Docking.MDIStyle style)
        {
            base.CoerceValue(LayoutGroup.DestroyOnClosingChildrenProperty);
            base.CoerceValue(LayoutGroup.SelectedItemProperty);
            foreach (BaseLayoutItem item in base.Items)
            {
                item.CoerceValue(BaseLayoutItem.IsTabPageProperty);
            }
            if (this.IsTabbed)
            {
                DockLayoutManager manager = this.FindDockLayoutManager();
                if ((manager != null) && (manager.ActiveMDIItem != null))
                {
                    if (base.Items.Contains(manager.ActiveMDIItem))
                    {
                        manager.ActiveMDIItem = null;
                    }
                    if (base.Items.Contains(manager.ActiveDockItem))
                    {
                        manager.ActiveDockItem = null;
                    }
                }
            }
            base.OnLayoutChanged();
            this.Update();
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            Action<LayoutGroup> action = <>c.<>9__76_0;
            if (<>c.<>9__76_0 == null)
            {
                Action<LayoutGroup> local1 = <>c.<>9__76_0;
                action = <>c.<>9__76_0 = x => x.CoerceValue(LayoutGroup.HasNotCollapsedItemsProperty);
            }
            base.Parent.Do<LayoutGroup>(action);
        }

        protected override void OnSelectedItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnSelectedItemChanged(item, oldItem);
            base.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
            if (this.ContainsMaximizedDocument() && ((item != null) && !item.AllowMaximize))
            {
                MDIControllerHelper.Restore(item);
            }
        }

        protected virtual void OnShowEmptyChanged(bool oldValue, bool newValue)
        {
            Action<LayoutGroup> action = <>c.<>9__78_0;
            if (<>c.<>9__78_0 == null)
            {
                Action<LayoutGroup> local1 = <>c.<>9__78_0;
                action = <>c.<>9__78_0 = x => x.CoerceValue(LayoutGroup.HasNotCollapsedItemsProperty);
            }
            base.Parent.Do<LayoutGroup>(action);
        }

        public static void SetPinLocation(DependencyObject obj, TabHeaderPinLocation value)
        {
            obj.SetValue(PinLocationProperty, value);
        }

        public static void SetPinned(DependencyObject obj, bool value)
        {
            obj.SetValue(PinnedProperty, value);
        }

        public static void SetShowPinButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowPinButtonProperty, value);
        }

        protected internal override void UpdateButtons()
        {
            base.UpdateButtons();
            base.CoerceValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);
            base.CoerceValue(BaseLayoutItem.IsDropDownButtonVisibleProperty);
        }

        [Description("Gets or sets whether Close buttons are displayed in individual tab pages and the tab container's header. This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public DevExpress.Xpf.Docking.ClosePageButtonShowMode ClosePageButtonShowMode
        {
            get => 
                (DevExpress.Xpf.Docking.ClosePageButtonShowMode) base.GetValue(ClosePageButtonShowModeProperty);
            set => 
                base.SetValue(ClosePageButtonShowModeProperty, value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override DevExpress.Xpf.Docking.GroupBorderStyle GroupBorderStyle
        {
            get => 
                base.GroupBorderStyle;
            set
            {
            }
        }

        [Description("Gets whether the DropDown button is displayed within the DocumentGroup's header.")]
        public bool IsDropDownButtonVisible =>
            (bool) base.GetValue(BaseLayoutItem.IsDropDownButtonVisibleProperty);

        [Description("Gets whether the DocumentGroup displays the Restore button.This is a dependency property.")]
        public bool IsRestoreButtonVisible =>
            (bool) base.GetValue(BaseLayoutItem.IsRestoreButtonVisibleProperty);

        [Description("Gets or sets how the DocumentGroup presents its child panels, as floaing windows, or using the tabbed UI.This is a dependency property."), XtraSerializableProperty, Category("Content")]
        public DevExpress.Xpf.Docking.MDIStyle MDIStyle
        {
            get => 
                (DevExpress.Xpf.Docking.MDIStyle) base.GetValue(MDIStyleProperty);
            set => 
                base.SetValue(MDIStyleProperty, value);
        }

        [Description("Gets or sets whether the DropDown button is visible within the current DocumentGroup."), XtraSerializableProperty, Category("Layout")]
        public bool ShowDropDownButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowDropDownButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowDropDownButtonProperty, value);
        }

        [Description("Gets or sets whether the Restore button is displayed within the DocumentGroup's title, allowing an end-user to restore a maximized panel to its normal state.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool ShowRestoreButton
        {
            get => 
                (bool) base.GetValue(BaseLayoutItem.ShowRestoreButtonProperty);
            set => 
                base.SetValue(BaseLayoutItem.ShowRestoreButtonProperty, value);
        }

        [Description("Gets or sets whether the DocumentGroup is displayed when it is empty.")]
        public bool ShowWhenEmpty
        {
            get => 
                (bool) base.GetValue(ShowWhenEmptyProperty);
            set => 
                base.SetValue(ShowWhenEmptyProperty, value);
        }

        [Description("Contains values that specify the DocumentPanel header displaying mode.")]
        public DevExpress.Xpf.Docking.TabbedGroupDisplayMode TabbedGroupDisplayMode
        {
            get => 
                (DevExpress.Xpf.Docking.TabbedGroupDisplayMode) base.GetValue(TabbedGroupDisplayModeProperty);
            set => 
                base.SetValue(TabbedGroupDisplayModeProperty, value);
        }

        protected internal bool IsTabbed =>
            this.MDIStyle != DevExpress.Xpf.Docking.MDIStyle.MDI;

        protected internal Size MDIAreaSize { get; set; }

        protected internal double MDIDocumentHeaderHeight { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentGroup.<>c <>9 = new DocumentGroup.<>c();
            public static Func<BaseLayoutItem, bool> <>9__68_0;
            public static Func<BaseLayoutItem, DateTime> <>9__68_1;
            public static Func<BaseLayoutItem, int> <>9__73_0;
            public static Action<LayoutGroup> <>9__76_0;
            public static Action<LayoutGroup> <>9__78_0;
            public static Func<BaseLayoutItem, int> <>9__81_0;

            internal void <.cctor>b__8_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentGroup) dObj).OnMDIStyleChanged((MDIStyle) e.NewValue);
            }

            internal void <.cctor>b__8_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentGroup) dObj).OnClosePageButtonShowModeChanged((ClosePageButtonShowMode) e.NewValue);
            }

            internal void <.cctor>b__8_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentGroup) dObj).OnGroupTemplateChanged();
            }

            internal void <.cctor>b__8_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentGroup) dObj).OnShowEmptyChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal int <BeforeItemRemoved>b__73_0(BaseLayoutItem x) => 
                x.ZIndex;

            internal bool <GetContainerHost>b__68_0(BaseLayoutItem item) => 
                (item is LayoutPanel) && (item.Parent is DocumentGroup);

            internal DateTime <GetContainerHost>b__68_1(BaseLayoutItem item) => 
                ((LayoutPanel) item).LastActivationDateTime;

            internal int <GetNextTabItem>b__81_0(BaseLayoutItem x) => 
                x.ZIndex;

            internal void <OnParentChanged>b__76_0(LayoutGroup x)
            {
                x.CoerceValue(LayoutGroup.HasNotCollapsedItemsProperty);
            }

            internal void <OnShowEmptyChanged>b__78_0(LayoutGroup x)
            {
                x.CoerceValue(LayoutGroup.HasNotCollapsedItemsProperty);
            }
        }

        private class DefaultTemplateSelector : DefaultItemTemplateSelectorWrapper.DefaultItemTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                DocumentPaneContentPresenter presenter = container as DocumentPaneContentPresenter;
                DocumentGroup group = item as DocumentGroup;
                if ((group != null) && ((presenter != null) && (presenter.Owner != null)))
                {
                    switch (group.MDIStyle)
                    {
                        case MDIStyle.Default:
                        case MDIStyle.Tabbed:
                            return presenter.Owner.TabbedTemplate;

                        case MDIStyle.MDI:
                            return presenter.Owner.MDITemplate;

                        default:
                            break;
                    }
                }
                return null;
            }
        }
    }
}

