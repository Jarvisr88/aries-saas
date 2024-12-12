namespace DevExpress.XtraSpellChecker.Native
{
    using DevExpress.XtraSpellChecker.Parser;
    using System;

    public interface ISpellCheckTextControlController : ISpellCheckTextController, IDisposable
    {
        void HideSelection();
        bool IsSelectionVisible();
        void ScrollToCaretPos();
        void Select(Position start, Position finish);
        void ShowSelection();
        void UpdateText();

        string EditControlText { get; set; }

        Position SelectionStart { get; }

        Position SelectionFinish { get; }

        bool HasSelection { get; }

        bool IsReadOnly { get; }
    }
}

