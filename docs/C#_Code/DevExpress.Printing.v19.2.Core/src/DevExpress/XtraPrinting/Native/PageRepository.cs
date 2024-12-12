namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class PageRepository : IPageRepository
    {
        private IList<Page> pages;

        public PageRepository(IList<Page> pages)
        {
            this.pages = pages;
        }

        bool IPageRepository.TryGetPageByID(long id, out Page page, out int index)
        {
            for (int i = 0; i < this.pages.Count; i++)
            {
                if (id == this.pages[i].ID)
                {
                    index = i;
                    page = this.pages[i];
                    return true;
                }
            }
            index = -1;
            page = null;
            return false;
        }

        bool IPageRepository.TryGetPageByIndex(int index, out Page page)
        {
            if ((index >= 0) && (index < this.pages.Count))
            {
                page = this.pages[index];
                return true;
            }
            page = null;
            return false;
        }
    }
}

