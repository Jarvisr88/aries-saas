namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;

    public class CachingPageList : PageList
    {
        public CachingPageList(Document document) : base(document)
        {
        }

        public CachingPageList(Document document, IList<Page> list) : base(document, list)
        {
        }

        protected override void ValidatePage(Page page)
        {
        }
    }
}

