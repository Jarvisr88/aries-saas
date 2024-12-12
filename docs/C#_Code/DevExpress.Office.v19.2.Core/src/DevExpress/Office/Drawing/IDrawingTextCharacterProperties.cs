namespace DevExpress.Office.Drawing
{
    using System;
    using System.Globalization;

    internal interface IDrawingTextCharacterProperties
    {
        ContainerEffect Effects { get; }

        int Baseline { get; }

        DrawingTextUnderlineType Underline { get; }

        IDrawingFill Fill { get; }

        IUnderlineFill UnderlineFill { get; }

        DevExpress.Office.Drawing.Outline Outline { get; }

        DrawingTextStrikeType Strikethrough { get; }

        int Spacing { get; }

        bool NormalizeHeight { get; }

        bool Bold { get; }

        CultureInfo Language { get; }

        DrawingTextFont Latin { get; }

        ITextCharacterOptions Options { get; }

        DrawingTextCapsType Caps { get; }

        bool Italic { get; }

        int FontSize { get; }
    }
}

