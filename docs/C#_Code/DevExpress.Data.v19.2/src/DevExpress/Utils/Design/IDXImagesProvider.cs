namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public interface IDXImagesProvider
    {
        IDXImageInfo[] GetAllImages();
        string[] GetBaseImages();
        string GetFile(string name, ImageSize imageSize = 0, ImageType imageType = 1);
        IEnumerable<string> GetFiles(string name);
        Image GetImage(string id, ImageSize imageSize = 0, ImageType imageType = 1);
        Image GetImageByPath(string path);
        bool IsBrowsable(string key);
        bool IsDevAVImage(string key);
        bool IsGrayScaledImage(string key);
        bool IsOffice2013Image(string key);
    }
}

