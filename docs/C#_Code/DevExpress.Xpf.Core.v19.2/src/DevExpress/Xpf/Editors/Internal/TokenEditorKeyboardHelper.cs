namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class TokenEditorKeyboardHelper
    {
        public TokenEditorKeyboardHelper(DevExpress.Xpf.Editors.Internal.TokenEditor tokenEditor)
        {
            this.TokenEditor = tokenEditor;
        }

        private bool CanNavigateLeft() => 
            this.IsFirstLeftTokenNotFocused() && this.CanNavigateLeftInActiveMode();

        private bool CanNavigateLeftInActiveMode() => 
            (this.ActivatedToken.CaretIndex == 0) || this.ActivatedToken.Text.Equals(this.ActivatedToken.SelectedText);

        private bool CanNavigateRight() => 
            this.IsLastRightTokenNotFocused() && this.CanNavigateRightInActiveMode();

        private bool CanNavigateRightInActiveMode() => 
            (this.ActivatedToken.CaretIndex == this.ActivatedToken.Text.Length) || this.ActivatedToken.Text.Equals(this.ActivatedToken.SelectedText);

        private bool CanNavigateToLine(Key key, ModifierKeys modifiers)
        {
            if (!this.TokenEditor.EnableTokenWrapping)
            {
                return false;
            }
            bool flag = this.IsUpOrDownKey(key);
            return (!this.HasActivatedToken ? flag : (this.IsCtrlPressed() & flag));
        }

        private bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            (this.FocusedToken != null) && (!this.TokenEditor.IsReadOnly && this.FocusedToken.IsActivatingKey(key, modifiers));

        private bool IsCommitTokenKey(Key key) => 
            (key == Key.Return) || (key == Key.Tab);

        public bool IsCtrlPressed() => 
            ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers);

        private bool IsDeleteTokenKey(Key key) => 
            (this.HasActivatedToken || (key != Key.Delete)) ? (this.IsTokenTextEmpty() && ((key == Key.Back) || (key == Key.Delete))) : true;

        private bool IsFirstLeftTokenNotFocused() => 
            (this.FocusedToken == null) || (!this.IsRightToLeft() ? !this.IsFirstTokenFocused() : !this.IsLastTokenFocused());

        private bool IsFirstTokenFocused() => 
            (this.FocusedToken != null) && this.TokenPanel.IsFirstToken(this.FocusedToken);

        private bool IsFocusedEditorInEndLine() => 
            this.TokenPanel.IsTokenInEndLine(this.FocusedToken);

        private bool IsFocusedEditorInFirstLine() => 
            this.TokenPanel.IsTokenInFirstLine(this.FocusedToken);

        private bool IsLastRightTokenNotFocused() => 
            (this.FocusedToken == null) || (!this.IsRightToLeft() ? !this.IsLastTokenFocused() : !this.IsFirstTokenFocused());

        private bool IsLastTokenFocused() => 
            (this.FocusedToken != null) && this.TokenPanel.IsLastToken(this.FocusedToken);

        private bool IsLeftNavigationKey(Key key) => 
            (key == Key.Left) || (key == Key.Home);

        private bool IsLeftRightKey(Key key) => 
            (key == Key.Left) || (key == Key.Right);

        private bool IsRightNavigationKey(Key key) => 
            (key == Key.Right) || (key == Key.End);

        private bool IsRightToLeft() => 
            this.TokenEditor.FlowDirection == FlowDirection.RightToLeft;

        private bool IsShiftPressed(ModifierKeys modifiers) => 
            ModifierKeysHelper.IsShiftPressed(modifiers);

        private bool IsTokenTextEmpty() => 
            (this.ActivatedToken != null) && string.IsNullOrEmpty(this.ActivatedToken.Text);

        private bool IsUpOrDownKey(Key key) => 
            (key == Key.Up) || (key == Key.Down);

        private bool NavigateDown(ModifierKeys modifiers)
        {
            if (!this.IsFocusedEditorInEndLine())
            {
                this.TokenEditor.OnNavigateDown(this.IsShiftPressed(modifiers));
            }
            return true;
        }

        private bool NavigateEnd() => 
            (this.TokenPanel.Orientation != Orientation.Horizontal) ? this.NavigateEndVertical() : this.NavigateEndHorizontal();

        private bool NavigateEndHorizontal() => 
            (!this.IsLastTokenFocused() || this.TokenEditor.ShowNewTokenFromEnd) ? (!this.HasActivatedToken ? this.TokenEditor.OnNavigateHorizontalEnd(false) : (this.CanNavigateRightInActiveMode() && this.TokenEditor.OnNavigateHorizontalEnd(true))) : false;

        private bool NavigateEndVertical()
        {
            this.TokenEditor.NavigateToVerticalEnd();
            return true;
        }

        private bool NavigateHome() => 
            (this.TokenPanel.Orientation != Orientation.Horizontal) ? this.NavigateHomeVertical() : this.NavigateHomeHorizontal();

        private bool NavigateHomeHorizontal() => 
            (!this.IsFirstTokenFocused() || !this.TokenEditor.ShowNewTokenFromEnd) ? (!this.HasActivatedToken ? this.TokenEditor.OnNavigateHorizontalStart(false) : (this.CanNavigateLeftInActiveMode() && this.TokenEditor.OnNavigateHorizontalStart(true))) : false;

        private bool NavigateHomeVertical()
        {
            this.TokenEditor.NavigateToVerticalStart();
            return true;
        }

        private bool NavigateLeft(ModifierKeys modifiers)
        {
            bool isShiftPressed = this.IsShiftPressed(modifiers);
            int index = (this.FocusedToken != null) ? (this.TokenPanel.GetVisibleIndex(this.FocusedToken) - 1) : this.TokenEditor.GetTokenByHorizontalOffset();
            return (this.NavigateLeftCore(index, isShiftPressed) | isShiftPressed);
        }

        private bool NavigateLeftCore(int index, bool isShiftPressed = false) => 
            (!this.IsFirstTokenFocused() || !this.TokenEditor.ShowNewTokenFromEnd) ? (!this.HasActivatedToken ? (isShiftPressed ? this.TokenEditor.SelectTokenOnNavigateLeft(index) : this.TokenEditor.OnNavigateLeft(index, false)) : (this.CanNavigateLeftInActiveMode() && this.TokenEditor.OnNavigateLeft(index, true))) : false;

        private bool NavigateLeftRight(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers();
            switch (e.Key)
            {
                case Key.End:
                    return (!this.IsRightToLeft() ? this.NavigateEnd() : this.NavigateHome());

                case Key.Home:
                    return (!this.IsRightToLeft() ? this.NavigateHome() : this.NavigateEnd());

                case Key.Left:
                    return (!this.IsRightToLeft() ? this.NavigateLeft(keyboardModifiers) : this.NavigateRight(keyboardModifiers));

                case Key.Right:
                    return (!this.IsRightToLeft() ? this.NavigateRight(keyboardModifiers) : this.NavigateLeft(keyboardModifiers));
            }
            return false;
        }

        private bool NavigateRight(ModifierKeys modifiers)
        {
            bool isShiftPressed = this.IsShiftPressed(modifiers);
            int index = (this.FocusedToken != null) ? (this.TokenPanel.GetVisibleIndex(this.FocusedToken) + 1) : this.TokenEditor.GetTokenByHorizontalOffset();
            return (this.NavigateRightCore(index, isShiftPressed) | isShiftPressed);
        }

        private bool NavigateRightCore(int index, bool isShiftPressed = false) => 
            (!this.IsLastTokenFocused() || this.TokenEditor.ShowNewTokenFromEnd) ? (!this.HasActivatedToken ? (isShiftPressed ? this.TokenEditor.SelectTokenOnNavigateRight(index) : this.TokenEditor.OnNavigateRight(index, false)) : ((index < (this.TokenPanel.Items.Count + 1)) && (this.CanNavigateRightInActiveMode() && this.TokenEditor.OnNavigateRight(index, true)))) : false;

        private bool NavigateUp(ModifierKeys modifiers)
        {
            if (!this.IsFocusedEditorInFirstLine())
            {
                this.TokenEditor.OnNavigateUp(this.IsShiftPressed(modifiers));
            }
            return true;
        }

        private bool NavigateUpDown(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers();
            Key key = e.Key;
            return ((key == Key.Up) ? this.NavigateUp(keyboardModifiers) : ((key == Key.Down) ? this.NavigateDown(keyboardModifiers) : false));
        }

        public bool NeedsKey(Key key, ModifierKeys modifiers) => 
            !this.IsCommitTokenKey(key) ? (!this.IsDeleteTokenKey(key) ? (!this.TokenEditor.IsInProcessNewValue ? (!this.CanNavigateToLine(key, modifiers) ? ((this.HasActivatedToken || (!this.IsActivatingKey(key, modifiers) || (!this.TokenEditor.CanActivateToken() || this.IsLeftRightKey(key)))) ? ((!this.TokenEditor.CanActivateToken() || !this.HasActivatedToken) ? (!this.IsShiftPressed(modifiers) ? (!this.IsLeftNavigationKey(key) ? (this.IsRightNavigationKey(key) && this.IsLastRightTokenNotFocused()) : this.IsFirstLeftTokenNotFocused()) : true) : this.NeedsNavigationKeyInEditableState(key, modifiers)) : true) : true) : false) : true) : true;

        private bool NeedsNavigationKeyInEditableState(Key key, ModifierKeys modifiers) => 
            ((key == Key.Left) || (key == Key.Home)) ? this.CanNavigateLeft() : (((key == Key.Right) || (key == Key.End)) ? this.CanNavigateRight() : false);

        internal void ProcessDeleteTokenKey(KeyEventArgs e)
        {
            e.Handled = true;
            if (this.TokenEditor.HasSelection)
            {
                this.TokenEditor.RemoveSelectedTokens();
            }
            else if (this.TokenEditor.IsNewTokenFocused())
            {
                int index = -1;
                if (((e.Key == Key.Back) && (this.TokenEditor.ShowNewTokenFromEnd && !this.IsRightToLeft())) || ((e.Key == Key.Delete) && (this.TokenEditor.ShowNewTokenFromEnd && this.IsRightToLeft())))
                {
                    index = this.TokenPanel.MaxVisibleIndex - 1;
                }
                else if (((e.Key == Key.Back) && (!this.TokenEditor.ShowNewTokenFromEnd && this.IsRightToLeft())) || ((e.Key == Key.Delete) && (!this.TokenEditor.ShowNewTokenFromEnd && !this.IsRightToLeft())))
                {
                    index = this.TokenPanel.MinVisibleIndex + 1;
                }
                if (index > -1)
                {
                    this.TokenEditor.RemoveToken(index);
                }
            }
        }

        public bool ProcessPreviewKeyDown(KeyEventArgs e)
        {
            if (this.IsCommitTokenKey(e.Key))
            {
                return !this.TokenEditor.ProcessCommitKey(e);
            }
            if (this.IsDeleteTokenKey(e.Key))
            {
                this.ProcessDeleteTokenKey(e);
                return e.Handled;
            }
            if (this.CanNavigateToLine(e.Key, ModifierKeysHelper.GetKeyboardModifiers()) && this.NavigateUpDown(e))
            {
                e.Handled = true;
            }
            else if (this.HasActivatedToken)
            {
                if (!this.TokenEditor.IsInProcessNewValue)
                {
                    bool flag = this.TokenEditor.IsNewTokenFocused();
                    int editableTokenIndex = this.TokenEditor.EditValueInternal.EditableTokenIndex;
                    e.Handled = this.NavigateLeftRight(e);
                    if ((e.Handled & flag) && !this.TokenEditor.IsInProcessNewValue)
                    {
                        this.TokenEditor.RemoveValueByIndex(editableTokenIndex);
                    }
                }
            }
            else
            {
                if (this.NavigateLeftRight(e))
                {
                    e.Handled = true;
                }
                if (!e.Handled && (this.TokenEditor.CanActivateToken() && (this.IsActivatingKey(e.Key, ModifierKeysHelper.GetKeyboardModifiers()) && (!this.IsUpOrDownKey(e.Key) && !this.IsLeftRightKey(e.Key)))))
                {
                    if (this.TokenEditor.ProcessTokenActivating(this.FocusedToken))
                    {
                        return true;
                    }
                    this.FocusedToken.ProcessActivatingKey(e);
                }
            }
            return e.Handled;
        }

        private DevExpress.Xpf.Editors.Internal.TokenEditor TokenEditor { get; set; }

        private TextEdit ActivatedToken =>
            this.TokenEditor.ActiveEditor;

        private TokenEditorPresenter FocusedToken =>
            this.TokenEditor.FocusedToken;

        private bool HasActivatedToken =>
            this.ActivatedToken != null;

        private TokenEditorPanel TokenPanel =>
            this.TokenEditor.GetTokenEditorPanel();
    }
}

