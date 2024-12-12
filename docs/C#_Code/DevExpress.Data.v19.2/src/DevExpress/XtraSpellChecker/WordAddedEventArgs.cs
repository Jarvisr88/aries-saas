namespace DevExpress.XtraSpellChecker
{
    using System;

    public class WordAddedEventArgs : EventArgs
    {
        private string word;

        public WordAddedEventArgs(string word)
        {
            this.word = word;
        }

        public string Word =>
            this.word;
    }
}

