namespace DevExpress.XtraPrinting.Drawing
{
    using System;

    public interface IStringImageSourceProvider
    {
        ImageSource GetImageSource(string id);
    }
}

