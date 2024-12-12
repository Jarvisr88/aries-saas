namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IPageBuildEngine
    {
        void Abort();
        void BuildPages(DocumentBand rootBand);
        int GetBuiltBandIndex(DocumentBand band);
        void Stop();

        bool Stopped { get; }
    }
}

