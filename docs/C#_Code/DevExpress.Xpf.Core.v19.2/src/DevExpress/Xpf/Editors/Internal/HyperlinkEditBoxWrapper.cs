namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class HyperlinkEditBoxWrapper : EditBoxWrapper
    {
        public const int MaxTextLengthDueTextBoxBugsWithWrapping = 0x3e8;
        private string editBlockTextForHighlighting;

        public HyperlinkEditBoxWrapper(HyperlinkEdit editor) : base(editor)
        {
        }

        public override void Copy()
        {
        }

        public override void Cut()
        {
        }

        public override int GetCharacterIndexFromLineIndex(int lineIndex) => 
            -1;

        public override int GetCharacterIndexFromPoint(Point point, bool snapToText) => 
            -1;

        public override int GetFirstVisibleLineIndex() => 
            -1;

        public override int GetLastVisibleLineIndex() => 
            -1;

        public override int GetLineIndexFromCharacterIndex(int charIndex) => 
            -1;

        public override int GetLineLength(int lineIndex) => 
            -1;

        public override string GetLineText(int lineIndex) => 
            null;

        public override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            TextBoxHelper.NeedKey(this.Editor.EditCore as TextBox, key);

        public override void Paste()
        {
        }

        public override void ScrollToHome()
        {
        }

        public override void Select(int start, int length)
        {
        }

        public override void SelectAll()
        {
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
                    if (this.EditBlock.TextWrapping == TextWrapping.NoWrap)
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
        }

        public override void UnselectAll()
        {
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

        private HyperlinkEdit Editor =>
            base.Editor as HyperlinkEdit;

        private TextBlock EditBlock =>
            this.EditCore as TextBlock;

        public override bool CanUndo =>
            false;

        public override int LineCount =>
            ((this.EditBlock == null) || string.IsNullOrEmpty(this.EditBlock.Text)) ? 0 : this.EditBlock.Text.Split(Environment.NewLine.ToCharArray()).Length;

        public override bool IsReadOnly
        {
            get => 
                true;
            set
            {
            }
        }

        public override string Text =>
            (this.EditBlock == null) ? string.Empty : this.EditBlock.Text;

        public override object EditValue
        {
            get => 
                (this.EditBlock == null) ? string.Empty : this.EditBlock.Text;
            set
            {
                if (this.EditBlock != null)
                {
                    string str = (string) value;
                    if (this.EditBlock != null)
                    {
                        this.SetEditBlockText(str, base.HighlightedText, base.HighlightedTextCriteria);
                    }
                }
            }
        }

        public override Brush Foreground =>
            this.EditBlock?.Foreground;

        public override string SelectedText
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        public override int SelectionLength
        {
            get => 
                0;
            set
            {
            }
        }

        public override int SelectionStart
        {
            get => 
                0;
            set
            {
            }
        }

        public override int MaxLength
        {
            get => 
                -1;
            set
            {
            }
        }

        public override int CaretIndex
        {
            get => 
                0;
            set
            {
            }
        }

        public override bool IsUndoEnabled
        {
            get => 
                false;
            set
            {
            }
        }

        private bool IsEditBlockRendered =>
            (this.EditBlock != null) ? (this.EditBlock.DataContext != null) : false;

        public override System.Windows.Controls.CharacterCasing CharacterCasing { get; set; }
    }
}

