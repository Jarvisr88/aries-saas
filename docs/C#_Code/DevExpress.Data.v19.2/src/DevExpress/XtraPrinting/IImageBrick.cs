namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IImageBrick : IVisualBrick, IBaseBrick, IBrick
    {
        System.Drawing.Image Image { get; set; }

        ImageSizeMode SizeMode { get; set; }
    }
}

