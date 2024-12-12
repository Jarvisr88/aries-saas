namespace DevExpress.XtraSpellChecker.Parser
{
    using System;

    public interface IUndoItem
    {
        void DoUndo();

        Position StartPosition { get; set; }

        Position FinishPosition { get; set; }

        string OldText { get; set; }

        bool NeedRecheckWord { get; }

        bool ShouldUpdateItemPosition { get; }
    }
}

