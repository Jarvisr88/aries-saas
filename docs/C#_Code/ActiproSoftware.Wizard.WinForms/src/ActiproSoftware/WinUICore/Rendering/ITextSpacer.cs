namespace ActiproSoftware.WinUICore.Rendering
{
    using System;
    using System.Drawing;

    public interface ITextSpacer
    {
        float Baseline { get; }

        int CharacterIndex { get; }

        object Key { get; }

        System.Drawing.Size Size { get; }
    }
}

