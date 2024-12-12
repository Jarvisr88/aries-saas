namespace DevExpress.XtraPrinting.Export.Web
{
    using System;

    [Obsolete("Use the INavigationService interface instead.")]
    internal interface IBookmarkService
    {
        string GetNavigationScript(int pageIndex, int[] birckIndices);
    }
}

