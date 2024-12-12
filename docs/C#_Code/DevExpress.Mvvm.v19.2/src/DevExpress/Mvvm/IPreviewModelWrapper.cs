namespace DevExpress.Mvvm
{
    using System;

    public interface IPreviewModelWrapper : IDisposable
    {
        object PreviewModel { get; }
    }
}

