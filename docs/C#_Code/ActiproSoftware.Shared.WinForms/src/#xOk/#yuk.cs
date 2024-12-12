namespace #xOk
{
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class #yuk : ITextSpacer
    {
        public #yuk(int characterIndex, object key, System.Drawing.Size size, float baseline)
        {
            this.CharacterIndex = characterIndex;
            this.Key = key;
            this.Size = size;
            this.Baseline = baseline;
        }

        public float Baseline { get; private set; }

        public int CharacterIndex { get; private set; }

        public object Key { get; private set; }

        public System.Drawing.Size Size { get; private set; }
    }
}

