namespace DMEWorks.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ShellLinkToolStripMenuItem : ToolStripMenuItem
    {
        private string _path;

        public ShellLinkToolStripMenuItem()
        {
        }

        public ShellLinkToolStripMenuItem(Image image) : base(image)
        {
        }

        public ShellLinkToolStripMenuItem(string text) : base(text)
        {
        }

        public ShellLinkToolStripMenuItem(string text, Image image) : base(text, image, (EventHandler) null)
        {
        }

        public ShellLinkToolStripMenuItem(string text, Image image, ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
        {
        }

        public ShellLinkToolStripMenuItem(string text, Image image, EventHandler onClick) : base(text, image, onClick)
        {
        }

        public ShellLinkToolStripMenuItem(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
        {
        }

        public ShellLinkToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys) : base(text, image, onClick, shortcutKeys)
        {
        }

        public string Path
        {
            get => 
                this._path;
            set => 
                this._path = value;
        }
    }
}

