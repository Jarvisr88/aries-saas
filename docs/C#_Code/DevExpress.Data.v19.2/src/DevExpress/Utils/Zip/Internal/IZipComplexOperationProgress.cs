namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public interface IZipComplexOperationProgress : IZipOperationProgress
    {
        void AddOperationProgress(IZipOperationProgress progressItem);
    }
}

