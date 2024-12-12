namespace DevExpress.XtraPrinting
{
    using DevExpress.Emf;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public interface IPdfGraphics : IGraphics, IGraphicsBase, IPrintingSystemContext, IServiceProvider, IDisposable
    {
        void AddCheckFormField(CheckEditingField checkEditingField, RectangleF rect);
        void AddCombTextFormField(TextEditingField textEditingField, RectangleF rect, string text, int length);
        void AddDeferredDestination(string destinationName, int pageIndex, float destinationTop);
        void AddOutlineEntries(BookmarkNode bmNode, int[] pageIndices);
        void AddPage(SizeF pageSize, int pageIndex = 0x7fffffff);
        void AddSignatureForm(RectangleF rect);
        void AddTextFormField(TextEditingField textEditingField, RectangleF rect);
        void DrawImage(EmfMetafile image, Image bitmap, RectangleF rect, Color underlyingColor);
        void Flush();
        void FlushPageContent();
        void SetDeferredGoToArea(RectangleF bounds, out string destinadionName);
        void SetGoToArea(int destPageIndex, float destTop, RectangleF bounds);
        void SetUriArea(string uri, RectangleF bounds);

        bool AcroFormSupported { get; }
    }
}

