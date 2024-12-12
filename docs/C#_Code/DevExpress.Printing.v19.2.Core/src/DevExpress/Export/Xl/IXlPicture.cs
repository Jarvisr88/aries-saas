namespace DevExpress.Export.Xl
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public interface IXlPicture : IDisposable
    {
        void FitToCell(XlCellPosition position, int cellWidth, int cellHeight, bool center);
        void SetAbsoluteAnchor(int x, int y, int width, int height);
        void SetOneCellAnchor(XlAnchorPoint topLeft, int width, int height);
        void SetTwoCellAnchor(XlAnchorPoint topLeft, XlAnchorPoint bottomRight, XlAnchorType behavior);
        void StretchToCell(XlCellPosition position);

        string Name { get; set; }

        System.Drawing.Image Image { get; set; }

        ImageFormat Format { get; set; }

        XlAnchorType AnchorType { get; set; }

        XlAnchorType AnchorBehavior { get; set; }

        XlAnchorPoint TopLeft { get; set; }

        XlAnchorPoint BottomRight { get; set; }

        XlPictureHyperlink HyperlinkClick { get; }

        XlSourceRectangle SourceRectangle { get; set; }
    }
}

