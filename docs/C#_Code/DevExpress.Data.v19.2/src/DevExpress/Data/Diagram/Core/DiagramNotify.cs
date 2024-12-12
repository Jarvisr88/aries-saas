namespace DevExpress.Data.Diagram.Core
{
    using System;
    using System.Linq.Expressions;

    public static class DiagramNotify
    {
        public static void RaiseCanExecuteChanged(object obj, Expression<Action> expression);
    }
}

