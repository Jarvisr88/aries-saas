namespace DevExpress.Utils.Design
{
    using System;

    public interface IDXImageInfo
    {
        string MakeUri();

        DevExpress.Utils.Design.ImageType ImageType { get; }

        string Name { get; }

        string Group { get; }
    }
}

