namespace DevExpress.XtraPrinting.XamlExport
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ImageResolveEventArgs : EventArgs
    {
        public ImageResolveEventArgs(System.Drawing.Image image)
        {
            this.Image = image;
        }

        public string Uri { get; set; }

        public System.Drawing.Image Image { get; private set; }
    }
}

