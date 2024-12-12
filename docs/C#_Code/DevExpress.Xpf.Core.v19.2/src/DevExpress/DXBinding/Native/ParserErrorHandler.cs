namespace DevExpress.DXBinding.Native
{
    using System;

    internal class ParserErrorHandler : IParserErrorHandler
    {
        private readonly ParserMode mode;
        private string error = string.Empty;

        public ParserErrorHandler(ParserMode mode)
        {
            this.mode = mode;
        }

        void IParserErrorHandler.Error(int pos, string msg)
        {
            if (!string.IsNullOrEmpty(this.error))
            {
                this.error = this.error + Environment.NewLine;
            }
            this.error = this.error + ErrorHelper.ReportParserError(pos, msg, this.mode);
        }

        public string GetError() => 
            this.error;

        public bool HasError =>
            !string.IsNullOrEmpty(this.error);
    }
}

