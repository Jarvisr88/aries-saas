namespace #xOk
{
    using #H;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct #luk
    {
        private int #VTc;
        private #juk #zD;
        public #luk(int characterIndex, #juk format)
        {
            this.#VTc = characterIndex;
            this.#zD = format;
        }

        public int CharacterIndex =>
            this.#VTc;
        public #juk Format =>
            this.#zD;
        public override string ToString()
        {
            object[] objArray1 = new object[2];
            object[] objArray2 = new object[2];
            objArray2[0] = this.#VTc;
            object[] args = objArray2;
            args[1] = this.#zD;
            return string.Format(CultureInfo.InvariantCulture, #G.#eg(0xd25), args);
        }
    }
}

