namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class KeyAction
    {
        public bool Alt { get; set; }

        public bool Ctrl { get; set; }

        public bool Shift { get; set; }

        public bool IsChar { get; set; }

        public bool IsSealedKey { get; set; }

        public bool SealedKeyState { get; set; }

        public int VCode { get; set; }

        public int Character { get; set; }

        public Keys Key =>
            (Keys) this.VCode;

        public char Char =>
            (char) this.Character;
    }
}

