namespace DevExpress.Office.Model
{
    using System;

    public class UndoableCollectionMoveEventArgs : EventArgs
    {
        private readonly int sourceIndex;
        private readonly int targetIndex;

        public UndoableCollectionMoveEventArgs(int sourceIndex, int targetIndex)
        {
            this.sourceIndex = sourceIndex;
            this.targetIndex = targetIndex;
        }

        public int SourceIndex =>
            this.sourceIndex;

        public int TargetIndex =>
            this.targetIndex;
    }
}

