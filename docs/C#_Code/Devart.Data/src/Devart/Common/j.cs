namespace Devart.Common
{
    using System;

    internal class j : Token
    {
        private readonly int a;
        private readonly int b;

        public j(Devart.Common.TokenType A_0, object A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, int A_8) : base(A_0, A_1, A_2, A_3, A_4, A_5, A_6)
        {
            this.b = A_7;
            this.a = A_8;
        }

        public override int Devart.Common.Token.EndLineBegin =>
            this.b;

        public override int Devart.Common.Token.EndLineNumber =>
            this.a;

        public override int Devart.Common.Token.EndLinePosition =>
            base.EndPosition - this.b;
    }
}

