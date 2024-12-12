namespace DevExpress.Office.Export.Html
{
    using DevExpress.Office.Utils;
    using System;

    public interface IOfficeImageRepository : IDisposable
    {
        string GetImageSource(OfficeImage img, bool autoDisposeImage);
    }
}

