namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class GridCellData : GridColumnData
    {
        public static readonly DependencyProperty DisplayMemberBindingValueProperty;
        private bool isSelected;
        private bool isFocusedCell;
        private DevExpress.Xpf.Grid.SelectionState selectionState;
        private bool isValueBinded;
        internal BindingBase displayMemberBinding;
        private DescriptorWeakEventHandler<GridCellData> simpleBindingHandler;
        private DevExpress.Xpf.Grid.Native.AnimationController animationControllerCore;
        private ConditionalClientAppearanceUpdaterBase conditionalUpdater;

        static GridCellData()
        {
            DisplayMemberBindingValueProperty = DependencyPropertyManager.Register("DisplayMemberBindingValue", typeof(object), typeof(GridCellData), new UIPropertyMetadata(null, (d, e) => ((GridCellData) d).OnDisplayMemberBindingValueChanged()));
        }

        public GridCellData(DevExpress.Xpf.Grid.RowData rowData) : base(rowData)
        {
            this.UpdateLanguage();
        }

        private static T CalculateWithOptionalLocker<T>(Locker locker, Func<T> calculate)
        {
            if (locker == null)
            {
                return calculate();
            }
            T result = default(T);
            locker.DoLockedAction<T>(delegate {
                T local;
                result = local = calculate();
                return local;
            });
            return result;
        }

        protected override bool CanRaiseContentChangedWhenDataChanged() => 
            this.RowData.RowHandle.Value != -2147483645;

        private bool CanSelect(bool checkIsCheckSelectorColumn = true) => 
            (base.Column == null) || ((base.View == null) || ((this.RowData == null) || ((this.RowData.RowHandle == null) || ((!checkIsCheckSelectorColumn || ReferenceEquals(base.Column, base.View.CheckBoxSelectorColumn)) ? base.View.RaiseCanSelectRow(this.RowData.RowHandle.Value) : true))));

        private bool CanUnselect(bool checkIsCheckSelectorColumn = true) => 
            (base.Column == null) || ((base.View == null) || ((this.RowData == null) || ((this.RowData.RowHandle == null) || ((!checkIsCheckSelectorColumn || ReferenceEquals(base.Column, base.View.CheckBoxSelectorColumn)) ? base.View.RaiseCanUnselectRow(this.RowData.RowHandle.Value) : true))));

        protected internal override void ClearBindingValue()
        {
            base.ClearValue(DisplayMemberBindingValueProperty);
            this.displayMemberBinding = null;
        }

        internal double GetActualCellWidth() => 
            double.IsInfinity(base.Column.ActualDataWidth) ? 0.0 : Math.Max((double) 0.0, (double) (((base.Column.ActualDataWidth + this.RowData.GetRowIndent(base.Column)) - this.RowData.GetTreeColumnOffset(base.Column)) + base.Column.GetHorizontalCorrection()));

        private PropertyDescriptor GetDescriptorToListen() => 
            this.simpleBindingHandler?.Descriptor;

        protected virtual object GetValue() => 
            this.RowData.treeBuilder.GetCellValue(this.RowData, base.Column.FieldName);

        private bool IsValidRowHandleForRaiseCustomError(RowHandle handle)
        {
            if ((handle == null) || (base.View == null))
            {
                return true;
            }
            int num = handle.Value;
            ITableView view = base.View as ITableView;
            return ((view == null) || (((num != -2147483645) || view.ShowAutoFilterRow) ? ((num != -2147483647) || view.NewItemRowIsDisplayed) : false));
        }

        private void OnDisplayMemberBindingValueChanged()
        {
            if (!this.IsEditing)
            {
                this.UpdateGridCellDataValue();
            }
        }

        protected override void OnEditorChanged()
        {
            base.OnEditorChanged();
            if (this.SelectionState != DevExpress.Xpf.Grid.SelectionState.None)
            {
                this.UpdateEditorSelectionState();
            }
            if (this.IsFocusedCell)
            {
                this.UpdateEditorIsFocusedCell();
            }
            this.UpdateCanSelectRow(true);
        }

        private void OnEditorDataChanged()
        {
            this.ConditionalUpdater.OnDataChanged();
        }

        internal void OnIsFocusedCellChanged()
        {
            this.UpdateSelectionState();
            this.UpdateEditorIsFocusedCell();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, Validation.ErrorsProperty) && this.RowData.CanUpdateErrors)
            {
                this.UpdateCellError(this.RowData.RowHandle, base.Column, true);
            }
        }

        internal void OnRowChanged()
        {
            base.RaisePropertyChanged("Row");
        }

        internal void OnRowChanging(object oldRow, object newRow)
        {
            if (!DevExpress.Xpf.Grid.RowData.GetIsReady(newRow))
            {
                this.ClearBindingValue();
            }
            this.UnsubscribeSimpleBinding();
            this.StopAnimation();
        }

        private void OnSelectionStateChanged()
        {
            this.StopAnimation();
            this.SyncCellContentPresenterProperties();
            this.UpdateEditorSelectionState();
        }

        private void OnSimplePropertyChanged()
        {
            this.DisplayMemberBindingValue = this.GetValue();
        }

        protected override void OnValueChanged(object oldValue)
        {
            if ((base.View != null) && ((this.RowData.RowHandle.Value != -2147483645) && ((base.View.ShowUpdateRowButtonsCore != ShowUpdateRowButtons.Never) && (base.View.IsEditing && ((base.View.DataControl != null) && ReferenceEquals(base.View.DataControl.CurrentColumn, base.ColumnCore))))))
            {
                this.RowData.SetUpdateRowButtonsCache(base.ColumnCore, base.Value);
                if ((base.Editor != null) && base.Editor.IsCustomEditor)
                {
                    if ((base.View.ShowUpdateRowButtonsCore == ShowUpdateRowButtons.OnCellValueChange) && (!base.View.AreUpdateRowButtonsShown && (base.Editor != null)))
                    {
                        base.View.AreUpdateRowButtonsShown = true;
                        base.View.RootView.AreUpdateRowButtonsShown = true;
                        if (base.View.ViewBehavior != null)
                        {
                            base.View.ViewBehavior.UpdateRowButtonsControl();
                        }
                    }
                    if ((base.View.ShowUpdateRowButtonsCore != ShowUpdateRowButtons.Never) && (base.Editor.Edit != null))
                    {
                        base.Editor.Edit.IsValueChanged = true;
                    }
                }
            }
            this.RowData.GetFormatCachingLocker().Unlock();
            base.OnValueChanged(oldValue);
            this.OnEditorDataChanged();
        }

        internal void OnViewChanged()
        {
            if (base.editor != null)
            {
                base.editor.OnViewChanged();
            }
        }

        internal void ResetAnimationFormatInfoProvider()
        {
            this.ConditionalUpdater.ResetAnimationFormatInfoProvider();
        }

        internal void ResetInitialValue()
        {
            this.InitialValue = null;
        }

        private void ResubscribeSimpleBinding()
        {
            if ((base.ColumnCore == null) || !base.ColumnCore.IsSimpleBindingEnabled)
            {
                this.UnsubscribeSimpleBinding();
            }
            else
            {
                PropertyDescriptor descriptorToListen = base.Column.SimpleBindingProcessor.DescriptorToListen;
                object row = this.RowData?.Row;
                if (!ReferenceEquals(this.GetDescriptorToListen(), descriptorToListen) && ((descriptorToListen != null) && (row != null)))
                {
                    this.UnsubscribeSimpleBinding();
                    Action<GridCellData, object, EventArgs> onEventAction = <>c.<>9__90_0;
                    if (<>c.<>9__90_0 == null)
                    {
                        Action<GridCellData, object, EventArgs> local1 = <>c.<>9__90_0;
                        onEventAction = <>c.<>9__90_0 = (cellData, _, __) => cellData.OnSimplePropertyChanged();
                    }
                    this.simpleBindingHandler = new DescriptorWeakEventHandler<GridCellData>(descriptorToListen, this, onEventAction);
                    descriptorToListen.AddValueChanged(row, this.simpleBindingHandler.Handler);
                }
            }
        }

        private void SetCellError(RowHandle handle, ColumnBase column, bool customValidate)
        {
            if (!customValidate || !this.SetCellErrorOnlyCustom(handle, column, null))
            {
                BaseValidationError newError = null;
                Func<ValidationError, object> containerExtractor = <>c.<>9__85_0;
                if (<>c.<>9__85_0 == null)
                {
                    Func<ValidationError, object> local1 = <>c.<>9__85_0;
                    containerExtractor = <>c.<>9__85_0 = rule => rule.ErrorContent;
                }
                MultiErrorInfo multiErrorInfo = ValidationMultiErrorReader.Read<ValidationError>(Validation.GetErrors(this).ToArray<ValidationError>(), containerExtractor);
                if (!multiErrorInfo.HasErrors())
                {
                    multiErrorInfo = base.View.DataProviderBase.GetMultiErrorInfo(handle, column.FieldName);
                }
                if (multiErrorInfo.HasErrors())
                {
                    newError = base.View.CreateCellValidationError(multiErrorInfo.ErrorText, multiErrorInfo.Errors, multiErrorInfo.ErrorType, handle.Value, base.Column, null);
                }
                this.SetDataErrorText(handle.Value, newError);
            }
        }

        private bool SetCellErrorOnlyCustom(RowHandle handle, ColumnBase column, RowValidationError info)
        {
            if (base.View.AllowLeaveInvalidEditor && this.IsValidRowHandleForRaiseCustomError(handle))
            {
                object local2;
                if (!base.IsValueDirty && (!base.View.AllowLeaveInvalidEditor || !base.View.EnableImmediatePosting))
                {
                    local2 = base.Value;
                }
                else
                {
                    Locker getValueLeaveInvalidEditorLocker;
                    DevExpress.Xpf.Grid.RowData rowData = this.RowData;
                    if (rowData != null)
                    {
                        getValueLeaveInvalidEditorLocker = rowData.GetValueLeaveInvalidEditorLocker;
                    }
                    else
                    {
                        DevExpress.Xpf.Grid.RowData local1 = rowData;
                        getValueLeaveInvalidEditorLocker = null;
                    }
                    local2 = CalculateWithOptionalLocker<object>(getValueLeaveInvalidEditorLocker, new Func<object>(this.GetValue));
                }
                object obj2 = local2;
                info = RowValidationHelper.ValidateEvents(base.View, this, obj2, handle.Value, column);
                if (info != null)
                {
                    this.SetDataErrorText(handle.Value, info);
                    return true;
                }
            }
            return false;
        }

        internal void SetDataErrorText(int rowHandle, BaseValidationError newError)
        {
            BaseValidationError validationError = BaseEdit.GetValidationError(this);
            if (!Equals(newError, validationError) && !this.HasCellEditorError)
            {
                this.SetValidationError(newError);
                this.RaiseContentChanged();
            }
        }

        internal void SetInitialValue()
        {
            this.InitialValue = base.Value;
        }

        internal void SetValidationError(BaseValidationError newError)
        {
            BaseEditHelper.SetValidationError(this, newError);
            this.HasErrorCore = newError != null;
        }

        internal override void StartAnimation(IList<IList<AnimationTimeline>> animations)
        {
            if (base.Editor != null)
            {
                this.AnimationController.Start(animations, base.Editor.CreateAnimationConnector());
            }
        }

        internal override void StopAnimation()
        {
            if (this.IsAnimationControllerInited)
            {
                this.AnimationController.Flush();
            }
        }

        internal void SyncCellContentPresenterProperties()
        {
            if (base.editor != null)
            {
                base.editor.SyncProperties();
            }
        }

        internal void SyncIsEnabled(bool isEnabled)
        {
            if (base.editor != null)
            {
                base.editor.SyncIsEnabled(isEnabled);
            }
        }

        internal void SyncLeftMargin(FrameworkElement cell)
        {
            double rowLeftMargin = this.RowData.GetRowLeftMargin(this);
            if (cell.Margin.Left != rowLeftMargin)
            {
                cell.Margin = new Thickness(rowLeftMargin, 0.0, 0.0, 0.0);
            }
        }

        private void UnsubscribeSimpleBinding()
        {
            if (this.GetDescriptorToListen() != null)
            {
                object row = this.RowData?.Row;
                if ((row != null) && ((row != null) && (this.simpleBindingHandler != null)))
                {
                    this.simpleBindingHandler.Detach(row);
                    this.simpleBindingHandler = null;
                }
            }
        }

        private void UpdateCanSelectRow(bool checkColumn = true)
        {
            if ((base.Column != null) && ((base.View != null) && ((this.RowData != null) && ((this.RowData.RowHandle != null) && (base.View.IsCheckBoxSelectorColumnVisible && (base.View.CheckBoxSelectorColumn != null))))))
            {
                GridCellData data = this;
                if (checkColumn)
                {
                    if (!ReferenceEquals(base.Column, base.View.CheckBoxSelectorColumn))
                    {
                        return;
                    }
                }
                else
                {
                    data = this.RowData.GetCellDataByColumn(base.View.CheckBoxSelectorColumn, false, false) as GridCellData;
                    if (data == null)
                    {
                        return;
                    }
                }
                data.UpdateIsEnabledCheckBoxSelectorColumn();
            }
        }

        protected internal void UpdateCellError(RowHandle handle, ColumnBase column, bool customValidate = true)
        {
            if ((this.RowData == null) || !this.RowData.GetValueLeaveInvalidEditorLocker.IsLocked)
            {
                if (this.CanShowCellError)
                {
                    this.SetCellError(handle, column, customValidate);
                }
                else if (!customValidate || !this.SetCellErrorOnlyCustom(handle, column, null))
                {
                    this.SetDataErrorText(handle.Value, null);
                }
            }
        }

        private void UpdateCellState()
        {
            if (base.editor != null)
            {
                base.editor.UpdateCellState();
            }
        }

        internal virtual void UpdateEditorButtonVisibility()
        {
        }

        internal void UpdateEditorDisplayText()
        {
            if (base.editor != null)
            {
                base.editor.UpdateDisplayText();
            }
        }

        internal void UpdateEditorHighlightingText(bool columnChanged)
        {
            if ((base.Column != null) && (base.View != null))
            {
                this.UpdateEditorHighlightingText(columnChanged, base.View.SearchPanelHighlightResults ? base.View.GetTextHighlightingProperties(base.Column) : null);
            }
        }

        internal void UpdateEditorHighlightingText(bool columnChanged, TextHighlightingProperties textHighlightingProperties)
        {
            if ((base.editor != null) && (base.Column != null))
            {
                base.editor.UpdateHighlightingText(textHighlightingProperties, columnChanged);
            }
        }

        private void UpdateEditorIsFocusedCell()
        {
            if (base.editor != null)
            {
                base.editor.GridCellEditorOwner.SetIsFocusedCell(this.IsFocusedCell);
            }
        }

        private void UpdateEditorSelectionState()
        {
            if (base.editor != null)
            {
                base.editor.GridCellEditorOwner.SetSelectionState(this.SelectionState);
            }
        }

        internal void UpdateFullState(int rowHandle)
        {
            this.UpdateIsSelected(rowHandle);
            this.UpdateIsFocusedCellCore(rowHandle);
            this.UpdateSelectionState();
        }

        internal void UpdateGridCellDataValue()
        {
            base.Value = this.DisplayMemberBindingValue;
        }

        private void UpdateIsEnabledCheckBoxSelectorColumn()
        {
            if ((base.View != null) && (ReferenceEquals(base.Column, base.View.CheckBoxSelectorColumn) && (base.Value as bool)))
            {
                bool flag = (bool) base.Value;
                bool isEnabled = true;
                if (base.editor != null)
                {
                    if (base.View.CanRaiseCanSelectRow() && (!this.CanSelect(true) && !flag))
                    {
                        isEnabled = false;
                    }
                    if (((isEnabled && base.View.CanRaiseCanUnselectRow()) && !this.CanUnselect(true)) & flag)
                    {
                        isEnabled = false;
                    }
                    base.editor.IsEnabled = isEnabled;
                }
                this.SyncIsEnabled(isEnabled);
            }
        }

        internal void UpdateIsFocusedCell(int rowHandle)
        {
            this.UpdateIsFocusedCellCore(rowHandle);
        }

        private void UpdateIsFocusedCellCore(int rowHandle)
        {
            this.IsFocusedCell = base.View.GetIsCellFocused(rowHandle, base.Column) && this.RowData.GetIsFocusable();
        }

        internal void UpdateIsReady()
        {
            if (base.editor != null)
            {
                base.editor.UpdateIsReady();
            }
        }

        internal void UpdateIsSelected(int rowHandle)
        {
            this.UpdateIsSelected(rowHandle, base.View.ViewBehavior.GetIsCellSelected(rowHandle, base.Column));
        }

        internal void UpdateIsSelected(int rowHandle, bool forceIsSelected)
        {
            this.IsSelected = forceIsSelected;
        }

        protected internal void UpdateLanguage()
        {
            if ((base.View != null) && (base.View.DataControl != null))
            {
                base.SetValue(FrameworkElement.LanguageProperty, base.View.DataControl.GetValue(FrameworkElement.LanguageProperty));
                if ((base.ColumnCore != null) && base.ColumnCore.IsSimpleBindingEnabled)
                {
                    this.UpdateValue(false);
                }
            }
        }

        internal void UpdatePrintingMergeValue()
        {
            if (base.editor != null)
            {
                base.editor.UpdatePrintingMergeValue();
            }
        }

        internal void UpdateSelectionState()
        {
            bool isMouseOver = false;
            if ((base.Editor != null) && ((base.Editor.GridCellEditorOwner != null) && ((base.Editor.GridCellEditorOwner.EditorRoot != null) && (base.Editor.GridCellEditorOwner.EditorRoot is UIElement))))
            {
                isMouseOver = ((UIElement) base.Editor.GridCellEditorOwner.EditorRoot).IsMouseOver;
            }
            this.SelectionState = base.View.GetCellSelectionState(this.RowData.RowHandle.Value, this.IsFocusedCell, this.IsSelected, isMouseOver, base.Column);
            this.UpdateCellState();
            this.RowData.UpdateIndentSelectionState();
        }

        protected internal override void UpdateValue(bool forceUpdate = false)
        {
            if (this.RowData.GetIsReady())
            {
                Tuple<bool, object> tuple = this.RowData.NeedUpdateRowButtonsInit(base.ColumnCore);
                if (!tuple.Item1 && ((base.Editor == null) || !base.Editor.CancelEditLocker.IsLocked))
                {
                    base.Value = tuple.Item2;
                }
                else
                {
                    this.ResubscribeSimpleBinding();
                    BindingBase actualBinding = base.ColumnCore?.ActualBinding;
                    object objB = base.Value;
                    if (this.isValueBinded && (!ReferenceEquals(actualBinding, this.displayMemberBinding) && (actualBinding == null)))
                    {
                        base.ClearValue(DisplayMemberBindingValueProperty);
                    }
                    if (!this.IsEditing && ((base.ColumnCore != null) && ((base.Data != null) && ((this.DataControl != null) && (base.ColumnCore.OwnerControl != null)))))
                    {
                        this.isValueBinded = actualBinding != null;
                        if (this.isValueBinded)
                        {
                            if (!ReferenceEquals(actualBinding, this.displayMemberBinding))
                            {
                                this.displayMemberBinding = actualBinding;
                                if (!base.View.IsDesignTime)
                                {
                                    BindingOperations.SetBinding(this, DisplayMemberBindingValueProperty, actualBinding);
                                    CellEditorBase editor = base.Editor;
                                    if (editor == null)
                                    {
                                        CellEditorBase local1 = editor;
                                    }
                                    else
                                    {
                                        editor.UpdateValidationErrorCore();
                                    }
                                }
                            }
                            if (base.Value != this.DisplayMemberBindingValue)
                            {
                                this.UpdateGridCellDataValue();
                            }
                        }
                        else
                        {
                            this.displayMemberBinding = null;
                            object newValue = this.GetValue();
                            if (base.ColumnCore.ActualBinding != null)
                            {
                                this.UpdateValue(false);
                                this.RowData.SetUpdateRowButtonsCache(base.ColumnCore, base.Value);
                                return;
                            }
                            if (AsyncServerModeDataController.IsNoValue(newValue))
                            {
                                return;
                            }
                            if (!Equals(base.Value, newValue))
                            {
                                this.RowData.GetFormatCachingLocker().DoLockedAction<object>(delegate {
                                    object obj2;
                                    this.Value = obj2 = newValue;
                                    return obj2;
                                });
                            }
                            else if (forceUpdate && (base.Editor != null))
                            {
                                base.editor.UpdateDisplayText();
                            }
                            if (base.ColumnCore.IsSimpleBindingEnabled)
                            {
                                this.DisplayMemberBindingValue = base.Value;
                                CellEditorBase editor = base.Editor;
                                if (editor == null)
                                {
                                    CellEditorBase local2 = editor;
                                }
                                else
                                {
                                    editor.UpdateValidationErrorCore();
                                }
                            }
                        }
                        this.RowData.SetUpdateRowButtonsCache(base.ColumnCore, base.Value);
                    }
                    if (!base.View.CanStartIncrementalSearch)
                    {
                        this.UpdateEditorHighlightingText(false);
                    }
                    if (!Equals(base.Value, objB))
                    {
                        this.UpdateCanSelectRow(false);
                    }
                    else
                    {
                        this.OnEditorDataChanged();
                        if ((base.Column != null) && ((base.View != null) && ReferenceEquals(base.Column, base.View.CheckBoxSelectorColumn)))
                        {
                            this.UpdateCanSelectRow(false);
                        }
                        if ((this.isValueBinded & forceUpdate) && (base.Editor != null))
                        {
                            base.Editor.UpdateDisplayText();
                        }
                    }
                }
            }
        }

        [Description("Gets whether the cell is selected.")]
        public bool IsSelected
        {
            get => 
                this.isSelected;
            private set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.UpdateSelectionState();
                    base.RaisePropertyChanged("IsSelected");
                }
            }
        }

        [Description("Gets whether the cell is focused. This is a dependency property.")]
        public bool IsFocusedCell
        {
            get => 
                this.isFocusedCell;
            private set
            {
                if (this.isFocusedCell != value)
                {
                    this.isFocusedCell = value;
                    this.OnIsFocusedCellChanged();
                    base.RaisePropertyChanged("IsFocusedCell");
                }
            }
        }

        [Description("Gets a value that indicates the cell's selection state. This is a dependency property.")]
        public DevExpress.Xpf.Grid.SelectionState SelectionState
        {
            get => 
                this.selectionState;
            private set
            {
                if (this.selectionState != value)
                {
                    this.selectionState = value;
                    this.OnSelectionStateChanged();
                    base.RaisePropertyChanged("SelectionState");
                }
            }
        }

        [Description("Gets or sets the information about a data row containing the cell.")]
        public DevExpress.Xpf.Grid.RowData RowData =>
            (DevExpress.Xpf.Grid.RowData) base.RowDataBase;

        public object Row =>
            this.RowData.Row;

        internal DataControlBase DataControl =>
            base.View.DataControl;

        internal virtual bool IsEditing =>
            false;

        internal object InitialValue { get; set; }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public object DisplayMemberBindingValue
        {
            get => 
                base.GetValue(DisplayMemberBindingValueProperty);
            set => 
                base.SetValue(DisplayMemberBindingValueProperty, value);
        }

        internal bool HasErrorCore { get; private set; }

        private bool HasCellEditorError =>
            base.View.HasCellEditorError && ReferenceEquals(base.View.ValidationError, BaseEdit.GetValidationError(this));

        private bool CanShowCellError =>
            (base.View.ItemsSourceErrorInfoShowMode & ItemsSourceErrorInfoShowMode.Cell) != ItemsSourceErrorInfoShowMode.None;

        private bool IsAnimationControllerInited =>
            this.animationControllerCore != null;

        internal DevExpress.Xpf.Grid.Native.AnimationController AnimationController
        {
            get
            {
                this.animationControllerCore ??= new DevExpress.Xpf.Grid.Native.AnimationController();
                return this.animationControllerCore;
            }
            set => 
                this.animationControllerCore = value;
        }

        private ConditionalClientAppearanceUpdaterBase ConditionalUpdater
        {
            get
            {
                this.conditionalUpdater ??= new ConditionalCellAppearanceUpdater(this);
                return this.conditionalUpdater;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridCellData.<>c <>9 = new GridCellData.<>c();
            public static Func<ValidationError, object> <>9__85_0;
            public static Action<GridCellData, object, EventArgs> <>9__90_0;

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridCellData) d).OnDisplayMemberBindingValueChanged();
            }

            internal void <ResubscribeSimpleBinding>b__90_0(GridCellData cellData, object _, EventArgs __)
            {
                cellData.OnSimplePropertyChanged();
            }

            internal object <SetCellError>b__85_0(ValidationError rule) => 
                rule.ErrorContent;
        }
    }
}

