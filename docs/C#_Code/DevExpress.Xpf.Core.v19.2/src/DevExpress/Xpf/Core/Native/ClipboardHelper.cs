namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class ClipboardHelper : IClipboardHelper
    {
        public bool ContainsText();
        public string GetText();
        public void SetDataFromClipboardDataProvider(IClipboardDataProvider сlipboardDataProvider);
        public void SetText(string text);
        private static void WorkaroundClipboardBugs(Action copyAction);
    }
}

