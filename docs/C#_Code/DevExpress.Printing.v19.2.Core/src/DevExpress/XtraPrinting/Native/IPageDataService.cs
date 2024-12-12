namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IPageDataService
    {
        void Clear();
        CustomPageData GetSource(ReadonlyPageData pageData);
        void SetSource(ReadonlyPageData pageData, CustomPageData source);
    }
}

