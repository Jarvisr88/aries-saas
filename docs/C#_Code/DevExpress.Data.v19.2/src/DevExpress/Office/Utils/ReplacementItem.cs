namespace DevExpress.Office.Utils
{
    using System;

    public class ReplacementItem
    {
        private readonly int charIndex;
        private readonly string replaceWith;

        public ReplacementItem(int charIndex, string replaceWith)
        {
            this.charIndex = charIndex;
            this.replaceWith = replaceWith;
        }

        public int CharIndex =>
            this.charIndex;

        public string ReplaceWith =>
            this.replaceWith;
    }
}

