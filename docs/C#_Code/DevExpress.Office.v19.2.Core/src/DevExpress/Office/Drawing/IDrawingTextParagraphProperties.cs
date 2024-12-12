namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingTextParagraphProperties : IDrawingTextSpacingsChanger, IDrawingTextSpacings, IDrawingTextMargin, IDrawingTextBullets, IDrawingTextParagraphPropertiesOptions
    {
        IDrawingTextSpacings Spacings { get; }

        IDrawingTextMargin Margin { get; }

        IDrawingTextBullets Bullets { get; }

        DrawingTextCharacterProperties DefaultCharacterProperties { get; }

        bool ApplyDefaultCharacterProperties { get; set; }

        DrawingTextAlignmentType TextAlignment { get; set; }

        DrawingFontAlignmentType FontAlignment { get; set; }

        int TextIndentLevel { get; set; }

        float DefaultTabSize { get; set; }

        float Indent { get; set; }

        bool RightToLeft { get; set; }

        bool HangingPunctuation { get; set; }

        bool EastAsianLineBreak { get; set; }

        bool LatinLineBreak { get; set; }

        DrawingTextTabStopCollection TabStopList { get; }

        IDrawingTextParagraphPropertiesOptions Options { get; }
    }
}

