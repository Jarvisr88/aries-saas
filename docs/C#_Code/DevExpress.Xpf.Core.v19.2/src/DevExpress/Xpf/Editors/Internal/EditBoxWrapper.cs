namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class EditBoxWrapper
    {
        protected static readonly DevExpress.Xpf.Core.Internal.ReflectionHelper ReflectionHelper = new DevExpress.Xpf.Core.Internal.ReflectionHelper();
        private string highlightedText = string.Empty;
        private DevExpress.Xpf.Editors.HighlightedTextCriteria highlightedTextCriteria;
        private bool showHighlightedText;

        protected EditBoxWrapper(BaseEdit editor)
        {
            this.Editor = editor;
        }

        public void AddCommandBinding(CommandBinding binding)
        {
            FrameworkElement editCore = this.EditCore;
            if (editCore == null)
            {
                FrameworkElement local1 = editCore;
            }
            else
            {
                editCore.CommandBindings.Add(binding);
            }
        }

        public void AddPreviewExecutedHandler(ExecutedRoutedEventHandler handler)
        {
            CommandManager.AddPreviewExecutedHandler(this.Editor, handler);
        }

        public virtual void AfterAcceptPopupValue()
        {
        }

        public virtual void BeforeAcceptPopupValue()
        {
        }

        public virtual void ClearUndoStack()
        {
        }

        public void ClearValue(DependencyProperty dependencyProperty)
        {
            FrameworkElement editCore = this.EditCore;
            if (editCore == null)
            {
                FrameworkElement local1 = editCore;
            }
            else
            {
                editCore.ClearValue(dependencyProperty);
            }
        }

        public abstract void Copy();
        public abstract void Cut();
        public void ExecuteCommand(RoutedCommand command, object parameter)
        {
            command.Execute(parameter, this.Editor);
        }

        public abstract int GetCharacterIndexFromLineIndex(int lineIndex);
        public abstract int GetCharacterIndexFromPoint(Point point, bool snapToText);
        public abstract int GetFirstVisibleLineIndex();
        public virtual bool GetIsInImeInput() => 
            false;

        public abstract int GetLastVisibleLineIndex();
        public abstract int GetLineIndexFromCharacterIndex(int charIndex);
        public abstract int GetLineLength(int lineIndex);
        public abstract string GetLineText(int lineIndex);
        public virtual void IsImeEnabled(bool value)
        {
        }

        public virtual bool NeedsEnterKey() => 
            false;

        public abstract bool NeedsKey(Key key, ModifierKeys modifiers);
        public virtual bool NeedsNavigationKey(Key key, ModifierKeys modifiers) => 
            false;

        public virtual void OnEditorPreviewLostFocus(bool isLostFocus)
        {
        }

        public virtual void OnRestoreDisplayText()
        {
        }

        public abstract void Paste();
        public virtual bool ProccessKeyDown(KeyEventArgs e) => 
            false;

        public void RemoveCommandBinding(CommandBinding binding)
        {
            FrameworkElement editCore = this.EditCore;
            if (editCore == null)
            {
                FrameworkElement local1 = editCore;
            }
            else
            {
                editCore.CommandBindings.Remove(binding);
            }
        }

        public void RemovePreviewExecutedHandler(ExecutedRoutedEventHandler handler)
        {
            CommandManager.RemovePreviewExecutedHandler(this.Editor, handler);
        }

        public abstract void ScrollToHome();
        public abstract void Select(int start, int length);
        public abstract void SelectAll();
        protected virtual void SetHighlightedTextCriteriaInternal(DevExpress.Xpf.Editors.HighlightedTextCriteria value)
        {
        }

        protected virtual void SetHighlightedTextInternal(string value)
        {
        }

        protected virtual void SetShowHighlightedTextInternal(bool value)
        {
        }

        public virtual void SyncWithValue(UpdateEditorSource updateSource)
        {
        }

        public abstract void Undo();
        public abstract void UnselectAll();
        public virtual void UpdateHighlighting()
        {
        }

        public virtual void UpdateIsTextTrimming()
        {
        }

        protected BaseEdit Editor { get; private set; }

        protected virtual FrameworkElement EditCore =>
            this.Editor.EditCore;

        public virtual bool AllowDrop { get; set; }

        public bool ShowHighlightedText
        {
            get => 
                this.showHighlightedText;
            set
            {
                this.showHighlightedText = value;
                this.SetShowHighlightedTextInternal(value);
            }
        }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                this.highlightedTextCriteria;
            set
            {
                this.highlightedTextCriteria = value;
                this.SetHighlightedTextCriteriaInternal(value);
            }
        }

        public string HighlightedText
        {
            get => 
                this.highlightedText;
            set
            {
                this.highlightedText = value;
                this.SetHighlightedTextInternal(value);
            }
        }

        public abstract Brush Foreground { get; }

        public abstract int LineCount { get; }

        public abstract string SelectedText { get; set; }

        public abstract int SelectionLength { get; set; }

        public abstract int SelectionStart { get; set; }

        public abstract int CaretIndex { get; set; }

        public abstract string Text { get; }

        public abstract object EditValue { get; set; }

        public abstract bool CanUndo { get; }

        public abstract int MaxLength { get; set; }

        public abstract System.Windows.Controls.CharacterCasing CharacterCasing { get; set; }

        public abstract bool IsReadOnly { get; set; }

        public abstract bool IsUndoEnabled { get; set; }
    }
}

