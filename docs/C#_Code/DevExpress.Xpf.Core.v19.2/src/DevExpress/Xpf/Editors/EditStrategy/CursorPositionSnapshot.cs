namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;

    internal class CursorPositionSnapshot
    {
        public CursorPositionSnapshot(int selectionStart, int selectionLength, string displayText, bool isAutoComplete)
        {
            this.IsAutoComplete = isAutoComplete;
            this.SelectionStart = selectionStart;
            this.SelectionLength = selectionLength;
            this.DisplayText = displayText;
        }

        public void ApplyToEdit(TextEditBase editor)
        {
            string text = editor.EditBox.Text;
            if (!string.IsNullOrEmpty(text) && (this.DisplayText != text))
            {
                if (this.IsSelectAll && !this.IsAutoComplete)
                {
                    this.SelectAll(editor);
                }
                else if (this.IsCursorAtStart)
                {
                    this.SetCursorStart(editor);
                }
                else if ((this.IsCursorAtEnd && !this.IsAutoComplete) || (text.Length < this.SelectionStart))
                {
                    this.SetCursorEnd(editor);
                }
                else
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        this.SetCursorPosition(editor, 0, false);
                    }
                    if ((this.DisplayText.Length < this.SelectionStart) || (text.Length < this.SelectionStart))
                    {
                        this.SetCursorEnd(editor);
                    }
                    int length = Math.Max(0, this.SelectionStart);
                    if ((this.DisplayText.Length < length) || (this.DisplayText.Substring(0, length) != text.Substring(0, length)))
                    {
                        this.SetCursorPosition(editor, this.SelectionStart, this.IsAutoComplete);
                    }
                    else if (this.IsSelectAll && text.StartsWith(this.DisplayText))
                    {
                        this.SetCursorPosition(editor, this.DisplayText.Length, true);
                    }
                    else
                    {
                        this.SetCursorPosition(editor, this.SelectionStart, this.IsAutoCompleteSelection);
                    }
                }
            }
        }

        private void SelectAll(TextEditBase editor)
        {
            editor.EditBox.SelectAll();
        }

        private void SetCursorEnd(TextEditBase editor)
        {
            editor.EditBox.Select(editor.EditBox.Text.Length + 1, 0);
        }

        private void SetCursorPosition(TextEditBase editor, int selectionStart, bool autoComplete)
        {
            EditBoxWrapper editBox = editor.EditBox;
            int start = Math.Min(selectionStart, editBox.Text.Length);
            editBox.Select(start, autoComplete ? Math.Max(editBox.Text.Length - selectionStart, 0) : 0);
        }

        private void SetCursorStart(TextEditBase editor)
        {
            editor.EditBox.Select(0, 0);
        }

        public bool IsAutoComplete { get; private set; }

        public int SelectionStart { get; private set; }

        public int SelectionLength { get; private set; }

        public string DisplayText { get; private set; }

        public bool IsCursorAtEnd =>
            string.IsNullOrEmpty(this.DisplayText) || (this.SelectionStart >= this.DisplayText.Length);

        public bool IsCursorAtStart =>
            !string.IsNullOrEmpty(this.DisplayText) && ((this.SelectionLength == 0) && (this.SelectionStart == 0));

        public bool IsSelectAll =>
            !string.IsNullOrEmpty(this.DisplayText) && ((this.SelectionStart == 0) && (this.SelectionLength == this.DisplayText.Length));

        private bool IsAutoCompleteSelection =>
            this.IsAutoComplete && ((this.SelectionStart + this.SelectionLength) == this.DisplayText.Length);
    }
}

