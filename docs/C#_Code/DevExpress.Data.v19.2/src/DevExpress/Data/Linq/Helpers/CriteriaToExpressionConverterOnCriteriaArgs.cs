namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public class CriteriaToExpressionConverterOnCriteriaArgs : EventArgs
    {
        private CriteriaOperator criteria;
        private Func<CriteriaOperator, Expression> processHandler;

        public CriteriaToExpressionConverterOnCriteriaArgs(CriteriaOperator criteria, Func<CriteriaOperator, Expression> processHandler);

        public CriteriaOperator Criteria { get; }

        public Func<CriteriaOperator, Expression> ProcessHandler { get; }

        public bool Handled { get; set; }

        public Expression Result { get; set; }
    }
}

