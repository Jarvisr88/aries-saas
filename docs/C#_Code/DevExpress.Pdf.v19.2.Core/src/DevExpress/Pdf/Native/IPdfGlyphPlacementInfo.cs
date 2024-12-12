namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public interface IPdfGlyphPlacementInfo
    {
        IList<IList<DXCluster>> GetClusters();

        string Text { get; }
    }
}

