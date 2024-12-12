namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EditFormOwner : IEditFormOwner
    {
        private readonly ITableView view;
        private const string CaptionSeparator = ":";
        private int editFormRowHandle;

        public EditFormOwner(ITableView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            this.view = view;
            this.editFormRowHandle = this.DataView.FocusedRowHandle;
        }

        private static object CalcCaption(ColumnBase column) => 
            (column.EditFormCaption == null) ? ((column.HeaderCaption != null) ? (column.HeaderCaption.ToString() + ":") : null) : column.EditFormCaption;

        private static bool CalcReadOnly(ColumnBase column, bool defaultAllowEditing) => 
            column.IsActualReadOnly || ((column.AllowEditing == DefaultBoolean.False) || ((column.AllowEditing == DefaultBoolean.Default) && !defaultAllowEditing));

        private static bool CalcVisible(ColumnBase column) => 
            (column.EditFormVisible != null) ? column.EditFormVisible.Value : column.Visible;

        public virtual IEnumerable<EditFormColumnSource> CreateEditFormColumnSource()
        {
            IEnumerable<ColumnBase> source = this.Grid.ColumnsCore.Cast<ColumnBase>();
            Func<ColumnBase, bool> predicate = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<ColumnBase, bool> local1 = <>c.<>9__8_0;
                predicate = <>c.<>9__8_0 = x => x.EditFormVisibleIndex != 0;
            }
            Func<ColumnBase, int> visibleIndexSelector = CreateVisibleIndexSelector(source.Any<ColumnBase>(predicate));
            bool defaultAllowEditing = this.DataView.AllowEditing;
            return (from x in source select CreateEditFormColumnSource(x, visibleIndexSelector, defaultAllowEditing));
        }

        private static EditFormColumnSource CreateEditFormColumnSource(ColumnBase column, Func<ColumnBase, int> visibleIndexSelector, bool defaultAllowEditing)
        {
            EditFormColumnSource source1 = new EditFormColumnSource();
            source1.FieldName = column.FieldName;
            source1.EditSettings = column.ActualEditSettings;
            source1.Caption = CalcCaption(column);
            source1.ColumnSpan = column.EditFormColumnSpan;
            source1.RowSpan = column.EditFormRowSpan;
            source1.StartNewRow = column.EditFormStartNewRow;
            source1.Visible = CalcVisible(column);
            source1.VisibleIndex = visibleIndexSelector(column);
            source1.EditorTemplate = column.EditFormTemplate;
            source1.ReadOnly = CalcReadOnly(column, defaultAllowEditing);
            source1.EditorViewInfo = column;
            return source1;
        }

        private static Func<ColumnBase, int> CreateVisibleIndexSelector(bool useEditFormIndexes) => 
            !useEditFormIndexes ? (<>c.<>9__13_1 ??= x => x.ActualVisibleIndex) : (<>c.<>9__13_0 ??= x => x.EditFormVisibleIndex);

        public void EnqueueImmediateAction(Action action)
        {
            this.DataView.EnqueueImmediateAction(action);
        }

        private ColumnBase GetColumn(EditFormCellData data) => 
            this.Grid.ColumnsCore[data.FieldName];

        public object GetValue(EditFormCellData data) => 
            this.Grid.GetCellValue(this.editFormRowHandle, data.FieldName);

        public void OnInlineFormClosed(bool success)
        {
            this.DataView.EditFormManager.OnInlineFormClosed(success);
        }

        public void OnIsModifiedChanged(bool isModified)
        {
            this.DataView.EditFormManager.OnIsModifiedChanged(isModified);
        }

        public void SetValue(EditFormCellData data)
        {
            try
            {
                this.Grid.SetCellValueCore(this.editFormRowHandle, data.FieldName, data.Value);
            }
            catch (Exception exception)
            {
                data.ValidationError = RowValidationHelper.CreateEditorValidationError(this.DataView, this.editFormRowHandle, exception, this.GetColumn(data));
            }
        }

        public BaseValidationError Validate(EditFormCellData data)
        {
            ColumnBase column = this.GetColumn(data);
            if (column == null)
            {
                return null;
            }
            BaseValidationError error = null;
            if (data.HasInnerError)
            {
                error = RowValidationHelper.CreateEditorValidationError(this.DataView, this.editFormRowHandle, null, column);
            }
            if (error != null)
            {
                return error;
            }
            error = column.ActualShowValidationAttributeErrors ? RowValidationHelper.ValidateAttributes(this.DataView, data.Value, this.editFormRowHandle, column) : null;
            return ((error == null) ? RowValidationHelper.ValidateEvents(this.DataView, data, data.Value, this.editFormRowHandle, column) : error);
        }

        internal DataControlBase Grid =>
            this.DataView.DataControl;

        private DataViewBase DataView =>
            this.view.ViewBase;

        public int ColumnCount =>
            this.view.EditFormColumnCount;

        public bool ShowUpdateCancelButtons =>
            this.view.ShowEditFormUpdateCancelButtons;

        public EditFormPostMode EditMode =>
            this.view.EditFormPostMode;

        public object Source =>
            this.DataView.GetRowData(this.editFormRowHandle);

        public bool IsEditing
        {
            get => 
                this.DataView.IsEditing;
            set => 
                this.DataView.IsEditing = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditFormOwner.<>c <>9 = new EditFormOwner.<>c();
            public static Func<ColumnBase, bool> <>9__8_0;
            public static Func<ColumnBase, int> <>9__13_0;
            public static Func<ColumnBase, int> <>9__13_1;

            internal bool <CreateEditFormColumnSource>b__8_0(ColumnBase x) => 
                x.EditFormVisibleIndex != 0;

            internal int <CreateVisibleIndexSelector>b__13_0(ColumnBase x) => 
                x.EditFormVisibleIndex;

            internal int <CreateVisibleIndexSelector>b__13_1(ColumnBase x) => 
                x.ActualVisibleIndex;
        }
    }
}

