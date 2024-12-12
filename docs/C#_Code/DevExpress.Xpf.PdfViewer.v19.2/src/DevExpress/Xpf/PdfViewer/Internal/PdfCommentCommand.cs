namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class PdfCommentCommand : PdfSetCursorModeItem
    {
        private Color commentColor;

        private void CommentColorChanged()
        {
            this.CommentColorChangedHandler.Do<Action<Color>>(x => x(this.CommentColor));
        }

        public Action<Color> CommentColorChangedHandler { get; set; }

        public Color CommentColor
        {
            get => 
                this.commentColor;
            set => 
                base.SetProperty<Color>(ref this.commentColor, value, Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfCommentCommand)), (MethodInfo) methodof(PdfCommentCommand.get_CommentColor)), new ParameterExpression[0]), new Action(this.CommentColorChanged));
        }
    }
}

