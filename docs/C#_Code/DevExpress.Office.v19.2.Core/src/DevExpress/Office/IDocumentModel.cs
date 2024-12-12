namespace DevExpress.Office
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.History;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public interface IDocumentModel : IServiceProvider, IOfficeServiceContainer, IDisposable
    {
        void BeginUpdate();
        OfficeReferenceImage CreateImage(MemoryStreamBasedImage image);
        OfficeReferenceImage CreateImage(Image nativeImage);
        OfficeReferenceImage CreateImage(Stream stream);
        void EndUpdate();
        OfficeImage GetImageById(IUniqueImageId id);

        DevExpress.Office.Utils.UriBasedImageReplaceQueue UriBasedImageReplaceQueue { get; }

        DocumentModelUnitConverter UnitConverter { get; }

        DocumentModelUnitToLayoutUnitConverter ToDocumentLayoutUnitConverter { get; }

        DocumentLayoutUnitConverter LayoutUnitConverter { get; }

        DocumentHistory History { get; }

        IDocumentModelPart MainPart { get; }

        DevExpress.Office.Drawing.FontCache FontCache { get; }

        IDrawingCache DrawingCache { get; }

        IOfficeTheme OfficeTheme { get; set; }

        bool IsDisposed { get; }

        ImageCacheBase ImageCache { get; }
    }
}

