namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingTextParagraphPropertiesOptions
    {
        bool HasTextIndentLevel { get; }

        bool HasDefaultTabSize { get; }

        bool HasTextAlignment { get; }

        bool HasFontAlignment { get; }

        bool HasEastAsianLineBreak { get; }

        bool HasLatinLineBreak { get; }

        bool HasHangingPunctuation { get; }

        bool HasRightToLeft { get; }

        bool HasIndent { get; }

        bool HasLeftMargin { get; }

        bool HasRightMargin { get; }
    }
}

