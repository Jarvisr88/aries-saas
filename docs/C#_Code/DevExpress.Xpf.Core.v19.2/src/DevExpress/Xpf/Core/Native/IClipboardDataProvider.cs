namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IClipboardDataProvider
    {
        object GetObjectFromClipboard();
        string GetTextFromClipboard();
    }
}

