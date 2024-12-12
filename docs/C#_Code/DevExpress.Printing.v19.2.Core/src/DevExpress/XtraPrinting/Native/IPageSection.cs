namespace DevExpress.XtraPrinting.Native
{
    using System;

    public interface IPageSection
    {
        DocumentBand GetBottomMargin();
        DocumentBand GetTopMargin();

        int ID { get; }

        DocumentBand Container { get; }
    }
}

