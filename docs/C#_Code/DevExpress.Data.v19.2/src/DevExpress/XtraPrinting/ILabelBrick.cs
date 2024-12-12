namespace DevExpress.XtraPrinting
{
    using System;

    public interface ILabelBrick : ITextBrick, IVisualBrick, IBaseBrick, IBrick
    {
        float Angle { get; set; }
    }
}

