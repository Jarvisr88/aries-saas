namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class Word
    {
        private string text;
        private int leadSpaceCount;

        public Word(string text);
        public Word(string text, int leadSpaceCount);
        public void DecrementLeadSpaceCount();
        public override bool Equals(object obj);
        public override int GetHashCode();
        public void IncrementLeadSpaceCount();

        public string Text { get; }

        public string TextWithSpaces { get; }

        public int LeadSpaceCount { get; }

        public bool SpacesOnly { get; }
    }
}

