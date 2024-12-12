namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class TabControlViewBase : Freezable
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyPropertyKey OwnerPropertyKey;
        public static readonly DependencyProperty OwnerProperty;
        public static readonly DependencyProperty AllowKeyboardNavigationProperty;
        public static readonly DependencyProperty TagProperty;
        public static readonly DependencyProperty HeaderLocationProperty;
        public static readonly DependencyProperty ShowHeaderMenuProperty;
        public static readonly DependencyProperty ShowVisibleTabItemsInHeaderMenuProperty;
        public static readonly DependencyProperty ShowHiddenTabItemsInHeaderMenuProperty;
        public static readonly DependencyProperty ShowDisabledTabItemsInHeaderMenuProperty;
        public static readonly DependencyProperty CloseHeaderMenuOnItemSelectingProperty;
        public static readonly DependencyProperty NewTabCommandProperty;
        public static readonly DependencyProperty NewTabCommandParameterProperty;
        public static readonly DependencyProperty NewButtonShowModeProperty;
        private static readonly DependencyPropertyKey MainNewButtonVisibilityPropertyKey;
        private static readonly DependencyPropertyKey PanelNewButtonVisibilityPropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never), IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty MainNewButtonVisibilityProperty;
        [IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty PanelNewButtonVisibilityProperty;
        public static readonly DependencyProperty HideButtonShowModeProperty;
        public static readonly DependencyProperty SingleTabItemHideModeProperty;
        public static readonly DependencyProperty RemoveTabItemsOnHidingProperty;
        private ControllerBehavior headerMenuCustomizationController;
        private bool lockNewTabControlCanExectureChanged;

        static TabControlViewBase()
        {
            OwnerPropertyKey = DependencyProperty.RegisterReadOnly("Owner", typeof(DXTabControl), typeof(TabControlViewBase), new PropertyMetadata(null, (d, e) => ((TabControlViewBase) d).OnOwnerChanged((DXTabControl) e.OldValue, (DXTabControl) e.NewValue)));
            OwnerProperty = OwnerPropertyKey.DependencyProperty;
            AllowKeyboardNavigationProperty = DependencyProperty.Register("AllowKeyboardNavigation", typeof(bool?), typeof(TabControlViewBase), new PropertyMetadata(null));
            TagProperty = FrameworkElement.TagProperty.AddOwner(typeof(TabControlViewBase));
            HeaderLocationProperty = DependencyProperty.Register("HeaderLocation", typeof(DevExpress.Xpf.Core.HeaderLocation), typeof(TabControlViewBase), new PropertyMetadata(DevExpress.Xpf.Core.HeaderLocation.Top, (d, e) => ((TabControlViewBase) d).UpdateViewProperties()));
            ShowHeaderMenuProperty = DependencyProperty.Register("ShowHeaderMenu", typeof(bool), typeof(TabControlViewBase), new PropertyMetadata(false));
            ShowVisibleTabItemsInHeaderMenuProperty = DependencyProperty.Register("ShowVisibleTabItemsInHeaderMenu", typeof(bool), typeof(TabControlViewBase), new PropertyMetadata(true, (d, e) => ((TabControlViewBase) d).UpdateViewProperties()));
            ShowHiddenTabItemsInHeaderMenuProperty = DependencyProperty.Register("ShowHiddenTabItemsInHeaderMenu", typeof(bool), typeof(TabControlViewBase), new PropertyMetadata(false, (d, e) => ((TabControlViewBase) d).UpdateViewProperties()));
            ShowDisabledTabItemsInHeaderMenuProperty = DependencyProperty.Register("ShowDisabledTabItemsInHeaderMenu", typeof(bool), typeof(TabControlViewBase), new PropertyMetadata(false, (d, e) => ((TabControlViewBase) d).UpdateViewProperties()));
            CloseHeaderMenuOnItemSelectingProperty = DependencyProperty.Register("CloseHeaderMenuOnItemSelecting", typeof(bool), typeof(TabControlViewBase), new PropertyMetadata(false));
            NewTabCommandProperty = DependencyProperty.Register("NewTabCommand", typeof(ICommand), typeof(TabControlViewBase), new PropertyMetadata(null, (d, e) => ((TabControlViewBase) d).OnNewTabCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue)));
            NewTabCommandParameterProperty = DependencyProperty.Register("NewTabCommandParameter", typeof(object), typeof(TabControlViewBase), new PropertyMetadata(null));
            NewButtonShowModeProperty = DependencyProperty.Register("NewButtonShowMode", typeof(DevExpress.Xpf.Core.NewButtonShowMode), typeof(TabControlViewBase), new PropertyMetadata(DevExpress.Xpf.Core.NewButtonShowMode.NoWhere, (d, e) => ((TabControlViewBase) d).UpdateNewButtonsVisibility()));
            MainNewButtonVisibilityPropertyKey = DependencyProperty.RegisterReadOnly("MainNewButtonVisibility", typeof(Visibility), typeof(TabControlViewBase), null);
            PanelNewButtonVisibilityPropertyKey = DependencyProperty.RegisterReadOnly("PanelNewButtonVisibility", typeof(Visibility), typeof(TabControlViewBase), null);
            MainNewButtonVisibilityProperty = MainNewButtonVisibilityPropertyKey.DependencyProperty;
            PanelNewButtonVisibilityProperty = PanelNewButtonVisibilityPropertyKey.DependencyProperty;
            HideButtonShowModeProperty = DependencyProperty.Register("HideButtonShowMode", typeof(DevExpress.Xpf.Core.HideButtonShowMode), typeof(TabControlViewBase), new PropertyMetadata(DevExpress.Xpf.Core.HideButtonShowMode.NoWhere, (d, e) => ((TabControlViewBase) d).UpdateViewProperties()));
            SingleTabItemHideModeProperty = DependencyProperty.Register("SingleTabItemHideMode", typeof(DevExpress.Xpf.Core.SingleTabItemHideMode), typeof(TabControlViewBase), new PropertyMetadata(DevExpress.Xpf.Core.SingleTabItemHideMode.Hide));
            RemoveTabItemsOnHidingProperty = DependencyProperty.Register("RemoveTabItemsOnHiding", typeof(bool), typeof(TabControlViewBase), new PropertyMetadata(false));
        }

        public TabControlViewBase()
        {
            this.CloseTabCommand = new DelegateCommand(delegate {
                Func<DXTabControl, DXTabItem> evaluator = <>c.<>9__88_1;
                if (<>c.<>9__88_1 == null)
                {
                    Func<DXTabControl, DXTabItem> local1 = <>c.<>9__88_1;
                    evaluator = <>c.<>9__88_1 = x => x.SelectedContainer;
                }
                Action<DXTabItem> action = <>c.<>9__88_2;
                if (<>c.<>9__88_2 == null)
                {
                    Action<DXTabItem> local2 = <>c.<>9__88_2;
                    action = <>c.<>9__88_2 = x => x.Close();
                }
                this.Owner.With<DXTabControl, DXTabItem>(evaluator).Do<DXTabItem>(action);
            }, delegate {
                Func<DXTabControl, DXTabItem> evaluator = <>c.<>9__88_4;
                if (<>c.<>9__88_4 == null)
                {
                    Func<DXTabControl, DXTabItem> local1 = <>c.<>9__88_4;
                    evaluator = <>c.<>9__88_4 = x => x.SelectedContainer;
                }
                Func<DXTabItem, bool> func2 = <>c.<>9__88_5;
                if (<>c.<>9__88_5 == null)
                {
                    Func<DXTabItem, bool> local2 = <>c.<>9__88_5;
                    func2 = <>c.<>9__88_5 = x => x.CanClose();
                }
                return this.Owner.With<DXTabControl, DXTabItem>(evaluator).Return<DXTabItem, bool>(func2, <>c.<>9__88_6 ??= () => false);
            }, false);
            this.ActualNewTabCommand = new DelegateCommand(new Action(this.AddNewTabItem), new Func<bool>(this.CanAddNewTabItem), false);
            this.ActualNewTabCommand.RaiseCanExecuteChanged();
        }

        internal void AddNewTabItem()
        {
            if (this.CanAddNewTabItem())
            {
                if (this.NewTabCommand != null)
                {
                    this.NewTabCommand.Execute(this.NewTabCommandParameter);
                }
                else
                {
                    Action<DXTabControl> action = <>c.<>9__99_0;
                    if (<>c.<>9__99_0 == null)
                    {
                        Action<DXTabControl> local1 = <>c.<>9__99_0;
                        action = <>c.<>9__99_0 = x => x.AddNewTabItem();
                    }
                    this.Owner.Do<DXTabControl>(action);
                }
            }
        }

        internal void Assign(DXTabControl owner)
        {
            this.Owner = owner;
        }

        private bool CanAddNewTabItem()
        {
            bool flag;
            if (this.NewTabCommand == null)
            {
                return true;
            }
            this.lockNewTabControlCanExectureChanged = true;
            try
            {
                flag = this.NewTabCommand.CanExecute(this.NewTabCommandParameter);
            }
            finally
            {
                this.lockNewTabControlCanExectureChanged = false;
            }
            return flag;
        }

        protected internal virtual bool CanCloseTabItem(DXTabItem item)
        {
            if ((this.Owner == null) || (this.Owner.VisibleItemsCount > 1))
            {
                return true;
            }
            switch (this.SingleTabItemHideMode)
            {
                case DevExpress.Xpf.Core.SingleTabItemHideMode.Hide:
                    return true;

                case DevExpress.Xpf.Core.SingleTabItemHideMode.HideAndShowNewItem:
                    return !item.IsNew;
            }
            return false;
        }

        protected override void CloneCore(Freezable sourceFreezable)
        {
            base.CloneCore(sourceFreezable);
            this.Owner = null;
        }

        protected internal virtual int CoerceSelection(int index, NotifyCollectionChangedAction? originativeAction) => 
            (this.Owner != null) ? this.Owner.GetCoercedSelectedIndexCore(this.Owner.GetContainers(), index) : index;

        private ControllerBehavior CreateHeaderMenuCustomizationController()
        {
            ControllerBehavior behavior = new ControllerBehavior();
            behavior.Actions.CollectionChanged += delegate (object d, NotifyCollectionChangedEventArgs e) {
                this.UpdateViewProperties();
            };
            this.Owner.Do<DXTabControl>(new Action<DXTabControl>(behavior.Attach));
            return behavior;
        }

        protected override Freezable CreateInstanceCore() => 
            (Freezable) Activator.CreateInstance(base.GetType());

        protected override bool FreezeCore(bool isChecking) => 
            false;

        private void OnNewTabCommandCanExecuteChanged(object sender, EventArgs e)
        {
            if (!this.lockNewTabControlCanExectureChanged)
            {
                this.ActualNewTabCommand.RaiseCanExecuteChanged();
            }
        }

        private void OnNewTabCommandChanged(ICommand oldValue, ICommand newValue)
        {
            oldValue.Do<ICommand>(delegate (ICommand x) {
                x.CanExecuteChanged -= new EventHandler(this.OnNewTabCommandCanExecuteChanged);
            });
            newValue.Do<ICommand>(delegate (ICommand x) {
                x.CanExecuteChanged += new EventHandler(this.OnNewTabCommandCanExecuteChanged);
                this.OnNewTabCommandCanExecuteChanged(null, null);
            });
        }

        protected virtual void OnOwnerChanged(DXTabControl oldValue, DXTabControl newValue)
        {
            oldValue.Do<DXTabControl>(x => this.HeaderMenuCustomizationController.Detach());
            newValue.Do<DXTabControl>(x => this.HeaderMenuCustomizationController.Attach(x));
        }

        protected internal virtual void OnTabItemClosed(DXTabItem item)
        {
            if (((this.Owner != null) && (this.Owner.VisibleItemsCount <= 0)) && (this.SingleTabItemHideMode == DevExpress.Xpf.Core.SingleTabItemHideMode.HideAndShowNewItem))
            {
                this.Owner.AddNewTabItem();
            }
        }

        private void UpdateNewButtonsVisibility()
        {
            switch (this.NewButtonShowMode)
            {
                case DevExpress.Xpf.Core.NewButtonShowMode.InHeaderArea:
                    base.SetValue(MainNewButtonVisibilityPropertyKey, Visibility.Visible);
                    base.SetValue(PanelNewButtonVisibilityPropertyKey, Visibility.Collapsed);
                    return;

                case DevExpress.Xpf.Core.NewButtonShowMode.InTabPanel:
                    base.SetValue(MainNewButtonVisibilityPropertyKey, Visibility.Collapsed);
                    base.SetValue(PanelNewButtonVisibilityPropertyKey, Visibility.Visible);
                    return;

                case DevExpress.Xpf.Core.NewButtonShowMode.InHeaderAreaAndTabPanel:
                    base.SetValue(MainNewButtonVisibilityPropertyKey, Visibility.Visible);
                    base.SetValue(PanelNewButtonVisibilityPropertyKey, Visibility.Visible);
                    return;
            }
            base.SetValue(MainNewButtonVisibilityPropertyKey, Visibility.Collapsed);
            base.SetValue(PanelNewButtonVisibilityPropertyKey, Visibility.Collapsed);
        }

        protected void UpdateViewProperties()
        {
            Action<DXTabControl> action = <>c.<>9__92_0;
            if (<>c.<>9__92_0 == null)
            {
                Action<DXTabControl> local1 = <>c.<>9__92_0;
                action = <>c.<>9__92_0 = x => x.UpdateViewProperties();
            }
            this.Owner.Do<DXTabControl>(action);
        }

        protected internal virtual void UpdateViewPropertiesCore()
        {
            this.UpdateNewButtonsVisibility();
            this.CloseTabCommand.RaiseCanExecuteChanged();
        }

        public bool? AllowKeyboardNavigation
        {
            get => 
                (bool?) base.GetValue(AllowKeyboardNavigationProperty);
            set => 
                base.SetValue(AllowKeyboardNavigationProperty, value);
        }

        public object Tag
        {
            get => 
                base.GetValue(TagProperty);
            set => 
                base.SetValue(TagProperty, value);
        }

        [Description("Gets or sets the location of the Header Panel, relative to the tab item. This is a dependency property.")]
        public DevExpress.Xpf.Core.HeaderLocation HeaderLocation
        {
            get => 
                (DevExpress.Xpf.Core.HeaderLocation) base.GetValue(HeaderLocationProperty);
            set => 
                base.SetValue(HeaderLocationProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<IControllerAction> HeaderMenuCustomizations =>
            this.HeaderMenuCustomizationController.Actions;

        [Description("Gets or sets whether the arrow button used to invoke the header menu is visible. This is a dependency property.")]
        public bool ShowHeaderMenu
        {
            get => 
                (bool) base.GetValue(ShowHeaderMenuProperty);
            set => 
                base.SetValue(ShowHeaderMenuProperty, value);
        }

        [Description("Gets or sets whether to show visible tab items in the header menu. This is a dependency property.")]
        public bool ShowVisibleTabItemsInHeaderMenu
        {
            get => 
                (bool) base.GetValue(ShowVisibleTabItemsInHeaderMenuProperty);
            set => 
                base.SetValue(ShowVisibleTabItemsInHeaderMenuProperty, value);
        }

        [Description("Gets or sets whether to show hidden tab items in the header menu. This is a dependency property.")]
        public bool ShowHiddenTabItemsInHeaderMenu
        {
            get => 
                (bool) base.GetValue(ShowHiddenTabItemsInHeaderMenuProperty);
            set => 
                base.SetValue(ShowHiddenTabItemsInHeaderMenuProperty, value);
        }

        [Description("Gets or sets whether to show disabled tab items in the header menu. This is a dependency property.")]
        public bool ShowDisabledTabItemsInHeaderMenu
        {
            get => 
                (bool) base.GetValue(ShowDisabledTabItemsInHeaderMenuProperty);
            set => 
                base.SetValue(ShowDisabledTabItemsInHeaderMenuProperty, value);
        }

        [Description("Gets or sets whether to close the header menu after an end-user has selected a tab item from it. This is a dependency property.")]
        public bool CloseHeaderMenuOnItemSelecting
        {
            get => 
                (bool) base.GetValue(CloseHeaderMenuOnItemSelectingProperty);
            set => 
                base.SetValue(CloseHeaderMenuOnItemSelectingProperty, value);
        }

        public ICommand NewTabCommand
        {
            get => 
                (ICommand) base.GetValue(NewTabCommandProperty);
            set => 
                base.SetValue(NewTabCommandProperty, value);
        }

        public object NewTabCommandParameter
        {
            get => 
                base.GetValue(NewTabCommandParameterProperty);
            set => 
                base.SetValue(NewTabCommandParameterProperty, value);
        }

        public DevExpress.Xpf.Core.NewButtonShowMode NewButtonShowMode
        {
            get => 
                (DevExpress.Xpf.Core.NewButtonShowMode) base.GetValue(NewButtonShowModeProperty);
            set => 
                base.SetValue(NewButtonShowModeProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DelegateCommand ActualNewTabCommand { get; private set; }

        public DevExpress.Xpf.Core.HideButtonShowMode HideButtonShowMode
        {
            get => 
                (DevExpress.Xpf.Core.HideButtonShowMode) base.GetValue(HideButtonShowModeProperty);
            set => 
                base.SetValue(HideButtonShowModeProperty, value);
        }

        [Obsolete("Use the HideButtonShowMode property.")]
        public bool AllowHideTabItems
        {
            get => 
                this.HideButtonShowMode != DevExpress.Xpf.Core.HideButtonShowMode.NoWhere;
            set => 
                this.HideButtonShowMode = value ? DevExpress.Xpf.Core.HideButtonShowMode.InAllTabs : DevExpress.Xpf.Core.HideButtonShowMode.NoWhere;
        }

        public DevExpress.Xpf.Core.SingleTabItemHideMode SingleTabItemHideMode
        {
            get => 
                (DevExpress.Xpf.Core.SingleTabItemHideMode) base.GetValue(SingleTabItemHideModeProperty);
            set => 
                base.SetValue(SingleTabItemHideModeProperty, value);
        }

        [Description("Gets or sets whether tab items are removed after being hidden. This is a dependency property.")]
        public bool RemoveTabItemsOnHiding
        {
            get => 
                (bool) base.GetValue(RemoveTabItemsOnHidingProperty);
            set => 
                base.SetValue(RemoveTabItemsOnHidingProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DelegateCommand CloseTabCommand { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TabControlScrollView ScrollView =>
            this as TabControlScrollView;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TabControlMultiLineView MultiLineView =>
            this as TabControlMultiLineView;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TabControlStretchView StretchView =>
            this as TabControlStretchView;

        public DXTabControl Owner
        {
            get => 
                (DXTabControl) base.GetValue(OwnerProperty);
            private set => 
                base.SetValue(OwnerPropertyKey, value);
        }

        protected internal ControllerBehavior HeaderMenuCustomizationController
        {
            get
            {
                ControllerBehavior headerMenuCustomizationController = this.headerMenuCustomizationController;
                if (this.headerMenuCustomizationController == null)
                {
                    ControllerBehavior local1 = this.headerMenuCustomizationController;
                    headerMenuCustomizationController = this.headerMenuCustomizationController = this.CreateHeaderMenuCustomizationController();
                }
                return headerMenuCustomizationController;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabControlViewBase.<>c <>9 = new TabControlViewBase.<>c();
            public static Func<DXTabControl, DXTabItem> <>9__88_1;
            public static Action<DXTabItem> <>9__88_2;
            public static Func<DXTabControl, DXTabItem> <>9__88_4;
            public static Func<DXTabItem, bool> <>9__88_5;
            public static Func<bool> <>9__88_6;
            public static Action<DXTabControl> <>9__92_0;
            public static Action<DXTabControl> <>9__99_0;

            internal void <.cctor>b__105_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).OnOwnerChanged((DXTabControl) e.OldValue, (DXTabControl) e.NewValue);
            }

            internal void <.cctor>b__105_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).UpdateViewProperties();
            }

            internal void <.cctor>b__105_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).UpdateViewProperties();
            }

            internal void <.cctor>b__105_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).UpdateViewProperties();
            }

            internal void <.cctor>b__105_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).UpdateViewProperties();
            }

            internal void <.cctor>b__105_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).OnNewTabCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }

            internal void <.cctor>b__105_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).UpdateNewButtonsVisibility();
            }

            internal void <.cctor>b__105_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlViewBase) d).UpdateViewProperties();
            }

            internal DXTabItem <.ctor>b__88_1(DXTabControl x) => 
                x.SelectedContainer;

            internal void <.ctor>b__88_2(DXTabItem x)
            {
                x.Close();
            }

            internal DXTabItem <.ctor>b__88_4(DXTabControl x) => 
                x.SelectedContainer;

            internal bool <.ctor>b__88_5(DXTabItem x) => 
                x.CanClose();

            internal bool <.ctor>b__88_6() => 
                false;

            internal void <AddNewTabItem>b__99_0(DXTabControl x)
            {
                x.AddNewTabItem();
            }

            internal void <UpdateViewProperties>b__92_0(DXTabControl x)
            {
                x.UpdateViewProperties();
            }
        }
    }
}

