namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.XtraPrinting;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class BookmarkNodeItem : DocumentMapItem
    {
        private readonly DevExpress.XtraPrinting.BookmarkNode node;
        private bool isHighlighted;

        internal BookmarkNodeItem(DevExpress.XtraPrinting.BookmarkNode node, int id, int parentId) : base(id, parentId, node.Text)
        {
            this.node = node;
            base.IsExpanded = true;
        }

        public bool IsHighlighted
        {
            get => 
                this.isHighlighted;
            set => 
                base.SetProperty<bool>(ref this.isHighlighted, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(BookmarkNodeItem)), (MethodInfo) methodof(BookmarkNodeItem.get_IsHighlighted)), new ParameterExpression[0]));
        }

        public DevExpress.XtraPrinting.BookmarkNode BookmarkNode =>
            this.node;
    }
}

