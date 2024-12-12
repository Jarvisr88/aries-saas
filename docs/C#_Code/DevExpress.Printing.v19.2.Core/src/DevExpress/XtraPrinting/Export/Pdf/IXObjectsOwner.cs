namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public interface IXObjectsOwner
    {
        void AddExistingXObject(PdfXObject xObject);
        void AddNewXObject(PdfXObject xObject);
        void AddPattern(PdfPattern pattern);
        void AddShading(PdfShading shading);
        void AddTransparencyGS(PdfTransparencyGS gs);
    }
}

