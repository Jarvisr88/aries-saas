namespace #xOk
{
    using #H;
    using System;
    using System.Runtime.CompilerServices;

    internal class #USk
    {
        public #USk #Ogb(int #SSk, int #TSk)
        {
            this.EndCharacterIndex = #SSk;
            this.Width = #TSk;
            return new #USk(#SSk, this.EndCharacterIndex, this.Analysis, this.X + #TSk, this.Width - #TSk, this.Height, this.Baseline, this.Format);
        }

        internal #USk(int startCharacterIndex, int endCharacterIndex, #Bi.#pTk analysis, int x, int width, int height, float baseline, #juk format)
        {
            this.StartCharacterIndex = startCharacterIndex;
            this.EndCharacterIndex = endCharacterIndex;
            this.Analysis = analysis;
            this.X = x;
            this.Width = width;
            this.Height = height;
            this.Baseline = baseline;
            this.Format = format;
        }

        public override string ToString()
        {
            object[] objArray1 = new object[4];
            object[] objArray2 = new object[4];
            objArray2[0] = this.StartCharacterIndex;
            object[] args = objArray2;
            args[1] = this.EndCharacterIndex;
            args[2] = this.X;
            args[3] = this.Width;
            return string.Format(#G.#eg(0xda5), args);
        }

        public #Bi.#pTk Analysis { get; private set; }

        public float Baseline { get; private set; }

        public int CharacterCount =>
            this.EndCharacterIndex - this.StartCharacterIndex;

        public int EndCharacterIndex { get; private set; }

        public #juk Format { get; private set; }

        public int Height { get; private set; }

        public bool IsRightToLeft
        {
            get
            {
                #Bi.#pTk analysis = this.Analysis;
                return analysis.IsRightToLeft;
            }
        }

        public int Length =>
            this.EndCharacterIndex - this.StartCharacterIndex;

        public int StartCharacterIndex { get; private set; }

        public int Width { get; private set; }

        public int X { get; internal set; }
    }
}

