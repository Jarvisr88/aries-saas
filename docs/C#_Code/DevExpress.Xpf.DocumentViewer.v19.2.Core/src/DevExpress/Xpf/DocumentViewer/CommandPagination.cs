namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CommandPagination : CommandBase
    {
        private int currentPageNumber;
        private int pageCount;

        public int CurrentPageNumber
        {
            get => 
                this.currentPageNumber;
            set => 
                base.SetProperty<int>(ref this.currentPageNumber, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CommandPagination)), (MethodInfo) methodof(CommandPagination.get_CurrentPageNumber)), new ParameterExpression[0]));
        }

        public int PageCount
        {
            get => 
                this.pageCount;
            set => 
                base.SetProperty<int>(ref this.pageCount, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CommandPagination)), (MethodInfo) methodof(CommandPagination.get_PageCount)), new ParameterExpression[0]));
        }
    }
}

