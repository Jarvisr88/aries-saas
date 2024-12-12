namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class PageBreakInfo
    {
        public PageBreakInfo(TemplatedLink link)
        {
            this.Link = link;
        }

        public bool PageBreakAfter { get; set; }

        public bool PageBreakBefore { get; set; }

        public TemplatedLink Link { get; private set; }
    }
}

