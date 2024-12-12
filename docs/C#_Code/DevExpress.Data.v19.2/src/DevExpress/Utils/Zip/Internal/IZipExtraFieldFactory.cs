namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public interface IZipExtraFieldFactory
    {
        IZipExtraField Create(int headerId);
    }
}

