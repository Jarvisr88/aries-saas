namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public abstract class CellEditorBase : InplaceEditorBase
    {
        public static readonly DependencyProperty RowDataProperty;
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty ColumnProperty;
        public static readonly DependencyProperty IsFocusedCellProperty;
        public static readonly DependencyProperty ToolTipContentProperty;
        private IGridCellEditorOwner gridCellEditorOwner;
        protected readonly Locker EditValueChangedLocker = new Locker();
        private ToolTip toolTip;
        internal readonly Locker CancelEditLocker = new Locker();

        static CellEditorBase()
        {
            RowDataProperty = DependencyPropertyManager.Register("RowData", typeof(DevExpress.Xpf.Grid.RowData), typeof(CellEditorBase), new PropertyMetadata(null, (d, e) => ((CellEditorBase) d).OnRowDataChanged((DevExpress.Xpf.Grid.RowData) e.OldValue)));
            FieldNameProperty = DependencyPropertyManager.Register("FieldName", typeof(string), typeof(CellEditorBase), new PropertyMetadata(null, (d, e) => ((CellEditorBase) d).OnFieldNameChanged()));
            ColumnProperty = DependencyPropertyManager.Register("Column", typeof(ColumnBase), typeof(CellEditorBase), new PropertyMetadata(null, (d, e) => ((CellEditorBase) d).OnColumnChanged((ColumnBase) e.OldValue, (ColumnBase) e.NewValue)));
            IsFocusedCellProperty = DependencyPropertyManager.Register("IsFocusedCell", typeof(bool), typeof(CellEditorBase), new PropertyMetadata(false, (d, e) => ((CellEditorBase) d).OnIsFocusedCellChanged()));
            ToolTipContentProperty = DependencyPropertyManager.Register("ToolTipContent", typeof(object), typeof(CellEditorBase), new PropertyMetadata(null, (d, e) => ((CellEditorBase) d).OnToolTipContentChanged()));
            ColumnBase.NavigationIndexProperty.AddOwner(typeof(CellEditorBase));
        }

        protected CellEditorBase()
        {
        }

        public override void CancelEditInVisibleEditor()
        {
            this.CancelEditLocker.DoLockedAction(() => base.CancelEditInVisibleEditor());
        }

        public override bool CanShowEditor() => 
            base.CanShowEditor() && this.View.CanShowEditor(this.RowHandle, this.Column);

        internal virtual IAnimationConnector CreateAnimationConnector() => 
            null;

        protected override bool? GetAllowDefaultButton() => 
            (this.Column == null) ? base.GetAllowDefaultButton() : new bool?(this.Column.GetAllowEditing());

        internal virtual IConditionalFormattingClientBase GetConditionalFormattingClient() => 
            null;

        protected sealed override object GetEditableValue() => 
            this.DataControl.GetCellValue(this.RowHandle, this.FieldNameCore);

        protected sealed override EditableDataObject GetEditorDataContext() => 
            this.CellData;

        private static bool GetIsInactiveEditorButtonVisible(EditorButtonShowMode editorShowMode, bool isFocusedCell, bool isFocusedRow)
        {
            switch (editorShowMode)
            {
                case EditorButtonShowMode.ShowOnlyInEditor:
                    return false;

                case EditorButtonShowMode.ShowForFocusedRow:
                    return isFocusedRow;

                case EditorButtonShowMode.ShowAlways:
                    return true;
            }
            return isFocusedCell;
        }

        protected sealed override bool HasEditorError() => 
            this.View.HasCellEditorError;

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            HitTestResult result = base.HitTestCore(hitTestParameters);
            return ((result == null) ? new PointHitTestResult(this, hitTestParameters.HitPoint) : result);
        }

        protected sealed override bool IsInactiveEditorButtonVisible()
        {
            if ((this.Column != null) && !this.Column.GetAllowEditing())
            {
                Func<ButtonEditSettings, bool> evaluator = <>c.<>9__92_0;
                if (<>c.<>9__92_0 == null)
                {
                    Func<ButtonEditSettings, bool> local1 = <>c.<>9__92_0;
                    evaluator = <>c.<>9__92_0 = delegate (ButtonEditSettings x) {
                        bool? allowDefaultButton = x.AllowDefaultButton;
                        bool flag = false;
                        return (allowDefaultButton.GetValueOrDefault() == flag) ? (allowDefaultButton == null) : true;
                    };
                }
                if ((this.Column.ActualEditSettings as ButtonEditSettings).Return<ButtonEditSettings, bool>(evaluator, <>c.<>9__92_1 ??= () => false))
                {
                    return false;
                }
            }
            return GetIsInactiveEditorButtonVisible(this.View.EditorButtonShowMode, this.IsFocusedCell, this.IsRowFocused);
        }

        protected override void NullEditorInEditorDataContext()
        {
            if (ReferenceEquals(this.CellData.Editor, this))
            {
                this.CellData.Editor = null;
            }
        }

        public virtual void OnColumnChanged(ColumnBase oldValue, ColumnBase newValue)
        {
            BindingOperations.ClearBinding(this, ToolTipContentProperty);
            this.ColumnCore = newValue;
            if ((base.editCore != null) && (base.EditorSourceType == InplaceEditorBase.BaseEditSourceType.CellTemplate))
            {
                BaseEditHelper.ApplySettings(base.editCore, this.EditorColumn.EditSettings, this.EditorColumn);
            }
            base.OnOwnerChanged(oldValue);
            if (this.CellData != null)
            {
                this.CellData.UpdateEditorHighlightingText(true);
            }
            this.toolTip = null;
            this.UpdateToolTip();
        }

        protected override void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
        {
            if (ReferenceEquals(e.Property, ColumnBase.DisplayTemplateProperty))
            {
                this.UpdateDisplayTemplate(true);
            }
            else
            {
                if (ReferenceEquals(e.Property, ColumnBase.ReadOnlyProperty))
                {
                    base.UpdateIsEditorReadOnly();
                }
                if (ReferenceEquals(e.Property, BaseColumn.ActualCellToolTipTemplateProperty))
                {
                    this.UpdateToolTip();
                }
                if (ReferenceEquals(e.Property, ColumnBase.FieldTypeProperty) && (base.EditorSourceType == InplaceEditorBase.BaseEditSourceType.EditSettings))
                {
                    base.UpdateViewInfoProperties();
                }
                base.OnColumnContentChanged(sender, e);
            }
        }

        internal void OnDataChanged()
        {
            this.UpdateConditionalAppearance();
        }

        protected override void OnEditorActivated()
        {
            base.OnEditorActivated();
            this.View.SetActiveEditor();
            this.View.ViewBehavior.OnEditorActivated();
            if ((base.editCore is IInplaceBaseEdit) && (((IInplaceBaseEdit) base.editCore).BaseEdit != null))
            {
                this.View.RaiseShownEditor(this.RowHandle, this.Column, ((IInplaceBaseEdit) base.editCore).BaseEdit);
            }
            else
            {
                this.View.RaiseShownEditor(this.RowHandle, this.Column, base.editCore);
            }
        }

        protected virtual void OnEditValueChanged()
        {
            if ((this.View != null) && ((this.RowHandle != -2147483645) && ((this.View.ShowUpdateRowButtonsCore == ShowUpdateRowButtons.OnCellValueChange) && !this.View.AreUpdateRowButtonsShown)))
            {
                this.View.AreUpdateRowButtonsShown = true;
                this.View.RootView.AreUpdateRowButtonsShown = true;
                if (this.View.ViewBehavior != null)
                {
                    this.View.ViewBehavior.UpdateRowButtonsControl();
                }
            }
        }

        protected sealed override void OnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.EditValueChangedLocker.DoLockedAction(delegate {
                if (this.View.EnableImmediatePosting)
                {
                    this.View.RaiseCellValueChanging(this.RowHandle, this.Column, this.Edit.EditValue, e.OldValue);
                }
                this.ResetValidationErrorCache();
                this.OnEditValueChanged();
                this.UpdateEditorDataContextValue(e.NewValue);
                this.ResetValidationErrorCache();
                if (!this.View.EnableImmediatePosting)
                {
                    this.View.RaiseCellValueChanging(this.RowHandle, this.Column, this.Edit.EditValue, e.OldValue);
                }
            });
        }

        private void OnFieldNameChanged()
        {
            if ((this.Column == null) && ((this.FieldName != null) && (this.View != null)))
            {
                this.Column = this.View.ColumnsCore[this.FieldName];
            }
        }

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.OnHiddenEditor(closeEditor);
            ScrollBarExtensions.SetPreventParentScrolling(this, false);
            this.View.OnHideEditor(this, closeEditor);
            this.View.RaiseHiddenEditor(this.RowHandle, this.Column, base.editCore);
        }

        protected virtual void OnRowDataChanged(DevExpress.Xpf.Grid.RowData oldValue)
        {
            this.RowDataCore = this.RowData;
            base.UpdateData();
            this.UpdateContent(true);
        }

        protected override void OnShowEditor()
        {
            ScrollBarExtensions.SetPreventParentScrolling(this, true);
            this.View.OnShowEditor(this);
        }

        private void OnToolTipContentChanged()
        {
            if (this.toolTip == null)
            {
                if (this.ToolTipContent != null)
                {
                    this.UpdateToolTip();
                }
            }
            else
            {
                this.toolTip.Content = this.ToolTipContent;
                if ((this.toolTip.Content == null) || (this.toolTip.Content == Binding.DoNothing))
                {
                    this.toolTip = null;
                    this.UpdateToolTip();
                }
            }
        }

        internal void OnViewChanged()
        {
            this.GridCellEditorOwner.OnViewChanged();
        }

        protected sealed override bool RaiseShowingEditor() => 
            this.View.RaiseShowingEditor(this.RowHandle, this.Column);

        private void ResetValidationErrorCache()
        {
            this.ValidationErrorCacheIsValid = false;
            this.ValidationErrorCache = null;
        }

        protected override void SetEdit(IBaseEdit value)
        {
            base.SetEdit(value);
            base.UpdateIsEditorReadOnly();
        }

        protected override void SetEditorInEditorDataContext()
        {
            this.CellData.Editor = this;
            this.UpdateConditionalAppearance();
        }

        internal void SyncIsEnabled(bool isEnabled)
        {
            if (this.ShouldSyncCellContentPresenterProperties)
            {
                this.GridCellEditorOwner.UpdateIsEnabled(isEnabled);
            }
        }

        internal void SyncProperties()
        {
            if (this.ShouldSyncCellContentPresenterProperties)
            {
                this.GridCellEditorOwner.SynProperties(this.CellData);
            }
        }

        internal void UpdateCellState()
        {
            this.GridCellEditorOwner.UpdateCellState();
        }

        protected virtual void UpdateConditionalAppearance()
        {
        }

        protected override void UpdateContent(bool updateDisplayTemplate = true)
        {
            base.UpdateContent(updateDisplayTemplate);
            this.UpdateEditorDataContext();
        }

        internal void UpdateEditableValue()
        {
            base.UpdateEditValue(base.editCore);
        }

        protected override void UpdateEditorDataContext()
        {
            if (this.CellData != null)
            {
                this.CellData.OnEditorContentUpdated();
            }
        }

        protected abstract void UpdateEditorDataContextValue(object newValue);
        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            if (!this.EditValueChangedLocker.IsLocked)
            {
                BaseEditHelper.SetEditValue(editor, base.EditorDataContext.Value);
            }
        }

        internal void UpdateIsReady()
        {
            this.GridCellEditorOwner.UpdateIsReady();
        }

        protected internal virtual void UpdatePrintingMergeValue()
        {
        }

        protected virtual void UpdateToolTip()
        {
            if (this.Column != null)
            {
                if (this.Column.CellToolTipBinding == null)
                {
                    BindingOperations.ClearBinding(this, ToolTipContentProperty);
                    this.toolTip = null;
                }
                else
                {
                    if (!ReferenceEquals(BindingOperations.GetBinding(this, ToolTipContentProperty), this.Column.CellToolTipBinding))
                    {
                        MultiBinding multiBinding = BindingOperations.GetMultiBinding(this, ToolTipContentProperty);
                        if ((multiBinding == null) || !ReferenceEquals(multiBinding, this.Column.CellToolTipBinding))
                        {
                            base.SetBinding(ToolTipContentProperty, this.Column.CellToolTipBinding);
                        }
                    }
                    if ((this.toolTip == null) && ((this.ToolTipContent != null) && (this.ToolTipContent != Binding.DoNothing)))
                    {
                        ToolTip tip1 = new ToolTip();
                        tip1.Content = this.ToolTipContent;
                        this.toolTip = tip1;
                    }
                }
                if (this.toolTip == null)
                {
                    base.ClearValue(FrameworkElement.ToolTipProperty);
                }
                else
                {
                    ToolTipService.SetToolTip(this, this.toolTip);
                }
                if ((this.toolTip != null) && !ReferenceEquals(this.toolTip.ContentTemplate, this.Column.ActualCellToolTipTemplate))
                {
                    this.toolTip.ContentTemplate = this.Column.ActualCellToolTipTemplate;
                }
            }
        }

        internal void UpdateValidationErrorCore()
        {
            this.UpdateValidationError();
        }

        protected DevExpress.Xpf.Grid.RowData RowDataCore { get; private set; }

        public DevExpress.Xpf.Grid.RowData RowData
        {
            get => 
                (DevExpress.Xpf.Grid.RowData) base.GetValue(RowDataProperty);
            set => 
                base.SetValue(RowDataProperty, value);
        }

        public string FieldName
        {
            get => 
                (string) base.GetValue(FieldNameProperty);
            set => 
                base.SetValue(FieldNameProperty, value);
        }

        internal ColumnBase ColumnCore { get; private set; }

        public ColumnBase Column
        {
            get => 
                (ColumnBase) base.GetValue(ColumnProperty);
            set => 
                base.SetValue(ColumnProperty, value);
        }

        public int NavigationIndex
        {
            get => 
                (int) base.GetValue(ColumnBase.NavigationIndexProperty);
            set => 
                base.SetValue(ColumnBase.NavigationIndexProperty, value);
        }

        public bool IsFocusedCell
        {
            get => 
                (bool) base.GetValue(IsFocusedCellProperty);
            set => 
                base.SetValue(IsFocusedCellProperty, value);
        }

        public object ToolTipContent
        {
            get => 
                base.GetValue(ToolTipContentProperty);
            set => 
                base.SetValue(ToolTipContentProperty, value);
        }

        internal IGridCellEditorOwner GridCellEditorOwner
        {
            get => 
                this.gridCellEditorOwner ?? NullGridCellEditorOwner.Instance;
            set => 
                this.gridCellEditorOwner = value;
        }

        protected override DependencyObject EditorRoot =>
            this.GridCellEditorOwner.EditorRoot;

        protected sealed override InplaceEditorOwnerBase Owner =>
            this.View?.InplaceEditorOwner;

        protected sealed override IInplaceEditorColumn EditorColumn =>
            this.ColumnCore;

        protected virtual bool IsRowFocused =>
            this.RowHandle == this.View.FocusedRowHandle;

        internal DataViewBase View =>
            this.RowDataCore?.View;

        protected DataControlBase DataControl =>
            this.View.DataControl;

        protected internal abstract int RowHandle { get; }

        protected string FieldNameCore =>
            this.Column.FieldName;

        public EditGridCellData CellData { get; internal set; }

        protected sealed override bool IsCellFocused =>
            this.IsFocusedCell;

        protected override bool AllowCustomEditors =>
            true;

        protected RowValidationError ValidationErrorCache { get; set; }

        protected bool ValidationErrorCacheIsValid { get; set; }

        protected virtual bool ShouldSyncCellContentPresenterProperties =>
            true;

        protected internal virtual bool HasBindingErrors =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CellEditorBase.<>c <>9 = new CellEditorBase.<>c();
            public static Func<ButtonEditSettings, bool> <>9__92_0;
            public static Func<bool> <>9__92_1;

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditorBase) d).OnRowDataChanged((RowData) e.OldValue);
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditorBase) d).OnFieldNameChanged();
            }

            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditorBase) d).OnColumnChanged((ColumnBase) e.OldValue, (ColumnBase) e.NewValue);
            }

            internal void <.cctor>b__5_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditorBase) d).OnIsFocusedCellChanged();
            }

            internal void <.cctor>b__5_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditorBase) d).OnToolTipContentChanged();
            }

            internal bool <IsInactiveEditorButtonVisible>b__92_0(ButtonEditSettings x)
            {
                bool? allowDefaultButton = x.AllowDefaultButton;
                bool flag = false;
                return ((allowDefaultButton.GetValueOrDefault() == flag) ? (allowDefaultButton == null) : true);
            }

            internal bool <IsInactiveEditorButtonVisible>b__92_1() => 
                false;
        }
    }
}

