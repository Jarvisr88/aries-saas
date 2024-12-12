namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Pdf;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfDocumentSelectionParameter : BindableBase
    {
        private PdfSelectionAction selectionAction;
        private PdfDocumentPosition position;
        private PdfDocumentPosition endPosition;
        private PdfDocumentArea area;

        public PdfSelectionAction SelectionAction
        {
            get => 
                this.selectionAction;
            set => 
                base.SetProperty<PdfSelectionAction>(ref this.selectionAction, value, Expression.Lambda<Func<PdfSelectionAction>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentSelectionParameter)), (MethodInfo) methodof(PdfDocumentSelectionParameter.get_SelectionAction)), new ParameterExpression[0]));
        }

        public PdfDocumentArea Area
        {
            get => 
                this.area;
            set => 
                base.SetProperty<PdfDocumentArea>(ref this.area, value, Expression.Lambda<Func<PdfDocumentArea>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentSelectionParameter)), (MethodInfo) methodof(PdfDocumentSelectionParameter.get_Area)), new ParameterExpression[0]));
        }

        public PdfDocumentPosition Position
        {
            get => 
                this.position;
            set => 
                base.SetProperty<PdfDocumentPosition>(ref this.position, value, Expression.Lambda<Func<PdfDocumentPosition>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentSelectionParameter)), (MethodInfo) methodof(PdfDocumentSelectionParameter.get_Position)), new ParameterExpression[0]));
        }

        public PdfDocumentPosition EndPosition
        {
            get => 
                this.endPosition;
            set => 
                base.SetProperty<PdfDocumentPosition>(ref this.endPosition, value, Expression.Lambda<Func<PdfDocumentPosition>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentSelectionParameter)), (MethodInfo) methodof(PdfDocumentSelectionParameter.get_EndPosition)), new ParameterExpression[0]));
        }
    }
}

