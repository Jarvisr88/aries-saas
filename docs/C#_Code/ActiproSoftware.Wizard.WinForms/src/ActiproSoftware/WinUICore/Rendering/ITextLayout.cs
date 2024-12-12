namespace ActiproSoftware.WinUICore.Rendering
{
    using System;
    using System.Collections.Generic;

    public interface ITextLayout : IDisposable
    {
        void RunTextFormatter(IDisposable batch);
        void SetFontFamily(int characterIndex, int characterCount, string fontFamilyName);
        void SetFontSize(int characterIndex, int characterCount, float fontSize);
        void SetFontStyle(int characterIndex, int characterCount, FontStyles fontStyle);
        void SetFontWeight(int characterIndex, int characterCount, FontWeights fontWeight);
        void SetForeground(int characterIndex, int characterCount, Color? brush);
        void SetStrikethrough(int characterIndex, int characterCount, bool hasStrikethrough);
        void SetStrikethrough(int characterIndex, int characterCount, LineKind lineKind, Color? brush, TextLineWeight lineWeight);
        void SetUnderline(int characterIndex, int characterCount, bool hasUnderline);
        void SetUnderline(int characterIndex, int characterCount, LineKind lineKind, Color? brush, TextLineWeight lineWeight);

        IList<ITextLayoutLine> Lines { get; }

        IList<ITextSpacer> Spacers { get; }

        int SpaceWidth { get; }

        int TabSize { get; set; }

        ITextProvider TextProvider { get; }

        TextLayoutWrapping TextWrapping { get; set; }
    }
}

