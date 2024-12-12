namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class EditorTextSearchHelper
    {
        public EditorTextSearchHelper(ISelectorEditStrategy editStrategy)
        {
            this.EditStrategy = editStrategy;
        }

        public void CancelTextSearch()
        {
            this.SearchEngine.CancelSearch();
        }

        public bool DoTextSearch(string text, int startIndex, ref object result)
        {
            if (this.SearchEngine.DoSearch(text, startIndex))
            {
                result = this.ItemsProvider.GetValueByIndex(this.SearchEngine.MatchedItemIndex, this.Handle);
            }
            return (result != null);
        }

        public object FindValueFromSearchText(int startIndex, bool isDown, bool isCaseSensitiveSearch)
        {
            int index = this.ItemsProvider.FindItemIndexByText(this.SearchEngine.SeachText, isCaseSensitiveSearch, true, this.Handle, startIndex, isDown, true);
            return ((index != -1) ? this.ItemsProvider.GetValueByIndex(index, this.Handle) : null);
        }

        private object Handle =>
            this.EditStrategy.CurrentDataViewHandle;

        private IItemsProvider2 ItemsProvider =>
            this.EditStrategy.ItemsProvider;

        private TextSearchEngine SearchEngine =>
            this.EditStrategy.TextSearchEngine;

        private ISelectorEditStrategy EditStrategy { get; set; }
    }
}

