namespace DevExpress.XtraPrinting
{
    using System;

    [Obsolete("This interface is now obsolete. You should use the IImageBrick interface instead.")]
    public interface IImageObjectBrick : IImageBrick, IVisualBrick, IBaseBrick, IBrick
    {
        int ImageIndex { get; set; }

        object Images { get; set; }
    }
}

