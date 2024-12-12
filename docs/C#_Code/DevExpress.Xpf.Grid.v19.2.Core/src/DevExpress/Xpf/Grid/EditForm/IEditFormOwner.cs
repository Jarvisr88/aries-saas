namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public interface IEditFormOwner
    {
        IEnumerable<EditFormColumnSource> CreateEditFormColumnSource();
        void EnqueueImmediateAction(Action action);
        object GetValue(EditFormCellData data);
        void OnInlineFormClosed(bool success);
        void OnIsModifiedChanged(bool isModified);
        void SetValue(EditFormCellData data);
        BaseValidationError Validate(EditFormCellData data);

        int ColumnCount { get; }

        bool ShowUpdateCancelButtons { get; }

        EditFormPostMode EditMode { get; }

        object Source { get; }

        bool IsEditing { get; set; }
    }
}

