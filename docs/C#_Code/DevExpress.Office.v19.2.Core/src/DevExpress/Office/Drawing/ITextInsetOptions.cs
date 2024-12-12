namespace DevExpress.Office.Drawing
{
    using System;

    public interface ITextInsetOptions
    {
        bool HasLeft { get; }

        bool HasRight { get; }

        bool HasTop { get; }

        bool HasBottom { get; }
    }
}

