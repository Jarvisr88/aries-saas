namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Windows.Input;

    public class TextInputSettings : TextInputSettingsBase
    {
        public TextInputSettings(TextEditBase editor) : base(editor)
        {
        }

        protected internal override bool CanUndo() => 
            base.EditBox.CanUndo;

        protected internal override void Cut()
        {
            if (this.CanCut())
            {
                base.EditBox.Cut();
            }
        }

        protected internal override void Delete()
        {
            if (this.CanDelete())
            {
                base.EditBox.SelectedText = string.Empty;
            }
        }

        protected internal override bool GetAcceptsReturn() => 
            base.OwnerEdit.AcceptsReturn;

        protected internal override void InsertText(string value)
        {
            base.InsertText(value);
            base.EditBox.SelectedText = value;
        }

        protected internal override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            (key != Key.Next) && ((key != Key.Prior) && (((key == Key.Left) || (key == Key.Home)) ? base.NeedsNavigateKeyLeftRight(key, modifiers, () => this.EditBox.NeedsKey(key, modifiers)) : (((key == Key.Right) || (key == Key.End)) ? base.NeedsNavigateKeyLeftRight(key, modifiers, () => this.EditBox.NeedsKey(key, modifiers)) : (((key == Key.Up) || (key == Key.Down)) ? base.NeedsNavigateUpDown(key, modifiers, () => this.EditBox.NeedsKey(key, modifiers)) : base.EditBox.NeedsKey(key, modifiers)))));

        protected internal override void Paste()
        {
            if (this.CanPaste())
            {
                base.EditBox.Paste();
            }
        }

        protected internal override void PerformGotFocus()
        {
            base.PerformGotFocus();
            base.EditStrategy.UpdateDisplayText();
        }

        protected internal override void PerformLostFocus()
        {
            base.PerformLostFocus();
            base.EditStrategy.UpdateDisplayText();
        }

        protected internal override void ProcessPreviewMouseDown(MouseEventArgs e)
        {
            base.ProcessPreviewMouseDown(e);
            if (base.EditStrategy.IsSelectAll)
            {
                int characterIndexFromPoint = base.EditBox.GetCharacterIndexFromPoint(e.GetPosition(base.OwnerEdit.EditCore), true);
                if (characterIndexFromPoint > -1)
                {
                    this.Select(characterIndexFromPoint, 0);
                }
            }
        }

        protected internal override void Undo()
        {
            if (this.CanUndo())
            {
                base.EditBox.Undo();
            }
        }

        protected internal override void UpdateIme()
        {
            base.UpdateIme();
            base.OwnerEdit.Do<TextEditBase>(x => base.EditBox.IsImeEnabled(true));
        }
    }
}

