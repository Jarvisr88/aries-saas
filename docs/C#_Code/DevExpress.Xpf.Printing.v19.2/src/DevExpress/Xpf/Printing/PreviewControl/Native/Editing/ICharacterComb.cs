namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.XtraPrinting.Native.CharacterComb;
    using System;

    public interface ICharacterComb
    {
        DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo CharacterCombInfo { get; }

        CharacterCombTextElement[,] CharacterCombTextElements { get; }

        int SelectionStart { get; }

        int SelectionLength { get; }

        string Text { get; }

        double Zoom { get; }
    }
}

