namespace DevExpress.XtraSpellChecker
{
    using DevExpress.XtraSpellChecker.Parser;
    using System;

    public class AfterCheckWordEventArgs : EventArgs
    {
        private object editControl;
        private string originalWord;
        private string changedWord;
        private SpellCheckOperation operation;
        private Position startPosition;

        public AfterCheckWordEventArgs(object editControl, string originalWord, string changedWord, SpellCheckOperation result, Position startPosition)
        {
            this.editControl = editControl;
            this.changedWord = changedWord;
            this.originalWord = originalWord;
            this.operation = result;
            this.startPosition = startPosition;
        }

        public object EditControl =>
            this.editControl;

        public string OriginalWord =>
            this.originalWord;

        public string ChangedWord =>
            this.changedWord;

        public SpellCheckOperation Operation =>
            this.operation;

        public Position StartPosition =>
            this.startPosition;
    }
}

