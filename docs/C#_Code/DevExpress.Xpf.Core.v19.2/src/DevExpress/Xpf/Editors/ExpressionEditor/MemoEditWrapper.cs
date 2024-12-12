namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Xpf.Editors;
    using System;

    public class MemoEditWrapper : IMemoEdit
    {
        private readonly TextEdit edit;

        public MemoEditWrapper(TextEdit edit)
        {
            this.edit = edit;
        }

        void IMemoEdit.Focus()
        {
            this.edit.Dispatcher.BeginInvoke(() => this.edit.Focus(), new object[0]);
        }

        int IMemoEdit.GetLineLength(int lineIndex) => 
            this.edit.GetLineLength(lineIndex);

        void IMemoEdit.Paste(string text)
        {
            this.edit.SelectedText = text;
        }

        int IMemoEdit.SelectionStart
        {
            get => 
                this.edit.SelectionStart;
            set => 
                this.edit.SelectionStart = value;
        }

        int IMemoEdit.SelectionLength
        {
            get => 
                this.edit.SelectionLength;
            set => 
                this.edit.SelectionLength = value;
        }

        string IMemoEdit.Text
        {
            get => 
                this.edit.Text;
            set => 
                this.edit.Text = value;
        }

        string IMemoEdit.SelectedText =>
            this.edit.SelectedText;
    }
}

