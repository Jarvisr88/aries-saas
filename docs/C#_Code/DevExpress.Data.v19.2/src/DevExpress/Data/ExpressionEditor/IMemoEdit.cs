namespace DevExpress.Data.ExpressionEditor
{
    using System;

    public interface IMemoEdit
    {
        void Focus();
        int GetLineLength(int lineIndex);
        void Paste(string text);

        int SelectionStart { get; set; }

        int SelectionLength { get; set; }

        string Text { get; set; }

        string SelectedText { get; }
    }
}

