namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;

    public class BaseSplitterItem : Thumb
    {
        public static readonly DependencyProperty DragIncrementProperty;
        public static readonly DependencyProperty KeyboardIncrementProperty;
        public static readonly DependencyProperty OrientationProperty;
        private DevExpress.Xpf.Docking.ResizeData _resizeData;
        private LayoutResizingPreviewHelper resizePreviewHelper;
        private IResizeCalculator resizeCalculatorCore;

        static BaseSplitterItem()
        {
            DependencyPropertyRegistrator<BaseSplitterItem> registrator = new DependencyPropertyRegistrator<BaseSplitterItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<double>("DragIncrement", ref DragIncrementProperty, 1.0, null, null);
            registrator.Register<double>("KeyboardIncrement", ref KeyboardIncrementProperty, 10.0, null, null);
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, (dObj, e) => ((BaseSplitterItem) dObj).OnOrientationChanged((System.Windows.Controls.Orientation) e.NewValue), null);
        }

        public BaseSplitterItem()
        {
            base.Focusable = false;
            base.IsTabStop = false;
        }

        public virtual void Activate()
        {
            if (!this.IsActivated)
            {
                this.IsActivated = true;
                this.SubscribeThumbEvents();
            }
        }

        private void CancelResize()
        {
            this.SetDefinitionLength(this._resizeData.Definition1, this._resizeData.OriginalDefinition1Length);
            this.SetDefinitionLength(this._resizeData.Definition2, this._resizeData.OriginalDefinition2Length);
            this.ResetResizing();
        }

        private bool CanResize()
        {
            bool isColumns = this.GetEffectiveResizeDirection() == GridResizeDirection.Columns;
            int num = (int) this.GetValue(isColumns ? Grid.ColumnSpanProperty : Grid.RowSpanProperty);
            Grid parentGrid = this.GetParentGrid();
            if ((parentGrid == null) || (num != 1))
            {
                return false;
            }
            int current = this.GetCurrent(isColumns);
            int prev = this.GetPrev(current);
            int next = this.GetNext(current);
            int num5 = isColumns ? parentGrid.ColumnDefinitions.Count : parentGrid.RowDefinitions.Count;
            return ((prev >= 0) && ((prev != current) && ((next < num5) && (next != current))));
        }

        private double CorrectChange(double change)
        {
            double num;
            double num2;
            if (base.FlowDirection != this._resizeData.Grid.FlowDirection)
            {
                change = -change;
            }
            this.GetDeltaConstraints(out num, out num2);
            return Math.Min(Math.Max(change, num), num2);
        }

        public virtual void Deactivate()
        {
            this.UnsubscribeThumbEvents();
            this.IsActivated = false;
        }

        private double GetActualChange(double horizontalChange, double verticalChange)
        {
            double dragIncrement = this.DragIncrement;
            return (Math.Round((double) ((this.IsHorizontal ? horizontalChange : verticalChange) / dragIncrement)) * dragIncrement);
        }

        private double GetActualLength(DefinitionBase definition)
        {
            ColumnDefinition definition2 = definition as ColumnDefinition;
            return ((definition2 == null) ? ((RowDefinition) definition).ActualHeight : definition2.ActualWidth);
        }

        protected virtual int GetCurrent(bool isColumns) => 
            (int) this.GetValue(isColumns ? Grid.ColumnProperty : Grid.RowProperty);

        private void GetDeltaConstraints(out double minDelta, out double maxDelta)
        {
            double actualLength = this.GetActualLength(this._resizeData.Definition1);
            double num2 = this.GetActualLength(this._resizeData.Definition2);
            double num3 = DefinitionsHelper.UserMinSizeValueCache(this._resizeData.Definition1);
            double num4 = DefinitionsHelper.UserMaxSizeValueCache(this._resizeData.Definition1);
            double num5 = DefinitionsHelper.UserMinSizeValueCache(this._resizeData.Definition2);
            double num6 = DefinitionsHelper.UserMaxSizeValueCache(this._resizeData.Definition2);
            if (this._resizeData.SplitterIndex == this._resizeData.Definition1Index)
            {
                num3 = Math.Max(num3, this._resizeData.SplitterLength);
            }
            else if (this._resizeData.SplitterIndex == this._resizeData.Definition2Index)
            {
                num5 = Math.Max(num5, this._resizeData.SplitterLength);
            }
            if (this._resizeData.SplitBehavior == DevExpress.Xpf.Docking.SplitBehavior.Split)
            {
                minDelta = -Math.Min((double) (actualLength - num3), (double) (num6 - num2));
                maxDelta = Math.Min((double) (num4 - actualLength), (double) (num2 - num5));
            }
            else if (this._resizeData.SplitBehavior == DevExpress.Xpf.Docking.SplitBehavior.Resize1)
            {
                minDelta = num3 - actualLength;
                maxDelta = num4 - actualLength;
            }
            else
            {
                minDelta = num2 - num6;
                maxDelta = num2 - num5;
            }
        }

        protected virtual GridResizeDirection GetEffectiveResizeDirection() => 
            (this.LayoutGroup == null) ? GridResizeDirection.Rows : ((this.LayoutGroup.Orientation == System.Windows.Controls.Orientation.Horizontal) ? GridResizeDirection.Columns : GridResizeDirection.Rows);

        private static DefinitionBase GetGridDefinition(Grid grid, int index, GridResizeDirection direction) => 
            (direction == GridResizeDirection.Columns) ? ((DefinitionBase) grid.ColumnDefinitions[index]) : ((DefinitionBase) grid.RowDefinitions[index]);

        private BaseLayoutItem GetItem(DefinitionBase definition, bool isColumn)
        {
            if (((this._resizeData != null) && (this._resizeData.Grid != null)) && (this.LayoutGroup != null))
            {
                int num = isColumn ? this._resizeData.Grid.ColumnDefinitions.IndexOf(definition as ColumnDefinition) : this._resizeData.Grid.RowDefinitions.IndexOf(definition as RowDefinition);
                if (num != -1)
                {
                    return (this.LayoutGroup.ItemsInternal[num] as BaseLayoutItem);
                }
            }
            return null;
        }

        private Tuple<BaseLayoutItem, BaseLayoutItem> GetItemsToResize()
        {
            Tuple<BaseLayoutItem, BaseLayoutItem> tuple = new Tuple<BaseLayoutItem, BaseLayoutItem>(null, null);
            if (this.IsDragStarted)
            {
                DefinitionBase definition = this._resizeData.Definition1;
                DefinitionBase base3 = this._resizeData.Definition2;
                if ((definition != null) && (base3 != null))
                {
                    bool isColumn = this._resizeData.ResizeDirection == GridResizeDirection.Columns;
                    tuple = new Tuple<BaseLayoutItem, BaseLayoutItem>(this.GetItem(definition, isColumn), this.GetItem(base3, isColumn));
                }
            }
            return tuple;
        }

        protected virtual double GetMin(DefinitionBase definition)
        {
            bool isColumn = definition is ColumnDefinition;
            BaseLayoutItem item = this.GetItem(definition, isColumn);
            return (!(item is FixedItem) ? (!(item is LayoutPanel) ? (!(item is TabbedGroup) ? (!(item is LayoutControlItem) ? 12.0 : (isColumn ? ((double) 0x18) : ((double) 10))) : 24.0) : (isColumn ? ((double) 12) : ((double) 0x12))) : 5.0);
        }

        protected virtual int GetNext(int current)
        {
            if (this.LayoutGroup != null)
            {
                int index = this.LayoutGroup.ItemsInternal.IndexOf(this);
                BaseLayoutItem item = this.LayoutGroup.ItemsInternal[index + 1] as BaseLayoutItem;
                for (int i = this.LayoutGroup.Items.IndexOf(item); i < this.LayoutGroup.Items.Count; i++)
                {
                    item = this.LayoutGroup.Items[i];
                    if (IsResizableItem(item, this.IsHorizontal))
                    {
                        return this.LayoutGroup.ItemsInternal.IndexOf(item);
                    }
                }
            }
            return current;
        }

        protected virtual Grid GetParentGrid() => 
            (Grid) LayoutHelper.GetParent(this, false);

        protected virtual int GetPrev(int current)
        {
            if (this.LayoutGroup != null)
            {
                int index = this.LayoutGroup.ItemsInternal.IndexOf(this);
                BaseLayoutItem item = this.LayoutGroup.ItemsInternal[index - 1] as BaseLayoutItem;
                for (int i = this.LayoutGroup.Items.IndexOf(item); i >= 0; i--)
                {
                    item = this.LayoutGroup.Items[i];
                    if (IsResizableItem(item, this.IsHorizontal))
                    {
                        return this.LayoutGroup.ItemsInternal.IndexOf(item);
                    }
                }
            }
            return current;
        }

        private bool InitializeData()
        {
            Grid parentGrid = this.GetParentGrid();
            if (parentGrid != null)
            {
                this._resizeData = new DevExpress.Xpf.Docking.ResizeData();
                this._resizeData.Grid = parentGrid;
                this._resizeData.ResizeDirection = this.GetEffectiveResizeDirection();
                this._resizeData.SplitterLength = Math.Min(base.ActualWidth, base.ActualHeight);
                if (!this.SetupDefinitionsToResize())
                {
                    this._resizeData = null;
                }
                else
                {
                    this.ResizeCalculator.Init(this._resizeData.SplitBehavior);
                    this.ResizeCalculator.Orientation = this.Orientation;
                    if (!this.RedrawContent)
                    {
                        Point start = new Point();
                        this.ResizePreviewHelper.InitResizing(start, this.Manager.GetViewElement(this as IUIElement));
                    }
                }
            }
            return (this._resizeData != null);
        }

        protected static bool IsResizableItem(BaseLayoutItem item, bool isHorizontal) => 
            LayoutItemsHelper.IsResizable(item, isHorizontal) && (item.Visibility != Visibility.Collapsed);

        private static bool IsStar(DefinitionBase definition) => 
            DefinitionsHelper.IsStar(definition);

        internal bool KeyboardMoveSplitter(double horizontalChange, double verticalChange)
        {
            if (this._resizeData != null)
            {
                return false;
            }
            this.InitializeData();
            if (this._resizeData == null)
            {
                return false;
            }
            this.MoveSplitter(this.CorrectChange(this.IsHorizontal ? horizontalChange : verticalChange));
            this.ResetResizing();
            return true;
        }

        private void MoveSplitter(double change)
        {
            using (new NotificationBatch(this.Manager))
            {
                Tuple<BaseLayoutItem, BaseLayoutItem> itemsToResize = this.GetItemsToResize();
                if (((itemsToResize != null) && (itemsToResize.Item1 != null)) && (itemsToResize.Item2 != null))
                {
                    this.ResizeCalculator.Resize(itemsToResize.Item1, itemsToResize.Item2, change);
                    this.NotifySizingAction(this.Manager);
                }
            }
        }

        private void NotifySizingAction(DockLayoutManager manager)
        {
            if ((this.LayoutGroup != null) && (manager != null))
            {
                int current = this.GetCurrent(this.LayoutGroup.Orientation == System.Windows.Controls.Orientation.Horizontal);
                BaseLayoutItem target = this.LayoutGroup.ItemsInternal[current - 1] as BaseLayoutItem;
                BaseLayoutItem item2 = this.LayoutGroup.ItemsInternal[current + 1] as BaseLayoutItem;
                DependencyProperty property = (this.LayoutGroup.Orientation == System.Windows.Controls.Orientation.Horizontal) ? BaseLayoutItem.ItemWidthProperty : BaseLayoutItem.ItemHeightProperty;
                if (target != null)
                {
                    NotificationBatch.Action(manager, target, property);
                }
                if (item2 != null)
                {
                    NotificationBatch.Action(manager, item2, property);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Manager = DockLayoutManager.Ensure(this, false);
            if (this.LayoutGroup != null)
            {
                BindingHelper.SetBinding(this, OrientationProperty, this.LayoutGroup, DevExpress.Xpf.Docking.LayoutGroup.OrientationProperty, BindingMode.OneWay);
            }
        }

        private void OnDragCompleted(DragCompletedEventArgs e)
        {
            if (this._resizeData != null)
            {
                if (!this.RedrawContent)
                {
                    Dpi dpiFromVisual = DevExpress.Xpf.Docking.Platform.Win32.DpiHelper.GetDpiFromVisual(this);
                    double change = this.CorrectChange(this.GetActualChange(e.HorizontalChange / dpiFromVisual.ScaleX, e.VerticalChange / dpiFromVisual.ScaleY));
                    this.MoveSplitter(change);
                }
                Tuple<BaseLayoutItem, BaseLayoutItem> itemsToResize = this.GetItemsToResize();
                this.RaiseDockOperationCompletedEvent(itemsToResize.Item1, itemsToResize.Item2);
                this.ResetResizing();
            }
            Action<ISupportBatchUpdate> action = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                Action<ISupportBatchUpdate> local1 = <>c.<>9__43_0;
                action = <>c.<>9__43_0 = x => x.EndUpdate();
            }
            this.Manager.Do<ISupportBatchUpdate>(action);
        }

        private static void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            (sender as BaseSplitterItem).OnDragCompleted(e);
        }

        private void OnDragDelta(DragDeltaEventArgs e)
        {
            if (this._resizeData != null)
            {
                double change = this.CorrectChange(this.GetActualChange(e.HorizontalChange, e.VerticalChange));
                if (this.RedrawContent)
                {
                    this.MoveSplitter(change);
                }
                else
                {
                    this.ResizePreviewHelper.Resize(this.IsHorizontal ? new Point(change, 0.0) : new Point(0.0, change));
                }
            }
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            (sender as BaseSplitterItem).OnDragDelta(e);
        }

        private void OnDragStarted(DragStartedEventArgs e)
        {
            Action<ISupportBatchUpdate> action = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Action<ISupportBatchUpdate> local1 = <>c.<>9__49_0;
                action = <>c.<>9__49_0 = x => x.BeginUpdate();
            }
            this.Manager.Do<ISupportBatchUpdate>(action);
            base.CaptureMouse();
            if (this.InitializeData())
            {
                Tuple<BaseLayoutItem, BaseLayoutItem> itemsToResize = this.GetItemsToResize();
                if (this.RaiseDockOpearationStarted(itemsToResize.Item1, itemsToResize.Item2))
                {
                    this.ResetResizing();
                }
            }
        }

        private static void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            (sender as BaseSplitterItem).OnDragStarted(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.Escape)
            {
                if (this._resizeData != null)
                {
                    this.CancelResize();
                    e.Handled = true;
                }
            }
            else
            {
                switch (key)
                {
                    case Key.Left:
                        e.Handled = this.KeyboardMoveSplitter(-this.KeyboardIncrement, 0.0);
                        return;

                    case Key.Up:
                        e.Handled = this.KeyboardMoveSplitter(0.0, -this.KeyboardIncrement);
                        return;

                    case Key.Right:
                        e.Handled = this.KeyboardMoveSplitter(this.KeyboardIncrement, 0.0);
                        return;

                    case Key.Down:
                        e.Handled = this.KeyboardMoveSplitter(0.0, this.KeyboardIncrement);
                        return;
                }
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateCursor();
        }

        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation orientation)
        {
            if (this.IsActivated)
            {
                this.UpdateCursor();
            }
        }

        private bool RaiseDockOpearationStarted(BaseLayoutItem item1, BaseLayoutItem item2) => 
            (this.Manager != null) && (this.Manager.RaiseDockOperationStartingEvent(DockOperation.Resize, item1, null) || this.Manager.RaiseDockOperationStartingEvent(DockOperation.Resize, item2, null));

        private void RaiseDockOperationCompletedEvent(BaseLayoutItem item1, BaseLayoutItem item2)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseDockOperationCompletedEvent(DockOperation.Resize, item1);
                this.Manager.RaiseDockOperationCompletedEvent(DockOperation.Resize, item2);
            }
        }

        private void ResetResizing()
        {
            if (!this.RedrawContent)
            {
                this.ResizePreviewHelper.EndResizing();
                this.resizePreviewHelper = null;
            }
            this._resizeData = null;
            base.ReleaseMouseCapture();
        }

        protected virtual IResizeCalculator ResolveResizeCalculator()
        {
            LayoutResizeCalculator calculator1 = new LayoutResizeCalculator();
            calculator1.Orientation = this.Orientation;
            return calculator1;
        }

        protected virtual void SetDefinitionLength(DefinitionBase definition, GridLength length)
        {
            if (length.Value >= 0.0)
            {
                bool isColumn = definition is ColumnDefinition;
                BaseLayoutItem item = this.GetItem(definition, isColumn);
                if (item != null)
                {
                    item.SetValue(isColumn ? BaseLayoutItem.ItemWidthProperty : BaseLayoutItem.ItemHeightProperty, length);
                }
            }
        }

        private bool SetupDefinitionsToResize()
        {
            bool isColumns = this._resizeData.ResizeDirection == GridResizeDirection.Columns;
            if (((int) this.GetValue(isColumns ? Grid.ColumnSpanProperty : Grid.RowSpanProperty)) == 1)
            {
                int current = this.GetCurrent(isColumns);
                int prev = this.GetPrev(current);
                int next = this.GetNext(current);
                int num5 = isColumns ? this._resizeData.Grid.ColumnDefinitions.Count : this._resizeData.Grid.RowDefinitions.Count;
                if ((prev >= 0) && ((prev != current) && ((next < num5) && (next != current))))
                {
                    this._resizeData.SplitterIndex = current;
                    this._resizeData.Definition1Index = prev;
                    this._resizeData.Definition1 = GetGridDefinition(this._resizeData.Grid, prev, this._resizeData.ResizeDirection);
                    this._resizeData.OriginalDefinition1Length = DefinitionsHelper.UserSizeValueCache(this._resizeData.Definition1);
                    this._resizeData.OriginalDefinition1ActualLength = this.GetActualLength(this._resizeData.Definition1);
                    this._resizeData.Definition2Index = next;
                    this._resizeData.Definition2 = GetGridDefinition(this._resizeData.Grid, next, this._resizeData.ResizeDirection);
                    this._resizeData.OriginalDefinition2Length = DefinitionsHelper.UserSizeValueCache(this._resizeData.Definition2);
                    this._resizeData.OriginalDefinition2ActualLength = this.GetActualLength(this._resizeData.Definition2);
                    bool flag2 = IsStar(this._resizeData.Definition1);
                    bool flag3 = IsStar(this._resizeData.Definition2);
                    this._resizeData.SplitBehavior = !(flag2 & flag3) ? (flag2 ? DevExpress.Xpf.Docking.SplitBehavior.Resize2 : (flag3 ? DevExpress.Xpf.Docking.SplitBehavior.Resize1 : DevExpress.Xpf.Docking.SplitBehavior.PixelSplit)) : DevExpress.Xpf.Docking.SplitBehavior.Split;
                    return true;
                }
            }
            return false;
        }

        private void SubscribeThumbEvents()
        {
            base.DragStarted += new DragStartedEventHandler(BaseSplitterItem.OnDragStarted);
            base.DragDelta += new DragDeltaEventHandler(BaseSplitterItem.OnDragDelta);
            base.DragCompleted += new DragCompletedEventHandler(BaseSplitterItem.OnDragCompleted);
        }

        private void UnsubscribeThumbEvents()
        {
            base.DragStarted -= new DragStartedEventHandler(BaseSplitterItem.OnDragStarted);
            base.DragDelta -= new DragDeltaEventHandler(BaseSplitterItem.OnDragDelta);
            base.DragCompleted -= new DragCompletedEventHandler(BaseSplitterItem.OnDragCompleted);
        }

        private void UpdateCursor()
        {
            if (this.LayoutGroup != null)
            {
                this.UpdateCursor(this.LayoutGroup.Orientation == System.Windows.Controls.Orientation.Horizontal);
            }
        }

        protected virtual void UpdateCursor(bool horz)
        {
            if (this.CanResize())
            {
                this.Cursor = horz ? Cursors.SizeWE : Cursors.SizeNS;
            }
            else
            {
                base.ClearValue(FrameworkElement.CursorProperty);
            }
        }

        protected bool IsActivated { get; private set; }

        protected bool IsHorizontal =>
            this.Orientation == System.Windows.Controls.Orientation.Horizontal;

        public DockLayoutManager Manager { get; private set; }

        public DevExpress.Xpf.Docking.LayoutGroup LayoutGroup { get; protected set; }

        private bool RedrawContent =>
            (this.Manager == null) || this.Manager.RedrawContentWhenResizing;

        private bool IsDragStarted =>
            this._resizeData != null;

        private LayoutResizingPreviewHelper ResizePreviewHelper
        {
            get
            {
                this.resizePreviewHelper ??= new LayoutResizingPreviewHelper(this.Manager.GetView(this.LayoutGroup) as LayoutView, this.LayoutGroup);
                return this.resizePreviewHelper;
            }
        }

        protected IResizeCalculator ResizeCalculator
        {
            [DebuggerStepThrough]
            get
            {
                this.resizeCalculatorCore ??= this.ResolveResizeCalculator();
                return this.resizeCalculatorCore;
            }
        }

        public double DragIncrement
        {
            get => 
                (double) base.GetValue(DragIncrementProperty);
            set => 
                base.SetValue(DragIncrementProperty, value);
        }

        public double KeyboardIncrement
        {
            get => 
                (double) base.GetValue(KeyboardIncrementProperty);
            set => 
                base.SetValue(KeyboardIncrementProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseSplitterItem.<>c <>9 = new BaseSplitterItem.<>c();
            public static Action<ISupportBatchUpdate> <>9__43_0;
            public static Action<ISupportBatchUpdate> <>9__49_0;

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseSplitterItem) dObj).OnOrientationChanged((Orientation) e.NewValue);
            }

            internal void <OnDragCompleted>b__43_0(ISupportBatchUpdate x)
            {
                x.EndUpdate();
            }

            internal void <OnDragStarted>b__49_0(ISupportBatchUpdate x)
            {
                x.BeginUpdate();
            }
        }
    }
}

