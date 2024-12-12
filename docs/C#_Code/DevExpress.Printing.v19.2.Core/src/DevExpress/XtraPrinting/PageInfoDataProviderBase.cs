namespace DevExpress.XtraPrinting
{
    using System;

    public abstract class PageInfoDataProviderBase
    {
        protected PageInfoDataProviderBase()
        {
        }

        public abstract string GetText(PrintingSystemBase ps, PageInfoTextBrickBase brick);
    }
}

