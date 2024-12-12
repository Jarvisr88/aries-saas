namespace DevExpress.DXBinding.Native
{
    using System;

    internal abstract class ErrorsBase
    {
        private readonly IParserErrorHandler errorHandler;

        public ErrorsBase(IParserErrorHandler errorHandler)
        {
            this.errorHandler = errorHandler;
        }

        protected abstract string GetError(int n);
        public void SemErr(string s)
        {
            this.WriteLine(-1, s);
        }

        public void SemErr(int line, int col, string s)
        {
            this.WriteLine(col, s);
        }

        public void SynErr(int line, int col, int n)
        {
            this.WriteLine(col, this.GetError(n));
        }

        public void Warning(string s)
        {
            this.WriteLine(-1, s);
        }

        public void Warning(int line, int col, string s)
        {
            this.WriteLine(col, s);
        }

        private void WriteLine(int col, string s)
        {
            this.errorHandler.Error(col, s);
        }
    }
}

