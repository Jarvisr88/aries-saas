namespace DMEWorks.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReportToolStripMenuItem : ToolStripMenuItem
    {
        private string _reportfilename;

        public ReportToolStripMenuItem()
        {
        }

        public ReportToolStripMenuItem(Image image) : base(image)
        {
        }

        public ReportToolStripMenuItem(string text) : base(text)
        {
        }

        public ReportToolStripMenuItem(string text, Image image) : base(text, image, (EventHandler) null)
        {
        }

        public ReportToolStripMenuItem(string text, Image image, ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
        {
        }

        public ReportToolStripMenuItem(string text, Image image, EventHandler onClick) : base(text, image, onClick)
        {
        }

        public ReportToolStripMenuItem(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
        {
        }

        public ReportToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys) : base(text, image, onClick, shortcutKeys)
        {
        }

        public string ReportFileName
        {
            get => 
                this._reportfilename;
            set => 
                this._reportfilename = value;
        }
    }
}

