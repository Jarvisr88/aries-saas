namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class ReplacementInfo
    {
        private readonly List<ReplacementItem> items = new List<ReplacementItem>();
        private int deltaLength;

        public void Add(int charIndex, string replaceWith)
        {
            this.items.Add(new ReplacementItem(charIndex, replaceWith));
            this.deltaLength += replaceWith.Length - 1;
        }

        public int DeltaLength =>
            this.deltaLength;

        public IList<ReplacementItem> Items =>
            this.items;
    }
}

