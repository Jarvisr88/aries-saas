namespace ActiproSoftware.WinUICore.Rendering
{
    using System;

    public interface ITextProvider
    {
        string GetSubstring(int characterIndex, int characterCount);
        int Translate(int characterIndex, TextProviderTranslateModes modes);

        int Length { get; }
    }
}

