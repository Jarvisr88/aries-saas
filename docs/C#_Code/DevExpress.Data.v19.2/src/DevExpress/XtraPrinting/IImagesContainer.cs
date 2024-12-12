namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IImagesContainer
    {
        bool ContainsImage(object key);
        Image GetImage(object key, Image image);
        Image GetImageByKey(object key);
    }
}

