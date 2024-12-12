namespace DevExpress.Office.Drawing
{
    using System;

    public interface ITextInset : ITextInsetOptions
    {
        ITextInsetOptions Options { get; }

        float Left { get; set; }

        float Right { get; set; }

        float Top { get; set; }

        float Bottom { get; set; }
    }
}

