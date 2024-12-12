namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.Drawing;

    public interface IRichTextBrick : IVisualBrick, IBaseBrick, IBrick
    {
        IList GetChildBricks();

        string RtfText { get; set; }

        bool CanShrinkAndGrow { get; set; }

        Font BaseFont { get; set; }

        int InfiniteHeight { get; }

        int EffectiveHeight { get; }
    }
}

