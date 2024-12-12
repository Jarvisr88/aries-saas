namespace DevExpress.XtraSpellChecker.Parser
{
    public interface IUndoController
    {
        IUndoItem GetUndoItemForDelete();
        IUndoItem GetUndoItemForIgnore();
        IUndoItem GetUndoItemForIgnoreAll();
        IUndoItem GetUndoItemForReplace();
        IUndoItem GetUndoItemForSilentReplace();
    }
}

