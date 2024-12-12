namespace DevExpress.Data.Linq.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class CriteriaToExpressionConverterEventsHelper
    {
        public event EventHandler<CriteriaToExpressionConverterOnCriteriaArgs> OnFunctionOperator;

        private void FunctionOperatorHandler(object sender, CriteriaToExpressionConverterOnCriteriaArgs e);
        public void Subscribe(CriteriaToExpressionConverterInternal converter);
        protected virtual void SubscribeInternal(CriteriaToExpressionConverterInternal converter);
    }
}

