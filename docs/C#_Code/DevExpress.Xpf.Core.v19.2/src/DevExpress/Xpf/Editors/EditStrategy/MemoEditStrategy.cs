namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class MemoEditStrategy : PopupBaseEditStrategy
    {
        private bool isMemoTextModified;

        public MemoEditStrategy(MemoEdit editor) : base(editor)
        {
        }

        protected internal override object ConvertEditValueForFormatDisplayText(object convertedValue) => 
            TextBlockService.GetFirstLineFromText(Convert.ToString(base.ConvertEditValueForFormatDisplayText(convertedValue)));

        public override void EditValueChanged(object oldValue, object newValue)
        {
            base.EditValueChanged(oldValue, newValue);
            this.SyncMemoWithEditor(this.Editor.Memo);
        }

        private void Memo_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.IsMemoTextModified ??= true;
        }

        public void OnPopupOpened()
        {
            this.IsMemoTextModified = false;
            if (this.Editor.Memo != null)
            {
                this.Editor.Memo.EditValueChanged += new EditValueChangedEventHandler(this.Memo_EditValueChanged);
            }
        }

        public void SyncMemoWithEditor(TextEdit memo)
        {
            if (memo != null)
            {
                bool isUndoEnabled = memo.EditBox.IsUndoEnabled;
                memo.EditBox.IsUndoEnabled = false;
                memo.EditValue = base.EditValue;
                memo.EditBox.IsUndoEnabled = isUndoEnabled;
                memo.EditBox.CaretIndex = 0;
            }
        }

        public void SyncWithMemo()
        {
            if (this.Editor.Memo != null)
            {
                base.ValueContainer.SetEditValue(this.Editor.Memo.EditValue, UpdateEditorSource.TextInput);
            }
        }

        protected MemoEdit Editor =>
            base.Editor as MemoEdit;

        protected bool IsMemoTextModified
        {
            get => 
                this.isMemoTextModified;
            set
            {
                this.isMemoTextModified = value;
                this.Editor.UpdateOkButtonIsEnabled(this.IsMemoTextModified);
            }
        }
    }
}

