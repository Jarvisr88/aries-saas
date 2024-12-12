namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public abstract class XPageContentEngine
    {
        protected XPageContentEngine();
        public static XPageContentEngine CreateInstance(bool smartXDivision);
        public abstract List<PSPage> CreatePages(PSPage source, RectangleF usefulArea);
    }
}

