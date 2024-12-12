namespace DevExpress.Pdf.Native
{
    using System;

    public enum PdfFontDirectionHint
    {
        FullyMixedDirectionalGlyphs = 0,
        OnlyStronglyLeftToRight = 1,
        OnlyStronglyLeftToRightButAlsoContainsNeutrals = 2,
        OnlyStronglyRightToLeft = -1,
        OnlyStronglyRightToLeftButAlsoContainsNeutrals = -2
    }
}

