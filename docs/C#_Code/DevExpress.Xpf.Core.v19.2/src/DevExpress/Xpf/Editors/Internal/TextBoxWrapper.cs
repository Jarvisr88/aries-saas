namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TextBoxWrapper : EditBoxWrapper
    {
        public const int MaxTextLengthDueTextBoxBugsWithWrapping = 0x3e8;
        private string editBlockTextForHighlighting;

        public TextBoxWrapper(TextEdit editor) : base(editor)
        {
        }

        public override void ClearUndoStack()
        {
            base.ClearUndoStack();
            if (this.EditBox != null)
            {
                try
                {
                    bool isUndoEnabled = this.EditBox.IsUndoEnabled;
                    this.EditBox.IsUndoEnabled = false;
                    this.EditBox.IsUndoEnabled = isUndoEnabled;
                }
                catch
                {
                }
            }
        }

        public override void Copy()
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                TextBox local1 = editBox;
            }
            else
            {
                editBox.Copy();
            }
        }

        public override void Cut()
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                TextBox local1 = editBox;
            }
            else
            {
                editBox.Cut();
            }
        }

        public override int GetCharacterIndexFromLineIndex(int lineIndex) => 
            (this.EditBox == null) ? -1 : this.EditBox.GetCharacterIndexFromLineIndex(lineIndex);

        public override int GetCharacterIndexFromPoint(Point point, bool snapToText)
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                return -1;
            }
            if (editBox.LineCount > 1)
            {
                return editBox.GetCharacterIndexFromPoint(point, snapToText);
            }
            Func<TextPointer, int> evaluator = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                Func<TextPointer, int> local1 = <>c.<>9__52_0;
                evaluator = <>c.<>9__52_0 = x => x.GetLineStartPosition(0).GetOffsetToPosition(x);
            }
            return GetTextPositionFromPointInternal(editBox, point, snapToText).Return<TextPointer, int>(evaluator, (<>c.<>9__52_1 ??= () => -1));
        }

        public override int GetFirstVisibleLineIndex() => 
            (this.EditBox == null) ? -1 : this.EditBox.GetFirstVisibleLineIndex();

        public override int GetLastVisibleLineIndex() => 
            (this.EditBox == null) ? -1 : this.EditBox.GetLastVisibleLineIndex();

        public override int GetLineIndexFromCharacterIndex(int charIndex) => 
            (this.EditBox == null) ? -1 : this.EditBox.GetLineIndexFromCharacterIndex(charIndex);

        public override int GetLineLength(int lineIndex) => 
            (this.EditBox == null) ? -1 : this.EditBox.GetLineLength(lineIndex);

        public override string GetLineText(int lineIndex)
        {
            TextBox editBox = this.EditBox;
            if (editBox != null)
            {
                return editBox.GetLineText(lineIndex);
            }
            TextBox local1 = editBox;
            return null;
        }

        private static TextPointer GetTextPositionFromPointInternal(TextBox editBox, Point point, bool snapToText)
        {
            if ((editBox == null) || !editBox.IsInVisualTree())
            {
                return null;
            }
            int? parametersCount = null;
            return EditBoxWrapper.ReflectionHelper.GetInstanceMethodHandler<Func<TextBox, Point, bool, TextPointer>>(editBox, "GetTextPositionFromPointInternal", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, editBox.GetType(), parametersCount, null, true)(editBox, point, snapToText);
        }

        public override void IsImeEnabled(bool value)
        {
            base.IsImeEnabled(value);
            if (this.EditBox != null)
            {
                InputMethod.SetIsInputMethodEnabled(this.EditBox, value);
            }
        }

        public override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            TextBoxHelper.NeedKey(this.Editor.EditCore as TextBox, key);

        public override void Paste()
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                TextBox local1 = editBox;
            }
            else
            {
                editBox.Paste();
            }
        }

        public override void ScrollToHome()
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                TextBox local1 = editBox;
            }
            else
            {
                editBox.ScrollToHome();
            }
        }

        public override void Select(int start, int length)
        {
            if ((this.EditBox != null) && ((this.EditBox.SelectionStart != start) || (this.EditBox.SelectionLength != length)))
            {
                this.EditBox.Select(start, length);
            }
        }

        public override void SelectAll()
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                TextBox local1 = editBox;
            }
            else
            {
                editBox.SelectAll();
            }
        }

        private void SetEditBlockText(string value, string highlightedText, HighlightedTextCriteria criteria)
        {
            if (this.SetupHighlighting())
            {
                this.editBlockTextForHighlighting = value;
                this.SetTextInfo(value, highlightedText, criteria);
            }
            else
            {
                this.editBlockTextForHighlighting = null;
                if (TextBlockService.GetTextInfo(this.EditBlock) != null)
                {
                    this.SetTextInfo(value, highlightedText, criteria);
                }
                else
                {
                    string str = string.IsNullOrEmpty(value) ? " " : value;
                    if ((this.EditBlock.TextWrapping == TextWrapping.NoWrap) && (this.EditBlock.TextTrimming != TextTrimming.None))
                    {
                        this.EditBlock.Text = (str.Length > 0x3e8) ? str.Substring(0, 0x3e8) : str;
                    }
                    else
                    {
                        this.EditBlock.Text = str;
                    }
                }
            }
        }

        protected override void SetHighlightedTextCriteriaInternal(HighlightedTextCriteria value)
        {
            base.SetHighlightedTextCriteriaInternal(value);
            this.UpdateHighlighting(value, base.HighlightedText);
        }

        protected override void SetHighlightedTextInternal(string value)
        {
            base.SetHighlightedTextInternal(value);
            this.UpdateHighlighting(base.HighlightedTextCriteria, value);
        }

        private void SetTextInfo(string value, string highlightedText, HighlightedTextCriteria criteria)
        {
            TextBlockInfo info1 = new TextBlockInfo();
            info1.Text = value;
            info1.HighlightedText = highlightedText;
            info1.HighlightedTextCriteria = criteria;
            TextBlockInfo info = info1;
            if (info != TextBlockService.GetTextInfo(this.EditBlock))
            {
                TextBlockService.SetTextInfo(this.EditBlock, info);
            }
        }

        private bool SetupHighlighting() => 
            !string.IsNullOrEmpty(base.HighlightedText) ? this.IsEditBlockRendered : false;

        public override void Undo()
        {
            TextBox editBox = this.EditBox;
            if (editBox == null)
            {
                TextBox local1 = editBox;
            }
            else
            {
                editBox.Undo();
            }
        }

        public override void UnselectAll()
        {
            if (this.EditBox != null)
            {
                this.EditBox.SelectedText = string.Empty;
            }
        }

        public override void UpdateHighlighting()
        {
            this.UpdateHighlighting(base.HighlightedTextCriteria, base.HighlightedText);
        }

        private void UpdateHighlighting(HighlightedTextCriteria criteria, string highlightedText)
        {
            if (this.EditBlock != null)
            {
                this.SetEditBlockText(string.IsNullOrEmpty(this.editBlockTextForHighlighting) ? this.EditBlock.Text : this.editBlockTextForHighlighting, highlightedText, criteria);
            }
        }

        private TextEdit Editor =>
            base.Editor as TextEdit;

        private TextBox EditBox =>
            this.EditCore as TextBox;

        private TextBlock EditBlock =>
            this.EditCore as TextBlock;

        public override bool CanUndo =>
            (this.EditBox != null) && this.EditBox.CanUndo;

        public override bool AllowDrop
        {
            get => 
                (this.EditBox == null) ? base.AllowDrop : this.EditBox.AllowDrop;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.AllowDrop = value;
                }
                else
                {
                    base.AllowDrop = value;
                }
            }
        }

        public override System.Windows.Controls.CharacterCasing CharacterCasing
        {
            get => 
                (this.EditBox == null) ? System.Windows.Controls.CharacterCasing.Normal : this.EditBox.CharacterCasing;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.CharacterCasing = value;
                }
            }
        }

        public override int LineCount =>
            (this.EditBox == null) ? (((this.EditBlock == null) || string.IsNullOrEmpty(this.EditBlock.Text)) ? 0 : this.EditBlock.Text.Split(Environment.NewLine.ToCharArray()).Length) : this.EditBox.LineCount;

        public override bool IsReadOnly
        {
            get => 
                (this.EditBox != null) && this.EditBox.IsReadOnly;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.IsReadOnly = value;
                }
            }
        }

        public override string Text =>
            (this.EditBox == null) ? ((this.EditBlock == null) ? string.Empty : this.EditBlock.Text) : this.EditBox.Text;

        public override object EditValue
        {
            get => 
                (this.EditBox == null) ? ((this.EditBlock == null) ? string.Empty : this.EditBlock.Text) : this.EditBox.Text;
            set
            {
                if ((this.EditBox != null) || (this.EditBlock != null))
                {
                    string str = (string) value;
                    if (this.EditBlock != null)
                    {
                        this.SetEditBlockText(str, base.HighlightedText, base.HighlightedTextCriteria);
                    }
                    else if ((this.EditBox != null) && ((this.EditBox.Text != str) && this.IsEditBoxRendered))
                    {
                        this.EditBox.Text = str;
                    }
                }
            }
        }

        public override Brush Foreground
        {
            get
            {
                if (this.EditBox != null)
                {
                    return this.EditBox.Foreground;
                }
                TextBlock editBlock = this.EditBlock;
                if (editBlock != null)
                {
                    return editBlock.Foreground;
                }
                TextBlock local1 = editBlock;
                return null;
            }
        }

        public override string SelectedText
        {
            get => 
                (this.EditBox != null) ? this.EditBox.SelectedText : string.Empty;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.SelectedText = (value != null) ? value : string.Empty;
                }
            }
        }

        public override int SelectionLength
        {
            get => 
                (this.EditBox == null) ? 0 : this.EditBox.SelectionLength;
            set
            {
                if ((this.EditBox != null) && (this.EditBox.SelectionLength != value))
                {
                    this.EditBox.SelectionLength = value;
                }
            }
        }

        public override int SelectionStart
        {
            get => 
                (this.EditBox == null) ? 0 : this.EditBox.SelectionStart;
            set
            {
                if ((this.EditBox != null) && (this.EditBox.SelectionStart != value))
                {
                    this.EditBox.SelectionStart = value;
                }
            }
        }

        public override int MaxLength
        {
            get => 
                (this.EditBox == null) ? -1 : this.EditBox.MaxLength;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.MaxLength = value;
                }
            }
        }

        public override int CaretIndex
        {
            get => 
                (this.EditBox == null) ? 0 : this.EditBox.CaretIndex;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.CaretIndex = value;
                }
            }
        }

        public override bool IsUndoEnabled
        {
            get => 
                (this.EditBox != null) && this.EditBox.IsUndoEnabled;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.IsUndoEnabled = value;
                }
            }
        }

        private bool IsEditBlockRendered
        {
            get
            {
                object dataContext;
                TextBlock editBlock = this.EditBlock;
                if (editBlock != null)
                {
                    dataContext = editBlock.DataContext;
                }
                else
                {
                    TextBlock local1 = editBlock;
                    dataContext = null;
                }
                return (dataContext != null);
            }
        }

        private bool IsEditBoxRendered =>
            (this.EditBox != null) ? ((this.Editor.EditMode != EditMode.InplaceInactive) || !this.Editor.IsEditorActive) : false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextBoxWrapper.<>c <>9 = new TextBoxWrapper.<>c();
            public static Func<TextPointer, int> <>9__52_0;
            public static Func<int> <>9__52_1;

            internal int <GetCharacterIndexFromPoint>b__52_0(TextPointer x) => 
                x.GetLineStartPosition(0).GetOffsetToPosition(x);

            internal int <GetCharacterIndexFromPoint>b__52_1() => 
                -1;
        }
    }
}

