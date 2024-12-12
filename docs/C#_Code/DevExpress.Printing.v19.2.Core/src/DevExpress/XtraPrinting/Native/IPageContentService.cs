namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IPageContentService
    {
        IPageContentAlgorithm GetAlgorithm(DocumentBand docBand);
        void OnBeforeBuildPages();
        void SetAlgorithm(DocumentBand docBand, IPageContentAlgorithm algorithm);
    }
}

