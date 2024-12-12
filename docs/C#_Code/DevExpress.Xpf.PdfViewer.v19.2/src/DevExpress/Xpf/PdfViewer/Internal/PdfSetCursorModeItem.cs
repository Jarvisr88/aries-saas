namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfSetCursorModeItem : CommandBase
    {
        private CursorModeType commandValue;
        private bool isChecked;
        private int groupIndex;

        public int GroupIndex
        {
            get => 
                this.groupIndex;
            set => 
                base.SetProperty<int>(ref this.groupIndex, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PdfSetCursorModeItem)), (MethodInfo) methodof(PdfSetCursorModeItem.get_GroupIndex)), new ParameterExpression[0]));
        }

        public CursorModeType CommandValue
        {
            get => 
                this.commandValue;
            set => 
                base.SetProperty<CursorModeType>(ref this.commandValue, value, Expression.Lambda<Func<CursorModeType>>(Expression.Property(Expression.Constant(this, typeof(PdfSetCursorModeItem)), (MethodInfo) methodof(PdfSetCursorModeItem.get_CommandValue)), new ParameterExpression[0]));
        }

        public bool IsChecked
        {
            get => 
                this.isChecked;
            set => 
                base.SetProperty<bool>(ref this.isChecked, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PdfSetCursorModeItem)), (MethodInfo) methodof(PdfSetCursorModeItem.get_IsChecked)), new ParameterExpression[0]));
        }
    }
}

