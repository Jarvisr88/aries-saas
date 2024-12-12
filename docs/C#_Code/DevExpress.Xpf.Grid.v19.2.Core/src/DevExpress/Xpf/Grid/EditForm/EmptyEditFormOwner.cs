namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EmptyEditFormOwner : IEditFormOwner
    {
        private static EmptyEditFormOwner instance;

        public IEnumerable<EditFormColumnSource> CreateEditFormColumnSource() => 
            Enumerable.Empty<EditFormColumnSource>();

        public void EnqueueImmediateAction(Action action)
        {
        }

        public object GetValue(EditFormCellData data) => 
            null;

        public void OnInlineFormClosed(bool success)
        {
        }

        public void OnIsModifiedChanged(bool isModified)
        {
        }

        public void SetValue(EditFormCellData data)
        {
        }

        public BaseValidationError Validate(EditFormCellData data) => 
            null;

        public static EmptyEditFormOwner Instance
        {
            get
            {
                instance ??= new EmptyEditFormOwner();
                return instance;
            }
        }

        public int ColumnCount =>
            0;

        public bool ShowUpdateCancelButtons =>
            false;

        public EditFormPostMode EditMode =>
            EditFormPostMode.Cached;

        public object Source =>
            null;

        public bool IsEditing { get; set; }
    }
}

