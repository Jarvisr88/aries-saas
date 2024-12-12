namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IProgressBarBrick : IVisualBrick, IBaseBrick, IBrick
    {
        Color ForeColor { get; set; }

        int Position { get; set; }
    }
}

