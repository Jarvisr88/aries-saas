namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Utils;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Automation;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    [TemplateVisualState(Name="Focused", GroupName="FocusStates"), TemplateVisualState(Name="Unfocused", GroupName="FocusStates"), TemplateVisualState(Name="Normal", GroupName="CommonStates"), TemplateVisualState(Name="MouseOver", GroupName="CommonStates"), TemplateVisualState(Name="Disabled", GroupName="CommonStates"), TemplateVisualState(Name="Selected", GroupName="SelectionStates"), TemplateVisualState(Name="Unselected", GroupName="SelectionStates")]
    public class DXTabItem : HeaderedSelectorItemBase<DXTabControl, DXTabItem>
    {
        public static readonly DependencyProperty BindableNameProperty;
        public static readonly DependencyProperty IsNewProperty;
        private static readonly DependencyPropertyKey ViewInfoPropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never), IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ViewInfoProperty;
        public static readonly DependencyProperty NormalBackgroundTemplateProperty;
        public static readonly DependencyProperty HoverBackgroundTemplateProperty;
        public static readonly DependencyProperty SelectedBackgroundTemplateProperty;
        public static readonly DependencyProperty FocusedBackgroundTemplateProperty;
        public static readonly DependencyProperty BackgroundColorProperty;
        public static readonly DependencyProperty BorderColorProperty;
        public static readonly DependencyProperty ShowToolTipForNonTrimmedHeaderProperty;
        public static readonly DependencyProperty VisibleInHeaderMenuProperty;
        public static readonly DependencyProperty HeaderMenuContentProperty;
        public static readonly DependencyProperty HeaderMenuContentTemplateProperty;
        public static readonly DependencyProperty HeaderMenuIconProperty;
        public static readonly DependencyProperty HeaderMenuGlyphProperty;
        private static readonly DependencyPropertyKey ActualHeaderMenuContentPropertyKey;
        private static readonly DependencyPropertyKey ActualHeaderMenuContentTemplatePropertyKey;
        private static readonly DependencyPropertyKey ActualHeaderMenuGlyphPropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never), IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualHeaderMenuContentProperty;
        [EditorBrowsable(EditorBrowsableState.Never), IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualHeaderMenuContentTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty ActualHeaderMenuGlyphProperty;
        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty IconWidthProperty;
        public static readonly DependencyProperty IconHeightProperty;
        public static readonly DependencyProperty GlyphProperty;
        public static readonly DependencyProperty GlyphTemplateProperty;
        public static readonly DependencyProperty AllowHideProperty;
        public static readonly DependencyProperty CloseCommandProperty;
        public static readonly DependencyProperty CloseCommandParameterProperty;
        public static readonly DependencyProperty CloseCommandTargetProperty;
        private static readonly DependencyPropertyKey ActualCloseCommandPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty ActualCloseCommandProperty;
        private static readonly DependencyPropertyKey ActualHideButtonVisibilityPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty ActualHideButtonVisibilityProperty;
        internal object UnderlyingData;
        private int visibleIndex = 0x7fffffff;

        static DXTabItem()
        {
            BindableNameProperty = DependencyProperty.Register("BindableName", typeof(string), typeof(DXTabItem), new FrameworkPropertyMetadata(null, (d, e) => ((DXTabItem) d).Name = (string) e.NewValue));
            IsNewProperty = DependencyProperty.Register("IsNew", typeof(bool), typeof(DXTabItem), new PropertyMetadata(false, (d, e) => ((DXTabItem) d).UpdateActualCloseCommand()));
            ViewInfoPropertyKey = DependencyProperty.RegisterReadOnly("ViewInfo", typeof(TabViewInfo), typeof(DXTabItem), null);
            ViewInfoProperty = ViewInfoPropertyKey.DependencyProperty;
            NormalBackgroundTemplateProperty = DependencyProperty.Register("NormalBackgroundTemplate", typeof(DataTemplate), typeof(DXTabItem));
            HoverBackgroundTemplateProperty = DependencyProperty.Register("HoverBackgroundTemplate", typeof(DataTemplate), typeof(DXTabItem));
            SelectedBackgroundTemplateProperty = DependencyProperty.Register("SelectedBackgroundTemplate", typeof(DataTemplate), typeof(DXTabItem));
            FocusedBackgroundTemplateProperty = DependencyProperty.Register("FocusedBackgroundTemplate", typeof(DataTemplate), typeof(DXTabItem));
            BackgroundColorProperty = DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(DXTabItem), new PropertyMetadata(Colors.Transparent, (d, e) => ((DXTabItem) d).UpdateViewPropertiesCore()));
            BorderColorProperty = DependencyProperty.Register("BorderColor", typeof(Color), typeof(DXTabItem), new PropertyMetadata(Colors.Transparent, (d, e) => ((DXTabItem) d).UpdateViewPropertiesCore()));
            ShowToolTipForNonTrimmedHeaderProperty = DependencyProperty.Register("ShowToolTipForNonTrimmedHeader", typeof(bool), typeof(DXTabItem), new PropertyMetadata(true));
            VisibleInHeaderMenuProperty = DependencyProperty.Register("VisibleInHeaderMenu", typeof(bool), typeof(DXTabItem), new PropertyMetadata(true));
            HeaderMenuContentProperty = DependencyProperty.Register("HeaderMenuContent", typeof(object), typeof(DXTabItem), new PropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            HeaderMenuContentTemplateProperty = DependencyProperty.Register("HeaderMenuContentTemplate", typeof(DataTemplate), typeof(DXTabItem), new PropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            HeaderMenuIconProperty = DependencyProperty.Register("HeaderMenuIcon", typeof(object), typeof(DXTabItem), new PropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            HeaderMenuGlyphProperty = DependencyProperty.Register("HeaderMenuGlyph", typeof(ImageSource), typeof(DXTabItem), new PropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            ActualHeaderMenuContentPropertyKey = DependencyProperty.RegisterReadOnly("ActualHeaderMenuContent", typeof(object), typeof(DXTabItem), null);
            ActualHeaderMenuContentTemplatePropertyKey = DependencyProperty.RegisterReadOnly("ActualHeaderMenuContentTemplate", typeof(DataTemplate), typeof(DXTabItem), null);
            ActualHeaderMenuGlyphPropertyKey = DependencyProperty.RegisterReadOnly("ActualHeaderMenuGlyph", typeof(ImageSource), typeof(DXTabItem), null);
            ActualHeaderMenuContentProperty = ActualHeaderMenuContentPropertyKey.DependencyProperty;
            ActualHeaderMenuContentTemplateProperty = ActualHeaderMenuContentTemplatePropertyKey.DependencyProperty;
            ActualHeaderMenuGlyphProperty = ActualHeaderMenuGlyphPropertyKey.DependencyProperty;
            IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(DXTabItem));
            IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(DXTabItem), new PropertyMetadata(16.0));
            IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(DXTabItem), new PropertyMetadata(16.0));
            GlyphProperty = DependencyProperty.Register("Glyph", typeof(ImageSource), typeof(DXTabItem));
            GlyphTemplateProperty = DependencyProperty.Register("GlyphTemplate", typeof(DataTemplate), typeof(DXTabItem));
            AllowHideProperty = DependencyProperty.Register("AllowHide", typeof(DefaultBoolean), typeof(DXTabItem), new PropertyMetadata(DefaultBoolean.Default, (d, e) => ((DXTabItem) d).UpdateActualAllowHide()));
            CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(DXTabItem), new PropertyMetadata((d, e) => ((DXTabItem) d).OnCloseCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue)));
            CloseCommandParameterProperty = DependencyProperty.Register("CloseCommandParameter", typeof(object), typeof(DXTabItem), new PropertyMetadata((d, e) => ((DXTabItem) d).OnCloseCommandParameterChanged()));
            CloseCommandTargetProperty = DependencyProperty.Register("CloseCommandTarget", typeof(IInputElement), typeof(DXTabItem));
            ActualCloseCommandPropertyKey = DependencyProperty.RegisterReadOnly("ActualCloseCommand", typeof(DelegateCommand), typeof(DXTabItem), null);
            ActualCloseCommandProperty = ActualCloseCommandPropertyKey.DependencyProperty;
            ActualHideButtonVisibilityPropertyKey = DependencyProperty.RegisterReadOnly("ActualHideButtonVisibility", typeof(Visibility), typeof(DXTabItem), null);
            ActualHideButtonVisibilityProperty = ActualHideButtonVisibilityPropertyKey.DependencyProperty;
            ToolTipService.IsEnabledProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata(false));
            ContentControl.ContentProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            ContentControl.ContentTemplateProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            ContentControl.ContentTemplateSelectorProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            HeaderedSelectorItemBase<DXTabControl, DXTabItem>.HeaderProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            HeaderedSelectorItemBase<DXTabControl, DXTabItem>.HeaderTemplateProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            HeaderedSelectorItemBase<DXTabControl, DXTabItem>.HeaderTemplateSelectorProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata((d, e) => ((DXTabItem) d).UpdateHeaderMenuActualProperties()));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXTabItem), new FrameworkPropertyMetadata(typeof(DXTabItem)));
        }

        public DXTabItem()
        {
            base.SetValue(ActualCloseCommandPropertyKey, new DelegateCommand(() => this.Close(), new Func<bool>(this.CanClose), false));
            BarNameScope.SetIsScopeOwner(this, true);
            base.GotFocus += (d, e) => this.UpdateState();
            base.LostFocus += (d, e) => this.UpdateState();
            base.MouseEnter += delegate (object d, MouseEventArgs e) {
                if (base.IsEnabled)
                {
                    VisualStateManager.GoToState(this, "MouseOver", true);
                }
            };
            base.MouseLeave += delegate (object d, MouseEventArgs e) {
                if (base.IsEnabled)
                {
                    VisualStateManager.GoToState(this, "Normal", true);
                }
            };
        }

        public bool CanClose() => 
            (base.Owner != null) && ((base.Visibility != Visibility.Collapsed) && ((this.AllowHide != DefaultBoolean.False) ? (base.Owner.View.CanCloseTabItem(this) ? ((this.CloseCommand == null) || (!(this.CloseCommand is RoutedCommand) ? this.CloseCommand.CanExecute(this.CloseCommandParameter) : ((RoutedCommand) this.CloseCommand).CanExecute(this.CloseCommandParameter, this))) : false) : false));

        private void ClearVisibleIndex()
        {
            this.VisibleIndex = 0x7fffffff;
        }

        public void Close()
        {
            if (this.CanClose())
            {
                TabControlViewBase view = base.Owner.View;
                base.Owner.HideTabItem(this, true);
                if (base.Visibility == Visibility.Collapsed)
                {
                    if (this.CloseCommand != null)
                    {
                        if (this.CloseCommand is RoutedCommand)
                        {
                            ((RoutedCommand) this.CloseCommand).Execute(this.CloseCommandParameter, this);
                        }
                        else
                        {
                            this.CloseCommand.Execute(this.CloseCommandParameter);
                        }
                    }
                    if (view.RemoveTabItemsOnHiding)
                    {
                        base.Owner.Do<DXTabControl>(x => x.RemoveTabItem(this));
                    }
                    view.OnTabItemClosed(this);
                }
            }
        }

        internal void DisableFocusedState()
        {
            VisualStateManager.GoToState(this, "Unfocused", false);
        }

        public FrameworkElement GetLayoutChild(string childName) => 
            (FrameworkElement) base.GetTemplateChild(childName);

        internal void Insert(DXTabControl target, int index)
        {
            object item = this;
            if (target.ItemsSource != null)
            {
                item = this.UnderlyingData;
                this.UnderlyingData = null;
            }
            target.InsertTabItem(item, index);
            target.SelectTabItem(item);
        }

        internal static bool IsMouseLeftButtonDownOnDXTabItem(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is DXTabItem))
            {
                return false;
            }
            DXTabItem objA = (DXTabItem) sender;
            DependencyObject source = (DependencyObject) e.Source;
            return (ReferenceEquals(objA, source) || ReferenceEquals(objA, LayoutTreeHelper.GetVisualParents(source, objA.Owner).OfType<DXTabItem>().FirstOrDefault<DXTabItem>()));
        }

        internal void Move(int index, bool realMove = true)
        {
            if (base.Owner != null)
            {
                if (realMove)
                {
                    base.Owner.MoveTabItem(this, index);
                }
                else
                {
                    this.UpdateVisibleIndexes();
                    Func<DXTabItem, int> keySelector = <>c.<>9__136_0;
                    if (<>c.<>9__136_0 == null)
                    {
                        Func<DXTabItem, int> local1 = <>c.<>9__136_0;
                        keySelector = <>c.<>9__136_0 = x => x.VisibleIndex;
                    }
                    List<DXTabItem> list = base.Owner.GetContainers().OrderBy<DXTabItem, int>(keySelector).ToList<DXTabItem>();
                    list.Remove(this);
                    list.Insert(index, this);
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].VisibleIndex = i;
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CloseButton = (Button) base.GetTemplateChild("PART_CloseButton");
            this.UpdateState();
        }

        private void OnCloseCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.UpdateActualCloseCommand();
        }

        private void OnCloseCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            oldCommand.Do<ICommand>(delegate (ICommand x) {
                x.CanExecuteChanged -= new EventHandler(this.OnCloseCommandCanExecuteChanged);
            });
            newCommand.Do<ICommand>(delegate (ICommand x) {
                x.CanExecuteChanged += new EventHandler(this.OnCloseCommandCanExecuteChanged);
            });
            this.UpdateActualCloseCommand();
        }

        private void OnCloseCommandParameterChanged()
        {
            this.UpdateActualCloseCommand();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            NavigationAutomationPeersCreator.Default.Create(this);

        protected override void OnIsEnabledChanged(bool oldValue, bool newValue)
        {
            base.OnIsEnabledChanged(oldValue, newValue);
            if (oldValue != newValue)
            {
                this.UpdateState();
            }
        }

        protected override void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            base.OnIsSelectedChanged(oldValue, newValue);
            if (newValue)
            {
                Func<DXTabControl, bool> evaluator = <>c.<>9__126_0;
                if (<>c.<>9__126_0 == null)
                {
                    Func<DXTabControl, bool> local1 = <>c.<>9__126_0;
                    evaluator = <>c.<>9__126_0 = x => x.IsKeyboardFocusWithin;
                }
                if (base.Owner.Return<DXTabControl, bool>(evaluator, <>c.<>9__126_1 ??= () => false))
                {
                    base.Focus();
                }
            }
            Action<DXTabControl> action = <>c.<>9__126_2;
            if (<>c.<>9__126_2 == null)
            {
                Action<DXTabControl> local3 = <>c.<>9__126_2;
                action = <>c.<>9__126_2 = x => x.UpdateViewProperties();
            }
            base.Owner.Do<DXTabControl>(action);
            this.UpdateState();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            TabControlKeyboardController.OnTabItemKeyDown(this, e);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.UpdateState();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if ((IsMouseLeftButtonDownOnDXTabItem(this, e) && ((base.Owner != null) && (e.ChangedButton == MouseButton.Middle))) && ((base.Owner.View.HideButtonShowMode != HideButtonShowMode.NoWhere) || (this.CloseCommand != null)))
            {
                this.Close();
                e.Handled = true;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (IsMouseLeftButtonDownOnDXTabItem(this, e))
            {
                base.Focus();
                if (!base.IsSelected)
                {
                    base.Owner.Do<DXTabControl>(x => x.SelectTabItem(this));
                    e.Handled = base.IsSelected;
                }
            }
        }

        protected override void OnOwnerChanged(DXTabControl oldValue, DXTabControl newValue)
        {
            base.OnOwnerChanged(oldValue, newValue);
            if (newValue == null)
            {
                this.ClearVisibleIndex();
            }
        }

        internal void Remove()
        {
            if (base.Owner != null)
            {
                if (base.Owner.ItemsSource != null)
                {
                    object obj2 = base.Owner.ItemContainerGenerator.ItemFromContainer(this);
                    this.UnderlyingData = obj2;
                }
                base.Owner.RemoveTabItem(this);
            }
        }

        private void UpdateActualAllowHide()
        {
            if (base.Owner != null)
            {
                if (!base.Owner.View.CanCloseTabItem(this))
                {
                    base.SetValue(ActualHideButtonVisibilityPropertyKey, Visibility.Collapsed);
                }
                else if (this.AllowHide == DefaultBoolean.True)
                {
                    base.SetValue(ActualHideButtonVisibilityPropertyKey, Visibility.Visible);
                }
                else if (this.AllowHide == DefaultBoolean.False)
                {
                    base.SetValue(ActualHideButtonVisibilityPropertyKey, Visibility.Collapsed);
                }
                else
                {
                    switch (base.Owner.View.HideButtonShowMode)
                    {
                        case HideButtonShowMode.NoWhere:
                        case HideButtonShowMode.InHeaderArea:
                            base.SetValue(ActualHideButtonVisibilityPropertyKey, Visibility.Collapsed);
                            return;

                        case HideButtonShowMode.InAllTabs:
                        case HideButtonShowMode.InAllTabsAndHeaderArea:
                            base.SetValue(ActualHideButtonVisibilityPropertyKey, Visibility.Visible);
                            return;

                        case HideButtonShowMode.InActiveTab:
                        case HideButtonShowMode.InActiveTabAndHeaderArea:
                            this.SetValue(ActualHideButtonVisibilityPropertyKey, base.IsSelected ? Visibility.Visible : Visibility.Hidden);
                            return;

                        case (HideButtonShowMode.InActiveTab | HideButtonShowMode.InAllTabs):
                            return;
                    }
                }
            }
        }

        private void UpdateActualCloseCommand()
        {
            DelegateCommand command = base.GetValue(ActualCloseCommandPropertyKey.DependencyProperty) as DelegateCommand;
            this.UpdateActualAllowHide();
            command.RaiseCanExecuteChanged();
        }

        private void UpdateHeaderMenuActualProperties()
        {
            if (this.HeaderMenuContent != null)
            {
                base.SetValue(ActualHeaderMenuContentPropertyKey, this.HeaderMenuContent);
            }
            else if (base.Header is UIElement)
            {
                base.SetValue(ActualHeaderMenuContentPropertyKey, base.Header.ToString());
            }
            else if (base.Header != null)
            {
                base.SetValue(ActualHeaderMenuContentPropertyKey, base.Header);
            }
            else
            {
                base.SetValue(ActualHeaderMenuContentPropertyKey, null);
            }
            if (this.HeaderMenuContentTemplate != null)
            {
                base.SetValue(ActualHeaderMenuContentTemplatePropertyKey, this.HeaderMenuContentTemplate);
            }
            else if ((base.HeaderTemplate == null) && (base.HeaderTemplateSelector == null))
            {
                base.SetValue(ActualHeaderMenuContentTemplatePropertyKey, null);
            }
            else
            {
                DataTemplate headerTemplate = base.HeaderTemplate;
                DataTemplate template2 = headerTemplate;
                if (headerTemplate == null)
                {
                    DataTemplate local1 = headerTemplate;
                    template2 = base.HeaderTemplateSelector.SelectTemplate(base.Header, this);
                }
                this.SetValue(ActualHeaderMenuContentTemplatePropertyKey, template2);
            }
            if (this.HeaderMenuGlyph != null)
            {
                base.SetValue(ActualHeaderMenuGlyphPropertyKey, this.HeaderMenuGlyph);
            }
            else if (this.Glyph != null)
            {
                base.SetValue(ActualHeaderMenuGlyphPropertyKey, this.Glyph);
            }
            else
            {
                base.SetValue(ActualHeaderMenuGlyphPropertyKey, null);
            }
        }

        private void UpdateState()
        {
            VisualStateManager.GoToState(this, base.IsEnabled ? "Normal" : "Disabled", true);
            VisualStateManager.GoToState(this, base.IsSelected ? "Selected" : "Unselected", true);
            VisualStateManager.GoToState(this, base.IsFocused ? "Focused" : "Unfocused", true);
        }

        internal void UpdateViewPropertiesCore()
        {
            if (base.Owner != null)
            {
                this.UpdateActualCloseCommand();
                TabViewInfo info = new TabViewInfo(this);
                if (!info.Equals((TabViewInfo) base.GetValue(ViewInfoProperty)))
                {
                    base.SetValue(ViewInfoPropertyKey, info);
                }
                ElementMergingBehavior undefined = ElementMergingBehavior.Undefined;
                bool? allowMerging = base.Owner.AllowMerging;
                bool flag = false;
                if ((allowMerging.GetValueOrDefault() == flag) ? (allowMerging == null) : true)
                {
                    undefined = base.IsSelected ? ElementMergingBehavior.InternalWithExternal : ElementMergingBehavior.None;
                }
                MergingProperties.SetElementMergingBehavior(this, undefined);
                DependencyObject content = base.Content as DependencyObject;
                if ((content != null) && !ReferenceEquals(LogicalTreeHelper.GetParent(content), this))
                {
                    MergingProperties.SetElementMergingBehavior(content, undefined);
                }
            }
        }

        private void UpdateVisibleIndexes()
        {
            Func<DXTabItem, int> keySelector = <>c.<>9__137_0;
            if (<>c.<>9__137_0 == null)
            {
                Func<DXTabItem, int> local1 = <>c.<>9__137_0;
                keySelector = <>c.<>9__137_0 = x => x.VisibleIndex;
            }
            List<DXTabItem> list = base.Owner.GetContainers().OrderBy<DXTabItem, int>(keySelector).ToList<DXTabItem>();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].VisibleIndex = i;
            }
        }

        public string BindableName
        {
            get => 
                (string) base.GetValue(BindableNameProperty);
            set => 
                base.SetValue(BindableNameProperty, value);
        }

        public bool IsNew
        {
            get => 
                (bool) base.GetValue(IsNewProperty);
            set => 
                base.SetValue(IsNewProperty, value);
        }

        public DataTemplate NormalBackgroundTemplate
        {
            get => 
                (DataTemplate) base.GetValue(NormalBackgroundTemplateProperty);
            set => 
                base.SetValue(NormalBackgroundTemplateProperty, value);
        }

        public DataTemplate HoverBackgroundTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HoverBackgroundTemplateProperty);
            set => 
                base.SetValue(HoverBackgroundTemplateProperty, value);
        }

        public DataTemplate SelectedBackgroundTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SelectedBackgroundTemplateProperty);
            set => 
                base.SetValue(SelectedBackgroundTemplateProperty, value);
        }

        public DataTemplate FocusedBackgroundTemplate
        {
            get => 
                (DataTemplate) base.GetValue(FocusedBackgroundTemplateProperty);
            set => 
                base.SetValue(FocusedBackgroundTemplateProperty, value);
        }

        public Color BackgroundColor
        {
            get => 
                (Color) base.GetValue(BackgroundColorProperty);
            set => 
                base.SetValue(BackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => 
                (Color) base.GetValue(BorderColorProperty);
            set => 
                base.SetValue(BorderColorProperty, value);
        }

        public bool ShowToolTipForNonTrimmedHeader
        {
            get => 
                (bool) base.GetValue(ShowToolTipForNonTrimmedHeaderProperty);
            set => 
                base.SetValue(ShowToolTipForNonTrimmedHeaderProperty, value);
        }

        [Description("Gets or sets whether the header menu displays a menu item that corresponds to the current tab item. This is a dependency property.")]
        public bool VisibleInHeaderMenu
        {
            get => 
                (bool) base.GetValue(VisibleInHeaderMenuProperty);
            set => 
                base.SetValue(VisibleInHeaderMenuProperty, value);
        }

        [Description("Gets or sets the content of the header menu item that corresponds to the current tab item. This is a dependency property."), TypeConverter(typeof(ObjectConverter))]
        public object HeaderMenuContent
        {
            get => 
                base.GetValue(HeaderMenuContentProperty);
            set => 
                base.SetValue(HeaderMenuContentProperty, value);
        }

        [Description("Gets or sets a template that defines the visual appearance of the header menu item, corresponding to the current tab item. This is a dependency property.")]
        public DataTemplate HeaderMenuContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderMenuContentTemplateProperty);
            set => 
                base.SetValue(HeaderMenuContentTemplateProperty, value);
        }

        [Obsolete("Use the HeaderMenuGlyph property instead.")]
        public object HeaderMenuIcon
        {
            get => 
                base.GetValue(HeaderMenuIconProperty);
            set => 
                base.SetValue(HeaderMenuIconProperty, value);
        }

        public ImageSource HeaderMenuGlyph
        {
            get => 
                (ImageSource) base.GetValue(HeaderMenuGlyphProperty);
            set => 
                base.SetValue(HeaderMenuGlyphProperty, value);
        }

        [Obsolete("Use the Glyph or GlyphTemplate property instead.")]
        public object Icon
        {
            get => 
                base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        [Obsolete("Use the GlyphTemplate property instead.")]
        public double IconWidth
        {
            get => 
                (double) base.GetValue(IconWidthProperty);
            set => 
                base.SetValue(IconWidthProperty, value);
        }

        [Obsolete("Use the GlyphTemplate property instead.")]
        public double IconHeight
        {
            get => 
                (double) base.GetValue(IconHeightProperty);
            set => 
                base.SetValue(IconHeightProperty, value);
        }

        public ImageSource Glyph
        {
            get => 
                (ImageSource) base.GetValue(GlyphProperty);
            set => 
                base.SetValue(GlyphProperty, value);
        }

        public DataTemplate GlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GlyphTemplateProperty);
            set => 
                base.SetValue(GlyphTemplateProperty, value);
        }

        [Description("Gets or sets whether the tab item can be hidden. This is a dependency property.")]
        public DefaultBoolean AllowHide
        {
            get => 
                (DefaultBoolean) base.GetValue(AllowHideProperty);
            set => 
                base.SetValue(AllowHideProperty, value);
        }

        [TypeConverter(typeof(CommandConverter)), Description("Gets or sets a command executed when the current tab item's Close/Hide button ('x') is clicked. This is a dependency property.")]
        public ICommand CloseCommand
        {
            get => 
                (ICommand) base.GetValue(CloseCommandProperty);
            set => 
                base.SetValue(CloseCommandProperty, value);
        }

        [Description("Gets or sets the DXTabItem.CloseCommand parameter. This is a dependency property.")]
        public object CloseCommandParameter
        {
            get => 
                base.GetValue(CloseCommandParameterProperty);
            set => 
                base.SetValue(CloseCommandParameterProperty, value);
        }

        [Obsolete("")]
        public IInputElement CloseCommandTarget { get; set; }

        [Description("Gets the close button, displayed within the tab item's header.")]
        public Button CloseButton { get; private set; }

        internal int VisibleIndex
        {
            get => 
                this.visibleIndex;
            set => 
                this.visibleIndex = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabItem.<>c <>9 = new DXTabItem.<>c();
            public static Func<DXTabControl, bool> <>9__126_0;
            public static Func<bool> <>9__126_1;
            public static Action<DXTabControl> <>9__126_2;
            public static Func<DXTabItem, int> <>9__136_0;
            public static Func<DXTabItem, int> <>9__137_0;

            internal void <.cctor>b__110_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateActualAllowHide();
            }

            internal void <.cctor>b__110_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).OnCloseCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }

            internal void <.cctor>b__110_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).OnCloseCommandParameterChanged();
            }

            internal void <.cctor>b__110_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateHeaderMenuActualProperties();
            }

            internal void <.cctor>b__110_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).Name = (string) e.NewValue;
            }

            internal void <.cctor>b__110_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateActualCloseCommand();
            }

            internal void <.cctor>b__110_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateViewPropertiesCore();
            }

            internal void <.cctor>b__110_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabItem) d).UpdateViewPropertiesCore();
            }

            internal int <Move>b__136_0(DXTabItem x) => 
                x.VisibleIndex;

            internal bool <OnIsSelectedChanged>b__126_0(DXTabControl x) => 
                x.IsKeyboardFocusWithin;

            internal bool <OnIsSelectedChanged>b__126_1() => 
                false;

            internal void <OnIsSelectedChanged>b__126_2(DXTabControl x)
            {
                x.UpdateViewProperties();
            }

            internal int <UpdateVisibleIndexes>b__137_0(DXTabItem x) => 
                x.VisibleIndex;
        }
    }
}

