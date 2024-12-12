namespace DevExpress.XtraPrinting
{
    using System;

    public interface IPageBrick
    {
        BrickAlignment Alignment { get; set; }

        BrickAlignment LineAlignment { get; set; }
    }
}

