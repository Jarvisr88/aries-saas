namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;

    public interface IPageRepository
    {
        bool TryGetPageByID(long id, out Page page, out int index);
        bool TryGetPageByIndex(int index, out Page page);
    }
}

