namespace DevExpress.DocumentView
{
    using System;
    using System.Drawing;

    public interface IPageSettings
    {
        int LeftMargin { get; set; }

        int RightMargin { get; set; }

        int TopMargin { get; set; }

        int BottomMargin { get; set; }

        float LeftMarginF { get; set; }

        float RightMarginF { get; set; }

        float TopMarginF { get; set; }

        float BottomMarginF { get; set; }

        SizeF PageSize { get; }
    }
}

