namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IClipboardHelper
    {
        bool ContainsText();
        string GetText();
        void SetDataFromClipboardDataProvider(IClipboardDataProvider сlipboardDataProvider);
        void SetText(string text);
    }
}

