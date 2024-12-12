namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class PagesCache
    {
        private readonly int cacheSize;
        private FrameworkElement firstPage;
        private Dictionary<int, FrameworkElement> cachedPages;
        private Dictionary<int, DateTime> timeMarkers;

        public PagesCache(int cacheSize)
        {
            if (cacheSize <= 0)
            {
                throw new ArgumentException("cacheSize");
            }
            this.cacheSize = cacheSize;
            this.cachedPages = new Dictionary<int, FrameworkElement>();
            this.timeMarkers = new Dictionary<int, DateTime>();
        }

        public void AddPage(FrameworkElement page, int pageIndex)
        {
            if (pageIndex == 0)
            {
                this.firstPage = page;
            }
            else if (!this.cachedPages.ContainsKey(pageIndex))
            {
                if (this.cachedPages.Count >= (this.cacheSize - 1))
                {
                    this.RemovePageCore(this.GetDeprecatedPageIndex());
                }
                this.AddPageCore(page, pageIndex);
            }
        }

        private void AddPageCore(FrameworkElement page, int pageIndex)
        {
            this.cachedPages.Add(pageIndex, page);
            this.timeMarkers.Add(pageIndex, DateTime.Now);
        }

        public void Clear()
        {
            this.firstPage = null;
            this.cachedPages.Clear();
            this.timeMarkers.Clear();
        }

        private int GetDeprecatedPageIndex()
        {
            DateTime maxValue = DateTime.MaxValue;
            int key = -1;
            foreach (KeyValuePair<int, DateTime> pair in this.timeMarkers)
            {
                if (maxValue > pair.Value)
                {
                    maxValue = pair.Value;
                    key = pair.Key;
                }
            }
            return key;
        }

        public FrameworkElement GetPage(int pageIndex)
        {
            if (pageIndex == 0)
            {
                return this.firstPage;
            }
            if (!this.cachedPages.ContainsKey(pageIndex))
            {
                return null;
            }
            this.timeMarkers[pageIndex] = DateTime.Now;
            return this.cachedPages[pageIndex];
        }

        private void RemovePageCore(int pageIndex)
        {
            this.cachedPages.Remove(pageIndex);
            this.timeMarkers.Remove(pageIndex);
        }

        public int CacheSize =>
            this.cacheSize;
    }
}

