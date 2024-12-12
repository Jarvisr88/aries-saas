namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Controls;

    public class XBAPClipboardHelper : IClipboardHelper
    {
        private System.Windows.Controls.TextBox textBox;

        public bool ContainsText();
        public string GetText();
        public void SetDataFromClipboardDataProvider(IClipboardDataProvider сlipboardDataProvider);
        public void SetText(string text);

        private System.Windows.Controls.TextBox TextBox { get; }
    }
}

