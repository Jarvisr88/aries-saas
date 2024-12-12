namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TokenEditorWrapper : EditBoxWrapper
    {
        public TokenEditorWrapper(LookUpEditBase editor) : base(editor)
        {
        }

        public override void AfterAcceptPopupValue()
        {
            if (this.HasEditBox)
            {
                this.EditBox.AfterAcceptPopupValue();
            }
        }

        public override void BeforeAcceptPopupValue()
        {
            if (this.HasEditBox)
            {
                this.EditBox.BeforeAcceptPopupValue();
            }
        }

        public override void Copy()
        {
            if (this.HasEditBox)
            {
                this.EditBox.Copy();
            }
        }

        public override void Cut()
        {
            if (this.HasEditBox)
            {
                this.EditBox.Cut();
            }
        }

        public override int GetCharacterIndexFromLineIndex(int lineIndex) => 
            this.HasEditBox ? this.EditBox.GetCharacterIndexFromLineIndex(lineIndex) : -1;

        public override int GetCharacterIndexFromPoint(Point point, bool snapToText) => 
            this.HasEditBox ? this.EditBox.GetCharacterIndexFromPoint(point, snapToText) : -1;

        public override int GetFirstVisibleLineIndex() => 
            this.HasEditBox ? this.EditBox.GetFirstVisibleLineIndex() : -1;

        public override bool GetIsInImeInput()
        {
            if (this.HasEditBox)
            {
                ButtonEdit activeEditor = this.EditBox.ActiveEditor;
                if (activeEditor != null)
                {
                    return activeEditor.IsInImeInput;
                }
                ButtonEdit local1 = activeEditor;
            }
            return false;
        }

        public override int GetLastVisibleLineIndex() => 
            this.HasEditBox ? this.EditBox.GetLastVisibleLineIndex() : -1;

        public override int GetLineIndexFromCharacterIndex(int charIndex) => 
            this.HasEditBox ? this.EditBox.GetLineIndexFromCharacterIndex(charIndex) : -1;

        public override int GetLineLength(int lineIndex) => 
            this.HasEditBox ? this.EditBox.GetLineLength(lineIndex) : 0;

        public override string GetLineText(int lineIndex) => 
            this.HasEditBox ? this.EditBox.GetLineText(lineIndex) : string.Empty;

        private bool IsInActiveMode() => 
            this.Editor.EditMode == EditMode.InplaceActive;

        private bool NeedsActivationKeyInInactiveMode(Key key, ModifierKeys modifiers) => 
            (this.EditBox.ActiveEditor == null) && this.EditBox.NeedsActivationKeyInInactiveMode(key, modifiers);

        private bool NeedsEnterInInplaceActiveMode(Key key) => 
            this.NeedsEnterKey() && (key == Key.Return);

        public override bool NeedsEnterKey() => 
            this.IsInActiveMode() && this.EditBox.IsValueChanged();

        public override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            (this.Editor.EditMode == EditMode.InplaceInactive) || (this.NeedsNavigationKey(key, modifiers) || (this.NeedsKeyInInplaceActiveMode(key, modifiers) || (this.NeedsEnterInInplaceActiveMode(key) || this.NeedsActivationKeyInInactiveMode(key, modifiers))));

        private bool NeedsKeyInInplaceActiveMode(Key key, ModifierKeys modifiers) => 
            this.IsInActiveMode() && ((this.EditBox.ActiveEditor != null) && this.EditBox.ActiveEditor.NeedsKey(key, modifiers));

        public override bool NeedsNavigationKey(Key key, ModifierKeys modifiers) => 
            this.HasEditBox && this.EditBox.NeedsKey(key, modifiers);

        public override void OnEditorPreviewLostFocus(bool isEditorLostFocus)
        {
            if (this.HasEditBox)
            {
                this.EditBox.OnEditorPreviewLostFocus(isEditorLostFocus);
            }
        }

        public override void OnRestoreDisplayText()
        {
            base.OnRestoreDisplayText();
            if (this.HasEditBox)
            {
                this.EditBox.OnRestoreDisplayText();
            }
        }

        public override void Paste()
        {
            if (this.HasEditBox)
            {
                this.EditBox.Paste();
            }
        }

        public override bool ProccessKeyDown(KeyEventArgs e) => 
            this.HasEditBox && this.EditBox.ProcessKeyDown(e);

        public override void ScrollToHome()
        {
            if (this.HasEditBox)
            {
                this.EditBox.ScrollToHome();
            }
        }

        public override void Select(int start, int length)
        {
            if (this.HasEditBox)
            {
                this.EditBox.Select(start, length);
            }
        }

        public override void SelectAll()
        {
            if (this.HasEditBox)
            {
                this.EditBox.SelectAll();
            }
        }

        public override void SyncWithValue(UpdateEditorSource updateSource)
        {
            base.SyncWithValue(updateSource);
            if (this.HasEditBox)
            {
                this.EditBox.SyncWithValue(updateSource);
            }
        }

        public override void Undo()
        {
            if (this.HasEditBox)
            {
                this.EditBox.Undo();
            }
        }

        public override void UnselectAll()
        {
            if (this.HasEditBox)
            {
                this.EditBox.SelectedText = string.Empty;
            }
        }

        private TokenEditor EditBox =>
            this.EditCore as TokenEditor;

        private LookUpEditBase Editor =>
            base.Editor as LookUpEditBase;

        private bool HasEditBox =>
            this.EditBox != null;

        public override Brush Foreground =>
            this.HasEditBox ? this.EditBox.Foreground : null;

        public override int LineCount =>
            this.HasEditBox ? this.EditBox.LinesCount : 0;

        public override string SelectedText
        {
            get => 
                this.HasEditBox ? this.EditBox.SelectedText : string.Empty;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.SelectedText = value;
                }
            }
        }

        public override int SelectionLength
        {
            get => 
                this.HasEditBox ? this.EditBox.SelectionLength : 0;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.SelectionLength = value;
                }
            }
        }

        public override int SelectionStart
        {
            get => 
                this.HasEditBox ? this.EditBox.SelectionStart : 0;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.SelectionStart = value;
                }
            }
        }

        public override int CaretIndex
        {
            get => 
                this.HasEditBox ? this.EditBox.CaretIndex : -1;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.CaretIndex = value;
                }
            }
        }

        public override bool CanUndo =>
            this.HasEditBox && this.EditBox.CanUndo;

        public override int MaxLength
        {
            get => 
                this.HasEditBox ? this.EditBox.MaxLength : 0;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.MaxLength = value;
                }
            }
        }

        public override System.Windows.Controls.CharacterCasing CharacterCasing
        {
            get => 
                this.HasEditBox ? this.EditBox.CharacterCasing : System.Windows.Controls.CharacterCasing.Normal;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.CharacterCasing = value;
                }
            }
        }

        public override string Text =>
            this.HasEditBox ? this.EditBox.Text : string.Empty;

        public override object EditValue
        {
            get => 
                this.EditBox?.EditValue;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.SetEditValue(value);
                }
            }
        }

        public override bool IsReadOnly
        {
            get => 
                this.HasEditBox && this.EditBox.IsReadOnly;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.IsReadOnly = value;
                }
            }
        }

        public override bool IsUndoEnabled
        {
            get => 
                this.HasEditBox && this.EditBox.IsUndoEnabled;
            set
            {
                if (this.HasEditBox)
                {
                    this.EditBox.IsUndoEnabled = value;
                }
            }
        }
    }
}

