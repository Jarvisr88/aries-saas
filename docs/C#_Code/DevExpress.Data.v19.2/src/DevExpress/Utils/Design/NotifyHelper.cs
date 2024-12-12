namespace DevExpress.Utils.Design
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Linq.Expressions;

    public static class NotifyHelper
    {
        public static void RaiseCanExecuteChanged(object @this, Expression<Action> expression)
        {
            @this.RaiseCanExecuteChanged(expression);
        }
    }
}

