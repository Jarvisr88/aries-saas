namespace DevExpress.XtraPrinting.Export.Web
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ImageEventArgs : EventArgs
    {
        public ImageEventArgs(System.Drawing.Image image)
        {
            this.<Image>k__BackingField = image;
        }

        public System.Drawing.Image Image { get; }
    }
}

