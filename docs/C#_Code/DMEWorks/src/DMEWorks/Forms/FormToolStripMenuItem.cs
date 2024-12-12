namespace DMEWorks.Forms
{
    using DMEWorks.Core;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormToolStripMenuItem : ToolStripMenuItem
    {
        private FormFactory F_Factory;
        private bool F_Modal;
        private FormParameters F_Parameters;

        public FormToolStripMenuItem()
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(Image image) : base(image)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(string text) : base(text)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(string text, Image image) : base(text, image, (EventHandler) null)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(string text, Image image, ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(string text, Image image, EventHandler onClick) : base(text, image, onClick)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        public FormToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys) : base(text, image, onClick, shortcutKeys)
        {
            this.F_Factory = null;
            this.F_Modal = false;
            this.F_Parameters = new FormParameters();
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), DefaultValue(null)]
        public FormFactory Factory
        {
            get => 
                this.F_Factory;
            set => 
                this.F_Factory = value;
        }

        [DefaultValue(false)]
        public bool Modal
        {
            get => 
                this.F_Modal;
            set => 
                this.F_Modal = value;
        }

        public FormParameters Parameters =>
            this.F_Parameters;
    }
}

