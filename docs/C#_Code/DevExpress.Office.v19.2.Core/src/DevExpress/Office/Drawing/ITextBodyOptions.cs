namespace DevExpress.Office.Drawing
{
    using System;

    public interface ITextBodyOptions
    {
        bool HasRotation { get; }

        bool HasParagraphSpacing { get; }

        bool HasVerticalOverflow { get; }

        bool HasHorizontalOverflow { get; }

        bool HasVerticalText { get; }

        bool HasTextWrapping { get; }

        bool HasNumberOfColumns { get; }

        bool HasSpaceBetweenColumns { get; }

        bool HasRightToLeftColumns { get; }

        bool HasFromWordArt { get; }

        bool HasAnchor { get; }

        bool HasAnchorCenter { get; }

        bool HasForceAntiAlias { get; }

        bool HasCompatibleLineSpacing { get; }
    }
}

