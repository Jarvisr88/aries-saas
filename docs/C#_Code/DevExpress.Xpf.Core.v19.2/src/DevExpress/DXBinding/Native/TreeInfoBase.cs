namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class TreeInfoBase
    {
        private readonly ExprInfo[] exprs;

        internal TreeInfoBase(ExprInfo[] exprs, IErrorHandler errorHandler)
        {
            this.exprs = exprs;
            this.ErrorHandler = errorHandler;
            for (int i = 0; i < exprs.Count<ExprInfo>(); i++)
            {
                exprs[i].Init(new Action<string>(this.Throw));
            }
        }

        internal NRoot GetExpr(int i) => 
            this.exprs.ElementAt<ExprInfo>(i).Expr;

        protected string GetExprString(int i) => 
            this.exprs.ElementAt<ExprInfo>(i).exprString;

        protected virtual void Throw(string msg)
        {
            this.ErrorHandler.Throw(msg, null);
        }

        public IErrorHandler ErrorHandler { get; private set; }

        internal class ExprInfo
        {
            public readonly string exprString;
            public readonly string defaultExprString;
            private readonly ParserMode parseMode;
            private Action<string> throwEx;
            private NRoot expr;

            public ExprInfo(string exprString, string defaultExpr, ParserMode parseMode)
            {
                this.exprString = exprString;
                this.defaultExprString = defaultExpr;
                this.parseMode = parseMode;
            }

            public void Init(Action<string> throwEx)
            {
                this.throwEx = throwEx;
            }

            public NRoot Expr
            {
                get
                {
                    if (this.expr == null)
                    {
                        ParserErrorHandler errorHandler = new ParserErrorHandler(this.parseMode);
                        string str = string.IsNullOrEmpty(this.exprString) ? this.defaultExprString : this.exprString;
                        if (string.IsNullOrEmpty(str))
                        {
                            return null;
                        }
                        this.expr = ParserHelper.GetSyntaxTree(str, this.parseMode, errorHandler);
                        if (errorHandler.HasError)
                        {
                            this.throwEx(errorHandler.GetError());
                        }
                    }
                    return this.expr;
                }
            }
        }
    }
}

