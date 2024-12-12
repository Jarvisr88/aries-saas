namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface ITrackBarBrick : IVisualBrick, IBaseBrick, IBrick
    {
        Color ForeColor { get; set; }

        int Position { get; set; }

        int Minimum { get; }

        int Maximum { get; }
    }
}

