namespace DevExpress.XtraPrinting
{
    using System;

    public interface IPageInfoFormatProvider
    {
        string Format { get; set; }

        DevExpress.XtraPrinting.PageInfo PageInfo { get; set; }

        int StartPageNumber { get; set; }
    }
}

