namespace ActiproSoftware.WinUICore.Rendering
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    public interface ITextBounds
    {
        int Bottom { get; }

        int Height { get; }

        bool IsRightToLeft { get; }

        int Left { get; }

        Rectangle Rect { get; }

        int Right { get; }

        int Top { get; }

        int Width { get; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="X")]
        int X { get; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Y")]
        int Y { get; }
    }
}

