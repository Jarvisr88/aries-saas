namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ToolTipControlInfo
    {
        public ToolTipControlInfo()
        {
        }

        public ToolTipControlInfo(object obj) : this(obj, null, null)
        {
        }

        public ToolTipControlInfo(object obj, string text) : this(obj, text, null)
        {
        }

        public ToolTipControlInfo(object obj, string text, string footer)
        {
            this.Object = obj;
            this.Text = text;
            this.Footer = footer;
        }

        public string Header { get; set; }

        public string Text { get; set; }

        public string Footer { get; set; }

        public object Object { get; set; }

        public Point Position { get; set; }
    }
}

