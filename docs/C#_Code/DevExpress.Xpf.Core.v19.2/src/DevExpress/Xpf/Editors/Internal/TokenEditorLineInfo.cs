namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TokenEditorLineInfo
    {
        public TokenEditorLineInfo(List<TokenInfo> tokens)
        {
            this.Tokens = tokens;
            this.Height = this.CalcHeight();
        }

        public TokenEditorLineInfo(List<TokenInfo> tokens, int index)
        {
            this.Tokens = tokens;
            this.Height = this.CalcHeight();
            this.Index = index;
        }

        private double CalcHeight()
        {
            double maxHeight = 0.0;
            this.Tokens.ForEach(delegate (TokenInfo x) {
                maxHeight = Math.Max(maxHeight, x.Size.Height);
            });
            return maxHeight;
        }

        public double Height { get; private set; }

        public List<TokenInfo> Tokens { get; private set; }

        public int Index { get; set; }
    }
}

