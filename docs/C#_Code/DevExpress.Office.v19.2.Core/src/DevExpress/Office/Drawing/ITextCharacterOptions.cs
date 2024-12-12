namespace DevExpress.Office.Drawing
{
    using System;

    public interface ITextCharacterOptions
    {
        bool HasKumimoji { get; }

        bool HasFontSize { get; }

        bool HasBold { get; }

        bool HasItalic { get; }

        bool HasUnderline { get; }

        bool HasStrikethrough { get; }

        bool HasKerning { get; }

        bool HasCaps { get; }

        bool HasSpacing { get; }

        bool HasNormalizeHeight { get; }

        bool HasBaseline { get; }

        bool HasNoProofing { get; }
    }
}

