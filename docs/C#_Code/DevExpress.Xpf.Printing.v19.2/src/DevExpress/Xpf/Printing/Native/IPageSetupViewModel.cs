namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;

    public interface IPageSetupViewModel
    {
        IEnumerable<PaperSize> PaperSizes { get; set; }

        System.Drawing.Printing.PaperKind PaperKind { get; set; }

        float PaperWidth { get; set; }

        float PaperHeight { get; set; }

        GraphicsUnit Unit { get; set; }

        bool Landscape { get; set; }

        float LeftMargin { get; set; }

        float RightMargin { get; set; }

        float TopMargin { get; set; }

        float BottomMargin { get; set; }

        bool EnableUnitsEditor { get; set; }
    }
}

