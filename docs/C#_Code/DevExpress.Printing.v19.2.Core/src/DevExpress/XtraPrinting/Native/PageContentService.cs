namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    internal class PageContentService : IPageContentService, IDisposable
    {
        private Dictionary<DocumentBand, IPageContentAlgorithm> dictiaonary = new Dictionary<DocumentBand, IPageContentAlgorithm>();

        public IPageContentAlgorithm GetAlgorithm(DocumentBand docBand)
        {
            IPageContentAlgorithm algorithm;
            return (this.dictiaonary.TryGetValue(docBand, out algorithm) ? algorithm : null);
        }

        public void OnBeforeBuildPages()
        {
            foreach (IPageContentAlgorithm algorithm in this.dictiaonary.Values)
            {
                algorithm.OnBeforeBuildPages();
            }
        }

        public void SetAlgorithm(DocumentBand docBand, IPageContentAlgorithm algorithm)
        {
            this.dictiaonary[docBand] = algorithm;
        }

        void IDisposable.Dispose()
        {
            this.dictiaonary.Clear();
        }
    }
}

