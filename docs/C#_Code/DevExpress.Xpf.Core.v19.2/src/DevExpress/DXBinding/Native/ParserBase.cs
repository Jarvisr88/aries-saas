namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal abstract class ParserBase
    {
        protected const bool _T = true;
        protected const bool _x = false;
        protected const int minErrDist = 2;
        protected static bool[,] set;
        protected Token t;
        protected Token la;
        private int errDist = 2;

        public ParserBase(DevExpress.DXBinding.Native.Scanner scanner, IParserErrorHandler errorHandler)
        {
            this.Errors = this.CreateErrorHandler(errorHandler);
            this.Scanner = scanner;
            set = this.GetSet();
        }

        protected abstract ErrorsBase CreateErrorHandler(IParserErrorHandler errorHandler);
        protected void Expect(int n)
        {
            if (this.la.kind == n)
            {
                this.Get();
            }
            else
            {
                this.SynErr(n);
            }
        }

        protected void ExpectWeak(int n, int follow)
        {
            if (this.la.kind == n)
            {
                this.Get();
            }
            else
            {
                this.SynErr(n);
                while (!this.StartOf(follow))
                {
                    this.Get();
                }
            }
        }

        protected void Get()
        {
            while (true)
            {
                this.t = this.la;
                this.la = this.Scanner.Scan();
                if (this.la.kind <= this.GetMaxTokenKind())
                {
                    this.errDist++;
                    return;
                }
                this.la = this.t;
            }
        }

        protected abstract int GetMaxTokenKind();
        protected abstract bool[,] GetSet();
        protected abstract bool IsEOF(int n);
        public void Parse()
        {
            this.la = new Token();
            this.la.val = "";
            this.Get();
            this.ParseRoot();
        }

        protected abstract void ParseRoot();
        protected abstract void Pragmas();
        protected void SemErr(string msg)
        {
            if (this.errDist >= 2)
            {
                this.Errors.SemErr(this.t.line, this.t.col, msg);
            }
            this.errDist = 0;
        }

        protected bool StartOf(int s) => 
            set[s, this.la.kind];

        protected void SynErr(int n)
        {
            if (this.errDist >= 2)
            {
                this.Errors.SynErr(this.la.line, this.la.col, n);
            }
            this.errDist = 0;
        }

        private bool TokenEquals(int n, Func<Token, bool> check)
        {
            if (n == 0)
            {
                return check(this.t);
            }
            Token la = this.la;
            this.Scanner.ResetPeek();
            for (int i = 1; i < n; i++)
            {
                la = this.Scanner.Peek();
            }
            this.Scanner.ResetPeek();
            return check(la);
        }

        protected bool TokenEquals(int n, params int[] value) => 
            this.TokenEquals(n, x => value.Contains<int>(x.kind));

        protected bool TokenEquals(int n, params string[] value) => 
            this.TokenEquals(n, x => value.Contains<string>(x.val));

        private bool TokenExists(int n, Func<Token, bool> check)
        {
            Token t = this.t;
            this.Scanner.ResetPeek();
            for (int i = 0; i < n; i++)
            {
                t = this.Scanner.Peek();
            }
            while (!check(t))
            {
                t = this.Scanner.Peek();
                if (this.IsEOF(t.kind))
                {
                    this.Scanner.ResetPeek();
                    return false;
                }
            }
            return true;
        }

        protected bool TokenExists(int n, params int[] value) => 
            this.TokenExists(n, x => value.Contains<int>(x.kind));

        protected bool TokenExists(int n, params string[] value) => 
            this.TokenExists(n, x => value.Contains<string>(x.val));

        protected bool WeakSeparator(int n, int syFol, int repFol)
        {
            int kind = this.la.kind;
            if (kind == n)
            {
                this.Get();
                return true;
            }
            if (this.StartOf(repFol))
            {
                return false;
            }
            this.SynErr(n);
            while (!set[syFol, kind] && (!set[repFol, kind] && !set[0, kind]))
            {
                this.Get();
                kind = this.la.kind;
            }
            return this.StartOf(syFol);
        }

        public ErrorsBase Errors { get; private set; }

        protected DevExpress.DXBinding.Native.Scanner Scanner { get; private set; }
    }
}

