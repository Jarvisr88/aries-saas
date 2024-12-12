namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class CellContentPresenter : Control, INotifyNavigationIndexChanged, IGridCellEditorOwner
    {
        public static readonly DependencyProperty RowDataProperty;
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty ColumnProperty;
        public static readonly DependencyProperty ColumnPositionProperty;
        public static readonly DependencyProperty HasRightSiblingProperty;
        public static readonly DependencyProperty HasLeftSiblingProperty;
        public static readonly DependencyProperty HasTopElementProperty;
        public static readonly DependencyProperty ShowVerticalLinesProperty;
        public static readonly DependencyProperty ShowHorizontalLinesProperty;
        public static readonly DependencyProperty BorderStateProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsFocusedCellProperty;
        public static readonly DependencyProperty SelectionStateProperty;
        public static readonly DependencyProperty IsReadyProperty;

        static CellContentPresenter()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CellContentPresenter), new FrameworkPropertyMetadata(typeof(CellContentPresenter)));
            RowDataProperty = DependencyPropertyManager.Register("RowData", typeof(DevExpress.Xpf.Grid.RowData), typeof(CellContentPresenter), new PropertyMetadata(null, (d, e) => ((CellContentPresenter) d).OnRowDataChanged()));
            FieldNameProperty = DependencyPropertyManager.Register("FieldName", typeof(string), typeof(CellContentPresenter), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(CellContentPresenter.OnFieldNameChanged)));
            ColumnProperty = DependencyPropertyManager.Register("Column", typeof(ColumnBase), typeof(CellContentPresenter), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(CellContentPresenter.OnColumnChanged)));
            ColumnPositionProperty = DependencyPropertyManager.Register("ColumnPosition", typeof(DevExpress.Xpf.Grid.ColumnPosition), typeof(CellContentPresenter), new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ColumnPosition.Middle, (d, e) => ((CellContentPresenter) d).UpdateLineState()));
            IsFocusedCellProperty = DataViewBase.IsFocusedCellProperty.AddOwner(typeof(CellContentPresenter), new FrameworkPropertyMetadata((d, e) => ((CellContentPresenter) d).OnIsFocusedCellChanged()));
            ColumnBase.NavigationIndexProperty.AddOwner(typeof(CellContentPresenter));
            HasRightSiblingProperty = DependencyPropertyManager.Register("HasRightSibling", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(true, (d, e) => ((CellContentPresenter) d).UpdateLineState()));
            HasLeftSiblingProperty = DependencyPropertyManager.Register("HasLeftSibling", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(true, (d, e) => ((CellContentPresenter) d).UpdateLineState()));
            HasTopElementProperty = DependencyPropertyManager.Register("HasTopElement", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(false, (d, e) => ((CellContentPresenter) d).UpdateLineState()));
            ShowVerticalLinesProperty = DependencyPropertyManager.Register("ShowVerticalLines", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(true, (d, e) => ((CellContentPresenter) d).UpdateLineState()));
            ShowHorizontalLinesProperty = DependencyPropertyManager.Register("ShowHorizontalLines", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(true, (d, e) => ((CellContentPresenter) d).UpdateLineState()));
            Thickness defaultValue = new Thickness();
            BorderStateProperty = DependencyPropertyManager.Register("BorderState", typeof(Thickness), typeof(CellContentPresenter), new FrameworkPropertyMetadata(defaultValue));
            IsSelectedProperty = DependencyPropertyManager.Register("IsSelected", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(false));
            SelectionStateProperty = DependencyPropertyManager.Register("SelectionState", typeof(DevExpress.Xpf.Grid.SelectionState), typeof(CellContentPresenter), new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.SelectionState.None, new PropertyChangedCallback(CellContentPresenter.OnSelectionStateChanged)));
            IsReadyProperty = DependencyPropertyManager.Register("IsReady", typeof(bool), typeof(CellContentPresenter), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(CellContentPresenter.OnIsReadyChanged)));
        }

        internal virtual bool CanRefreshContent() => 
            LayoutHelper.IsChildElement(this.RowData.RowElement, this);

        void IGridCellEditorOwner.OnViewChanged()
        {
            this.OnCellContentPresenterRowChanged();
        }

        void IGridCellEditorOwner.SetIsFocusedCell(bool isFocusedCell)
        {
        }

        void IGridCellEditorOwner.SetSelectionState(DevExpress.Xpf.Grid.SelectionState state)
        {
        }

        void IGridCellEditorOwner.SynProperties(GridCellData cellData)
        {
            this.SyncProperties(cellData);
        }

        void IGridCellEditorOwner.UpdateCellBackgroundAppearance()
        {
        }

        void IGridCellEditorOwner.UpdateCellForegroundAppearance()
        {
        }

        void IGridCellEditorOwner.UpdateCellState()
        {
            this.UpdateRowSelectionState();
        }

        void IGridCellEditorOwner.UpdateIsEnabled(bool isEnabled)
        {
            base.IsEnabled = isEnabled;
        }

        void IGridCellEditorOwner.UpdateIsReady()
        {
            this.IsReady = this.RowData.IsReady;
        }

        void INotifyNavigationIndexChanged.OnNavigationIndexChanged()
        {
            this.OnNavigationIndexChanged();
        }

        private EditGridCellData GetCellData()
        {
            EditGridCellData dataContext = base.DataContext as EditGridCellData;
            return ((dataContext == null) ? ((this.RowData != null) ? ((EditGridCellData) this.RowData.GetCellDataByColumn(this.Column)) : null) : dataContext);
        }

        protected virtual string GetSelectionState() => 
            (this.RowData != null) ? (((this.SelectionState != DevExpress.Xpf.Grid.SelectionState.None) || (this.RowData.SelectionState != DevExpress.Xpf.Grid.SelectionState.Focused)) ? (((this.SelectionState != DevExpress.Xpf.Grid.SelectionState.None) || (this.RowData.SelectionState != DevExpress.Xpf.Grid.SelectionState.Selected)) ? Enum.GetName(typeof(DevExpress.Xpf.Grid.SelectionState), this.SelectionState) : "RowSelected") : "RowFocused") : "None";

        protected virtual string GetShowVerticalLineState() => 
            ((this.RowData == null) || (this.RowData.SelectionState != DevExpress.Xpf.Grid.SelectionState.Focused)) ? "Visible" : "VisibleFocused";

        protected virtual void GoToState(string state)
        {
            VisualStateManager.GoToState(this, state, false);
        }

        private void MouseEnterOrLeave()
        {
            if ((this.View != null) && ((this.CellData != null) && ((this.CellData.RowData != null) && this.View.CanHighlightedState(this.CellData.RowData.RowHandle.Value, this.CellData.Column, true, this.CellData.SelectionState, false))))
            {
                this.CellData.UpdateSelectionState();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Editor = base.GetTemplateChild("PART_CellEditor") as CellEditorBase;
            if (this.Editor != null)
            {
                this.Editor.GridCellEditorOwner = this;
                this.UpdateEditorCellData();
            }
            this.OnIsFocusedCellChanged();
            this.OnNavigationIndexChanged();
            this.OnRowDataChanged();
            this.SyncColumn();
            this.SyncFieldName();
            this.UpdateSelectionState();
            this.UpdateLineState();
        }

        private void OnCellContentPresenterRowChanged()
        {
            if ((this.View != null) && ((this.RowData == null) || !this.RowData.treeBuilder.IsPrinting))
            {
                this.View.ViewBehavior.OnCellContentPresenterRowChanged(this);
            }
        }

        protected virtual void OnColumnChanged()
        {
            this.SyncColumn();
            this.UpdateEditorCellData();
        }

        private static void OnColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CellContentPresenter) d).OnColumnChanged();
        }

        private void OnFieldNameChanged()
        {
            if ((this.View != null) && ((this.Column == null) && (this.FieldName != null)))
            {
                this.Column = this.View.ColumnsCore[this.FieldName];
            }
            this.SyncFieldName();
        }

        private static void OnFieldNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CellContentPresenter) d).OnFieldNameChanged();
        }

        private void OnIsFocusedCellChanged()
        {
            if (this.Editor != null)
            {
                this.Editor.IsFocusedCell = this.IsFocusedCell;
            }
        }

        protected internal virtual void OnIsReadyChanged()
        {
        }

        private static void OnIsReadyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CellContentPresenter) d).OnIsReadyChanged();
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.MouseEnterOrLeave();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.MouseEnterOrLeave();
        }

        private void OnNavigationIndexChanged()
        {
            if (this.Editor != null)
            {
                this.Editor.NavigationIndex = this.NavigationIndex;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, FrameworkElement.DataContextProperty) && (base.DataContext is DevExpress.Xpf.Grid.RowData))
            {
                this.RowData = base.DataContext as DevExpress.Xpf.Grid.RowData;
            }
        }

        protected virtual void OnRowDataChanged()
        {
            if (this.Editor != null)
            {
                this.Editor.RowData = this.RowData;
            }
            this.OnCellContentPresenterRowChanged();
        }

        private static void OnSelectionStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CellContentPresenter) d).UpdateSelectionState();
        }

        private void SyncCellEditor(DependencyProperty property, DependencyProperty editorProperty)
        {
            if (this.Editor != null)
            {
                this.Editor.SetValue(editorProperty, base.GetValue(property));
            }
        }

        private void SyncColumn()
        {
            this.SyncCellEditor(ColumnProperty, CellEditorBase.ColumnProperty);
        }

        private void SyncFieldName()
        {
            this.SyncCellEditor(FieldNameProperty, CellEditorBase.FieldNameProperty);
        }

        internal virtual void SyncProperties(GridCellData cellData)
        {
        }

        private void UpdateContentBorder()
        {
            double left = ((this.Column == null) || ((this.ColumnPosition != DevExpress.Xpf.Grid.ColumnPosition.Left) || (!this.HasLeftSibling || !this.ShowVerticalLines))) ? 0.0 : 1.0;
            double top = (!this.HasTopElement || !this.ShowHorizontalLines) ? 0.0 : 1.0;
            Thickness thickness = new Thickness(left, top, (!this.HasRightSibling || !this.ShowVerticalLines) ? 0.0 : 1.0, 0.0);
            if (this.BorderState != thickness)
            {
                this.BorderState = thickness;
            }
        }

        private void UpdateEditorCellData()
        {
            if ((this.Editor != null) && (this.Editor.CellData == null))
            {
                this.Editor.CellData = this.GetCellData();
            }
        }

        protected internal virtual void UpdateLineState()
        {
            this.GoToState(this.GetShowVerticalLineState());
            this.UpdateContentBorder();
        }

        protected internal virtual void UpdateRowSelectionState()
        {
            this.UpdateSelectionState();
            this.UpdateLineState();
        }

        private void UpdateSelectionState()
        {
            this.GoToState(this.GetSelectionState());
        }

        [Description("Gets or sets the information about a data row which contains the cell. This is a dependency property.")]
        public DevExpress.Xpf.Grid.RowData RowData
        {
            get => 
                (DevExpress.Xpf.Grid.RowData) base.GetValue(RowDataProperty);
            set => 
                base.SetValue(RowDataProperty, value);
        }

        [Description("Gets the data source field name of the column which owns the cell.")]
        public string FieldName
        {
            get => 
                (string) base.GetValue(FieldNameProperty);
            set => 
                base.SetValue(FieldNameProperty, value);
        }

        [Description("Gets or sets a column which owns the cell. This is a dependency property.")]
        public ColumnBase Column
        {
            get => 
                (ColumnBase) base.GetValue(ColumnProperty);
            set => 
                base.SetValue(ColumnProperty, value);
        }

        [Description("Gets or sets the position of the column that owns the cell. This is a dependency property.")]
        public DevExpress.Xpf.Grid.ColumnPosition ColumnPosition
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnPosition) base.GetValue(ColumnPositionProperty);
            set => 
                base.SetValue(ColumnPositionProperty, value);
        }

        [Description("Indicates whether the cell belongs to a column that has a sibling column displayed on its right. This is a dependency property.")]
        public bool HasRightSibling
        {
            get => 
                (bool) base.GetValue(HasRightSiblingProperty);
            set => 
                base.SetValue(HasRightSiblingProperty, value);
        }

        [Description("Indicates whether the cell belongs to a column that has a sibling column displayed on its left. This is a dependency property.")]
        public bool HasLeftSibling
        {
            get => 
                (bool) base.GetValue(HasLeftSiblingProperty);
            set => 
                base.SetValue(HasLeftSiblingProperty, value);
        }

        [Description("Indicates whether the cell has an element above it. This is a dependency property.")]
        public bool HasTopElement
        {
            get => 
                (bool) base.GetValue(HasTopElementProperty);
            set => 
                base.SetValue(HasTopElementProperty, value);
        }

        [Description("Gets whether a View displays vertical lines. This is a dependency property.")]
        public bool ShowVerticalLines
        {
            get => 
                (bool) base.GetValue(ShowVerticalLinesProperty);
            set => 
                base.SetValue(ShowVerticalLinesProperty, value);
        }

        [Description("Gets or sets whether horizontal lines are displayed. This is a dependency property.")]
        public bool ShowHorizontalLines
        {
            get => 
                (bool) base.GetValue(ShowHorizontalLinesProperty);
            set => 
                base.SetValue(ShowHorizontalLinesProperty, value);
        }

        [Description("Gets the outer margin of the column.")]
        public Thickness BorderState
        {
            get => 
                (Thickness) base.GetValue(BorderStateProperty);
            set => 
                base.SetValue(BorderStateProperty, value);
        }

        [Description("Gets or sets whether the cell is selected. This is a dependency property.")]
        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        [Description("Gets or sets whether the cell is focused. This is a dependency property.")]
        public bool IsFocusedCell
        {
            get => 
                (bool) base.GetValue(IsFocusedCellProperty);
            set => 
                base.SetValue(IsFocusedCellProperty, value);
        }

        [Description("Gets a value that indicates the cell's selection state. This is a dependency property.")]
        public DevExpress.Xpf.Grid.SelectionState SelectionState
        {
            get => 
                (DevExpress.Xpf.Grid.SelectionState) base.GetValue(SelectionStateProperty);
            set => 
                base.SetValue(SelectionStateProperty, value);
        }

        [Description("Gets or sets whether a cell's data has been loaded or not. This is a dependency property.")]
        public bool IsReady
        {
            get => 
                (bool) base.GetValue(IsReadyProperty);
            set => 
                base.SetValue(IsReadyProperty, value);
        }

        internal CellEditorBase Editor { get; set; }

        [Description("This property supports the internal infrastructure and is not intended to be used directly from your code.")]
        public int NavigationIndex
        {
            get => 
                (int) base.GetValue(ColumnBase.NavigationIndexProperty);
            set => 
                base.SetValue(ColumnBase.NavigationIndexProperty, value);
        }

        protected DataViewBase View =>
            this.RowData?.View;

        DependencyObject IGridCellEditorOwner.EditorRoot =>
            this;

        ColumnBase IGridCellEditorOwner.AssociatedColumn =>
            !(base.DataContext is EditGridCellData) ? this.Column : ((EditGridCellData) base.DataContext).ColumnCore;

        bool IGridCellEditorOwner.CanRefreshContent =>
            this.CanRefreshContent();

        private GridCellData CellData =>
            base.DataContext as GridCellData;

        double IGridCellEditorOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CellContentPresenter.<>c <>9 = new CellContentPresenter.<>c();

            internal void <.cctor>b__14_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).OnRowDataChanged();
            }

            internal void <.cctor>b__14_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).UpdateLineState();
            }

            internal void <.cctor>b__14_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).OnIsFocusedCellChanged();
            }

            internal void <.cctor>b__14_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).UpdateLineState();
            }

            internal void <.cctor>b__14_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).UpdateLineState();
            }

            internal void <.cctor>b__14_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).UpdateLineState();
            }

            internal void <.cctor>b__14_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).UpdateLineState();
            }

            internal void <.cctor>b__14_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellContentPresenter) d).UpdateLineState();
            }
        }
    }
}

