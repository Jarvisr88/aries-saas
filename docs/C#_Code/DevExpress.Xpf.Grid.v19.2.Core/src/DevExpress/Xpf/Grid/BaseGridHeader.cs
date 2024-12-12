namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class BaseGridHeader : GridColumnHeaderBase, IColumnPropertyOwner, ISupportDragDropColumnHeader, ISupportDragDrop, ISupportDragDropPlatformIndependent
    {
        public static readonly DependencyProperty GridColumnProperty;
        public static readonly DependencyProperty DropPlaceOrientationProperty;
        public static readonly DependencyProperty DragElementTemplateProperty;
        public static readonly DependencyProperty CanGroupMergeStateProperty;
        public static readonly DependencyProperty IsGroupPanelProperty;
        public static readonly DependencyProperty IsColumnChooserProperty;
        public static readonly DependencyProperty ExtendedColumnChooserViewProperty;
        internal const string DragElementTemplatePropertyName = "DragElementTemplate";
        private DevExpress.Xpf.Grid.BaseColumn draggedColumnCore;
        private DragDropElementHelper dragDropHelper;
        internal const string ColumnHeaderContentTemplateName = "PART_LayoutPanel";
        internal const string ColumnHeaderLayoutTemplateName = "PART_Layout";
        private ColumnHeaderDockPanel layoutPanelCore;
        private System.Windows.Controls.TextBlock textBlockCore;
        private DevExpress.Xpf.Editors.CheckEdit checkEditCore;
        private ContentControl headerPresenterCore;
        private UIElement actualContentContainerCore;
        private Thumb headerGripperCore;
        private ContentPresenter headerCustomizationAreaCore;
        private System.Windows.Controls.Image imageCore;
        private ColumnHeaderLayoutBase columnHeaderLayoutCore;
        private double? defaultWidth;
        private bool useInnerLayoutHeaderPresenter;

        static BaseGridHeader()
        {
            Type ownerType = typeof(BaseGridHeader);
            GridColumnProperty = DependencyPropertyManager.RegisterAttached("GridColumn", typeof(DevExpress.Xpf.Grid.BaseColumn), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BaseGridHeader.OnGridColumnChanged)));
            DropPlaceOrientationProperty = DependencyPropertyManager.RegisterAttached("DropPlaceOrientation", typeof(Orientation), ownerType, new PropertyMetadata(Orientation.Horizontal));
            DragElementTemplateProperty = DependencyPropertyManager.RegisterAttached("DragElementTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            CanGroupMergeStateProperty = DependencyProperty.RegisterAttached("CanGroupMergeState", typeof(bool), ownerType, new PropertyMetadata(false));
            IsGroupPanelProperty = DependencyProperty.RegisterAttached("IsGroupPanel", typeof(bool), ownerType, new PropertyMetadata(false));
            IsColumnChooserProperty = DependencyProperty.RegisterAttached("IsColumnChooser", typeof(bool), ownerType, new PropertyMetadata(false));
            ExtendedColumnChooserViewProperty = DependencyProperty.RegisterAttached("ExtendedColumnChooserView", typeof(IExtendedColumnChooserView), ownerType, new PropertyMetadata(null));
        }

        public BaseGridHeader()
        {
            Action<BaseGridHeader, object, ColumnContentChangedEventArgs> onEventAction = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Action<BaseGridHeader, object, ColumnContentChangedEventArgs> local1 = <>c.<>9__24_0;
                onEventAction = <>c.<>9__24_0 = (owner, o, e) => owner.OnColumnContentChanged(o, e);
            }
            this.ColumnContentChangedEventHandler = new ColumnContentChangedEventHandler<BaseGridHeader>(this, onEventAction, <>c.<>9__24_1 ??= delegate (WeakEventHandler<BaseGridHeader, ColumnContentChangedEventArgs, DevExpress.Xpf.Editors.ColumnContentChangedEventHandler> h, object e) {
                ((DevExpress.Xpf.Grid.BaseColumn) e).ContentChanged -= h.Handler;
            });
            this.UpdateDragDropHeaderContent();
        }

        internal bool CanStartDrag(DependencyObject element)
        {
            if ((this.GridView == null) || !this.GridView.CommitEditing())
            {
                return false;
            }
            if (!this.BaseColumn.CanStartDragSingleColumn && (LayoutHelper.IsChildElement(this, element) && (this.HeaderPresenterType == DevExpress.Xpf.Grid.HeaderPresenterType.Headers)))
            {
                return false;
            }
            if (!this.BaseColumn.ActualAllowMoving)
            {
                return false;
            }
            DataControlBase ownerControl = this.Column?.OwnerControl;
            if ((ownerControl != null) && DesignerProperties.GetIsInDesignMode(ownerControl))
            {
                using (IEnumerator enumerator = ownerControl.ColumnsCore.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ColumnBase current = (ColumnBase) enumerator.Current;
                        if (current.IsAutoGenerated)
                        {
                            return false;
                        }
                    }
                }
            }
            return this.GridView.CanStartDrag(element as GridColumnHeaderBase);
        }

        protected virtual XPFContentControl CreateCustomHeaderPresenter() => 
            new XPFContentControl();

        protected virtual FrameworkElement CreateDesignTimeSelectionControl() => 
            new Control();

        protected virtual DragDropElementHelper CreateDragDropHelper() => 
            new GridDragDropElementHelper(this, true);

        protected virtual IDragElement CreateDragElement(System.Windows.Point offset) => 
            this.GridView.CreateDragElement(this, offset);

        protected internal virtual DependencyObject CreateDragElementDataContext() => 
            null;

        protected virtual IDropTarget CreateEmptyDropTargetCore() => 
            this.GridView?.CreateEmptyDropTarget();

        protected virtual Thumb CreateGripper() => 
            new Thumb();

        protected virtual ContentPresenter CreateHeaderCustomizationArea() => 
            new ContentPresenter();

        protected virtual System.Windows.Controls.TextBlock CreateTextBlock()
        {
            System.Windows.Controls.TextBlock block1 = new System.Windows.Controls.TextBlock();
            block1.Name = "PART_Content";
            System.Windows.Controls.TextBlock element = block1;
            element.TextTrimming = TextTrimming.CharacterEllipsis;
            TextBlockService.SetAllowIsTextTrimmed(element, true);
            TextBlockService.AddIsTextTrimmedChangedHandler(element, new RoutedEventHandler(this.OnIsTextTrimmedChanged));
            return element;
        }

        bool ISupportDragDrop.CanStartDrag(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        IDragElement ISupportDragDrop.CreateDragElement(System.Windows.Point offset)
        {
            this.DraggedColumn = this.BaseColumn;
            return this.CreateDragElement(offset);
        }

        IDropTarget ISupportDragDrop.CreateEmptyDropTarget() => 
            this.CreateEmptyDropTargetCore();

        IEnumerable<UIElement> ISupportDragDrop.GetTopLevelDropContainers() => 
            this.RootGridView.GetTopLevelDropContainers();

        bool ISupportDragDrop.IsCompatibleDropTargetFactory(IDropTargetFactory factory, UIElement dropTargetElement) => 
            this.IsCompatibleDropTargetFactoryCore(factory, dropTargetElement);

        void ISupportDragDropColumnHeader.UpdateLocation(IndependentMouseEventArgs e)
        {
            this.GridView.ViewBehavior.UpdateLastPostition(e);
        }

        bool ISupportDragDropPlatformIndependent.CanStartDrag(object sender, IndependentMouseButtonEventArgs e) => 
            this.CanStartDrag((DependencyObject) sender);

        FixedStyle IColumnPropertyOwner.GetActualFixedStyle() => 
            ((this.BaseColumn == null) || (this.GridView == null)) ? FixedStyle.None : (((this.BaseColumn.ParentBandInternal == null) || ((this.GridView.DataControl == null) || (this.GridView.DataControl.BandsLayoutCore == null))) ? this.BaseColumn.Fixed : this.GridView.DataControl.BandsLayoutCore.GetRootBand(this.BaseColumn.ParentBandInternal).Fixed);

        protected object GetActualHeaderContent(object caption) => 
            ((caption == null) || ((caption is string) && string.IsNullOrEmpty(caption as string))) ? " " : caption;

        public static bool GetCanGroupMergeState(DependencyObject element) => 
            (element != null) ? ((bool) element.GetValue(CanGroupMergeStateProperty)) : false;

        private object GetCaption() => 
            ((this.HeaderPresenterType != DevExpress.Xpf.Grid.HeaderPresenterType.ColumnChooser) || (this.Column == null)) ? this.BaseColumn.HeaderCaption : this.Column.ActualColumnChooserHeaderCaption;

        private GridColumnData GetColumnData() => 
            ((this.Column == null) || ((this.Column.View == null) || (this.Column.View.HeadersData == null))) ? null : this.Column.View.HeadersData.GetCellDataByColumn(this.Column);

        protected virtual FrameworkElement GetDragDropTopVisual() => 
            LayoutHelper.GetTopLevelVisual(this.RootGridView);

        public static DataTemplate GetDragElementTemplate(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (DataTemplate) element.GetValue(DragElementTemplateProperty);
        }

        public static Orientation GetDropPlaceOrientation(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Orientation) element.GetValue(DropPlaceOrientationProperty);
        }

        public static IExtendedColumnChooserView GetExtendedColumnChooserView(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (IExtendedColumnChooserView) element.GetValue(ExtendedColumnChooserViewProperty);
        }

        public static DevExpress.Xpf.Grid.BaseColumn GetGridColumn(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (DevExpress.Xpf.Grid.BaseColumn) element.GetValue(GridColumnProperty);
        }

        public static bool GetIsColumnChooser(DependencyObject element) => 
            (element != null) ? ((bool) element.GetValue(IsColumnChooserProperty)) : false;

        public static bool GetIsGroupPanel(DependencyObject element) => 
            (element != null) ? ((bool) element.GetValue(IsGroupPanelProperty)) : false;

        internal virtual void InitInnerLayoutElements()
        {
            if (this.ColumnHeaderLayout != null)
            {
                this.ColumnHeaderLayout.LayoutPanel.Do<ColumnHeaderDockPanel>(x => this.LayoutPanel = x);
                this.ColumnHeaderLayout.HeaderPresenter.Do<ContentControl>(delegate (ContentControl x) {
                    this.HeaderPresenter = x;
                    this.useInnerLayoutHeaderPresenter = true;
                    this.UpdateHeaderPresenter();
                });
                this.ColumnHeaderLayout.HeaderGripper.Do<Thumb>(delegate (Thumb x) {
                    this.HeaderGripper = x;
                    this.UpdateResizeHelper();
                    this.UpdateGripper();
                });
            }
        }

        protected virtual bool IsCompatibleDropTargetFactoryCore(IDropTargetFactory factory, UIElement dropTargetElement) => 
            (factory is GridDropTargetFactoryBase) && ((GridDropTargetFactoryBase) factory).IsCompatibleDropTargetFactory(dropTargetElement, this);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ResetPanelChildren();
            this.ColumnHeaderLayout = base.GetTemplateChild("PART_Layout") as ColumnHeaderLayoutBase;
            if (this.ColumnHeaderLayout == null)
            {
                this.LayoutPanel = base.GetTemplateChild("PART_LayoutPanel") as ColumnHeaderDockPanel;
            }
            this.ContentBorder = base.GetTemplateChild("PART_Border") as Decorator;
            this.UpdateHeaderPresenter();
            this.UpdateImage();
            this.UpdateHeaderCustomizationArea();
            this.UpdateHeaderContainer();
            this.UpdateDesignTimeSelectionControl();
        }

        protected virtual void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
        {
            if (ReferenceEquals(e.Property, ColumnBase.ActualHeaderCustomizationAreaTemplateSelectorProperty))
            {
                this.UpdateHeaderCustomizationArea();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ActualAllowResizingProperty) || ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.FixedProperty))
            {
                this.UpdateGripper();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HeaderCaptionProperty) || (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ActualHeaderTemplateSelectorProperty) || (ReferenceEquals(e.Property, ColumnBase.ActualColumnChooserHeaderCaptionProperty) || ReferenceEquals(e.Property, ColumnBase.ActualColumnHeaderContentStyleProperty))))
            {
                this.UpdateHeaderPresenter();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HorizontalHeaderContentAlignmentProperty))
            {
                this.UpdateHeaderContainer();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ImageAlignmentProperty))
            {
                this.UpdateHeaderPresenter();
                this.UpdateHeaderContainer();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ImageProperty))
            {
                this.UpdateImage();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HeaderToolTipProperty) || ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ActualHeaderToolTipTemplateProperty))
            {
                this.UpdateTooltip();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ColumnPositionProperty))
            {
                this.UpdateColumnPosition();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HasTopElementProperty))
            {
                this.UpdateHasTopElement();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HasBottomElementProperty))
            {
                this.UpdateHasBottomElement();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HasRightSiblingProperty))
            {
                this.UpdateHasRightSiblingState();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.HasLeftSiblingProperty))
            {
                this.UpdateHasEmptySiblingState();
            }
            if (ReferenceEquals(e.Property, DevExpress.Xpf.Grid.BaseColumn.ActualHeaderStyleProperty))
            {
                this.UpdateHeaderStyleProperty();
            }
            if (ReferenceEquals(e.Property, ColumnBase.ActualShowCheckBoxInHeaderProperty))
            {
                this.UpdateShowCheckBoxInHeader();
            }
            if (ReferenceEquals(e.Property, ColumnBase.AllowEditingProperty))
            {
                this.UpdateEnableShowCheckBoxInHeader();
            }
        }

        private void OnColumnHeaderLayoutChanged(ColumnHeaderLayoutBase old)
        {
            Action<ColumnHeaderLayoutBase> action = <>c.<>9__132_0;
            if (<>c.<>9__132_0 == null)
            {
                Action<ColumnHeaderLayoutBase> local1 = <>c.<>9__132_0;
                action = <>c.<>9__132_0 = x => x.Header = null;
            }
            old.Do<ColumnHeaderLayoutBase>(action);
            this.ColumnHeaderLayout.Do<ColumnHeaderLayoutBase>(x => x.Header = this);
            this.UpdateDragDropHeaderContent();
        }

        protected virtual void OnDraggedColumnChanged(DevExpress.Xpf.Grid.BaseColumn oldValue, DevExpress.Xpf.Grid.BaseColumn newValue)
        {
        }

        protected virtual void OnGridColumnChanged(DevExpress.Xpf.Grid.BaseColumn oldValue)
        {
            if (oldValue != null)
            {
                oldValue.ContentChanged -= this.ColumnContentChangedEventHandler.Handler;
            }
            if (this.BaseColumn != null)
            {
                this.BaseColumn.ContentChanged += this.ColumnContentChangedEventHandler.Handler;
            }
            this.UpdateGripper();
            this.UpdateHeaderPresenter();
            this.UpdateImage();
            this.UpdateTooltip();
            this.UpdateColumnPosition();
            this.UpdateHasTopElement();
            this.UpdateHasBottomElement();
            this.UpdateHasRightSiblingState();
            this.UpdateHasEmptySiblingState();
            this.UpdateHeaderCustomizationArea();
            this.UpdateHeaderContainer();
            this.UpdateHeaderStyleProperty();
            this.UpdateShowCheckBoxInHeader();
        }

        private static void OnGridColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseGridHeader header = d as BaseGridHeader;
            if (header != null)
            {
                header.OnGridColumnChanged((DevExpress.Xpf.Grid.BaseColumn) e.OldValue);
            }
        }

        private void OnIsTextTrimmedChanged(object o, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBlock element = o as System.Windows.Controls.TextBlock;
            if (element != null)
            {
                object headerToolTip;
                DevExpress.Xpf.Grid.BaseColumn baseColumn = this.BaseColumn;
                if (baseColumn != null)
                {
                    headerToolTip = baseColumn.HeaderToolTip;
                }
                else
                {
                    DevExpress.Xpf.Grid.BaseColumn local1 = baseColumn;
                    headerToolTip = null;
                }
                if (headerToolTip == null)
                {
                    ToolTipService.SetToolTip(element, TextBlockService.GetIsTextTrimmed(element) ? element.Text : DependencyProperty.UnsetValue);
                }
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateGripper();
            this.DragDropHelper ??= this.CreateDragDropHelper();
        }

        protected virtual void ResetPanelChildren()
        {
            this.LayoutPanel = null;
            this.ColumnHeaderLayout = null;
            this.TextBlock = null;
            this.HeaderPresenter = null;
            this.HeaderGripper = null;
            this.HeaderCustomizationArea = null;
            this.ActualContentContainer = null;
            this.useInnerLayoutHeaderPresenter = false;
        }

        public static void SetCanGroupMergeState(DependencyObject element, bool value)
        {
            if (element != null)
            {
                element.SetValue(CanGroupMergeStateProperty, value);
            }
        }

        public static void SetDragElementTemplate(DependencyObject element, DataTemplate value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(DragElementTemplateProperty, value);
        }

        public static void SetDropPlaceOrientation(DependencyObject element, Orientation value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(DropPlaceOrientationProperty, value);
        }

        [Browsable(false)]
        public static void SetExtendedColumnChooserView(DependencyObject element, IExtendedColumnChooserView value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ExtendedColumnChooserViewProperty, value);
        }

        public static void SetGridColumn(DependencyObject element, DevExpress.Xpf.Grid.BaseColumn value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(GridColumnProperty, value);
        }

        protected internal override void SetIsBestFitElement()
        {
            if (this.CheckEdit != null)
            {
                this.CheckEdit.Margin = new Thickness(this.CheckEdit.Margin.Left, this.CheckEdit.Margin.Top, this.CheckEdit.Margin.Right + 7.0, this.CheckEdit.Margin.Bottom);
            }
        }

        public static void SetIsColumnChooser(DependencyObject element, bool value)
        {
            if (element != null)
            {
                element.SetValue(IsColumnChooserProperty, value);
            }
        }

        public static void SetIsGroupPanel(DependencyObject element, bool value)
        {
            if (element != null)
            {
                element.SetValue(IsGroupPanelProperty, value);
            }
        }

        private bool ShouldCreateHeaderGripper() => 
            base.IsMouseOver && (this.HeaderPresenterType == DevExpress.Xpf.Grid.HeaderPresenterType.Headers);

        private void UpdateActualContentContainer()
        {
            if (this.HeaderPresenter != null)
            {
                this.ActualContentContainer = this.HeaderPresenter;
            }
            else
            {
                this.ActualContentContainer = this.TextBlock;
            }
        }

        protected void UpdateColumnPosition()
        {
            if ((this.BaseColumn != null) && this.CanSyncColumnPosition)
            {
                base.ColumnPosition = this.BaseColumn.ColumnPosition;
            }
        }

        private void UpdateCustomHeaderPresenter()
        {
            this.HeaderPresenter ??= this.CreateCustomHeaderPresenter();
            if (this.TextBlock != null)
            {
                this.TextBlock = null;
            }
            this.HeaderPresenter.Content = this.GetActualHeaderContent(this.GetCaption());
            this.HeaderPresenter.ContentTemplateSelector = this.BaseColumn.ActualHeaderTemplateSelector;
            if (this.Column != null)
            {
                this.HeaderPresenter.Style = this.Column.ActualColumnHeaderContentStyle;
            }
            SetGridColumn(this.HeaderPresenter, this.BaseColumn);
        }

        protected override void UpdateDesignTimeSelectionControl()
        {
            if (((base.DesignTimeSelectionControl != null) || GetIsSelectedInDesignTime(this)) && (this.ContentBorder != null))
            {
                if ((base.DesignTimeSelectionControl == null) && GetIsSelectedInDesignTime(this))
                {
                    base.DesignTimeSelectionControl = this.CreateDesignTimeSelectionControl();
                    this.ContentBorder.Child = base.DesignTimeSelectionControl;
                }
                else if ((base.DesignTimeSelectionControl != null) && !GetIsSelectedInDesignTime(this))
                {
                    this.ContentBorder.Child = null;
                    base.DesignTimeSelectionControl = null;
                }
            }
        }

        private void UpdateDragDropHeaderContent()
        {
            FrameworkElement layoutPanel = this.LayoutPanel;
            layoutPanel ??= this.ColumnHeaderLayout;
            layoutPanel ??= this;
            this.HeaderContent = layoutPanel;
        }

        private void UpdateEnableShowCheckBoxInHeader()
        {
            if ((this.Column != null) && (this.CheckEdit != null))
            {
                this.CheckEdit.IsEnabled = this.Column.GetAllowEditing();
            }
        }

        private void UpdateGripper()
        {
            if (this.BaseColumn != null)
            {
                if ((this.HeaderGripper == null) && this.ShouldCreateHeaderGripper())
                {
                    this.HeaderGripper = this.CreateGripper();
                    this.HeaderGripper.Cursor = Cursors.SizeWE;
                    this.UpdateResizeHelper();
                }
                if (this.HeaderGripper != null)
                {
                    this.HeaderGripper.HorizontalAlignment = (((IColumnPropertyOwner) this).GetActualFixedStyle() == FixedStyle.Right) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
                    if (this.BaseColumn != null)
                    {
                        this.HeaderGripper.Margin = new Thickness(0.0, 0.0, -this.BaseColumn.ActualBandRightSeparatorWidthCore, 0.0);
                        if (this.defaultWidth == null)
                        {
                            this.defaultWidth = new double?(this.HeaderGripper.ActualWidth);
                        }
                        this.HeaderGripper.Width = this.defaultWidth.Value + this.BaseColumn.ActualBandRightSeparatorWidthCore;
                    }
                    this.HeaderGripper.Visibility = this.BaseColumn.ActualAllowResizing ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void UpdateHasBottomElement()
        {
            if (this.BaseColumn != null)
            {
                base.HasBottomElement = this.BaseColumn.HasBottomElement;
            }
        }

        protected override void UpdateHasEmptySiblingState()
        {
            if (this.BaseColumn != null)
            {
                this.HasLeftSibling = (base.ColumnPosition == ColumnPosition.Left) && this.BaseColumn.HasLeftSibling;
            }
        }

        protected virtual void UpdateHasRightSiblingState()
        {
            if (this.BaseColumn != null)
            {
                this.HasRightSibling = (this.HeaderPresenterType == DevExpress.Xpf.Grid.HeaderPresenterType.Headers) && this.BaseColumn.HasRightSibling;
            }
        }

        protected virtual void UpdateHasTopElement()
        {
            if (this.BaseColumn != null)
            {
                this.HasTopElement = (this.HeaderPresenterType == DevExpress.Xpf.Grid.HeaderPresenterType.Headers) ? this.BaseColumn.HasTopElement : false;
            }
        }

        protected virtual void UpdateHeaderContainer()
        {
            if (this.BaseColumn != null)
            {
                HorizontalAlignment alignment = this.BaseColumn.HorizontalHeaderContentAlignment;
                this.LayoutPanel.Do<ColumnHeaderDockPanel>(delegate (ColumnHeaderDockPanel x) {
                    if (ColumnBase.GetHeaderPresenterType(this.LayoutPanel) == DevExpress.Xpf.Grid.HeaderPresenterType.ColumnChooser)
                    {
                        alignment = HorizontalAlignment.Left;
                    }
                    x.ContainerAlignment = alignment;
                    x.ImageAlignment = this.BaseColumn.ImageAlignment;
                });
                this.HeaderPresenter.Do<ContentControl>(x => x.HorizontalAlignment = alignment);
            }
        }

        private void UpdateHeaderCustomizationArea()
        {
            if (this.Column != null)
            {
                if (this.UseDefaultTemplate((ActualTemplateSelectorWrapper) this.Column.ActualHeaderCustomizationAreaTemplateSelector) || (this.HeaderPresenterType == DevExpress.Xpf.Grid.HeaderPresenterType.ColumnChooser))
                {
                    this.HeaderCustomizationArea = null;
                }
                else
                {
                    this.HeaderCustomizationArea ??= this.CreateHeaderCustomizationArea();
                    this.HeaderCustomizationArea.Content = this.GetColumnData();
                    this.HeaderCustomizationArea.ContentTemplateSelector = this.Column.ActualHeaderCustomizationAreaTemplateSelector;
                }
            }
        }

        protected void UpdateHeaderPresenter()
        {
            if (this.BaseColumn != null)
            {
                if (this.BaseColumn.ImageAlignment == StringAlignment.Center)
                {
                    this.TextBlock = null;
                    this.HeaderPresenter = null;
                }
                else
                {
                    if (this.UseDefaultTemplate((ActualTemplateSelectorWrapper) this.BaseColumn.ActualHeaderTemplateSelector))
                    {
                        Func<ColumnBase, Style> evaluator = <>c.<>9__141_0;
                        if (<>c.<>9__141_0 == null)
                        {
                            Func<ColumnBase, Style> local1 = <>c.<>9__141_0;
                            evaluator = <>c.<>9__141_0 = x => x.ActualColumnHeaderContentStyle;
                        }
                        if ((this.Column.With<ColumnBase, Style>(evaluator) == null) && (!this.useInnerLayoutHeaderPresenter && (this.BaseColumn.Image == null)))
                        {
                            this.UpdateTextBlock();
                            return;
                        }
                    }
                    this.UpdateCustomHeaderPresenter();
                }
            }
        }

        protected void UpdateHeaderStyleProperty()
        {
            if (this.BaseColumn != null)
            {
                if (this.BaseColumn.ActualHeaderStyle == null)
                {
                    base.ClearValue(FrameworkElement.StyleProperty);
                }
                else
                {
                    base.Style = this.BaseColumn.ActualHeaderStyle;
                }
            }
        }

        private void UpdateImage()
        {
            Func<DevExpress.Xpf.Grid.BaseColumn, ImageSource> evaluator = <>c.<>9__137_0;
            if (<>c.<>9__137_0 == null)
            {
                Func<DevExpress.Xpf.Grid.BaseColumn, ImageSource> local1 = <>c.<>9__137_0;
                evaluator = <>c.<>9__137_0 = x => x.Image;
            }
            ImageSource source = this.BaseColumn.With<DevExpress.Xpf.Grid.BaseColumn, ImageSource>(evaluator);
            Visibility visibility = (source == null) ? Visibility.Collapsed : Visibility.Visible;
            if ((visibility != Visibility.Collapsed) && (this.Image == null))
            {
                this.Image = new DXImage();
                this.Image.Name = "PART_Image";
            }
            if (this.Image != null)
            {
                SetGridColumn(this.Image, this.BaseColumn);
                this.Image.Source = source;
                this.Image.Visibility = visibility;
                Func<DevExpress.Xpf.Grid.BaseColumn, Style> func2 = <>c.<>9__137_1;
                if (<>c.<>9__137_1 == null)
                {
                    Func<DevExpress.Xpf.Grid.BaseColumn, Style> local2 = <>c.<>9__137_1;
                    func2 = <>c.<>9__137_1 = x => x.ActualHeaderImageStyle;
                }
                this.Image.Style = this.BaseColumn.With<DevExpress.Xpf.Grid.BaseColumn, Style>(func2);
            }
        }

        protected virtual void UpdateLayoutPanel()
        {
            if (this.LayoutPanel != null)
            {
                this.LayoutPanel.HeaderPresenter = this.ActualContentContainer;
                this.LayoutPanel.HeaderGripper = this.HeaderGripper;
                this.LayoutPanel.HeaderCustomizationArea = this.HeaderCustomizationArea;
                this.LayoutPanel.Image = this.Image;
                this.LayoutPanel.CheckEdit = this.CheckEdit;
                this.UpdateDragDropHeaderContent();
                this.UpdateHeaderContainer();
            }
        }

        private void UpdateResizeHelper()
        {
            if (this.HeaderGripper != null)
            {
                new ResizeHelper(new ColumnResizeHelperOwner(this)).Init(this.HeaderGripper);
            }
        }

        private void UpdateShowCheckBoxInHeader()
        {
            if (this.Column != null)
            {
                Visibility visibility = ((this.HeaderPresenterType != DevExpress.Xpf.Grid.HeaderPresenterType.Headers) || !this.Column.ActualShowCheckBoxInHeader) ? Visibility.Collapsed : Visibility.Visible;
                if ((visibility != Visibility.Collapsed) && (this.CheckEdit == null))
                {
                    DevExpress.Xpf.Editors.CheckEdit edit1 = new DevExpress.Xpf.Editors.CheckEdit();
                    edit1.Margin = new Thickness(0.0, -5.0, 0.0, -5.0);
                    this.CheckEdit = edit1;
                    this.CheckEdit.Name = "PART_CheckEdit";
                }
                if (this.CheckEdit != null)
                {
                    BindingOperations.ClearBinding(this.CheckEdit, DevExpress.Xpf.Editors.CheckEdit.IsCheckedProperty);
                    Binding binding1 = new Binding(ColumnBase.IsCheckedProperty.Name);
                    binding1.Source = this.Column;
                    Binding binding = binding1;
                    this.CheckEdit.SetBinding(DevExpress.Xpf.Editors.CheckEdit.IsCheckedProperty, binding);
                    SetGridColumn(this.CheckEdit, this.BaseColumn);
                    this.CheckEdit.Visibility = visibility;
                    this.UpdateEnableShowCheckBoxInHeader();
                }
            }
        }

        private void UpdateTextBlock()
        {
            this.TextBlock ??= this.CreateTextBlock();
            if (this.HeaderPresenter != null)
            {
                this.HeaderPresenter = null;
            }
            this.TextBlock.Text = this.GetActualHeaderContent(this.GetCaption()).ToString();
            this.TextBlock.VerticalAlignment = VerticalAlignment.Center;
        }

        protected void UpdateTooltip()
        {
            ToolTip tip = null;
            if ((this.BaseColumn != null) && ((this.BaseColumn.HeaderToolTip != null) || (this.BaseColumn.ActualHeaderToolTipTemplate != null)))
            {
                ToolTip tip1 = new ToolTip();
                tip1.Content = this.BaseColumn.HeaderToolTip;
                tip1.ContentTemplate = this.BaseColumn.ActualHeaderToolTipTemplate;
                tip = tip1;
            }
            ToolTipService.SetToolTip(this, tip);
        }

        protected bool UseDefaultTemplate(ActualTemplateSelectorWrapper wrapper) => 
            ((wrapper.Template is DefaultDataTemplate) || (wrapper.Template == null)) ? ReferenceEquals(wrapper.Selector, null) : false;

        protected ColumnContentChangedEventHandler<BaseGridHeader> ColumnContentChangedEventHandler { get; set; }

        internal DevExpress.Xpf.Grid.BaseColumn BaseColumn =>
            GetGridColumn(this);

        internal ColumnBase Column =>
            this.BaseColumn as ColumnBase;

        internal DataViewBase GridView =>
            this.BaseColumn.ResizeOwner as DataViewBase;

        internal DataViewBase RootGridView =>
            this.GridView.RootView;

        protected DevExpress.Xpf.Grid.HeaderPresenterType HeaderPresenterType =>
            ColumnBase.GetHeaderPresenterType(this);

        internal FrameworkElement HeaderContent { get; private set; }

        protected internal virtual bool CanSyncColumnPosition { get; set; }

        FrameworkElement ISupportDragDrop.SourceElement =>
            this;

        bool ISupportDragDropColumnHeader.SkipHitTestVisibleChecking =>
            (this.BaseColumn != null) && ((this.BaseColumn.View != null) && this.BaseColumn.View.DataControl.DesignTimeAdorner.IsDesignTime);

        internal DevExpress.Xpf.Grid.BaseColumn DraggedColumn
        {
            get => 
                this.draggedColumnCore;
            set
            {
                if (!ReferenceEquals(this.draggedColumnCore, value))
                {
                    DevExpress.Xpf.Grid.BaseColumn draggedColumnCore = this.draggedColumnCore;
                    this.draggedColumnCore = value;
                    this.OnDraggedColumnChanged(draggedColumnCore, this.draggedColumnCore);
                }
            }
        }

        FrameworkElement ISupportDragDropColumnHeader.RelativeDragElement =>
            this.HeaderContent;

        FrameworkElement ISupportDragDropColumnHeader.TopVisual =>
            this.GetDragDropTopVisual();

        internal DragDropElementHelper DragDropHelper
        {
            get => 
                this.dragDropHelper;
            set
            {
                if (this.dragDropHelper != null)
                {
                    this.dragDropHelper.Destroy();
                }
                this.dragDropHelper = value;
            }
        }

        DevExpress.Xpf.Grid.BaseColumn IColumnPropertyOwner.Column =>
            this.BaseColumn;

        protected Decorator ContentBorder { get; private set; }

        protected ColumnHeaderDockPanel LayoutPanel
        {
            get => 
                this.layoutPanelCore;
            private set
            {
                if (!ReferenceEquals(this.layoutPanelCore, value))
                {
                    this.layoutPanelCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        protected System.Windows.Controls.TextBlock TextBlock
        {
            get => 
                this.textBlockCore;
            private set
            {
                if (!ReferenceEquals(this.textBlockCore, value))
                {
                    this.textBlockCore = value;
                    this.UpdateActualContentContainer();
                }
            }
        }

        protected DevExpress.Xpf.Editors.CheckEdit CheckEdit
        {
            get => 
                this.checkEditCore;
            private set
            {
                if (!ReferenceEquals(this.checkEditCore, value))
                {
                    this.checkEditCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        protected ContentControl HeaderPresenter
        {
            get => 
                this.headerPresenterCore;
            private set
            {
                if (!ReferenceEquals(this.headerPresenterCore, value))
                {
                    this.headerPresenterCore = value;
                    this.UpdateActualContentContainer();
                }
            }
        }

        private UIElement ActualContentContainer
        {
            get => 
                this.actualContentContainerCore;
            set
            {
                if (!ReferenceEquals(this.actualContentContainerCore, value))
                {
                    this.actualContentContainerCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        protected Thumb HeaderGripper
        {
            get => 
                this.headerGripperCore;
            private set
            {
                if (!ReferenceEquals(this.headerGripperCore, value))
                {
                    this.headerGripperCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        protected ContentPresenter HeaderCustomizationArea
        {
            get => 
                this.headerCustomizationAreaCore;
            private set
            {
                if (!ReferenceEquals(this.headerCustomizationAreaCore, value))
                {
                    this.headerCustomizationAreaCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        private System.Windows.Controls.Image Image
        {
            get => 
                this.imageCore;
            set
            {
                if (!ReferenceEquals(this.imageCore, value))
                {
                    this.imageCore = value;
                    this.UpdateLayoutPanel();
                }
            }
        }

        protected ColumnHeaderLayoutBase ColumnHeaderLayout
        {
            get => 
                this.columnHeaderLayoutCore;
            private set
            {
                if (!ReferenceEquals(this.columnHeaderLayoutCore, value))
                {
                    ColumnHeaderLayoutBase columnHeaderLayoutCore = this.columnHeaderLayoutCore;
                    this.columnHeaderLayoutCore = value;
                    this.OnColumnHeaderLayoutChanged(columnHeaderLayoutCore);
                }
            }
        }

        double IColumnPropertyOwner.ActualWidth =>
            base.ActualWidth;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseGridHeader.<>c <>9 = new BaseGridHeader.<>c();
            public static Action<BaseGridHeader, object, ColumnContentChangedEventArgs> <>9__24_0;
            public static Action<WeakEventHandler<BaseGridHeader, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler>, object> <>9__24_1;
            public static Action<ColumnHeaderLayoutBase> <>9__132_0;
            public static Func<BaseColumn, ImageSource> <>9__137_0;
            public static Func<BaseColumn, Style> <>9__137_1;
            public static Func<ColumnBase, Style> <>9__141_0;

            internal void <.ctor>b__24_0(BaseGridHeader owner, object o, ColumnContentChangedEventArgs e)
            {
                owner.OnColumnContentChanged(o, e);
            }

            internal void <.ctor>b__24_1(WeakEventHandler<BaseGridHeader, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler> h, object e)
            {
                ((BaseColumn) e).ContentChanged -= h.Handler;
            }

            internal void <OnColumnHeaderLayoutChanged>b__132_0(ColumnHeaderLayoutBase x)
            {
                x.Header = null;
            }

            internal Style <UpdateHeaderPresenter>b__141_0(ColumnBase x) => 
                x.ActualColumnHeaderContentStyle;

            internal ImageSource <UpdateImage>b__137_0(BaseColumn x) => 
                x.Image;

            internal Style <UpdateImage>b__137_1(BaseColumn x) => 
                x.ActualHeaderImageStyle;
        }
    }
}

