namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class OwnerDrawContextMenu : ContextMenu
    {
        private WindowsColorScheme #w2d;
        private System.Windows.Forms.ImageList #sZd;

        public OwnerDrawContextMenu()
        {
        }

        public OwnerDrawContextMenu(System.Windows.Forms.ImageList imageList)
        {
            this.#sZd = imageList;
        }

        internal WindowsColorScheme ColorSchemeResolved =>
            (this.#w2d == null) ? WindowsColorScheme.WindowsDefault : this.#w2d;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WindowsColorScheme ColorScheme
        {
            get => 
                this.#w2d;
            set => 
                this.#w2d = value;
        }

        [Category("Behavior"), Description("The ImageList control used by the control."), DefaultValue((string) null)]
        public System.Windows.Forms.ImageList ImageList
        {
            get => 
                this.#sZd;
            set => 
                this.#sZd = value;
        }
    }
}

