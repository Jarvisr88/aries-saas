namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public class ByIntDictionary<T> : IByIntDictionary
    {
        protected readonly int PageSize;
        protected ByIntDictionaryPage<T>[] Pages;

        public ByIntDictionary();
        public void Add(int index, object value);
        private int ComposeFromPageIndex(int pageIndex, int indexWithinPage);
        public bool ContainsKey(int index);
        public bool ContainsValue(object value);
        public int GetFirstFilledIndex(int startIndex, bool isBackward);
        private int GetFirstFilledIndexBackward(int startIndex);
        private int GetFirstFilledIndexForward(int startIndex);
        private void SplitToPage(int index, out int pageIndex, out int indexWithinPage);
        public bool TryGetKeyByValue(object value, out int index, int minIndex, int maxIndex);
        public bool TryGetValue(int index, out object value);
    }
}

