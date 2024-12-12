namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class BaseGridColumnHeader : BaseGridHeader, IColumnPropertyOwner
    {
        private static readonly DependencyPropertyKey IsInDropAreaPropertyKey;
        public static readonly DependencyProperty IsInDropAreaProperty;
        public static readonly DependencyProperty CorrectDragIndicatorLocationProperty;
        private static readonly DependencyPropertyKey DragElementSizePropertyKey;
        public static readonly DependencyProperty DragElementSizeProperty;
        private static readonly DependencyPropertyKey DragElementAllowTransparencyPropertyKey;
        public static readonly DependencyProperty DragElementAllowTransparencyProperty;
        public static readonly DependencyProperty ColumnFilterPopupStyleProperty;
        public static readonly DependencyProperty ShowFilterButtonOnHoverProperty;
        private ContentControl columnFilterPopupContainerCore;
        private PopupBaseEdit columnFilterPopupCore;
        private FrameworkElement sortIndicatorCore;

        static BaseGridColumnHeader()
        {
            ShowFilterButtonOnHoverProperty = DependencyProperty.Register("ShowFilterButtonOnHover", typeof(bool), typeof(BaseGridColumnHeader), new PropertyMetadata(true, (d, e) => ((BaseGridColumnHeader) d).UpdateIsFilterButtonVisible()));
            Type ownerType = typeof(BaseGridColumnHeader);
            IsInDropAreaPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("IsInDropArea", typeof(bool), ownerType, new PropertyMetadata(false));
            IsInDropAreaProperty = IsInDropAreaPropertyKey.DependencyProperty;
            CorrectDragIndicatorLocationProperty = DependencyPropertyManager.RegisterAttached("CorrectDragIndicatorLocation", typeof(bool), ownerType, new PropertyMetadata(true));
            Size defaultValue = new Size();
            DragElementSizePropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("DragElementSize", typeof(Size), ownerType, new PropertyMetadata(defaultValue));
            DragElementSizeProperty = DragElementSizePropertyKey.DependencyProperty;
            DragElementAllowTransparencyPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("DragElementAllowTransparency", typeof(bool), ownerType, new PropertyMetadata(true));
            DragElementAllowTransparencyProperty = DragElementAllowTransparencyPropertyKey.DependencyProperty;
            ColumnFilterPopupStyleProperty = DependencyPropertyManager.RegisterAttached("ColumnFilterPopupStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((BaseGridColumnHeader) d).OnColumnFilterPopupStyleChanged()));
        }

        private void ColumnFilterPopupClosed(object sender, ClosePopupEventArgs e)
        {
            this.ColumnFilterPopupClosedCore(e.CloseMode != PopupCloseMode.Cancel);
        }

        private void ColumnFilterPopupClosedCore(bool applyFilter)
        {
            this.UpdateIsFilterButtonVisible();
            base.Column.ColumnFilterInfo.OnPopupClosed(this.ColumnFilterPopup, applyFilter);
        }

        private void ColumnFilterPopupOpened(object sender, RoutedEventArgs e)
        {
            base.Column.ColumnFilterInfo.OnPopupOpened(this.ColumnFilterPopup);
        }

        private void ColumnFilterPopupOpening(object sender, OpenPopupEventArgs e)
        {
            this.UpdateColumnFilterPopup(false);
            if (string.IsNullOrEmpty(base.Column.FieldName))
            {
                e.Cancel = true;
            }
            else
            {
                base.Column.ColumnFilterInfo.OnPopupOpening(this.ColumnFilterPopup);
            }
        }

        private void ContainerizeColumnFilterPopup()
        {
            if (this.ColumnFilterPopupContainer != null)
            {
                base.LayoutPanel.Do<ColumnHeaderDockPanel>(x => x.FilterPresenter = this.ColumnFilterPopupContainer);
                this.ColumnFilterPopupContainer.Content = this.ColumnFilterPopup;
            }
        }

        protected internal override DependencyObject CreateDragElementDataContext() => 
            base.GridView.HeadersData.CreateCellDataByColumn(base.Column);

        protected virtual PopupBaseEdit CreateFilterPopup()
        {
            PopupBaseEdit edit = base.Column.ColumnFilterInfo.CreateColumnFilterPopup();
            edit.IgnorePopupSizeConstraints = true;
            return edit;
        }

        protected virtual FrameworkElement CreateSortIndicator() => 
            null;

        internal static ColumnBase GetColumnByDragElement(UIElement element) => 
            (ColumnBase) GetGridColumn(LayoutHelper.FindParentObject<BaseGridColumnHeader>(element));

        public static bool GetCorrectDragIndicatorLocation(DependencyObject obj) => 
            (bool) obj.GetValue(CorrectDragIndicatorLocationProperty);

        public static bool GetDragElementAllowTransparency(DependencyObject obj) => 
            (bool) obj.GetValue(DragElementAllowTransparencyProperty);

        public static Size GetDragElementSize(DependencyObject obj) => 
            (Size) obj.GetValue(DragElementSizeProperty);

        public static HeaderPresenterType GetHeaderPresenterTypeFromGridColumnHeader(DependencyObject d) => 
            GetHeaderPresenterTypeFromLocalValue(d);

        public static HeaderPresenterType GetHeaderPresenterTypeFromLocalValue(DependencyObject d) => 
            ColumnBase.GetHeaderPresenterType(d);

        public static bool GetIsInDropArea(DependencyObject obj) => 
            (bool) obj.GetValue(IsInDropAreaProperty);

        private bool HasVisibleParentBands(ColumnBase column)
        {
            for (BandBase base2 = column.ParentBand; base2 != null; base2 = base2.ParentBand)
            {
                if (base2.ActualShowInBandsPanel)
                {
                    return true;
                }
            }
            return false;
        }

        internal override void InitInnerLayoutElements()
        {
            base.InitInnerLayoutElements();
            if (base.ColumnHeaderLayout != null)
            {
                base.ColumnHeaderLayout.SortIndicator.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    this.SortIndicator = x;
                    this.UpdateSortIndicators();
                });
                base.ColumnHeaderLayout.Filter.Do<ContentControl>(delegate (ContentControl x) {
                    this.ColumnFilterPopupContainer = x;
                    this.UpdateIsFilterButtonVisible();
                });
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateIsFilterButtonVisible();
            this.UpdateSortIndicators();
            this.UpdateAllowHighlighting();
        }

        protected override void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
        {
            base.OnColumnContentChanged(sender, e);
            if (ReferenceEquals(e.Property, ColumnBase.EditSettingsProperty) || (ReferenceEquals(e.Property, ColumnBase.FilterPopupModeProperty) || (ReferenceEquals(e.Property, ColumnBase.AllowedDateTimeFiltersProperty) || ReferenceEquals(e.Property, ColumnBase.ShowEmptyDateFilterProperty))))
            {
                this.UpdateColumnFilterPopup();
            }
            if (ReferenceEquals(e.Property, ColumnBase.SortOrderProperty))
            {
                this.UpdateSortIndicators();
            }
            if (ReferenceEquals(e.Property, BaseColumn.ActualHeaderWidthProperty))
            {
                this.UpdateWidth();
            }
            if (ReferenceEquals(e.Property, ColumnBase.IsFilteredProperty))
            {
                this.UpdateIsFilterButtonVisible();
            }
            if (ReferenceEquals(e.Property, ColumnBase.ActualAllowColumnFilteringProperty))
            {
                this.UpdateIsFilterButtonVisible();
            }
            if (ReferenceEquals(e.Property, BaseColumn.ColumnPositionProperty))
            {
                base.UpdateColumnPosition();
            }
            if (ReferenceEquals(e.Property, BaseColumn.HasTopElementProperty))
            {
                this.UpdateHasTopElement();
            }
            if (ReferenceEquals(e.Property, BaseColumn.AllowSearchHeaderHighlightingProperty))
            {
                this.UpdateAllowHighlighting();
            }
        }

        private void OnColumnFilterPopupChanged(PopupBaseEdit oldValue)
        {
            if (oldValue != null)
            {
                oldValue.PopupOpening -= new OpenPopupEventHandler(this.ColumnFilterPopupOpening);
                oldValue.PopupOpened -= new RoutedEventHandler(this.ColumnFilterPopupOpened);
                oldValue.PopupClosed -= new ClosePopupEventHandler(this.ColumnFilterPopupClosed);
                if (oldValue.IsPopupOpen)
                {
                    this.ColumnFilterPopupClosedCore(false);
                }
            }
            if (this.ColumnFilterPopup != null)
            {
                this.ColumnFilterPopup.PopupOpening += new OpenPopupEventHandler(this.ColumnFilterPopupOpening);
                this.ColumnFilterPopup.PopupOpened += new RoutedEventHandler(this.ColumnFilterPopupOpened);
                this.ColumnFilterPopup.PopupClosed += new ClosePopupEventHandler(this.ColumnFilterPopupClosed);
            }
            this.UpdateIsFilterButtonVisible();
        }

        private void OnColumnFilterPopupContainerChanged(ContentControl old)
        {
            Action<ContentControl> action = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Action<ContentControl> local1 = <>c.<>9__54_0;
                action = <>c.<>9__54_0 = x => x.Content = null;
            }
            old.Do<ContentControl>(action);
            this.ContainerizeColumnFilterPopup();
            this.UpdateLayoutPanel();
        }

        private void OnColumnFilterPopupStyleChanged()
        {
            if (this.ColumnFilterPopup != null)
            {
                this.ColumnFilterPopup.Style = this.ColumnFilterPopupStyle;
            }
        }

        private void OnDraggColumnOwnerChanging(object sender, EventArgs e)
        {
            if ((base.DragDropHelper != null) && base.DragDropHelper.IsDragging)
            {
                base.DragDropHelper.CancelDragging();
            }
        }

        protected override void OnDraggedColumnChanged(BaseColumn oldValue, BaseColumn newValue)
        {
            if (oldValue is ColumnBase)
            {
                ((ColumnBase) oldValue).OwnerChanging -= new EventHandler(this.OnDraggColumnOwnerChanging);
            }
            if (newValue is ColumnBase)
            {
                ((ColumnBase) newValue).OwnerChanging += new EventHandler(this.OnDraggColumnOwnerChanging);
            }
        }

        protected override void OnGridColumnChanged(BaseColumn oldValue)
        {
            base.OnGridColumnChanged(oldValue);
            this.UpdateIsFilterButtonVisible();
            this.UpdateColumnFilterPopup();
            this.UpdateSortIndicators();
            this.UpdateWidth();
            base.UpdateHeaderPresenter();
            this.UpdateDesignTimeSelection();
            this.UpdateAllowHighlighting();
        }

        protected override void OnIsSelectedInDesignTimeChangedCore()
        {
            SetIsSelectedInDesignTime(base.Column, GetIsSelectedInDesignTime(this));
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateIsFilterButtonVisible();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.UpdateIsFilterButtonVisible();
        }

        protected override void ResetPanelChildren()
        {
            base.ResetPanelChildren();
            this.ColumnFilterPopup = null;
            this.ColumnFilterPopupContainer = null;
            this.SortIndicator = null;
        }

        public static void SetCorrectDragIndicatorLocation(DependencyObject obj, bool value)
        {
            obj.SetValue(CorrectDragIndicatorLocationProperty, value);
        }

        internal static void SetDragElementAllowTransparency(DependencyObject obj, bool value)
        {
            obj.SetValue(DragElementAllowTransparencyPropertyKey, value);
        }

        internal static void SetDragElementSize(DependencyObject obj, Size value)
        {
            obj.SetValue(DragElementSizePropertyKey, value);
        }

        protected virtual void SetFilterHitTestAcceptor(PopupBaseEdit popup)
        {
        }

        internal static void SetIsInDropArea(DependencyObject obj, bool value)
        {
            obj.SetValue(IsInDropAreaPropertyKey, value);
        }

        protected void UpdateAllowHighlighting()
        {
            if ((base.TextBlock != null) && (base.BaseColumn != null))
            {
                if (base.BaseColumn.AllowSearchHeaderHighlighting)
                {
                    base.TextBlock.FontStyle = FontStyles.Italic;
                }
                else
                {
                    base.TextBlock.ClearValue(TextBlock.FontStyleProperty);
                }
            }
        }

        protected void UpdateColumnFilterPopup()
        {
            if ((this.ColumnFilterPopup != null) && ((base.Column != null) && (base.Column.View != null)))
            {
                this.ColumnFilterPopup = this.CreateFilterPopup();
            }
        }

        protected virtual void UpdateColumnFilterPopup(bool allowHide)
        {
            if (base.Column != null)
            {
                bool flag = (this.ShowFilterButtonOnHover && (!base.IsMouseOver && !this.IsPopupOpen)) && !base.Column.IsFiltered;
                bool flag2 = (base.HeaderPresenterType == HeaderPresenterType.ColumnChooser) || !DesignerHelper.GetValue(this, base.Column.ActualAllowColumnFiltering, false);
                if (!allowHide)
                {
                    flag = false;
                    flag2 = false;
                }
                if (flag2)
                {
                    this.ColumnFilterPopup = null;
                }
                else if (!flag || (base.HeaderPresenterType == HeaderPresenterType.GroupPanel))
                {
                    this.ColumnFilterPopup ??= this.CreateFilterPopup();
                    this.ColumnFilterPopup.Visibility = (!flag || (base.HeaderPresenterType != HeaderPresenterType.GroupPanel)) ? Visibility.Visible : Visibility.Hidden;
                    this.ColumnFilterPopup.Style = this.ColumnFilterPopupStyle;
                    SetGridColumn(this.ColumnFilterPopup, base.Column);
                    this.SetFilterHitTestAcceptor(this.ColumnFilterPopup);
                }
                else
                {
                    Action<PopupBaseEdit> action = <>c.<>9__69_0;
                    if (<>c.<>9__69_0 == null)
                    {
                        Action<PopupBaseEdit> local1 = <>c.<>9__69_0;
                        action = <>c.<>9__69_0 = x => x.Visibility = Visibility.Collapsed;
                    }
                    this.ColumnFilterPopup.Do<PopupBaseEdit>(action);
                }
            }
        }

        private void UpdateColumnFilterPopupContainer()
        {
            this.ContainerizeColumnFilterPopup();
            this.UpdateLayoutPanel();
        }

        private void UpdateDesignTimeSelection()
        {
            if (base.Column != null)
            {
                SetIsSelectedInDesignTime(this, GetIsSelectedInDesignTime(base.Column));
            }
        }

        protected override void UpdateHasTopElement()
        {
            if ((base.Column != null) && ((base.Column.OwnerControl != null) && ((base.Column.OwnerControl.BandsLayoutCore != null) && (base.Column.OwnerControl.BandsLayoutCore.ShowBandsPanel && (base.HeaderPresenterType == HeaderPresenterType.Headers)))))
            {
                this.HasTopElement = base.Column.HasTopElement || this.HasVisibleParentBands(base.Column);
            }
            else
            {
                base.UpdateHasTopElement();
            }
        }

        protected void UpdateIsFilterButtonVisible()
        {
            this.UpdateColumnFilterPopup(true);
        }

        protected override void UpdateLayoutPanel()
        {
            base.UpdateLayoutPanel();
            if (base.LayoutPanel != null)
            {
                base.LayoutPanel.FilterPresenter = (this.ColumnFilterPopupContainer == null) ? ((UIElement) this.ColumnFilterPopup) : ((UIElement) this.ColumnFilterPopupContainer);
                base.LayoutPanel.SortIndicator = this.SortIndicator;
            }
        }

        protected virtual void UpdateSortIndicator(bool isAscending)
        {
        }

        private void UpdateSortIndicators()
        {
            if (base.Column != null)
            {
                Visibility visibility = (base.Column.SortOrder == ColumnSortOrder.None) ? Visibility.Collapsed : Visibility.Visible;
                if ((this.SortIndicator == null) && (visibility != Visibility.Collapsed))
                {
                    this.SortIndicator = this.CreateSortIndicator();
                }
                if (this.SortIndicator != null)
                {
                    this.UpdateSortIndicator(base.Column.SortOrder == ColumnSortOrder.Ascending);
                    this.SortIndicator.Visibility = visibility;
                }
            }
        }

        private void UpdateWidth()
        {
            if ((base.Column != null) && (this.CanSyncWidth && !double.IsInfinity(base.Column.ActualHeaderWidth)))
            {
                base.Width = base.Column.ActualHeaderWidth;
            }
        }

        public Style ColumnFilterPopupStyle
        {
            get => 
                (Style) base.GetValue(ColumnFilterPopupStyleProperty);
            set => 
                base.SetValue(ColumnFilterPopupStyleProperty, value);
        }

        [Browsable(false)]
        public bool CanSyncWidth { get; set; }

        public bool ShowFilterButtonOnHover
        {
            get => 
                (bool) base.GetValue(ShowFilterButtonOnHoverProperty);
            set => 
                base.SetValue(ShowFilterButtonOnHoverProperty, value);
        }

        private ContentControl ColumnFilterPopupContainer
        {
            get => 
                this.columnFilterPopupContainerCore;
            set
            {
                if (!ReferenceEquals(this.columnFilterPopupContainerCore, value))
                {
                    ContentControl columnFilterPopupContainerCore = this.columnFilterPopupContainerCore;
                    this.columnFilterPopupContainerCore = value;
                    this.OnColumnFilterPopupContainerChanged(columnFilterPopupContainerCore);
                }
            }
        }

        public PopupBaseEdit ColumnFilterPopup
        {
            get => 
                this.columnFilterPopupCore;
            set
            {
                if (!ReferenceEquals(this.columnFilterPopupCore, value))
                {
                    PopupBaseEdit columnFilterPopupCore = this.columnFilterPopupCore;
                    this.columnFilterPopupCore = value;
                    this.OnColumnFilterPopupChanged(columnFilterPopupCore);
                    this.UpdateColumnFilterPopupContainer();
                }
            }
        }

        protected FrameworkElement SortIndicator
        {
            get => 
                this.sortIndicatorCore;
            private set
            {
                if (!ReferenceEquals(this.sortIndicatorCore, value))
                {
                    this.sortIndicatorCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        private bool IsPopupOpen =>
            (this.ColumnFilterPopup != null) && this.ColumnFilterPopup.IsPopupOpen;

        double IColumnPropertyOwner.ActualWidth =>
            base.ActualWidth;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseGridColumnHeader.<>c <>9 = new BaseGridColumnHeader.<>c();
            public static Action<ContentControl> <>9__54_0;
            public static Action<PopupBaseEdit> <>9__69_0;

            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseGridColumnHeader) d).OnColumnFilterPopupStyleChanged();
            }

            internal void <.cctor>b__8_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseGridColumnHeader) d).UpdateIsFilterButtonVisible();
            }

            internal void <OnColumnFilterPopupContainerChanged>b__54_0(ContentControl x)
            {
                x.Content = null;
            }

            internal void <UpdateColumnFilterPopup>b__69_0(PopupBaseEdit x)
            {
                x.Visibility = Visibility.Collapsed;
            }
        }
    }
}

