namespace DevExpress.XtraPrinting.Export.Web
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public interface IImageRepository : IDisposable
    {
        event ImageEventHandler RequestImageSource;

        string GetImageSource(Image img, bool autoDisposeImage);
    }
}

