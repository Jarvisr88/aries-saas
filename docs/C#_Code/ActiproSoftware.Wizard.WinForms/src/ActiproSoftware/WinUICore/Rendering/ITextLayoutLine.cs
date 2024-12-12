namespace ActiproSoftware.WinUICore.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    public interface ITextLayoutLine
    {
        ITextBounds GetCharacterBounds(int characterIndex, bool allowVirtualSpace);
        IEnumerable<ITextBounds> GetTextBounds(int characterIndex, int characterCount, bool allowVirtualSpace);
        int HitTest(Point location);

        float Baseline { get; }

        int CharacterCount { get; }

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId="HardLine")]
        bool HasHardLineBreak { get; }

        int Height { get; }

        int StartCharacterIndex { get; }

        int Width { get; }
    }
}

